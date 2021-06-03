namespace SmartUp
{
    partial class frmFileToSQL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFileToSQL));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblError = new System.Windows.Forms.Label();
            this.txtResults = new System.Windows.Forms.RichTextBox();
            this.CmdStopStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(12, 424);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(65, 17);
            this.lblError.TabIndex = 15;
            this.lblError.Text = "Error....";
            this.lblError.Visible = false;
            // 
            // txtResults
            // 
            this.txtResults.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.txtResults.ForeColor = System.Drawing.Color.Lime;
            this.txtResults.Location = new System.Drawing.Point(12, 12);
            this.txtResults.Name = "txtResults";
            this.txtResults.Size = new System.Drawing.Size(1034, 372);
            this.txtResults.TabIndex = 16;
            this.txtResults.Text = "";
            // 
            // CmdStopStart
            // 
            this.CmdStopStart.Location = new System.Drawing.Point(971, 390);
            this.CmdStopStart.Name = "CmdStopStart";
            this.CmdStopStart.Size = new System.Drawing.Size(75, 29);
            this.CmdStopStart.TabIndex = 17;
            this.CmdStopStart.Text = "Stop";
            this.CmdStopStart.UseVisualStyleBackColor = true;
            this.CmdStopStart.Click += new System.EventHandler(this.CmdStopStart_Click);
            // 
            // frmFileToSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 450);
            this.Controls.Add(this.CmdStopStart);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.lblError);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFileToSQL";
            this.Text = "SmartUp - SmartLab Solutions - File to SQL";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.RichTextBox txtResults;
        private System.Windows.Forms.Button CmdStopStart;
    }
}