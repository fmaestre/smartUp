using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace SmartUp
{
    public partial class frmFileToSQL : Form
    {

        public string strFmtDelete = "DELETE {0} WHERE {1} ='{2}'";
        public string strFmtInsert4 = "INSERT INTO {0} ({1},{2},{3}) VALUES ('{4}','{5}',getdate())";
        public List<string> LstrInserts;


        public frmFileToSQL()
        {
            InitializeComponent();
            timer1.Interval = int.Parse(Program.pos["frequence"]);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            LstrInserts = new List<string>();
            string fileName = "";
            try
            {
                
                var path = Program.pos["SavePath"]; //the path of all interface files

                var files = Directory.GetFiles(@path, "*.txt");

                foreach (var file in files)
                {
                    DateTime creation = File.GetCreationTime(file);
                    if (creation < DateTime.Today) continue;
                    fileName = file;
                    var allText = System.IO.File.ReadAllText(file);
                    allText  = allText.Replace("'", "''");
                    //scape specials                    
                    txtResults.Text = allText;
                    LstrInserts.Add(string.Format(strFmtDelete, Program.db["Table"], "filen", file));
                    LstrInserts.Add(string.Format(strFmtInsert4, Program.db["Table"], "filen", "value","datec", file, allText));                                        
                }

                foreach (var s in LstrInserts)
                {
                    Insert(s);
                }                   
            }
            catch (Exception ex)
            {
                lblError.Text = fileName + " --> " + ex.Message;
                lblError.Visible = true;                
            }
            finally
            {
                timer1.Enabled = true;
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

        private void CmdStopStart_Click(object sender, EventArgs e)
        {
            CmdStopStart.Text = timer1.Enabled ? "Start" : "Stop";
            timer1.Enabled = !timer1.Enabled;
        }
    }
}
