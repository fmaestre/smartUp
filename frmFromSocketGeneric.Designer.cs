namespace SmartUp
{
    partial class FrmFromSocketGeneric
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFromSocketGeneric));
            this.txtResults = new System.Windows.Forms.RichTextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.btnConectar = new System.Windows.Forms.Button();
            this.lblConnected = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // txtResults
            // 
            this.txtResults.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.txtResults.ForeColor = System.Drawing.Color.Lime;
            this.txtResults.Location = new System.Drawing.Point(13, 12);
            this.txtResults.Name = "txtResults";
            this.txtResults.Size = new System.Drawing.Size(1034, 372);
            this.txtResults.TabIndex = 9;
            this.txtResults.Text = "";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(12, 399);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(65, 17);
            this.lblError.TabIndex = 14;
            this.lblError.Text = "Error....";
            this.lblError.Visible = false;
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(972, 390);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(75, 23);
            this.btnConectar.TabIndex = 15;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // lblConnected
            // 
            this.lblConnected.AutoSize = true;
            this.lblConnected.ForeColor = System.Drawing.Color.Green;
            this.lblConnected.Location = new System.Drawing.Point(969, 413);
            this.lblConnected.Name = "lblConnected";
            this.lblConnected.Size = new System.Drawing.Size(76, 17);
            this.lblConnected.TabIndex = 16;
            this.lblConnected.Text = "Conectado";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmFromSocketGeneric
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 431);
            this.Controls.Add(this.lblConnected);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.txtResults);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmFromSocketGeneric";
            this.Text = "SmartUp - SmartLab Solutions";
            this.Shown += new System.EventHandler(this.FrmFromSocketGeneric_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox txtResults;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Label lblConnected;
        private System.Windows.Forms.Timer timer1;
    }
}

