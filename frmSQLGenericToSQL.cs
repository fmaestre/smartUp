using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace SmartUp
{
    public partial class frmSQLGenericToSQL : Form
    {
        public string strFmtDelete = "SELECT 1 FROM {0} WHERE {1} ='{2}' AND {3} = '{4}' ";
        public string strFmtInsert4 = "INSERT INTO {0} ({1},{2},{3},{4},{5}) VALUES ('{6}','{7}','{8}','{9}',getdate())";
        public List<string> LstrInserts;
        const string _NEWLINE_ = "\r\n";


        public frmSQLGenericToSQL()
        {
            InitializeComponent();
            timer1.Interval = int.Parse(Program.pos["frequence"]);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            LstrInserts = new List<string>();
            try
            {
                //select generic tables
                MoveResults("Select * from ResultadosGeneric where datec > cast(getdate() as date)"); 


                    timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                lblError.Text =  ex.Message;
                lblError.Visible = true;
                timer1.Enabled = true;
            }
        }

        private void MoveResults(string prmQuery)
        {
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

                var reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    Process (reader[0].ToString(), reader[1].ToString());
                }
            }

        }

        private void Process(string fileName,string data)
        {
            if (fileName.Contains("MINDRAYBS200E"))
            {
                MindrayBS200E(data);

              
            }
        }

        private void MindrayBS200E(string data)
        {
            string field1 = "Analisis",
                   field2 = "Resultado", 
                   field3 = "Ident_Muestra", 
                   field4 = "Fecha", 
                   field5 = "FechaReg";
            string table = "ResultadosMindray";
            string[] separator = (new string[] { "\r\n", "\r" });
            var array = data.Split(separator, StringSplitOptions.None);
            string[] lineValues;
            string Analisis="", Result="", IdRec="", _date="";
            var line = "";
            //loop backwards
            separator = new string[] { "|" };
            LstrInserts.Clear();
            for (int i = array.Length-1; i >= 0; i--)
            {
                try
                {
                    line = array[i];
                    if (line.StartsWith("OBX"))
                    {
                        lineValues = line.Split(separator, StringSplitOptions.None);
                        Analisis = lineValues[3];
                        Result = lineValues[5];
                        _date = lineValues[14];
                        _date = new DateTime(int.Parse(_date.Substring(0, 4)), int.Parse(_date.Substring(4, 2)), int.Parse(_date.Substring(6, 2))).ToString("yyyy-MM-dd");
                    }
                    else if (line.StartsWith("OBR"))
                    {
                        lineValues = line.Split(separator, StringSplitOptions.None);
                        IdRec = lineValues[2];
                    }
                    else if (line.StartsWith("PID")) //Insert and Clear ALL
                    {

                        if (Analisis != "")
                        {
                            LstrInserts.Add(string.Format(strFmtDelete, table, field1, Analisis, field3, IdRec));
                            LstrInserts.Add(string.Format(strFmtInsert4, table, field1, field2, field3, field4, field5,
                                                                                        Analisis, Result, IdRec, _date));
                        }
                        Analisis = ""; Result = ""; IdRec = "";
                    }
                }
                catch
                {
                    Analisis = ""; //si hay error se limpia para no pasar nada a la lista
                }
            }

            bool ExistAnalisis = false;
            foreach (var s in LstrInserts)
            {
                if (s.StartsWith("SELECT 1 FROM"))
                    ExistAnalisis = Exists(s);
                else if(!ExistAnalisis)  // si el registro existe 
                    Exec(s);

                if (!ExistAnalisis)
                    txtResults.Text +=s.Replace(table, "%$%#%#%#@").Replace("SELECT", "SmartProxy").Replace("WHERE", "").Replace("INSERT", "SmartProxyLN").Replace("INTO", "").Replace("AND", "").Replace("VALUES", "").Replace("getdate", "{Xlin}") + _NEWLINE_;
                else
                    txtResults.Text += "*..";

                if (txtResults.Text.Length > 10000) txtResults.Text = "";

                Application.DoEvents();
            }
        }

        private int Exec(string prmQuery)
        {
            int response = 0;
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

            return response;
        }

        private bool Exists(string prmQuery)
        {
           
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

                var reader = sqlCmd.ExecuteReader();

                return reader.Read();

                //response = sqlCmd.ExecuteReader();
            }

            //return false;
        }


        private void CmdStopStart_Click(object sender, EventArgs e)
        {
            CmdStopStart.Text = timer1.Enabled ? "Start" : "Stop";
            timer1.Enabled = !timer1.Enabled;
        }
    }
}
