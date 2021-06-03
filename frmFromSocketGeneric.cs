
using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;

using System.Threading;
using System.Text;

namespace SmartUp
{

    public partial class FrmFromSocketGeneric : Form
    {
        private string _interfaceName = "";
        private int _ReadCounts = 0;
        public FrmFromSocketGeneric(string interfaceName)
        {
            try
            {
                InitializeComponent();
                _interfaceName = interfaceName;
                
                this.Text += "- [" + interfaceName + "] " + Program.pos["SocketIP"] + " - " + Program.pos["SocketPort"] + " ***" + Program.AssemblyName + "***";
                //openPort();
                new Thread(checkConnection).Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 
        
        private string fileName()
        {            
            var path = Program.pos["SavePath"]; //the path to save interface files
            var file = path + _interfaceName + DateTime.Today.ToString("MMddyyyy") + ".txt";
            return file;            
        }

        private void SaveToFile(string newText)
        {
            if (!string.IsNullOrEmpty(newText))
            {
               this.AddText(newText);
               
                //save to file                
                var file = fileName();
                
                if (!System.IO.File.Exists(file))
                {
                    //System.IO.File.Create(file).Close();                    
                    System.IO.File.Create(file).Dispose(); //2021Feb23 - to fix The Process Cannot Access The File XYZ Because It Is Being Used By Another Process
                }

                var allText = System.IO.File.ReadAllText(file);

                System.IO.File.WriteAllText(file, allText + newText);
            }
        }

        private void checkConnection()
        {
            while (true)
            {
                //check if client is connected
                if (cs != null && cs.IsConnected())
                {
                    Set_lblConnected("Conectado", System.Drawing.Color.Green);
                }
                else
                {
                    Set_lblConnected("Desconectado", System.Drawing.Color.Red);
                    Thread.Sleep(6000);
                    connect();
                }

                Thread.Sleep(2000);

            }


        }


        public void Set_lblConnected(string r, System.Drawing.Color color)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(() => this.Set_lblConnected(r, color)));
                }
                else
                {
                    lblConnected.Text = r;
                    lblConnected.ForeColor = color;

                }
            }
            catch { }
        }


        private void FrmFromSocketGeneric_Shown(object sender, EventArgs e)
        {
            connect();
        }

        ClientSocket cs ;
        private void connect()
        {
            //Thread th = new Thread(OpenPort);
            //th.Start();
            try
            {
                cs = new ClientSocket();
                cs.ConnectToServer(Program.pos["SocketIP"], int.Parse(Program.pos["SocketPort"]));
                cs.OnDataReceived += Cs_OnDataReceived;
                AddText("Conectado");
                Set_lblConnected("Conectado", System.Drawing.Color.Green);
            }
            catch(Exception ex)
            {
                AddError(ex.Message);
                Set_lblConnected("Desconectado", System.Drawing.Color.Red);                
            }
        }

        private void Cs_OnDataReceived(byte[] data, int bytesRead)
        {
            try
            {
                string rdata = Encoding.UTF8.GetString(data).TrimEnd('\0'); ;
                SaveToFile(rdata);
            }
            catch(Exception e) { AddError(e.Message); }
        }

      
        public void AddText(string r)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => this.AddText(r)));
            }
            else
            {
                if (++_ReadCounts > int.Parse(Program.pos["OutBufferSize"]))
                {
                    this.txtResults.Text = "";
                    _ReadCounts = 0;
                }
                this.txtResults.Text += r;
                lblError.Visible = false;
            }
        }


        public void AddError(string r)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(() => this.AddError(r)));
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = string.Format("SocketException: {0}", r);

                }
            }
            catch { }
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            connect();
        }
        //cada segundo verifica la coneccion y la reintenta
        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }    


    
}


