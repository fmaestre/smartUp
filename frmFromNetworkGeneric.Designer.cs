namespace SmartUp
{
    partial class FrmFromNetworkGeneric
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFromNetworkGeneric));
            this.txtResults = new System.Windows.Forms.RichTextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
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
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(897, 390);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(150, 29);
            this.btnReset.TabIndex = 15;
            this.btnReset.Text = "Borrar temporales";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // FrmFromNetworkGeneric
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 431);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.txtResults);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmFromNetworkGeneric";
            this.Text = "SmartUp - SmartLab Solutions";
            this.Shown += new System.EventHandler(this.FrmFromNetworkGeneric_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox txtResults;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnReset;
    }
}

