namespace JsonImageDownloader
{
    partial class Form1
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
            this.btnStartAppPool = new System.Windows.Forms.Button();
            this.btnStopAppPool = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartAppPool
            // 
            this.btnStartAppPool.Location = new System.Drawing.Point(93, 45);
            this.btnStartAppPool.Name = "btnStartAppPool";
            this.btnStartAppPool.Size = new System.Drawing.Size(80, 55);
            this.btnStartAppPool.TabIndex = 0;
            this.btnStartAppPool.Text = "Start App Pool";
            this.btnStartAppPool.UseVisualStyleBackColor = true;
            this.btnStartAppPool.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnStopAppPool
            // 
            this.btnStopAppPool.Location = new System.Drawing.Point(12, 45);
            this.btnStopAppPool.Name = "btnStopAppPool";
            this.btnStopAppPool.Size = new System.Drawing.Size(75, 55);
            this.btnStopAppPool.TabIndex = 1;
            this.btnStopAppPool.Text = "Stop App Pool";
            this.btnStopAppPool.UseVisualStyleBackColor = true;
            this.btnStopAppPool.Click += new System.EventHandler(this.btnStopAppPool_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnStopAppPool);
            this.Controls.Add(this.btnStartAppPool);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartAppPool;
        private System.Windows.Forms.Button btnStopAppPool;
    }
}

