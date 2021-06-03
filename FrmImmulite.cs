
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;

namespace SmartUp
{
    public partial class FrmImmulite : Form
    {
        const string _NEWLINE_ = "\r\n";
        const string _TAB_ = "\t";

        public List<string> fileContent;
        public string //defaultTable = "ResultadosImmulite", 
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
        public FrmImmulite()
        {
            
            try
            {
                InitializeComponent();
                cmdInterface.Enabled = false;

 
                //setConfig(ctx);
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
                fileContent = Program.getTextContent(Program.getFileName(2));
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
            string[] vec;
            txt2Upload.Text = "";
            LstrInserts = new List<string>();
            foreach (var s in fileContent)
            {
                vec = s.Split(new string[] { "\t" },StringSplitOptions.None);
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
            try
            {
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
                MessageBox.Show("Completado.!");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"ERROR");
            }
            finally
            {
                cmdInterface.Enabled = false;
                progressBar1.Value = 0;
            }

        }
    }
}
