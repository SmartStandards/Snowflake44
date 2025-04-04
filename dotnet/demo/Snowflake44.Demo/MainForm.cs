using System;
using System.SmartStandards;
using System.Text.SmartStandards;
using System.Windows.Forms;

namespace SmartStandards {

  public partial class MainForm : Form {

    public MainForm() {
      InitializeComponent();
    }

    protected override void OnLoad(EventArgs e) {
      base.OnLoad(e);
      EncodingErrorLabel.Text = "";
      this.CreateActionRequested();
      this.NowActionRequested();
    }

    protected override void OnShown(EventArgs e) {
      base.OnShown(e);
      TokenTextBox.Focus();
    }

    public void CreateActionRequested() {

      UidTextBox.Text = Snowflake44.Generate().ToString();

      if (CopyToClipboard.Checked) {
        Clipboard.SetText(UidTextBox.Text);
      }
    }

    private void CreateUidButton_Click(object sender, EventArgs e) {
      this.CreateActionRequested();
    }

    private void UidTextBox_TextChanged(object sender, EventArgs e) {

      long numericValue;

      if (long.TryParse(UidTextBox.Text, out numericValue)) {
        DecodedDateTimeTextBox.Text = Snowflake44.DecodeDateTime(numericValue).ToString();
      } else {
        DecodedDateTimeTextBox.Text = "";
      }

    }

    private void TokenTextBox_TextChanged(object sender, EventArgs e) {

      EncodingErrorLabel.Text = "";

      try {

        string text = TokenTextBox.Text;

        if (text.Length >= 1) {
          char u = Char.ToUpper(text[0], System.Globalization.CultureInfo.InvariantCulture);
          if (text[0] != u) {
            text = u + text.Substring(1);
            int cursor = TokenTextBox.SelectionStart;
            TokenTextBox.Text = text;
            TokenTextBox.SelectionStart = cursor;
          }
        }

        string encodedToken = TokenEncoder.Encode(text).ToString();

        if (EncodedTokenTextBox.Text != encodedToken) EncodedTokenTextBox.Text = encodedToken;

      } catch (Exception ex) {
        EncodingErrorLabel.Text = ex.Message;
      }

    }

    private void EncodedTokenTextBox_TextChanged(object sender, EventArgs e) {
      long numericValue;


      string token = "";
      string rawToken = "";

      if (long.TryParse(EncodedTokenTextBox.Text, out numericValue)) {
        rawToken = TextToIntegerCodec.FromInt64(numericValue);
        token = TokenEncoder.Decode(numericValue);
      }

      if (TokenTextBox.Text != token) TokenTextBox.Text = token;

      TokenRawTextBox.Text = rawToken;

    }

    private void NowButton_Click(object sender, EventArgs e) {
      this.NowActionRequested();
    }

    public void NowActionRequested() {

      Int10SecondsTextBox.Text = DateTimeUtility.ToInteger10SecondsResolution(DateTime.UtcNow).ToString();
    }

    private void Int10SecondsTextBox_TextChanged(object sender, EventArgs e) {

      int inputInt;

      if (int.TryParse(Int10SecondsTextBox.Text, out inputInt)) {

        DateTime fromIntDateTime = DateTimeUtility.FromInteger10SecondsResolution(inputInt);

        FromIntDateTimeTextBox.Text = fromIntDateTime.ToString("yyyy-MM-dd HH:mm:ss");

      }
    }
  }

}
