namespace SmartStandards {
  partial class MainForm {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      label1 = new Label();
      UidTextBox = new TextBox();
      CreateUidButton = new Button();
      DecodedDateTimeTextBox = new TextBox();
      label2 = new Label();
      TokenTextBox = new TextBox();
      label3 = new Label();
      EncodedTokenTextBox = new TextBox();
      TokenRawTextBox = new TextBox();
      EncodingErrorLabel = new Label();
      this.SuspendLayout();
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label1.Location = new Point(12, 9);
      label1.Name = "label1";
      label1.Size = new Size(103, 21);
      label1.TabIndex = 0;
      label1.Text = "SnowFlake44";
      // 
      // UidTextBox
      // 
      UidTextBox.Location = new Point(93, 35);
      UidTextBox.Name = "UidTextBox";
      UidTextBox.Size = new Size(130, 23);
      UidTextBox.TabIndex = 1;
      UidTextBox.TextChanged += this.UidTextBox_TextChanged;
      // 
      // CreateUidButton
      // 
      CreateUidButton.Location = new Point(12, 35);
      CreateUidButton.Name = "CreateUidButton";
      CreateUidButton.Size = new Size(75, 23);
      CreateUidButton.TabIndex = 2;
      CreateUidButton.Text = "Create";
      CreateUidButton.UseVisualStyleBackColor = true;
      CreateUidButton.Click += this.CreateUidButton_Click;
      // 
      // DecodedDateTimeTextBox
      // 
      DecodedDateTimeTextBox.Location = new Point(229, 35);
      DecodedDateTimeTextBox.Name = "DecodedDateTimeTextBox";
      DecodedDateTimeTextBox.ReadOnly = true;
      DecodedDateTimeTextBox.Size = new Size(130, 23);
      DecodedDateTimeTextBox.TabIndex = 1;
      // 
      // label2
      // 
      label2.AutoSize = true;
      label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label2.Location = new Point(12, 73);
      label2.Name = "label2";
      label2.Size = new Size(109, 21);
      label2.TabIndex = 0;
      label2.Text = "EncodedToken";
      // 
      // TokenTextBox
      // 
      TokenTextBox.Location = new Point(93, 101);
      TokenTextBox.Name = "TokenTextBox";
      TokenTextBox.Size = new Size(130, 23);
      TokenTextBox.TabIndex = 1;
      TokenTextBox.TextChanged += this.TokenTextBox_TextChanged;
      // 
      // label3
      // 
      label3.AutoSize = true;
      label3.Location = new Point(12, 104);
      label3.Name = "label3";
      label3.Size = new Size(38, 15);
      label3.TabIndex = 0;
      label3.Text = "Token";
      // 
      // EncodedTokenTextBox
      // 
      EncodedTokenTextBox.Location = new Point(229, 101);
      EncodedTokenTextBox.Name = "EncodedTokenTextBox";
      EncodedTokenTextBox.Size = new Size(130, 23);
      EncodedTokenTextBox.TabIndex = 1;
      EncodedTokenTextBox.TextChanged += this.EncodedTokenTextBox_TextChanged;
      // 
      // TokenRawTextBox
      // 
      TokenRawTextBox.Location = new Point(365, 101);
      TokenRawTextBox.Name = "TokenRawTextBox";
      TokenRawTextBox.ReadOnly = true;
      TokenRawTextBox.Size = new Size(130, 23);
      TokenRawTextBox.TabIndex = 1;
      // 
      // EncodingErrorLabel
      // 
      EncodingErrorLabel.AutoSize = true;
      EncodingErrorLabel.ForeColor = Color.Red;
      EncodingErrorLabel.Location = new Point(12, 134);
      EncodingErrorLabel.Name = "EncodingErrorLabel";
      EncodingErrorLabel.Size = new Size(118, 15);
      EncodingErrorLabel.TabIndex = 0;
      EncodingErrorLabel.Text = "{EncodingErrorLabel}";
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new SizeF(7F, 15F);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(800, 450);
      this.Controls.Add(EncodingErrorLabel);
      this.Controls.Add(label3);
      this.Controls.Add(label2);
      this.Controls.Add(label1);
      this.Controls.Add(CreateUidButton);
      this.Controls.Add(EncodedTokenTextBox);
      this.Controls.Add(TokenTextBox);
      this.Controls.Add(UidTextBox);
      this.Controls.Add(TokenRawTextBox);
      this.Controls.Add(DecodedDateTimeTextBox);
      this.Name = "MainForm";
      this.Text = "Form1";
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    #endregion

    private Label label1;
    private TextBox UidTextBox;
    private Button CreateUidButton;
    private TextBox DecodedDateTimeTextBox;
    private Label label2;
    private TextBox TokenTextBox;
    private Label label3;
    private TextBox EncodedTokenTextBox;
    private TextBox TokenRawTextBox;
    private Label EncodingErrorLabel;
  }
}
