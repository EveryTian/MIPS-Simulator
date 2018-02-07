namespace Simulator
{
    partial class ChangeValueDialog
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.registerSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.newValueTextBox = new System.Windows.Forms.TextBox();
            this.locationLabel = new System.Windows.Forms.Label();
            this.valueLabel = new System.Windows.Forms.Label();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.registerModePromptLabel = new System.Windows.Forms.Label();
            this.memoryModePromptLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(36, 147);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(153, 147);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // registerSelectionComboBox
            // 
            this.registerSelectionComboBox.FormattingEnabled = true;
            this.registerSelectionComboBox.Items.AddRange(new object[] {
            "PC",
            "$at",
            "$v0",
            "$v1",
            "$a0",
            "$a1",
            "$a2",
            "$a3",
            "$t0",
            "$t1",
            "$t2",
            "$t3",
            "$t4",
            "$t5",
            "$t6",
            "$t7",
            "$s0",
            "$s1",
            "$s2",
            "$s3",
            "$s4",
            "$s5",
            "$s6",
            "$s7",
            "$t8",
            "$t9",
            "$k0",
            "$k1",
            "$gp",
            "$sp",
            "$fp",
            "$ra"});
            this.registerSelectionComboBox.Location = new System.Drawing.Point(77, 68);
            this.registerSelectionComboBox.Name = "registerSelectionComboBox";
            this.registerSelectionComboBox.Size = new System.Drawing.Size(81, 20);
            this.registerSelectionComboBox.TabIndex = 3;
            this.registerSelectionComboBox.Visible = false;
            // 
            // newValueTextBox
            // 
            this.newValueTextBox.Location = new System.Drawing.Point(77, 103);
            this.newValueTextBox.Name = "newValueTextBox";
            this.newValueTextBox.Size = new System.Drawing.Size(81, 21);
            this.newValueTextBox.TabIndex = 4;
            this.newValueTextBox.Text = "x00000000";
            // 
            // locationLabel
            // 
            this.locationLabel.AutoSize = true;
            this.locationLabel.Location = new System.Drawing.Point(12, 73);
            this.locationLabel.Name = "locationLabel";
            this.locationLabel.Size = new System.Drawing.Size(53, 12);
            this.locationLabel.TabIndex = 5;
            this.locationLabel.Text = "Location";
            // 
            // valueLabel
            // 
            this.valueLabel.AutoSize = true;
            this.valueLabel.Location = new System.Drawing.Point(11, 109);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(35, 12);
            this.valueLabel.TabIndex = 5;
            this.valueLabel.Text = "Value";
            // 
            // addressTextBox
            // 
            this.addressTextBox.Location = new System.Drawing.Point(77, 67);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(81, 21);
            this.addressTextBox.TabIndex = 7;
            this.addressTextBox.Text = "x00000000";
            this.addressTextBox.Visible = false;
            // 
            // registerModePromptLabel
            // 
            this.registerModePromptLabel.AutoSize = true;
            this.registerModePromptLabel.Location = new System.Drawing.Point(12, 10);
            this.registerModePromptLabel.Name = "registerModePromptLabel";
            this.registerModePromptLabel.Size = new System.Drawing.Size(227, 48);
            this.registerModePromptLabel.TabIndex = 6;
            this.registerModePromptLabel.Text = "  The input value can be either a hex\r\nnumber or a dec num. \r\n(The value of PC sh" +
    "all be a num which\r\nis divisible by 4.)";
            this.registerModePromptLabel.Visible = false;
            // 
            // memoryModePromptLabel
            // 
            this.memoryModePromptLabel.AutoSize = true;
            this.memoryModePromptLabel.Location = new System.Drawing.Point(12, 10);
            this.memoryModePromptLabel.Name = "memoryModePromptLabel";
            this.memoryModePromptLabel.Size = new System.Drawing.Size(227, 48);
            this.memoryModePromptLabel.TabIndex = 6;
            this.memoryModePromptLabel.Text = "  The input address shall be a num\r\nwhich can be divisible by 4.\r\n  Both two inpu" +
    "t values can be either\r\na hex number or a dec num.\r\n";
            this.memoryModePromptLabel.Visible = false;
            // 
            // ChangeValueDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(262, 191);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.registerModePromptLabel);
            this.Controls.Add(this.newValueTextBox);
            this.Controls.Add(this.registerSelectionComboBox);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.locationLabel);
            this.Controls.Add(this.valueLabel);
            this.Controls.Add(this.memoryModePromptLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ChangeValueDialog";
            this.Text = "ChangeValueDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox registerSelectionComboBox;
        private System.Windows.Forms.TextBox newValueTextBox;
        private System.Windows.Forms.Label locationLabel;
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.Label registerModePromptLabel;
        private System.Windows.Forms.Label memoryModePromptLabel;
    }
}