
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;


namespace SmartUp
{
    #region Input for this interface - Use it to test the interface (UNICODEs INSIDE!!!!!!!!)
    /*                    
                           _ _ _ _ _ _ _ _ _ _ _ _ _ _

                                                        Sample test: 67
                                                                       Date  time: 2020-06-16 22:50:42
                       - - - - - - - - - - - - - -
                                                   Patient ID :
                                                                Name       :
                                                                             Ope
rator ID:
          Sample No  :
                       Na   = 138.8 mmol/L
                                           K    = 3.78 mmol/L
                                                              iCa  = 0.99 mmol/L

 Li   = 0.03 mmol/L
                    pH   = 7.490
                                Cl   = 108.0 mmol/L

                                                      _ _ _ _ _ _ _ _ _ _ _ _ _
_

    */
    #endregion
    public partial class FrmFromSerialST200 : Form
    {
        const string _NEWLINE_ = "\r\n";
        const string _TAB_ = "\t";

        public List<string> fileContent;
        public string
                            field1 = Program.pos["Analisis"], //"TEST_CD",//"Analisis", 
                            field2 = Program.pos["Resultado"], //"DATA",//"Resultado", 
                            field3 = Program.pos["Ident_Muestra"], //"Sample_No",//"Ident_Muestra",
                            field4 = Program.pos["Fecha"], //"DDate",//"Fecha",
                            field5 = Program.pos["FechaReg"], //"FechaTrans",//"FechaReg",
                            field6 = ""
            ;
        public string strFmtDelete = "DELETE {0} WHERE {1} ='{2}' AND {3} = '{4}' ";
        public string strFmtInsert4 = "INSERT INTO {0} ({1},{2},{3},{4},{5}) VALUES ('{6}','{7}','{8}','{9}',getdate())";
        public List<string> LstrInserts;


    

        public FrmFromSerialST200(string interfaceName)
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

            string sRead = "";
            buffer.Append(sp.ReadExisting());

            //if (EofMarkRes == "")
            //{
            //    sRead = buffer.ToString();
            //    this.Invoke(d, new object[] { sRead });
            //    buffer.Length = 0;
            //}
            //else
            if (buffer.ToString().Contains(EofMarkRes)) //acumula hasta encontrar la marca y envia todo
            {
                sRead = buffer.ToString();
                sRead = sRead.Replace("  ", "").Replace("\u001b","").Replace("\0","");
                this.Invoke(d, new object[] { sRead });
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
            var s = prmString;
            var errmsg = "";
            var lasti = -1;
            try
            {
                lblError.Visible = false;
                lblCompleted.Visible = false;

                string[] vec;
                txt2Upload.Text = "";
                LstrInserts = new List<string>();
                               
                string[] separator;
                if (Program.pos["sep"] == "semicolon")  separator = new string[] { ";" };
                else if (Program.pos["sep"] == "comma") separator = new string[] { "," };
                else if (Program.pos["sep"] == "tab")   separator = new string[] { "\t" };
                else if (Program.pos["sep"] == "pipe")  separator = new string[] { "|" };
                else if (Program.pos["sep"] == "break") separator = (new string[] { "\n"}); 
                
                else separator = new string[] { "," };

                vec = s.Split(separator, StringSplitOptions.None);

                
                //int _id = int.Parse(Program.pos["Id"]) - 1;
                //int _fecha = int.Parse(Program.pos["Fec"]) - 1;
                bool IdFound = false;
                bool DateFound = false;
                string IdRec = "";
                string _date = "";
                //escapar hasta encontrar el punto (pat & date)
                for (int i = 0; i < vec.Length; i++){

                    if (!DateFound && vec[i].IndexOf("Date  time") < 0) continue;
                    
                    if (!DateFound) //date has been found.
                    {
                        DateFound = true;
                        _date = vec[i].Split(':')[1];   //i.e 07-22-2020  08:29:04
                    }

                    if (!IdFound && vec[i].IndexOf("Patient ID") < 0) continue;
                    
                    if (!IdFound) //ID has been found.
                    {
                        IdFound = true;
                        IdRec = vec[i].Split(':')[1];   //i.e No.000002
                    }

                    if (vec[i].IndexOf("=") < 0) continue; //no es un analito


                    errmsg += vec[i];
                    lasti = i;
                    string Analisis = "", Result = "";     //i.e PT-s:40.6 (10.0-15.0)    //i.e R:3.36                     
                    
                    ///get data                                                 
                    Analisis = vec[i].Split('=')[0].Trim();
                    Result   = vec[i].Split('=')[1].Trim();

                    if (Result.IndexOf(" ") > 0) //it has UoM. remove it.
                    {
                        Result = Result.Split(' ')[0];
                    }
                                        
                    txt2Upload.Text +=  Analisis + _TAB_ +
                                        Result + _TAB_ +
                                        IdRec + _TAB_ +
                                        _date + _NEWLINE_;

                    LstrInserts.Add(string.Format(strFmtDelete, Program.db["Table"], field1, Analisis, field3, IdRec));
                    LstrInserts.Add(string.Format(strFmtInsert4, Program.db["Table"], field1, field2, field3, field4, field5,
                                                                                Analisis, Result, IdRec, _date));
                }

                lblError.Visible = false;
                lblCompleted.Visible = true;

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message + "___" + errmsg + " LastIdx=" + lasti.ToString();
                
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


