namespace Store
{
    partial class Splash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splash));
            this.Progressbar = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.PercentageLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Progess = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Progess)).BeginInit();
            this.SuspendLayout();
            // 
            // Progressbar
            // 
            this.Progressbar.BorderRadius = 10;
            this.Progressbar.Location = new System.Drawing.Point(156, 388);
            this.Progressbar.Name = "Progressbar";
            this.Progressbar.Size = new System.Drawing.Size(604, 41);
            this.Progressbar.TabIndex = 0;
            this.Progressbar.Text = "guna2ProgressBar1";
            this.Progressbar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Heavy", 24F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(23, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(782, 61);
            this.label1.TabIndex = 2;
            this.label1.Text = "Online Shop Management System";
            // 
            // PercentageLbl
            // 
            this.PercentageLbl.AutoSize = true;
            this.PercentageLbl.Font = new System.Drawing.Font("Franklin Gothic Heavy", 16F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PercentageLbl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.PercentageLbl.Location = new System.Drawing.Point(374, 197);
            this.PercentageLbl.Name = "PercentageLbl";
            this.PercentageLbl.Size = new System.Drawing.Size(67, 37);
            this.PercentageLbl.TabIndex = 3;
            this.PercentageLbl.Text = "%%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Franklin Gothic Heavy", 20F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(189, 318);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(339, 47);
            this.label3.TabIndex = 4;
            this.label3.Text = "Loading Modules";
            // 
            // Progess
            // 
            this.Progess.Image = ((System.Drawing.Image)(resources.GetObject("Progess.Image")));
            this.Progess.Location = new System.Drawing.Point(34, 358);
            this.Progess.Name = "Progess";
            this.Progess.Size = new System.Drawing.Size(104, 82);
            this.Progess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Progess.TabIndex = 5;
            this.Progess.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkBlue;
            this.ClientSize = new System.Drawing.Size(806, 465);
            this.Controls.Add(this.Progess);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PercentageLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Progressbar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Splash";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Progess)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ProgressBar Progressbar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label PercentageLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox Progess;
        private System.Windows.Forms.Timer timer1;
    }
}

