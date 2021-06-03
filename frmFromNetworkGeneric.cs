
using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;

using System.Threading;

namespace SmartUp
{

    public partial class FrmFromNetworkGeneric : Form
    {
        private string _interfaceName = "";
        private int _ReadCounts = 0;
        Thread th;
        public FrmFromNetworkGeneric(string interfaceName)
        {
            try
            {
                InitializeComponent();
                _interfaceName = interfaceName;
                
                this.Text += "- [" + interfaceName + "] " + Program.pos["NetworkPort"] + " ***" + Program.AssemblyName + "***";
                //openPort();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }        
        private void SetText(string newText)
        {
            if (!string.IsNullOrEmpty(newText))
            {
               this.AddText(newText);
               
                //save to file
                var path = Program.pos["SavePath"]; //the path to save interface files
                var file = path + _interfaceName + DateTime.Today.ToString("MMddyyyy") + ".txt";
                
                if (!System.IO.File.Exists(file))
                {
                    //System.IO.File.Create(file).Close();                    
                    System.IO.File.Create(file).Dispose(); //2021Feb23 - to fix The Process Cannot Access The File XYZ Because It Is Being Used By Another Process
                }

                var allText = System.IO.File.ReadAllText(file);

                System.IO.File.WriteAllText(file, allText + newText);
            }
        }
        private void FrmFromNetworkGeneric_Shown(object sender, EventArgs e)
        {
            //openPort();
            startThread();
        }

        private void startThread()
        {
            th = new Thread(OpenPort);
            th.Start();            
        }

        public void OpenPort()
        {

            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = int.Parse(Program.pos["NetworkPort"]);
                string IpAdrress = Program.pos["NetworkIP"];
                IPAddress localAddr = IPAddress.Parse(IpAdrress);

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                // Enter the listening loop.
                while (true)
                {
                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    //txtResults.Text += "Connected!";


                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;                    
                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        SetText(data);
                        

                        /*
                        // Process the data sent by the client.
                        data = data.ToUpper();

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        txtResults2.Text += string.Format("Sent: {0}", data) + _NEWLINE_;
                        */
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException ex)
            {
                this.AddError(ex.Message);                
            }
            catch(Exception ex)
            {
                this.AddError(ex.Message);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }



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

        private void btnReset_Click(object sender, EventArgs e)
        {
            var path = Program.pos["SavePath"]; //the path of all interface files
            var files = Directory.GetFiles(@path, "*.txt");

            foreach (var file in files)
            {
                try
                {
                    System.IO.File.Delete(file);                    
                }
                catch(UnauthorizedAccessException)
                {
                    lblError.Text += "Usuarion no tiene acceso para borrar archivos en" + @path;
                }
                catch (Exception ex)
                {
                    lblError.Text += ex.Message;
                }                
            }

        }
    }    


    
}


