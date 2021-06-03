
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SmartUp
{
    public partial class FrmXmlNaval : Form
    {
        const string _NEWLINE_ = "\r\n";
        const string _TAB_ = "\t";
        public Dictionary<string, string> pos = new Dictionary<string, string>();
        public Dictionary<string, string> db = new Dictionary<string, string>();
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
        public FrmXmlNaval()
        {
            
            try
            {
                InitializeComponent();
                cmdInterface.Enabled = false;

                List<string> ctx = Program.getTextContent(/*Application.StartupPath +*/ "C:\\SmartUp\\config.txt");
                
                if (ctx.Count >0)
                {
                    string[] split = ctx[1].Split(';');
                    foreach (var s in split)
                    {
                        var v = s.Split('=');
                        if (v[0] == "") continue;
                        pos.Add(v[0], v[1]);
                    }

                    split = ctx[0].Split(';');
                    foreach (var s in split)
                    {
                        var v = s.Split('=');
                        if (v[0] == "") continue;
                        db.Add(v[0], v[1]);
                    }
                } 
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
                string fileName = Program.getFileName(3);

                /*
                fileContent = Program.getTextContent(fileName);
                foreach (var s in fileContent)
                {
                    txtResults.Text += s + _NEWLINE_;
                    Application.DoEvents();
                }
                */
                XDocument doc = XDocument.Load(fileName);

                var examens = doc.Element("StiSerializer").Element("Pages").Element("Page1").Element("Components")
                              .Elements()
                              .Select(x => new Examen
                              {
                                  Analisis = (string)x.Attribute("name"),
                                  Resultado = (string)x.Attribute("text")
                              })
                              .ToList();


                foreach (var examen in examens)
                {
                    txtResults.Text +=  string.Format("Ana:{0} \t Res:{1}", examen.Analisis, examen.Resultado) + _NEWLINE_;
                    Application.DoEvents();
                }


                Parse(examens);

                cmdInterface.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Parse(List<Examen> examens)
        {

                  
            /*
            string[] vec;
            txt2Upload.Text = "";
            LstrInserts = new List<string>();
            foreach (var s in fileContent)
            {
                vec = s.Split(new string[] { "\t" },StringSplitOptions.None);
                int _ana =   int.Parse(pos["Ana"])  -1;
                int _res = int.Parse(pos["Res"])    -1;
                int _id = int.Parse(pos["Id"])      -1;
                int _fecha = int.Parse(pos["Fec"])  -1;

                txt2Upload.Text +=  vec[_ana]   + _TAB_ +
                                    vec[_res]   + _TAB_ +
                                    vec[_id]    + _TAB_ +
                                    vec[_fecha] + _NEWLINE_;
                LstrInserts.Add(string.Format(strFmtDelete, db["Table"], field1, vec[_ana], field3, vec[_id]));
                LstrInserts.Add(string.Format(strFmtInsert4, db["Table"], field1, field2, field3, field4, field5,
                                                                           vec[_ana], vec[_res], vec[_id], vec[_fecha]));
            }


           */


        }

        private void Insert(string prmQuery)
        {
            int response;
            string connetionString, insSql;
            SqlConnection sqlConn;
            connetionString = @"Data Source={0};Initial Catalog={1};User ID={2};Password={3}";
            connetionString = string.Format(connetionString, db["Server"], db["Db"], db["User"], db["Password"]);
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
                    txt2Upload.Text += s.Replace(db["Table"],"%$%#%#%#@").Replace("DELETE","SmartProxy").Replace("WHERE", "").Replace("INSERT", "SmartProxyLN").Replace("INTO", "").Replace("AND", "").Replace("VALUES", "").Replace("getdate", "{Xlin}") + _NEWLINE_;                    
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

        private class Examen
        {
            public string Analisis { get; set; }
            public string Resultado { get; set; }
            public string Id { get; set; }
            public string Fecha { get; set; }
        }
    }
}
