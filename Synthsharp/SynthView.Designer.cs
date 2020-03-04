namespace Synthsharp
{
    partial class SynthView
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNoise1 = new System.Windows.Forms.Button();
            this.btnTriangle1 = new System.Windows.Forms.Button();
            this.btnSawTooth1 = new System.Windows.Forms.Button();
            this.btnSquare1 = new System.Windows.Forms.Button();
            this.btnSine1 = new System.Windows.Forms.Button();
            this.trbOscillator1 = new System.Windows.Forms.TrackBar();
            this.chkOnOscillator1 = new System.Windows.Forms.CheckBox();
            this.groupbox2 = new System.Windows.Forms.GroupBox();
            this.trbOscillator2 = new System.Windows.Forms.TrackBar();
            this.btnNoise2 = new System.Windows.Forms.Button();
            this.btnTriangle2 = new System.Windows.Forms.Button();
            this.btnSawTooth2 = new System.Windows.Forms.Button();
            this.btnSquare2 = new System.Windows.Forms.Button();
            this.btnSine2 = new System.Windows.Forms.Button();
            this.chkOnOscillator2 = new System.Windows.Forms.CheckBox();
            this.cbxDevice = new System.Windows.Forms.ComboBox();
            this.btnSine3 = new System.Windows.Forms.Button();
            this.btnSquare3 = new System.Windows.Forms.Button();
            this.btnSawTooth3 = new System.Windows.Forms.Button();
            this.btnTriangle3 = new System.Windows.Forms.Button();
            this.chkOnOscillator3 = new System.Windows.Forms.CheckBox();
            this.btnNoise3 = new System.Windows.Forms.Button();
            this.trbOscillator3 = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbOscillator1)).BeginInit();
            this.groupbox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbOscillator2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbOscillator3)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNoise1);
            this.groupBox1.Controls.Add(this.btnTriangle1);
            this.groupBox1.Controls.Add(this.btnSawTooth1);
            this.groupBox1.Controls.Add(this.btnSquare1);
            this.groupBox1.Controls.Add(this.btnSine1);
            this.groupBox1.Controls.Add(this.trbOscillator1);
            this.groupBox1.Controls.Add(this.chkOnOscillator1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(251, 131);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Oscillator 1";
            // 
            // btnNoise1
            // 
            this.btnNoise1.Location = new System.Drawing.Point(81, 48);
            this.btnNoise1.Name = "btnNoise1";
            this.btnNoise1.Size = new System.Drawing.Size(69, 23);
            this.btnNoise1.TabIndex = 4;
            this.btnNoise1.Tag = "1N";
            this.btnNoise1.Text = "Noise";
            this.btnNoise1.UseVisualStyleBackColor = true;
            this.btnNoise1.Click += new System.EventHandler(this.BtnWaveForm_Click);
            // 
            // btnTriangle1
            // 
            this.btnTriangle1.Location = new System.Drawing.Point(156, 19);
            this.btnTriangle1.Name = "btnTriangle1";
            this.btnTriangle1.Size = new System.Drawing.Size(69, 23);
            this.btnTriangle1.TabIndex = 5;
            this.btnTriangle1.Tag = "1T";
            this.btnTriangle1.Text = "Triangle";
            this.btnTriangle1.UseVisualStyleBackColor = true;
            this.btnTriangle1.Click += new System.EventHandler(this.BtnWaveForm_Click);
            // 
            // btnSawTooth1
            // 
            this.btnSawTooth1.Location = new System.Drawing.Point(6, 48);
            this.btnSawTooth1.Name = "btnSawTooth1";
            this.btnSawTooth1.Size = new System.Drawing.Size(69, 23);
            this.btnSawTooth1.TabIndex = 3;
            this.btnSawTooth1.Tag = "1A";
            this.btnSawTooth1.Text = "SawTooth";
            this.btnSawTooth1.UseVisualStyleBackColor = true;
            this.btnSawTooth1.Click += new System.EventHandler(this.BtnWaveForm_Click);
            // 
            // btnSquare1
            // 
            this.btnSquare1.Location = new System.Drawing.Point(81, 19);
            this.btnSquare1.Name = "btnSquare1";
            this.btnSquare1.Size = new System.Drawing.Size(69, 23);
            this.btnSquare1.TabIndex = 2;
            this.btnSquare1.Tag = "1Q";
            this.btnSquare1.Text = "Square";
            this.btnSquare1.UseVisualStyleBackColor = true;
            this.btnSquare1.Click += new System.EventHandler(this.BtnWaveForm_Click);
            // 
            // btnSine1
            // 
            this.btnSine1.Location = new System.Drawing.Point(6, 19);
            this.btnSine1.Name = "btnSine1";
            this.btnSine1.Size = new System.Drawing.Size(69, 23);
            this.btnSine1.TabIndex = 1;
            this.btnSine1.Tag = "1S";
            this.btnSine1.Text = "Sin";
            this.btnSine1.UseVisualStyleBackColor = true;
            this.btnSine1.Click += new System.EventHandler(this.BtnWaveForm_Click);
            // 
            // trbOscillator1
            // 
            this.trbOscillator1.Cursor = System.Windows.Forms.Cursors.Default;
            this.trbOscillator1.Location = new System.Drawing.Point(6, 77);
            this.trbOscillator1.Maximum = 100;
            this.trbOscillator1.Name = "trbOscillator1";
            this.trbOscillator1.Size = new System.Drawing.Size(234, 45);
            this.trbOscillator1.SmallChange = 1000;
            this.trbOscillator1.TabIndex = 12;
            this.trbOscillator1.Value = 100;
            this.trbOscillator1.Scroll += new System.EventHandler(this.TrbOscillator1_Scroll);
            // 
            // chkOnOscillator1
            // 
            this.chkOnOscillator1.AutoSize = true;
            this.chkOnOscillator1.Checked = true;
            this.chkOnOscillator1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOnOscillator1.Location = new System.Drawing.Point(185, 54);
            this.chkOnOscillator1.Name = "chkOnOscillator1";
            this.chkOnOscillator1.Size = new System.Drawing.Size(40, 17);
            this.chkOnOscillator1.TabIndex = 8;
            this.chkOnOscillator1.Tag = "1";
            this.chkOnOscillator1.Text = "On";
            this.chkOnOscillator1.UseVisualStyleBackColor = true;
            this.chkOnOscillator1.CheckedChanged += new System.EventHandler(this.ChkOnOscillator_CheckedChanged);
            // 
            // groupbox2
            // 
            this.groupbox2.Controls.Add(this.trbOscillator2);
            this.groupbox2.Controls.Add(this.btnNoise2);
            this.groupbox2.Controls.Add(this.btnTriangle2);
            this.groupbox2.Controls.Add(this.btnSawTooth2);
            this.groupbox2.Controls.Add(this.btnSquare2);
            this.groupbox2.Controls.Add(this.btnSine2);
            this.groupbox2.Controls.Add(this.chkOnOscillator2);
            this.groupbox2.Location = new System.Drawing.Point(12, 149);
            this.groupbox2.Name = "groupbox2";
            this.groupbox2.Size = new System.Drawing.Size(251, 131);
            this.groupbox2.TabIndex = 2;
            this.groupbox2.TabStop = false;
            this.groupbox2.Text = "Oscillator 2";
            // 
            // trbOscillator2
            // 
            this.trbOscillator2.Cursor = System.Windows.Forms.Cursors.Default;
            this.trbOscillator2.Location = new System.Drawing.Point(9, 78);
            this.trbOscillator2.Maximum = 100;
            this.trbOscillator2.Name = "trbOscillator2";
            this.trbOscillator2.Size = new System.Drawing.Size(234, 45);
            this.trbOscillator2.SmallChange = 1000;
            this.trbOscillator2.TabIndex = 24;
            this.trbOscillator2.Value = 100;
            this.trbOscillator2.Scroll += new System.EventHandler(this.TrbOscillator2_Scroll);
            // 
            // btnNoise2
            // 
            this.btnNoise2.Location = new System.Drawing.Point(81, 49);
            this.btnNoise2.Name = "btnNoise2";
            this.btnNoise2.Size = new System.Drawing.Size(69, 23);
            this.btnNoise2.TabIndex = 16;
            this.btnNoise2.Tag = "2N";
            this.btnNoise2.Text = "Noise";
            this.btnNoise2.UseVisualStyleBackColor = true;
            this.btnNoise2.Click += new System.EventHandler(this.BtnWaveForm_Click);
            // 
            // btnTriangle2
            // 
            this.btnTriangle2.Location = new System.Drawing.Point(156, 20);
            this.btnTriangle2.Name = "btnTriangle2";
            this.btnTriangle2.Size = new System.Drawing.Size(69, 23);
            this.btnTriangle2.TabIndex = 17;
            this.btnTriangle2.Tag = "2T";
            this.btnTriangle2.Text = "Triangle";
            this.btnTriangle2.UseVisualStyleBackColor = true;
            this.btnTriangle2.Click += new System.EventHandler(this.BtnWaveForm_Click);
            // 
            // btnSawTooth2
            // 
            this.btnSawTooth2.Location = new System.Drawing.Point(6, 49);
            this.btnSawTooth2.Name = "btnSawTooth2";
            this.btnSawTooth2.Size = new System.Drawing.Size(69, 23);
            this.btnSawTooth2.TabIndex = 15;
            this.btnSawTooth2.Tag = "2A";
            this.btnSawTooth2.Text = "SawTooth";
            this.btnSawTooth2.UseVisualStyleBackColor = true;
            this.btnSawTooth2.Click += new System.EventHandler(this.BtnWaveForm_Click);
            // 
            // btnSquare2
            // 
            this.btnSquare2.Location = new System.Drawing.Point(81, 20);
            this.btnSquare2.Name = "btnSquare2";
            this.btnSquare2.Size = new System.Drawing.Size(69, 23);
            this.btnSquare2.TabIndex = 14;
            this.btnSquare2.Tag = "2Q";
            this.btnSquare2.Text = "Square";
            this.btnSquare2.UseVisualStyleBackColor = true;
            this.btnSquare2.Click += new System.EventHandler(this.BtnWaveForm_Click);
            // 
            // btnSine2
            // 
            this.btnSine2.Location = new System.Drawing.Point(6, 20);
            this.btnSine2.Name = "btnSine2";
            this.btnSine2.Size = new System.Drawing.Size(69, 23);
            this.btnSine2.TabIndex = 13;
            this.btnSine2.Tag = "2S";
            this.btnSine2.Text = "Sin";
            this.btnSine2.UseVisualStyleBackColor = true;
            this.btnSine2.Click += new System.EventHandler(this.BtnWaveForm_Click);
            // 
            // chkOnOscillator2
            // 
            this.chkOnOscillator2.AutoSize = true;
            this.chkOnOscillator2.Checked = true;
            this.chkOnOscillator2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOnOscillator2.Location = new System.Drawing.Point(185, 53);
            this.chkOnOscillator2.Name = "chkOnOscillator2";
            this.chkOnOscillator2.Size = new System.Drawing.Size(40, 17);
            this.chkOnOscillator2.TabIndex = 20;
            this.chkOnOscillator2.Tag = "2";
            this.chkOnOscillator2.Text = "On";
            this.chkOnOscillator2.UseVisualStyleBackColor = true;
            this.chkOnOscillator2.CheckedChanged += new System.EventHandler(this.ChkOnOscillator_CheckedChanged);
            // 
            // cbxDevice
            // 
            this.cbxDevice.FormattingEnabled = true;
            this.cbxDevice.Location = new System.Drawing.Point(12, 423);
            this.cbxDevice.Name = "cbxDevice";
            this.cbxDevice.Size = new System.Drawing.Size(170, 21);
            this.cbxDevice.TabIndex = 4;
            this.cbxDevice.SelectedIndexChanged += new System.EventHandler(this.CbxDevice_SelectedIndexChanged);
            // 
            // btnSine3
            // 
            this.btnSine3.Location = new System.Drawing.Point(6, 20);
            this.btnSine3.Name = "btnSine3";
            this.btnSine3.Size = new System.Drawing.Size(69, 23);
            this.btnSine3.TabIndex = 25;
            this.btnSine3.Tag = "3S";
            this.btnSine3.Text = "Sin";
            this.btnSine3.UseVisualStyleBackColor = true;
            this.btnSine3.Click += new System.EventHandler(this.BtnWaveForm_Click);
            // 
            // btnSquare3
            // 
            this.btnSquare3.Location = new System.Drawing.Point(81, 20);
            this.btnSquare3.Name = "btnSquare3";
            this.btnSquare3.Size = new System.Drawing.Size(69, 23);
            this.btnSquare3.TabIndex = 26;
            this.btnSquare3.Tag = "3Q";
            this.btnSquare3.Text = "Square";
            this.btnSquare3.UseVisualStyleBackColor = true;
            this.btnSquare3.Click += new System.EventHandler(this.BtnWaveForm_Click);
            // 
            // btnSawTooth3
            // 
            this.btnSawTooth3.Location = new System.Drawing.Point(6, 49);
            this.btnSawTooth3.Name = "btnSawTooth3";
            this.btnSawTooth3.Size = new System.Drawing.Size(69, 23);
            this.btnSawTooth3.TabIndex = 27;
            this.btnSawTooth3.Tag = "3A";
            this.btnSawTooth3.Text = "SawTooth";
            this.btnSawTooth3.UseVisualStyleBackColor = true;
            this.btnSawTooth3.Click += new System.EventHandler(this.BtnWaveForm_Click);
            // 
            // btnTriangle3
            // 
            this.btnTriangle3.Location = new System.Drawing.Point(156, 20);
            this.btnTriangle3.Name = "btnTriangle3";
            this.btnTriangle3.Size = new System.Drawing.Size(69, 23);
            this.btnTriangle3.TabIndex = 29;
            this.btnTriangle3.Tag = "3T";
            this.btnTriangle3.Text = "Triangle";
            this.btnTriangle3.UseVisualStyleBackColor = true;
            this.btnTriangle3.Click += new System.EventHandler(this.BtnWaveForm_Click);
            // 
            // chkOnOscillator3
            // 
            this.chkOnOscillator3.AutoSize = true;
            this.chkOnOscillator3.Checked = true;
            this.chkOnOscillator3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOnOscillator3.Location = new System.Drawing.Point(185, 53);
            this.chkOnOscillator3.Name = "chkOnOscillator3";
            this.chkOnOscillator3.Size = new System.Drawing.Size(40, 17);
            this.chkOnOscillator3.TabIndex = 32;
            this.chkOnOscillator3.Tag = "3";
            this.chkOnOscillator3.Text = "On";
            this.chkOnOscillator3.UseVisualStyleBackColor = true;
            this.chkOnOscillator3.CheckedChanged += new System.EventHandler(this.ChkOnOscillator_CheckedChanged);
            // 
            // btnNoise3
            // 
            this.btnNoise3.Location = new System.Drawing.Point(81, 49);
            this.btnNoise3.Name = "btnNoise3";
            this.btnNoise3.Size = new System.Drawing.Size(69, 23);
            this.btnNoise3.TabIndex = 28;
            this.btnNoise3.Tag = "3N";
            this.btnNoise3.Text = "Noise";
            this.btnNoise3.UseVisualStyleBackColor = true;
            this.btnNoise3.Click += new System.EventHandler(this.BtnWaveForm_Click);
            // 
            // trbOscillator3
            // 
            this.trbOscillator3.Cursor = System.Windows.Forms.Cursors.Default;
            this.trbOscillator3.Location = new System.Drawing.Point(6, 78);
            this.trbOscillator3.Maximum = 100;
            this.trbOscillator3.Name = "trbOscillator3";
            this.trbOscillator3.Size = new System.Drawing.Size(234, 45);
            this.trbOscillator3.SmallChange = 1000;
            this.trbOscillator3.TabIndex = 36;
            this.trbOscillator3.Tag = "";
            this.trbOscillator3.Value = 100;
            this.trbOscillator3.Scroll += new System.EventHandler(this.TrbOscillator3_Scroll);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.trbOscillator3);
            this.groupBox3.Controls.Add(this.btnNoise3);
            this.groupBox3.Controls.Add(this.chkOnOscillator3);
            this.groupBox3.Controls.Add(this.btnTriangle3);
            this.groupBox3.Controls.Add(this.btnSawTooth3);
            this.groupBox3.Controls.Add(this.btnSquare3);
            this.groupBox3.Controls.Add(this.btnSine3);
            this.groupBox3.Location = new System.Drawing.Point(12, 286);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(251, 131);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Oscillator 3";
            // 
            // SynthView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 453);
            this.Controls.Add(this.cbxDevice);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupbox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SynthView";
            this.Text = "Synthsharp";
            this.Load += new System.EventHandler(this.SynthView_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbOscillator1)).EndInit();
            this.groupbox2.ResumeLayout(false);
            this.groupbox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbOscillator2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbOscillator3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkOnOscillator1;
        private System.Windows.Forms.GroupBox groupbox2;
        private System.Windows.Forms.CheckBox chkOnOscillator2;
        private System.Windows.Forms.TrackBar trbOscillator1;
        private System.Windows.Forms.Button btnSine1;
        private System.Windows.Forms.Button btnSquare1;
        private System.Windows.Forms.Button btnNoise1;
        private System.Windows.Forms.Button btnTriangle1;
        private System.Windows.Forms.Button btnSawTooth1;
        private System.Windows.Forms.Button btnNoise2;
        private System.Windows.Forms.Button btnTriangle2;
        private System.Windows.Forms.Button btnSawTooth2;
        private System.Windows.Forms.Button btnSquare2;
        private System.Windows.Forms.Button btnSine2;
        private System.Windows.Forms.TrackBar trbOscillator2;
        private System.Windows.Forms.ComboBox cbxDevice;
        private System.Windows.Forms.Button btnSine3;
        private System.Windows.Forms.Button btnSquare3;
        private System.Windows.Forms.Button btnSawTooth3;
        private System.Windows.Forms.Button btnTriangle3;
        private System.Windows.Forms.CheckBox chkOnOscillator3;
        private System.Windows.Forms.Button btnNoise3;
        private System.Windows.Forms.TrackBar trbOscillator3;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

