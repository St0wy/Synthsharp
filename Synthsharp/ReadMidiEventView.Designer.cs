namespace Synthsharp
{
    partial class ReadMidiEventView
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
            this.cbxDevice = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectDevice = new System.Windows.Forms.Button();
            this.lbxMessage = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // cbxDevice
            // 
            this.cbxDevice.FormattingEnabled = true;
            this.cbxDevice.Location = new System.Drawing.Point(12, 92);
            this.cbxDevice.Name = "cbxDevice";
            this.cbxDevice.Size = new System.Drawing.Size(162, 21);
            this.cbxDevice.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Dispositif de sortie :";
            // 
            // btnSelectDevice
            // 
            this.btnSelectDevice.Location = new System.Drawing.Point(12, 119);
            this.btnSelectDevice.Name = "btnSelectDevice";
            this.btnSelectDevice.Size = new System.Drawing.Size(162, 23);
            this.btnSelectDevice.TabIndex = 3;
            this.btnSelectDevice.Text = "Selectionner";
            this.btnSelectDevice.UseVisualStyleBackColor = true;
            this.btnSelectDevice.Click += new System.EventHandler(this.BtnSelectDevice_Click);
            // 
            // lbxMessage
            // 
            this.lbxMessage.FormattingEnabled = true;
            this.lbxMessage.Location = new System.Drawing.Point(191, 92);
            this.lbxMessage.Name = "lbxMessage";
            this.lbxMessage.ScrollAlwaysVisible = true;
            this.lbxMessage.Size = new System.Drawing.Size(566, 329);
            this.lbxMessage.TabIndex = 4;
            // 
            // ReadMidiEventView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbxMessage);
            this.Controls.Add(this.btnSelectDevice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxDevice);
            this.Name = "ReadMidiEventView";
            this.Text = "ReadMidiEventView";
            this.Load += new System.EventHandler(this.ReadMidiEventView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxDevice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectDevice;
        private System.Windows.Forms.ListBox lbxMessage;
    }
}