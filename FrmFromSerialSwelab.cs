
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace SmartUp
{
    #region Input for this interface - Use it to test the interface
    /// Output por this interface
    /*
<!--:Begin:Chksum:1:-->
<!--:Begin:Msg:2:0:-->
<sample>
<ver>1.1</ver>
<instrinfo>
<p><n>PRDI</n><v>BM800</v></p>
<p><n>FIWV</n><v>2.9.4</v></p>
<p><n>SNO</n><v>27756</v></p>
<p><n>BRND</n><v>S</v></p>
<p><n>IAPL</n><v>H</v></p>
<p><n>IID</n></p>
<p><n>LMOF</n><v>1</v></p>
<p><n>PMPM</n><v>24</v></p>
</instrinfo>
<smpinfo>
<p><n>ID</n><v>BX20060139</v></p>
<p><n>SEQ</n><v>1062</v></p>
<p><n>DATE</n><v>2020-06-10T19:21:07</v></p>
<p><n>OPID</n></p>
<p><n>APNU</n><v>1</v></p>
<p><n>APNA</n><v>BLOOD</v></p>
<p><n>ASPM</n><v>OT</v></p>
<p><n>ASPS</n><v>1</v></p>
<p><n>SORC</n><v>0</v></p>
<p><n>BLMD</n><v>0</v></p>
<p><n>BLNK</n><v>0</v></p>
<p><n>STYP</n><v>0</v></p>
<p><n>RGED</n></p>
<p><n>RGEL</n></p>
<p><n>RGEC</n></p>
<p><n>RDLI</n><v>1904-003</v></p>
<p><n>RDPN</n><v>1266</v></p>
<p><n>RDED</n><v>2022-03-31</v></p>
<p><n>RLLI</n><v>1903-001</v></p>
<p><n>RLPN</n><v>323</v></p>
<p><n>RLED</n><v>2022-02-28</v></p>
<p><n>RCLI</n></p>
<p><n>RCPN</n></p>
<p><n>RCED</n></p>
<p><n>RELI</n></p>
<p><n>REPN</n></p>
<p><n>REED</n></p>
<p><n>RPD</n><v>30</v></p>
<p><n>RPDS</n><v>1</v></p>
<p><n>RPDL</n><v>15</v></p>
<p><n>RPDH</n><v>30</v></p>
<p><n>RPDF</n><v>27</v></p>
<p><n>MBTE</n><v>28.6</v></p>
<p><n>MCVO</n><v>+0.0</v></p>
<p><n>WDDM</n><v>0</v></p>
<p><n>WDDP</n><v>45</v></p>
<p><n>WDMS</n><v>2</v></p>
<p><n>WDMA</n><v>2</v></p>
<p><n>WDFB</n><v>0</v></p>
<p><n>WDLL</n></p>
<p><n>WDLH</n></p>
<p><n>WDCL</n></p>
<p><n>WDCH</n></p>
<p><n>WLGL</n></p>
<p><n>WLGH</n></p>
<p><n>WDIL</n><v>140</v></p>
<p><n>WDIH</n><v>180</v></p>
<p><n>WDOM</n><v>0</v></p>
<p><n>WDWD</n><v>3</v></p>
<p><n>XLT</n></p>
<p><n>EOMD</n></p>
<p><n>EODL</n></p>
<p><n>EODH</n></p>
<p><n>EOAC</n></p>
<p><n>EOWC</n></p>
<p><n>XEIT</n></p>
<p><n>EASS</n></p>
<p><n>CAPL</n></p>
<p><n>CLVL</n></p>
<p><n>CEXP</n></p>
<p><n>CEXT</n></p>
<p><n>EXCL</n></p>
<p><n>ASWP</n></p>
<p><n>ID2</n></p>
<p><n>MCVX</n><v>0</v></p>
</smpinfo>
<smpresults>
<p><n>RBC</n><v>4.95</v><l>3.50</l><h>5.50</h></p>
<p><n>MCV</n><v>88.6</v><l>75.0</l><h>100.0</h></p>
<p><n>HCT</n><v>43.9</v><l>35.0</l><h>55.0</h></p>
<p><n>MCH</n><v>29.1</v><l>25.0</l><h>35.0</h></p>
<p><n>MCHC</n><v>32.9</v><l>31.0</l><h>38.0</h></p>
<p><n>RDWR</n><v>12.7</v><l>11.0</l><h>16.0</h></p>
<p><n>RDWA</n><v>61.1</v><l>30.0</l><h>150.0</h></p>
<p><n>PLT</n><v>212</v><l>100</l><h>400</h></p>
<p><n>MPV</n><v>9.8</v><l>8.0</l><h>11.0</h></p>
<p><n>PCT</n><v>0.20</v><l>0.01</l><h>9.99</h></p>
<p><n>PDW</n><v>13.1</v><l>0.1</l><h>99.9</h></p>
<p><n>LPCR</n><v>26.8</v><l>0.1</l><h>99.9</h></p>
<p><n>HGB</n><v>14.4</v><l>11.5</l><h>16.5</h></p>
<p><n>WBC</n><v>5.9</v><l>3.5</l><h>10.0</h></p>
<p><n>LA</n><v>2.2</v><l>0.5</l><h>5.0</h></p>
<p><n>MA</n><v>0.2</v><l>0.1</l><h>1.5</h></p>
<p><n>GA</n><v>3.5</v><l>1.2</l><h>8.0</h></p>
<p><n>LR</n><v>36.8</v><l>15.0</l><h>50.0</h></p>
<p><n>MR</n><v>4.0</v><l>2.0</l><h>15.0</h></p>
<p><n>GR</n><v>59.2</v><l>35.0</l><h>80.0</h></p>
</smpresults>
<tparams>
<p><n>RCT</n><v>14414</v></p>
<p><n>WCT</n><v>9651</v></p>
<p><n>aspt</n><v>1262</v></p>
</tparams>
</sample>
<!--:End:Msg:2:0:-->
<!--:End:Chksum:1:175:163:-->
     */
    #endregion
    public partial class FrmFromSerialSwelab : Form
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


    

        public FrmFromSerialSwelab(string interfaceName)
        {            
            try
            {
                InitializeComponent();
                cmdInterface.Enabled = false;
                this.Text += "- [" + interfaceName + "] " + Program.pos["serialport"] + " ***" +  Program.AssemblyName  + "***";
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
            
            if (EofMarkRes == "")
            {
                this.Invoke(d, new object[] { buffer.ToString() });
                buffer.Length = 0;
            }            
            else if (buffer.ToString().Contains(EofMarkRes)) //acumula hasta encontrar la marca y envia todo
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
                //string[] array = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                //fileContent = array.ToList(); ;

                Parse(text);
                process();
            }
        }

        
    
        private void Parse(string xml)
        {

            try
            {
                 string _xml_ = "";
                //only obtain from <BofMark> to  <EofMark> 
                string BofMark = "";
                string EofMark = "";
                try {
                    BofMark = Program.pos["BofMark"];
                    EofMark = Program.pos["EofMark"];

                } catch { }

                if (BofMark != "")
                {
                    int cutFromHereToEnd = xml.IndexOf(BofMark);
                    int cutEndHere = xml.IndexOf(EofMark);
                    if (cutFromHereToEnd <0) throw new Exception(Program.pos["BofMark"] + " not received.");

                    _xml_ = xml.Substring(cutFromHereToEnd, cutEndHere - cutFromHereToEnd + BofMark.Length + 1);
                }

                XDocument xdoc = XDocument.Load(new StringReader(_xml_));
                //Run query
                var lv1s = from lv1 in xdoc.Descendants("p")                           
                           select new
                           {
                               ns = lv1.Descendants("n"),
                               vs = lv1.Descendants("v")
                           };


                string receptionId = "";
                string anaId = "";
                string result = "";
                LstrInserts = new List<string>();                

                //Loop through results
                int i = 0;
                foreach (var lv1 in lv1s)
                {
                    //only one record expected
                    //foreach (var n in lv1.ns)
                    //{
                    //    txt2Upload.Text += n.Value + _NEWLINE_;
                    //    anaId = n.Value;
                    //}
                    foreach (var v in lv1.vs)
                    {
                        receptionId = v.Value;
                        break;
                    }

                    //if (i > 0) //Zero loop holds the ID
                    //{
                    //    LstrInserts.Add(string.Format(strFmtDelete, Program.db["Table"], field1, anaId, field3, receptionId));
                    //    LstrInserts.Add(string.Format(strFmtInsert4, Program.db["Table"], field1, field2, field3, field4, field5,
                    //                                                           anaId, result, receptionId, DateTime.Today.ToString()));

                       
                    //}
                    break;//Only the ID matters
                }

                //****************************************************************
                //now read the second part
                //****************************************************************
                BofMark = Program.pos["BofMarkRes"];
                EofMark = Program.pos["EofMarkRes"];


                if (BofMark != "")
                {
                    int cutFromHereToEnd = xml.IndexOf(BofMark);
                    int cutEndHere = xml.IndexOf(EofMark);
                    if (cutFromHereToEnd < 0) throw new Exception(Program.pos["BofMark"] + " not received.");

                    _xml_ = xml.Substring(cutFromHereToEnd, cutEndHere- cutFromHereToEnd + BofMark.Length + 1);
                }

                xdoc = XDocument.Load(new StringReader(_xml_));
                //Run query
                lv1s = from lv1 in xdoc.Descendants("p")
                           select new
                           {
                               ns = lv1.Descendants("n"),
                               vs = lv1.Descendants("v")
                           };

                //reset variables
                anaId = "";
                result = "";
                LstrInserts = new List<string>();

                //Loop through results                
                foreach (var lv1 in lv1s)
                {
                    //only one record expected
                    foreach (var n in lv1.ns)
                    {
                        txt2Upload.Text += n.Value + _NEWLINE_;
                        anaId = n.Value;
                    }
                    foreach (var v in lv1.vs)
                    {
                       result = v.Value;

                       txt2Upload.Text += v.Value + _NEWLINE_;
                    }
                    
                        LstrInserts.Add(string.Format(strFmtDelete, Program.db["Table"], field1, anaId, field3, receptionId));
                        LstrInserts.Add(string.Format(strFmtInsert4, Program.db["Table"], field1, field2, field3, field4, field5,
                                                                               anaId, result, receptionId, DateTime.Today.ToString()));                    
                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }

            lblError.Visible = false;
            lblCompleted.Visible = false;

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
