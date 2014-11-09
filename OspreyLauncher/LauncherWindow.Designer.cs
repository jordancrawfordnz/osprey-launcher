namespace OspreyLauncher
{
    partial class LauncherWindow
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
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.icon3 = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.icon2 = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.icon1 = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Launch";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(93, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Hide";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(174, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 9;
            this.button5.Text = "Exit";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.icon3,
            this.icon2,
            this.icon1});
            this.shapeContainer1.Size = new System.Drawing.Size(1045, 579);
            this.shapeContainer1.TabIndex = 11;
            this.shapeContainer1.TabStop = false;
            // 
            // icon3
            // 
            this.icon3.BackColor = System.Drawing.Color.White;
            this.icon3.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.icon3.BorderColor = System.Drawing.Color.White;
            this.icon3.BorderWidth = 5;
            this.icon3.FillColor = System.Drawing.SystemColors.ControlDarkDark;
            this.icon3.FillGradientColor = System.Drawing.Color.DimGray;
            this.icon3.FillGradientStyle = Microsoft.VisualBasic.PowerPacks.FillGradientStyle.Central;
            this.icon3.Location = new System.Drawing.Point(687, 99);
            this.icon3.Name = "icon3";
            this.icon3.SelectionColor = System.Drawing.Color.Transparent;
            this.icon3.Size = new System.Drawing.Size(250, 250);
            // 
            // icon2
            // 
            this.icon2.BackColor = System.Drawing.Color.White;
            this.icon2.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.icon2.BorderColor = System.Drawing.Color.White;
            this.icon2.BorderWidth = 5;
            this.icon2.FillColor = System.Drawing.SystemColors.ControlDarkDark;
            this.icon2.FillGradientColor = System.Drawing.Color.DimGray;
            this.icon2.FillGradientStyle = Microsoft.VisualBasic.PowerPacks.FillGradientStyle.Central;
            this.icon2.Location = new System.Drawing.Point(342, 84);
            this.icon2.Name = "icon2";
            this.icon2.SelectionColor = System.Drawing.Color.Transparent;
            this.icon2.Size = new System.Drawing.Size(250, 250);
            // 
            // icon1
            // 
            this.icon1.BackColor = System.Drawing.Color.White;
            this.icon1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.icon1.BorderColor = System.Drawing.Color.White;
            this.icon1.BorderWidth = 5;
            this.icon1.FillColor = System.Drawing.SystemColors.ControlDarkDark;
            this.icon1.FillGradientColor = System.Drawing.Color.DimGray;
            this.icon1.FillGradientStyle = Microsoft.VisualBasic.PowerPacks.FillGradientStyle.Central;
            this.icon1.Location = new System.Drawing.Point(42, 88);
            this.icon1.Name = "icon1";
            this.icon1.SelectionColor = System.Drawing.Color.Transparent;
            this.icon1.Size = new System.Drawing.Size(250, 250);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(44, 373);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 31);
            this.label1.TabIndex = 12;
            this.label1.Text = "Movies and Shows";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(439, 391);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "TV Recordings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(764, 388);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Other Sources";
            // 
            // LauncherWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::OspreyLauncher.Properties.Resources._14408052591_6afa684f34_k;
            this.ClientSize = new System.Drawing.Size(1045, 579);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "LauncherWindow";
            this.Text = "LauncherWindow";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LauncherWindow_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.OvalShape icon3;
        private Microsoft.VisualBasic.PowerPacks.OvalShape icon2;
        private Microsoft.VisualBasic.PowerPacks.OvalShape icon1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}