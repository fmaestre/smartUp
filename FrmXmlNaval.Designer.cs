namespace SmartUp
{
    partial class FrmXmlNaval
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmXmlNaval));
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.txtResults = new System.Windows.Forms.RichTextBox();
            this.txt2Upload = new System.Windows.Forms.RichTextBox();
            this.cmdInterface = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.ForeColor = System.Drawing.Color.Black;
            this.btnLoadFile.Location = new System.Drawing.Point(13, 13);
            this.btnLoadFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(209, 27);
            this.btnLoadFile.TabIndex = 8;
            this.btnLoadFile.Text = "Cargar archivo XML===>";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // txtResults
            // 
            this.txtResults.Location = new System.Drawing.Point(13, 48);
            this.txtResults.Name = "txtResults";
            this.txtResults.Size = new System.Drawing.Size(1493, 77);
            this.txtResults.TabIndex = 9;
            this.txtResults.Text = "";
            // 
            // txt2Upload
            // 
            this.txt2Upload.Location = new System.Drawing.Point(12, 143);
            this.txt2Upload.Name = "txt2Upload";
            this.txt2Upload.Size = new System.Drawing.Size(1494, 77);
            this.txt2Upload.TabIndex = 10;
            this.txt2Upload.Text = "";
            // 
            // cmdInterface
            // 
            this.cmdInterface.ForeColor = System.Drawing.Color.Black;
            this.cmdInterface.Location = new System.Drawing.Point(12, 227);
            this.cmdInterface.Margin = new System.Windows.Forms.Padding(4);
            this.cmdInterface.Name = "cmdInterface";
            this.cmdInterface.Size = new System.Drawing.Size(209, 27);
            this.cmdInterface.TabIndex = 11;
            this.cmdInterface.Text = "Grabar en Interface===>";
            this.cmdInterface.UseVisualStyleBackColor = true;
            this.cmdInterface.Click += new System.EventHandler(this.cmdInterface_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(228, 229);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(322, 23);
            this.progressBar1.TabIndex = 12;
            // 
            // FrmXmlNaval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1518, 267);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.cmdInterface);
            this.Controls.Add(this.txt2Upload);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.btnLoadFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmXmlNaval";
            this.Text = "SmartUp - SmartLab Solutions";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.RichTextBox txtResults;
        private System.Windows.Forms.RichTextBox txt2Upload;
        private System.Windows.Forms.Button cmdInterface;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

