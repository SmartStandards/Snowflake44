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
      label4 = new Label();
      NowButton = new Button();
      Int10SecondsTextBox = new TextBox();
      FromIntDateTimeTextBox = new TextBox();
      CopyToClipboard = new CheckBox();
      UidString = new TextBox();
      label5 = new Label();
      label6 = new Label();
      UidGuid = new TextBox();
      this.SuspendLayout();
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label1.Location = new Point(12, 8);
      label1.Name = "label1";
      label1.Size = new Size(169, 21);
      label1.TabIndex = 0;
      label1.Text = "SnowFlake44 (64 Bit)";
      // 
      // UidTextBox
      // 
      UidTextBox.Location = new Point(93, 35);
      UidTextBox.Name = "UidTextBox";
      UidTextBox.Size = new Size(130, 23);
      UidTextBox.TabIndex = 2;
      UidTextBox.TextChanged += this.UidTextBox_TextChanged;
      // 
      // CreateUidButton
      // 
      CreateUidButton.Location = new Point(12, 35);
      CreateUidButton.Name = "CreateUidButton";
      CreateUidButton.Size = new Size(75, 23);
      CreateUidButton.TabIndex = 1;
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
      DecodedDateTimeTextBox.TabIndex = 3;
      // 
      // label2
      // 
      label2.AutoSize = true;
      label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label2.Location = new Point(12, 134);
      label2.Name = "label2";
      label2.Size = new Size(181, 21);
      label2.TabIndex = 4;
      label2.Text = "EncodedToken (64 Bit)";
      // 
      // TokenTextBox
      // 
      TokenTextBox.Location = new Point(93, 162);
      TokenTextBox.Name = "TokenTextBox";
      TokenTextBox.Size = new Size(130, 23);
      TokenTextBox.TabIndex = 6;
      TokenTextBox.TextChanged += this.TokenTextBox_TextChanged;
      // 
      // label3
      // 
      label3.AutoSize = true;
      label3.Location = new Point(12, 165);
      label3.Name = "label3";
      label3.Size = new Size(38, 15);
      label3.TabIndex = 5;
      label3.Text = "Token";
      // 
      // EncodedTokenTextBox
      // 
      EncodedTokenTextBox.Location = new Point(229, 162);
      EncodedTokenTextBox.Name = "EncodedTokenTextBox";
      EncodedTokenTextBox.Size = new Size(130, 23);
      EncodedTokenTextBox.TabIndex = 7;
      EncodedTokenTextBox.TextChanged += this.EncodedTokenTextBox_TextChanged;
      // 
      // TokenRawTextBox
      // 
      TokenRawTextBox.Location = new Point(365, 162);
      TokenRawTextBox.Name = "TokenRawTextBox";
      TokenRawTextBox.ReadOnly = true;
      TokenRawTextBox.Size = new Size(130, 23);
      TokenRawTextBox.TabIndex = 8;
      // 
      // EncodingErrorLabel
      // 
      EncodingErrorLabel.AutoSize = true;
      EncodingErrorLabel.ForeColor = Color.Red;
      EncodingErrorLabel.Location = new Point(12, 195);
      EncodingErrorLabel.Name = "EncodingErrorLabel";
      EncodingErrorLabel.Size = new Size(118, 15);
      EncodingErrorLabel.TabIndex = 9;
      EncodingErrorLabel.Text = "{EncodingErrorLabel}";
      // 
      // label4
      // 
      label4.AutoSize = true;
      label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label4.Location = new Point(12, 226);
      label4.Name = "label4";
      label4.Size = new Size(402, 21);
      label4.TabIndex = 10;
      label4.Text = "DateTime <=> Integer10SecondsResolution (32 Bit)";
      // 
      // NowButton
      // 
      NowButton.Location = new Point(12, 260);
      NowButton.Name = "NowButton";
      NowButton.Size = new Size(75, 23);
      NowButton.TabIndex = 11;
      NowButton.Text = "Now";
      NowButton.UseVisualStyleBackColor = true;
      NowButton.Click += this.NowButton_Click;
      // 
      // Int10SecondsTextBox
      // 
      Int10SecondsTextBox.Location = new Point(93, 260);
      Int10SecondsTextBox.Name = "Int10SecondsTextBox";
      Int10SecondsTextBox.Size = new Size(130, 23);
      Int10SecondsTextBox.TabIndex = 12;
      Int10SecondsTextBox.TextChanged += this.Int10SecondsTextBox_TextChanged;
      // 
      // FromIntDateTimeTextBox
      // 
      FromIntDateTimeTextBox.Location = new Point(229, 260);
      FromIntDateTimeTextBox.Name = "FromIntDateTimeTextBox";
      FromIntDateTimeTextBox.Size = new Size(130, 23);
      FromIntDateTimeTextBox.TabIndex = 13;
      // 
      // CopyToClipboard
      // 
      CopyToClipboard.AutoSize = true;
      CopyToClipboard.Checked = true;
      CopyToClipboard.CheckState = CheckState.Checked;
      CopyToClipboard.Location = new Point(365, 37);
      CopyToClipboard.Name = "CopyToClipboard";
      CopyToClipboard.Size = new Size(123, 19);
      CopyToClipboard.TabIndex = 14;
      CopyToClipboard.Text = "Copy to Clipboard";
      CopyToClipboard.UseVisualStyleBackColor = true;
      // 
      // UidString
      // 
      UidString.Location = new Point(135, 64);
      UidString.Name = "UidString";
      UidString.ReadOnly = true;
      UidString.Size = new Size(224, 23);
      UidString.TabIndex = 3;
      // 
      // label5
      // 
      label5.AutoSize = true;
      label5.Location = new Point(12, 70);
      label5.Name = "label5";
      label5.Size = new Size(122, 15);
      label5.TabIndex = 15;
      label5.Text = "String-Representation";
      // 
      // label6
      // 
      label6.AutoSize = true;
      label6.Location = new Point(12, 96);
      label6.Name = "label6";
      label6.Size = new Size(118, 15);
      label6.TabIndex = 15;
      label6.Text = "GUID-Representation";
      // 
      // UidGuid
      // 
      UidGuid.Location = new Point(135, 93);
      UidGuid.Name = "UidGuid";
      UidGuid.ReadOnly = true;
      UidGuid.Size = new Size(224, 23);
      UidGuid.TabIndex = 3;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new SizeF(7F, 15F);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(516, 302);
      this.Controls.Add(label6);
      this.Controls.Add(label5);
      this.Controls.Add(label1);
      this.Controls.Add(CreateUidButton);
      this.Controls.Add(UidTextBox);
      this.Controls.Add(UidString);
      this.Controls.Add(UidGuid);
      this.Controls.Add(DecodedDateTimeTextBox);
      this.Controls.Add(CopyToClipboard);
      this.Controls.Add(label2);
      this.Controls.Add(label3);
      this.Controls.Add(TokenTextBox);
      this.Controls.Add(EncodedTokenTextBox);
      this.Controls.Add(TokenRawTextBox);
      this.Controls.Add(EncodingErrorLabel);
      this.Controls.Add(label4);
      this.Controls.Add(NowButton);
      this.Controls.Add(Int10SecondsTextBox);
      this.Controls.Add(FromIntDateTimeTextBox);
      this.Name = "MainForm";
      this.Text = "Snowflake44.Demo";
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
    private Label label4;
    private Button NowButton;
    private TextBox Int10SecondsTextBox;
    private TextBox FromIntDateTimeTextBox;
    private CheckBox CopyToClipboard;
    private TextBox UidString;
    private Label label5;
    private Label label6;
    private TextBox UidGuid;
  }
}
