namespace HalyomorphaHalys.DesktopApp
{
    partial class HalyomorphaHalysClassifier
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HalyomorphaHalysClassifier));
            pictureBox1 = new PictureBox();
            groupBox1 = new GroupBox();
            pbBugImage = new PictureBox();
            groupBox2 = new GroupBox();
            label1 = new Label();
            btnSelectImage = new Button();
            txtImagePath = new TextBox();
            groupBox3 = new GroupBox();
            txtClassificationResult = new TextBox();
            btnPredict = new Button();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbBugImage).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(7, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(786, 289);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(pbBugImage);
            groupBox1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            groupBox1.Location = new Point(-10, 10);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(409, 387);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Bug Image";
            // 
            // pbBugImage
            // 
            pbBugImage.Dock = DockStyle.Fill;
            pbBugImage.Location = new Point(3, 35);
            pbBugImage.Name = "pbBugImage";
            pbBugImage.Size = new Size(403, 349);
            pbBugImage.SizeMode = PictureBoxSizeMode.StretchImage;
            pbBugImage.TabIndex = 0;
            pbBugImage.TabStop = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(btnSelectImage);
            groupBox2.Controls.Add(txtImagePath);
            groupBox2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            groupBox2.Location = new Point(406, 10);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(357, 180);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Hazelnut Image";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ButtonHighlight;
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Palatino Linotype", 12F, FontStyle.Bold);
            label1.Location = new Point(6, 35);
            label1.Name = "label1";
            label1.Size = new Size(193, 29);
            label1.TabIndex = 7;
            label1.Text = "Selected Bug Image";
            // 
            // btnSelectImage
            // 
            btnSelectImage.Dock = DockStyle.Bottom;
            btnSelectImage.Font = new Font("Palatino Linotype", 12F, FontStyle.Bold);
            btnSelectImage.Location = new Point(3, 133);
            btnSelectImage.Name = "btnSelectImage";
            btnSelectImage.Size = new Size(351, 44);
            btnSelectImage.TabIndex = 6;
            btnSelectImage.Text = "Select Image...";
            btnSelectImage.UseVisualStyleBackColor = true;
            btnSelectImage.Click += btnSelectImage_Click;
            // 
            // txtImagePath
            // 
            txtImagePath.BorderStyle = BorderStyle.FixedSingle;
            txtImagePath.Font = new Font("Palatino Linotype", 12F);
            txtImagePath.Location = new Point(6, 77);
            txtImagePath.Name = "txtImagePath";
            txtImagePath.ReadOnly = true;
            txtImagePath.Size = new Size(348, 34);
            txtImagePath.TabIndex = 5;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(txtClassificationResult);
            groupBox3.Controls.Add(btnPredict);
            groupBox3.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            groupBox3.Location = new Point(409, 199);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(357, 198);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Classification Result";
            // 
            // txtClassificationResult
            // 
            txtClassificationResult.BorderStyle = BorderStyle.FixedSingle;
            txtClassificationResult.Dock = DockStyle.Bottom;
            txtClassificationResult.Enabled = false;
            txtClassificationResult.Font = new Font("Palatino Linotype", 16F, FontStyle.Bold);
            txtClassificationResult.Location = new Point(3, 84);
            txtClassificationResult.Multiline = true;
            txtClassificationResult.Name = "txtClassificationResult";
            txtClassificationResult.Size = new Size(351, 111);
            txtClassificationResult.TabIndex = 3;
            txtClassificationResult.TextAlign = HorizontalAlignment.Center;
            // 
            // btnPredict
            // 
            btnPredict.Dock = DockStyle.Top;
            btnPredict.Font = new Font("Palatino Linotype", 12F, FontStyle.Bold);
            btnPredict.Location = new Point(3, 35);
            btnPredict.Name = "btnPredict";
            btnPredict.Size = new Size(351, 39);
            btnPredict.TabIndex = 2;
            btnPredict.Text = "Predict Bug";
            btnPredict.UseVisualStyleBackColor = true;
            btnPredict.Click += btnPredict_Click;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(groupBox3);
            panel1.Controls.Add(groupBox2);
            panel1.Location = new Point(7, 299);
            panel1.Name = "panel1";
            panel1.Size = new Size(786, 402);
            panel1.TabIndex = 4;
            // 
            // HalyomorphaHalysClassifier
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(798, 704);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            Name = "HalyomorphaHalysClassifier";
            Text = "Halyomorpha Halys Classifier";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbBugImage).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private GroupBox groupBox1;
        private PictureBox pbBugImage;
        private GroupBox groupBox2;
        private Label label1;
        private Button btnSelectImage;
        private TextBox txtImagePath;
        private GroupBox groupBox3;
        private TextBox txtClassificationResult;
        private Button btnPredict;
        private Panel panel1;
    }
}
