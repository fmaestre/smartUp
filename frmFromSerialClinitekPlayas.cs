
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;


namespace SmartUp
{
    #region Input for this interface - Use it to test the interface
    /*
#0-001      10-10-20
ID=NX20100091
Color: COLURICA
Aspecto:
GLU         NEGATIVO
BIL         NEGATIVO
CET         NEGATIVO
DEN            1.020
SAN         NEGATIVO
pH               7.0
PRO         NEGATIVO
URO      0.2 E.U./dL
NIT         NEGATIVO
LEU         NEGATIVO
B3
*/
/*

    #0-079      24-10-20
    ID=NX283            
    Color: AMARILLO     
    Aspecto: TURBIA     
    GLU*    >=1000 mg/dL
    BIL         NEGATIVO
    CET         NEGATIVO
    DEN            1.020
    SAN*   Apr 10 eri/uL
    pH               5.5
    PRO         NEGATIVO
    URO      0.2 E.U./dL
    NIT         NEGATIVO
    LEU         NEGATIVO
    BD 
     */
    #endregion
    public partial class FrmFromSerialClinitekPlayas : Form
    {
        const string _NEWLINE_ = "\r\n";
        const string _TAB_ = "\t";

        public List<string> fileContent;
        public string
                            field1 = Program.pos["Analisis"],
                            field2 = Program.pos["Resultado"],
                            field3 = Program.pos["Ident_Muestra"],
                            field4 = Program.pos["Fecha"],
                            field5 = Program.pos["FechaReg"],
                            field6 = "";
            
        public string strFmtDelete = "DELETE {0} WHERE {1} ='{2}' AND {3} = '{4}' ";
        public string strFmtInsert4 = "INSERT INTO {0} ({1},{2},{3},{4},{5}) VALUES ('{6}','{7}','{8}','{9}',getdate())";
        public List<string> LstrInserts;


    

        public FrmFromSerialClinitekPlayas(string interfaceName)
        {
            
            try
            {
                InitializeComponent();
                cmdInterface.Enabled = false;
                this.Text += "- [" + interfaceName + "] " + Program.pos["serialport"] + " ***" + Program.AssemblyName + "***";
               

                openPort();
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        SerialPort mySerialPort;
        private void openPort()
        {

            mySerialPort = new SerialPort(Program.pos["serialport"]);
            //mySerialPort.ReadBufferSize = 9192;
                
            mySerialPort.BaudRate = int.Parse(Program.pos["BaudRate"]);//9600;
            mySerialPort.Parity = (Parity)Enum.Parse(typeof(Parity), Program.pos["Parity"]);   //Parity.None;
            mySerialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Program.pos["StopBits"]);//StopBits.One;
            mySerialPort.DataBits = int.Parse(Program.pos["DataBits"]);//8;
            mySerialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), Program.pos["Handshake"]);//Handshake.XOnXOff;
            //mySerialPort.Handshake = Handshake.RequestToSend;
            mySerialPort.RtsEnable = bool.Parse(Program.pos["RtsEnable"]); //TRUE
            mySerialPort.DtrEnable = bool.Parse(Program.pos["DtrEnable"]); //TRUE
            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(MySerialPort_DataReceived);
            mySerialPort.ErrorReceived += new SerialErrorReceivedEventHandler(MySerialPort_ErrorReceived);


            mySerialPort.Open();
            mySerialPort.DiscardInBuffer();
        }

        private void MySerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        delegate void SetTextCallback(string text);
        StringBuilder buffer = new StringBuilder();
        private void MySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialData x;
            string EofMarkRes = "";
            try { EofMarkRes = Program.pos["EofMarkRes"]; } catch { } finally { EofMarkRes = EofMarkRes == "" ? "03" : EofMarkRes; }

            //SetTextCallback d = new SetTextCallback(SetText);
            SetTextCallback d = SetText;
            //SerialPort sp = (SerialPort)sender;

            //this.Invoke(d, sp.ReadExisting());

            buffer.Append(mySerialPort.ReadExisting());

            byte[] bytes = Encoding.UTF8.GetBytes(buffer.ToString());
            string LookingFor = BitConverter.ToString(bytes);

