using System.Drawing;
using System.Windows.Forms;

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
      SnowFlakeLabel = new Label();
      UidTextBox = new TextBox();
      CreateUidButton = new Button();
      DecodedDateTimeTextBox = new TextBox();
      EncodedTokenLabel = new Label();
      TokenTextBox = new TextBox();
      TokenLabel = new Label();
      EncodedTokenTextBox = new TextBox();
      TokenRawTextBox = new TextBox();
      EncodingErrorLabel = new Label();
      Time32Label = new Label();
      Time32Button = new Button();
      Time32TextBox = new TextBox();
      Time32DecodedTextBox = new TextBox();
      UiAsdStringTextBox = new TextBox();
      UidAsStringLabel = new Label();
      UidAsGuidLabel = new Label();
      UidAsGuidTextBox = new TextBox();
      CopyUidButton = new Button();
      label1 = new Label();
      this.SuspendLayout();
      // 
      // SnowFlakeLabel
      // 
      SnowFlakeLabel.AutoSize = true;
      SnowFlakeLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
      SnowFlakeLabel.Location = new Point(12, 8);
      SnowFlakeLabel.Name = "SnowFlakeLabel";
      SnowFlakeLabel.Size = new Size(169, 21);
      SnowFlakeLabel.TabIndex = 0;
      SnowFlakeLabel.Text = "SnowFlake44 (64 Bit)";
      // 
      // UidTextBox
      // 
      UidTextBox.Location = new Point(93, 35);
      UidTextBox.Name = "UidTextBox";
      UidTextBox.Size = new Size(130, 23);
      UidTextBox.TabIndex = 2;
      UidTextBox.TextChanged += this.AnyTextBox_TextChanged;
      // 
      // CreateUidButton
      // 
      CreateUidButton.Location = new Point(12, 35);
      CreateUidButton.Name = "CreateUidButton";
      CreateUidButton.Size = new Size(75, 23);
      CreateUidButton.TabIndex = 1;
      CreateUidButton.Tag = "Uid.Create";
      CreateUidButton.Text = "Create";
      CreateUidButton.UseVisualStyleBackColor = true;
      // 
      // DecodedDateTimeTextBox
      // 
      DecodedDateTimeTextBox.Location = new Point(229, 35);
      DecodedDateTimeTextBox.Name = "DecodedDateTimeTextBox";
      DecodedDateTimeTextBox.ReadOnly = true;
      DecodedDateTimeTextBox.Size = new Size(130, 23);
      DecodedDateTimeTextBox.TabIndex = 3;
      // 
      // EncodedTokenLabel
      // 
      EncodedTokenLabel.AutoSize = true;
      EncodedTokenLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
      EncodedTokenLabel.Location = new Point(12, 134);
      EncodedTokenLabel.Name = "EncodedTokenLabel";
      EncodedTokenLabel.Size = new Size(181, 21);
      EncodedTokenLabel.TabIndex = 9;
      EncodedTokenLabel.Text = "EncodedToken (64 Bit)";
      // 
      // TokenTextBox
      // 
      TokenTextBox.Location = new Point(93, 162);
      TokenTextBox.Name = "TokenTextBox";
      TokenTextBox.Size = new Size(130, 23);
      TokenTextBox.TabIndex = 11;
      TokenTextBox.TextChanged += this.AnyTextBox_TextChanged;
      // 
      // TokenLabel
      // 
      TokenLabel.AutoSize = true;
      TokenLabel.Location = new Point(12, 165);
      TokenLabel.Name = "TokenLabel";
      TokenLabel.Size = new Size(38, 15);
      TokenLabel.TabIndex = 10;
      TokenLabel.Text = "Token";
      // 
      // EncodedTokenTextBox
      // 
      EncodedTokenTextBox.Location = new Point(229, 162);
      EncodedTokenTextBox.Name = "EncodedTokenTextBox";
      EncodedTokenTextBox.Size = new Size(130, 23);
      EncodedTokenTextBox.TabIndex = 12;
      EncodedTokenTextBox.TextChanged += this.AnyTextBox_TextChanged;
      // 
      // TokenRawTextBox
      // 
      TokenRawTextBox.Location = new Point(365, 162);
      TokenRawTextBox.Name = "TokenRawTextBox";
      TokenRawTextBox.ReadOnly = true;
      TokenRawTextBox.Size = new Size(130, 23);
      TokenRawTextBox.TabIndex = 13;
      // 
      // EncodingErrorLabel
      // 
      EncodingErrorLabel.AutoSize = true;
      EncodingErrorLabel.ForeColor = Color.Red;
      EncodingErrorLabel.Location = new Point(12, 195);
      EncodingErrorLabel.Name = "EncodingErrorLabel";
      EncodingErrorLabel.Size = new Size(118, 15);
      EncodingErrorLabel.TabIndex = 14;
      EncodingErrorLabel.Text = "{EncodingErrorLabel}";
      // 
      // Time32Label
      // 
      Time32Label.AutoSize = true;
      Time32Label.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
      Time32Label.Location = new Point(12, 226);
      Time32Label.Name = "Time32Label";
      Time32Label.Size = new Size(402, 21);
      Time32Label.TabIndex = 15;
      Time32Label.Text = "DateTime <=> Integer10SecondsResolution (32 Bit)";
      // 
      // Time32Button
      // 
      Time32Button.Location = new Point(12, 260);
      Time32Button.Name = "Time32Button";
      Time32Button.Size = new Size(75, 23);
      Time32Button.TabIndex = 16;
      Time32Button.Tag = "Time32.Create";
      Time32Button.Text = "Now";
      Time32Button.UseVisualStyleBackColor = true;
      // 
      // Time32TextBox
      // 
      Time32TextBox.Location = new Point(93, 260);
      Time32TextBox.Name = "Time32TextBox";
      Time32TextBox.Size = new Size(130, 23);
      Time32TextBox.TabIndex = 17;
      Time32TextBox.TextChanged += this.AnyTextBox_TextChanged;
      // 
      // Time32DecodedTextBox
      // 
      Time32DecodedTextBox.Location = new Point(229, 260);
      Time32DecodedTextBox.Name = "Time32DecodedTextBox";
      Time32DecodedTextBox.Size = new Size(130, 23);
      Time32DecodedTextBox.TabIndex = 18;
      // 
      // UiAsdStringTextBox
      // 
      UiAsdStringTextBox.Location = new Point(135, 67);
      UiAsdStringTextBox.Name = "UiAsdStringTextBox";
      UiAsdStringTextBox.ReadOnly = true;
      UiAsdStringTextBox.Size = new Size(224, 23);
      UiAsdStringTextBox.TabIndex = 6;
      // 
      // UidAsStringLabel
      // 
      UidAsStringLabel.AutoSize = true;
      UidAsStringLabel.Location = new Point(12, 70);
      UidAsStringLabel.Name = "UidAsStringLabel";
      UidAsStringLabel.Size = new Size(122, 15);
      UidAsStringLabel.TabIndex = 5;
      UidAsStringLabel.Text = "String-Representation";
      // 
      // UidAsGuidLabel
      // 
      UidAsGuidLabel.AutoSize = true;
      UidAsGuidLabel.Location = new Point(12, 96);
      UidAsGuidLabel.Name = "UidAsGuidLabel";
      UidAsGuidLabel.Size = new Size(118, 15);
      UidAsGuidLabel.TabIndex = 7;
      UidAsGuidLabel.Text = "GUID-Representation";
      // 
      // UidAsGuidTextBox
      // 
      UidAsGuidTextBox.Location = new Point(135, 93);
      UidAsGuidTextBox.Name = "UidAsGuidTextBox";
      UidAsGuidTextBox.ReadOnly = true;
      UidAsGuidTextBox.Size = new Size(224, 23);
      UidAsGuidTextBox.TabIndex = 8;
      // 
      // CopyUidButton
      // 
      CopyUidButton.Location = new Point(365, 35);
      CopyUidButton.Name = "CopyUidButton";
      CopyUidButton.Size = new Size(49, 23);
      CopyUidButton.TabIndex = 4;
      CopyUidButton.Tag = "Uid.CopyToClipboard";
      CopyUidButton.Text = "Copy";
      CopyUidButton.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.ForeColor = Color.Red;
      label1.Location = new Point(356, 70);
      label1.Name = "label1";
      label1.Size = new Size(76, 15);
      label1.TabIndex = 19;
      label1.Text = "DO NOT USE!";
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new SizeF(7F, 15F);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(516, 302);
      this.Controls.Add(label1);
      this.Controls.Add(SnowFlakeLabel);
      this.Controls.Add(CreateUidButton);
      this.Controls.Add(UidTextBox);
      this.Controls.Add(DecodedDateTimeTextBox);
      this.Controls.Add(CopyUidButton);
      this.Controls.Add(UidAsStringLabel);
      this.Controls.Add(UiAsdStringTextBox);
      this.Controls.Add(UidAsGuidLabel);
      this.Controls.Add(UidAsGuidTextBox);
      this.Controls.Add(EncodedTokenLabel);
      this.Controls.Add(TokenLabel);
      this.Controls.Add(TokenTextBox);
      this.Controls.Add(EncodedTokenTextBox);
      this.Controls.Add(TokenRawTextBox);
      this.Controls.Add(EncodingErrorLabel);
      this.Controls.Add(Time32Label);
      this.Controls.Add(Time32Button);
      this.Controls.Add(Time32TextBox);
      this.Controls.Add(Time32DecodedTextBox);
      this.Name = "MainForm";
      this.Text = "Snowflake44.Demo";
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    #endregion

    private Label SnowFlakeLabel;
    private TextBox UidTextBox;
    private Button CreateUidButton;
    private TextBox DecodedDateTimeTextBox;
    private Label EncodedTokenLabel;
    private TextBox TokenTextBox;
    private Label TokenLabel;
    private TextBox EncodedTokenTextBox;
    private TextBox TokenRawTextBox;
    private Label EncodingErrorLabel;
    private Label Time32Label;
    private Button Time32Button;
    private TextBox Time32TextBox;
    private TextBox Time32DecodedTextBox;
    private TextBox UiAsdStringTextBox;
    private Label UidAsStringLabel;
    private Label UidAsGuidLabel;
    private TextBox UidAsGuidTextBox;
    private Button CopyUidButton;
    private Label label1;
  }
}
