
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
5401,07-12-2017,09:47,,NX17120103,,,,,Multistix 10 SG,Amarillo,Claro,GLU,Negativo,,BIL,Negativo,,CET,Negativo,,DEN,>=1.030,,SAN,Negativo,,pH ,6.5,,PRO,30 mg/dL,*,URO,0.2 E.U./dL,,NIT,Negativo,,LEU,Negativo,
    */
    /*
5420,28-07-2020,22:31,,BM20070002,,,,,Multistix 10 SG,No introducido,No introducido,E58
5420,28-07-2020,22:31,,BM20070002,,,,,Multistix 10 SG,No introducido,No introducido,E58      
     */
    #endregion
    public partial class FrmFromSerialClinitek : Form
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


    

        public FrmFromSerialClinitek(string interfaceName)
        {
            
            try
            {
                InitializeComponent();
                cmdInterface.Enabled = false;
                this.Text += "- [" + interfaceName + "] " + Program.pos["serialport"] + " ***" + Program.AssemblyName + "***";
                openPort();
                mySerialPort.DiscardInBuffer();
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

            mySerialPort.BaudRate = int.Parse(Program.pos["BaudRate"]);//9600;
            mySerialPort.Parity = (Parity)Enum.Parse(typeof(Parity), Program.pos["Parity"]);   //Parity.None;
            mySerialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Program.pos["StopBits"]);//StopBits.One;
            mySerialPort.DataBits = int.Parse(Program.pos["DataBits"]);//8;
            mySerialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), Program.pos["Handshake"]);//Handshake.XOnXOff;

            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(MySerialPort_DataReceived);


            mySerialPort.Open();
        }
        delegate void SetTextCallback(string text);
        StringBuilder buffer = new StringBuilder();
        private void MySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string EofMarkRes = "";
            try { EofMarkRes = Program.pos["EofMarkRes"]; } catch { }

            SetTextCallback d = new SetTextCallback(SetText);
            SerialPort sp = (SerialPort)sender;
            
            buffer.Append(sp.ReadExisting());

            //if (EofMarkRes == "")
            //{
            //    this.Invoke(d, new object[] { buffer.ToString() });
            //    buffer.Length = 0;
            //}
            //else
            byte[] bytes = Encoding.UTF8.GetBytes(buffer.ToString());
            string LookingFor = BitConverter.ToString(bytes);

            if (LookingFor.Contains("0D")) //acumula hasta encontrar la marca y envia todo 0D es nulo en Unicode
            {                
                this.Invoke(d, new object[] { buffer.ToString() });
                buffer.Length = 0;
            }
            

        }
        private void SetText(string text)
        {

            if (!string.IsNullOrEmpty(text))
            {
                txtResults.Text += text;

                Parse(text);
                process();
            }
        }

        
    
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
                               
                var separator = "";
                if (Program.pos["sep"] == "semicolon") separator = ";";
                else if (Program.pos["sep"] == "comma") separator = ",";
                else if (Program.pos["sep"] == "tab") separator = "\t";
                else if (Program.pos["sep"] == "pipe") separator = "|";
                else separator = ",";

                vec = s.Split(new string[] { separator }, StringSplitOptions.None);
                int _anaFirstOne = int.Parse(Program.pos["Ana"]) - 1;
                int _resIncrement = int.Parse(Program.pos["Res"]); //jump x places to get new record.
                int _id = int.Parse(Program.pos["Id"]) - 1;
                int _fecha = int.Parse(Program.pos["Fec"]) - 1;
                
                string IdRec = vec[_id];
                string _date = vec[_fecha];
                string[] arrDate = _date.Split(new string[] { "-" }, StringSplitOptions.None);

                _date = string.Format("{0}-{1}-{2}", arrDate[2], arrDate[1], arrDate[0]);

                for (int i = _anaFirstOne; i < vec.Length; i+=_resIncrement){

                    Analisis = vec[i];
                    err.fire(vec.Length < i + 1 || vec.Length < i + 2, "Analito sin resultados / no encontrados.");
                    string Result = vec[i + 1] + " " + vec[i + 2]; //values are in two positions, one next to each other 
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


