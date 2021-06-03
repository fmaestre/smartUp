
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;

#region Input for this interface - Use it to test the interface
/*
BX20050231;;;D;20200603;20:27:18;;Glucose;GLUX;PAP;mg/dL;0;;0.00000;71.12977;-;0;0.00000;;S;1.00000;0;74.00000;106.00000
BX20050231;;;D;20200603;20:27:18;;CREA;CREA;JAFFE;mg/dl;0;;0.00000;1.16439;;0;0.00000;;S;1.00000;0;0.60000;1.20000
BX20050231;;;D;20200603;20:27:18;;UREA;UREA;Urease/GLDH;mg/dL;0;;0.00000;14.34314;;0;0.00000;;S;1.00000;0;12.90000;42.90000
BX20050231;;;D;20200603;20:27:18;;ACIDO URICO;AU-E;Uricase Colorimetric;mg/dl;0;;0.00000;12.28546;+;0;0.00000;;S;1.00000;0;3.50000;7.20000
BX20050231;;;D;20200603;20:27:18;;Albumin;ALB;Bromocresol Green;g/dL;0;;0.00000;4.89854;;0;0.00000;;S;1.00000;0;3.80000;5.50000
BX20050231;;;D;20200603;20:27:18;;Calcium;CA;Arsenazo III;mg/dL;0;;0.00000;10.12818;;0;0.00000;;S;1.00000;0;8.60000;10.30000
BX20060050;SOTO;KENIA;D;20200603;20:35:41;;Glucose;GLU;PAP;mg/dL;0;;0.00000;1000000.90000;S;0;0.00000;;S;1.00000;0;74.00000;106.00000
BX20060050;SOTO;KENIA;D;20200603;20:35:41;;CREA;CREA;JAFFE;mg/dl;0;;0.00000;1000000.90000;S;0;0.00000;;S;1.00000;0;0.60000;1.20000
BX20060050;SOTO;KENIA;D;20200603;20:35:41;;UREA;UREA;Urease/GLDH;mg/dL;0;;0.00000;1000000.90000;S;0;0.00000;;S;1.00000;0;12.90000;42.90000
BX20060050;SOTO;KENIA;D;20200603;20:35:41;;ACIDO URICO;AU-E;Uricase Colorimetric;mg/dl;0;;0.00000;1000000.90000;S;0;0.00000;;S;1.00000;0;3.50000;7.20000
BX20060050;SOTO;KENIA;D;20200603;20:35:41;;Total Protein;PT;Biuret;g/dL;0;;0.00000;1000000.90000;S;0;0.00000;;S;1.00000;0;6.40000;8.30000
BX20060050;SOTO;KENIA;D;20200603;20:35:41;;Albumin;ALB;Bromocresol Green;g/dL;0;;0.00000;1000000.90000;S;0;0.00000;;S;1.00000;0;3.80000;5.50000
BX20060050;SOTO;KENIA;D;20200603;20:35:41;;Total Bilirubin;TBIL;Diazonium Ion;mg/dL;0;;0.00000;1000000.90000;S;0;0.00000;;S;1.00000;0;0.00000;0.00000
BX20060050;SOTO;KENIA;D;20200603;20:35:41;;Direct Bilirubin;DBIL;DIAZO (BLANKED);mg/dL;0;;0.00000;1000000.90000;S;0;0.00000;;S;1.00000;0;0.00000;0.00000
BX20060050;SOTO;KENIA;D;20200603;20:35:41;;TGO;TGO;IFCC Modified;U/L;0;;0.00000;1000000.90000;S;0;0.00000;;S;1.00000;0;0.00000;40.00000
BX20060050;SOTO;KENIA;D;20200603;20:35:41;;ALT;TGP;IFCC Modified;U/L;0;;0.00000;1000000.90000;S;0;0.00000;;S;1.00000;0;0.00000;45.00000
BX20060050;SOTO;KENIA;D;20200603;20:35:41;;LDH;LDH;Lactate to pyruvate;U/L;0;;0.00000;1000000.90000;S;0;0.00000;;S;1.00000;0;125.00000;220.00000
BX20060050;SOTO;KENIA;D;20200603;20:35:41;;Cholesterol;CHOL;Enzymatic;mg/dL;0;;0.00000;1000000.90000;S;0;0.00000;;S;1.00000;0;50.00000;200.00000
BX20060050;SOTO;KENIA;D;20200603;20:35:41;;Triglycerides;TRIG;Enzymatic GPO;mg/dL;0;;0.00000;1000000.90000;S;0;0.00000;;S;1.00000;0;30.00000;150.00000
BX20060050;SOTO;KENIA;D;20200603;20:35:41;;Calcium;CA;Arsenazo III;mg/dL;0;;0.00000;1000000.90000;S;0;0.00000;;S;1.00000;0;8.60000;10.30000
BX20060050;SOTO;KENIA;D;20200603;20:35:41;;Phosphorus;PHOS;Phosphomolybdate-UV;mg/dL;0;;0.00000;1000000.90000;S;0;0.00000;;S;1.00000;0;2.50000;4.50000
BX20060050;SOTO;KENIA;D;20200603;20:35:41;;Glucose;GLU;PAP;mg/dL;0;;0.00000;1000000.90000;S;0;0.00000;;S;1.00000;0;74.00000;106.00000
 */
