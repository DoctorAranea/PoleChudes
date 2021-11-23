namespace Pole_Chudes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.poleChudes1 = new Pole_Chudes.PoleChudes();
            this.SuspendLayout();
            // 
            // poleChudes1
            // 
            this.poleChudes1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.poleChudes1.Cursor = System.Windows.Forms.Cursors.Default;
            this.poleChudes1.Location = new System.Drawing.Point(1, 1);
            this.poleChudes1.Name = "poleChudes1";
            this.poleChudes1.PlayerCount = 2;
            this.poleChudes1.Size = new System.Drawing.Size(551, 417);
            this.poleChudes1.TabIndex = 0;
            this.poleChudes1.Text = "poleChudes1";
            this.poleChudes1.SizeChanged += new System.EventHandler(this.poleChudes1_SizeChanged);
            this.poleChudes1.Click += new System.EventHandler(this.poleChudes1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(553, 419);
            this.Controls.Add(this.poleChudes1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Поле Чудес";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private PoleChudes poleChudes1;
    }
}