            if (LookingFor.Contains(EofMarkRes)) //acumula hasta encontrar la marca y envia todo 0D es nulo en Unicode
            {
                this.Invoke(d, new object[] { buffer.ToString() });
                buffer = null;
                buffer = new StringBuilder();
                mySerialPort.Close(); 
                mySerialPort.Open();
            } else if (buffer.Length < 500)
            {
                this.Invoke(d, new object[] { buffer.ToString() });
            }
            else if (buffer.Length > 500)
            {
                buffer = null;
                buffer = new StringBuilder();
                mySerialPort.Close();
                try
                {
                    mySerialPort.Open();
                }
                catch
                {
                    mySerialPort = null;
                    openPort();
                }
            }
        }

        private void SetText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                txtResults.Text = text;

                Parse(text);
                process();
            }
        }

        string IdRec;
        string _date;

        private void Parse(string prmString)
        {
            int iIndex = 0;
            var s = prmString;
            string Analisis = "";
            try
            {
                lblError.Visible = false;
                lblCompleted.Visible = false;

                string[] vec;
                txt2Upload.Text = "";
                LstrInserts = new List<string>();

                string[] separator;
                if (Program.pos["sep"] == "semicolon") separator = new string[] { ";" };
                else if (Program.pos["sep"] == "comma") separator = new string[] { "," };
                else if (Program.pos["sep"] == "tab") separator = new string[] { "\t" };
                else if (Program.pos["sep"] == "pipe") separator = new string[] { "|" };
                else if (Program.pos["sep"] == "break") separator = (new string[] { "\r\n", "\r" });
                else separator = new string[] { "," };

                vec = s.Split( separator , StringSplitOptions.None);
                int _anaFirstOne = int.Parse(Program.pos["Ana"]) - 1;
                int _resIncrement = int.Parse(Program.pos["Res"]); //jump x places to get new record.
                int _id = int.Parse(Program.pos["Id"]) - 1;
                int _fecha = int.Parse(Program.pos["Fec"]) - 1;
                string dateFormat = Program.pos["dateformat"];

                //detectar si el envio trae el ID
                bool IdTransmited = prmString.Contains("ID=");
                bool DateTransmited = prmString.Contains("#");
                //detectar si el envio trae la fecha
                if (DateTransmited)
                {
                    //string[] _datex = vec[0].Split(new char[0], StringSplitOptions.None);
                    string[] _datex = vec[1].Split(new char[0], StringSplitOptions.None);
                    string[] arrDate = _datex[_datex.Length - 1].Split(new string[] { "-" }, StringSplitOptions.None);


                    _date =  dateFormat.ToUpper() == "DDMMYY" ?  string.Format("{0}-{1}-{2}", arrDate[0], arrDate[1], arrDate[2])
                                                     : string.Format("{0}-{1}-{2}", arrDate[1], arrDate[0], arrDate[2])
                        ; //global
                }
               
                if (IdTransmited)
                {
                    //IdRec = vec[DateTransmited?1:0].Split(new string[] { "=" }, StringSplitOptions.None)[1]; //global
                    IdRec = vec[DateTransmited ? 2 : 1].Split(new string[] { "=" }, StringSplitOptions.None)[1]; //global
                }
                
                string Result = "";
                int init = 0; // 0;
                if (IdTransmited && DateTransmited) init = 2;//2;
                if (IdTransmited && !DateTransmited) init = 1;// 1;

                for (int i = 0; i <= vec.Length -1 && IdRec != ""; i++){
                    string raw = vec[i];
                    if (raw.Contains("#")) continue; //es la fecha y no se debe insertar.
                    if (raw.Contains("=") && !raw.Contains(">=") && !raw.Contains("<=")) continue; //es el id y no se debe insertar.

                    if (raw.Contains(":"))
                    {
                        Analisis = raw.Split(new string[] { ":" }, StringSplitOptions.None)[0];
                        Result = raw.Split(new string[] { ":" }, StringSplitOptions.None)[1];
                    }
                    else
                    {
                        string[] temp = raw.Split(new char[0], StringSplitOptions.None);
                        Analisis = temp[0];
                        Result = "";
                        for (int j=1; j <= temp.Length -1; j++) { 
                            Result += temp[j];
                            Result = Result.Trim();
                        }
                    }

                    if (Analisis == "" && Result == "") continue;

                    //Analisis = vec[i];
                    //err.fire(vec.Length < i + 1 || vec.Length < i + 2, "Analito sin resultados / no encontrados.");
                    //string Result = vec[i + 1] + " " + vec[i + 2]; //values are in two positions, one next to each other 
                    iIndex = i;
                    txt2Upload.Text +=  Analisis + _TAB_ +
                                        Result + _TAB_ +
                                        IdRec + _TAB_ +
                                        _date + _NEWLINE_;

                    LstrInserts.Add(string.Format(strFmtDelete, Program.db["Table"], field1, Analisis, field3, IdRec));
                    LstrInserts.Add(string.Format(strFmtInsert4, Program.db["Table"], field1, field2, field3, field4, field5,
                                                                                Analisis, Result, IdRec, _date));
                }

                lblError.Visible = false;
                lblCompleted.Visible = false;

            }
            catch (Exception ex)
            {
                lblError.Text = string.Format("Position:{0} Analisis{1} - Error:{2}", iIndex.ToString(), Analisis, ex.Message);
                lblError.Visible = true;
            }



        }




        private void Insert(string prmQuery)
        {
            int response;
            string connetionString, insSql;
            SqlConnection sqlConn;
            connetionString = @"Data Source={0};Initial Catalog={1};User ID={2};Password={3}";
            connetionString = string.Format(connetionString, Program.db["Server"], Program.db["Db"], Program.db["User"], Program.db["Password"]);
            insSql = prmQuery;
            using (sqlConn = new SqlConnection(connetionString))
            {
                var sqlCmd = new SqlCommand(insSql) { CommandType = CommandType.Text };
                sqlConn.Open();
                sqlCmd.Connection = sqlConn;

                response = sqlCmd.ExecuteNonQuery();
            }            
        }


        private void process()
        {
            try
            {
                progressBar1.Value = 0;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = LstrInserts.Count;
                txt2Upload.Text = "";
                foreach (var s in LstrInserts)
                {                    
                    Insert(s);
                    txt2Upload.Text += s.Replace(Program.db["Table"],"%$%#%#%#@").Replace("DELETE","SmartProxy").Replace("WHERE", "").Replace("INSERT", "SmartProxyLN").Replace("INTO", "").Replace("AND", "").Replace("VALUES", "").Replace("getdate", "{Xlin}") + _NEWLINE_;
                    progressBar1.Value++;                    
                    Application.DoEvents();                    
                }
                
                lblCompleted.Visible = true;
                
            }
            catch (Exception ex)
            {                
                lblError.Text = ex.Message;
                lblError.Visible = true;

            }
            finally
            {
                cmdInterface.Enabled = false;
            }
        }
    }
}