#endregion

namespace SmartUp
{
    public partial class FrmFromFile : Form
    {
        const string _NEWLINE_ = "\r\n";
        const string _TAB_ = "\t";

        public List<string> fileContent;
        public string 
                            field1 = "Analisis", 
                            field2 = "Resultado", 
                            field3 = "Ident_Muestra",
                            field4 = "Fecha",
                            field5 = "FechaReg",
                            field6 = ""
            ;
        public string strFmtDelete = "DELETE {0} WHERE {1} ='{2}' AND {3} = '{4}' ";
        public string strFmtInsert4 = "INSERT INTO {0} ({1},{2},{3},{4},{5}) VALUES ('{6}','{7}','{8}','{9}',getdate())";
        public List<string> LstrInserts;

        private void timer1_Tick(object sender, EventArgs e)
        {
            

            cmdInterface.Enabled = false;
            try
            {
                fileContent = Program.getTextContent(Program.pos["file"]);
                foreach (var s in fileContent)
                {
                    txtResults.Text += s + _NEWLINE_;
                    Application.DoEvents();
                }
                Parse();

                cmdInterface.Enabled = true;
                process();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        private void chkAutomatic_CheckedChanged(object sender, EventArgs e)
        {
            btnLoadFile.Enabled = !chkAutomatic.Checked;
            timer1.Enabled = chkAutomatic.Checked;
        }

        public FrmFromFile(string interfaceName,int frequence)
        {
            
            try
            {
                InitializeComponent();
                cmdInterface.Enabled = false;

                timer1.Interval = frequence;
                this.Text +=  "- [" + interfaceName + "] - Interval:" + frequence.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      
        private void btnLoadFile_Click(object sender, System.EventArgs e)
        {
            cmdInterface.Enabled = false;
            try
            {
                fileContent = Program.getTextContent(Program.getFileName(int.Parse(Program.pos["filetype"])));
                foreach (var s in fileContent)
                {
                    txtResults.Text += s + _NEWLINE_;
                    Application.DoEvents();
                }
                Parse();

                cmdInterface.Enabled = true;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Parse()
        {
            lblError.Visible = false;
            lblCompleted.Visible = false;

            string[] vec;
            txt2Upload.Text = "";
            LstrInserts = new List<string>();
            foreach (var s in fileContent)
            {
                var separator = "";
                if (Program.pos["sep"] == "semicolon") separator = ";";
                else if (Program.pos["sep"] == "comma") separator = ",";
                else if (Program.pos["sep"] == "tab") separator = "\t";
                else if (Program.pos["sep"] == "pipe") separator = "|";
                else  separator = ",";

                vec = s.Split(new string[] { separator },StringSplitOptions.None);
                int _ana =   int.Parse(Program.pos["Ana"])  -1;
                int _res = int.Parse(Program.pos["Res"])    -1;
                int _id = int.Parse(Program.pos["Id"])      -1;
                int _fecha = int.Parse(Program.pos["Fec"])  -1;

                txt2Upload.Text +=  vec[_ana]   + _TAB_ +
                                    vec[_res]   + _TAB_ +
                                    vec[_id]    + _TAB_ +
                                    vec[_fecha] + _NEWLINE_;
                LstrInserts.Add(string.Format(strFmtDelete, Program.db["Table"], field1, vec[_ana], field3, vec[_id]));
                LstrInserts.Add(string.Format(strFmtInsert4, Program.db["Table"], field1, field2, field3, field4, field5,
                                                                           vec[_ana], vec[_res], vec[_id], vec[_fecha]));
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

        private void cmdInterface_Click(object sender, EventArgs e)
        {
            process();
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
