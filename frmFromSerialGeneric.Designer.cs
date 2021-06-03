namespace SmartUp
{
    partial class FrmFromSerialGeneric
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFromSerialGeneric));
            this.txtResults = new System.Windows.Forms.RichTextBox();
            this.lblError = new System.Windows.Forms.Label();
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
            this.lblError.Location = new System.Drawing.Point(12, 405);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(65, 17);
            this.lblError.TabIndex = 14;
            this.lblError.Text = "Error....";
            this.lblError.Visible = false;
            // 
            // FrmFromSerialGeneric
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 431);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.txtResults);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmFromSerialGeneric";
            this.Text = "SmartUp - SmartLab Solutions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox txtResults;
        private System.Windows.Forms.Label lblError;
    }
}

