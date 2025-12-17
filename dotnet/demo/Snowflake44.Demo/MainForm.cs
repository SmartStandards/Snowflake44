using System;
using System.Collections.Generic;
using System.Reflection;
using System.SmartStandards;
using System.Text.SmartStandards;
using System.Windows.Forms;

namespace SmartStandards {

  public partial class MainForm : Form {

    private static MainForm _Instance;

    public static MainForm Instance { get { return _Instance; } }

    private List<Tuple<string, string>> _RefreshingDependencies = new();

    /// <summary>
    ///   Constructor.
    /// </summary>
    public MainForm() {
      InitializeComponent();
      _Instance = this;
    }

    protected override void OnLoad(EventArgs e) {
      base.OnLoad(e);
      EncodingErrorLabel.Text = "";
    }

    protected override void OnShown(EventArgs e) {
      base.OnShown(e);

      // Wire up refreshing dependencies

      _RefreshingDependencies.Add(new Tuple<string, string>("DecodedDateTimeTextBox", "UidTextBox"));
      _RefreshingDependencies.Add(new Tuple<string, string>("UidAsStringTextBox", "UidTextBox"));
      _RefreshingDependencies.Add(new Tuple<string, string>("UidAsGuidTextBox", "UidTextBox"));
      _RefreshingDependencies.Add(new Tuple<string, string>("TokenTextBox", "EncodedTokenTextBox"));
      _RefreshingDependencies.Add(new Tuple<string, string>("EncodedTokenTextBox", "TokenTextBox"));
      _RefreshingDependencies.Add(new Tuple<string, string>("Time32DecodedTextBox", "Time32TextBox"));

      WireUpCommandTriggers(this);

      this.UidCreate_ActionRequested();
      this.Time32Create_ActionRequested();

      TokenTextBox.Focus();
    }

    private static void AnyCommandTrigger_Click(object sender, EventArgs e) {
      AnyCommand_Requested(((Control)sender).Tag.ToString());
    }

    public static void AnyCommand_Requested(string commandName) {

      switch (commandName) {

        case "Uid.Create":
          MainForm.Instance.UidCreate_ActionRequested();
          break;


        case "Uid.CopyToClipboard":
          Clipboard.SetText(MainForm.Instance.UidTextBox.Text);
          break;

        case "Time32.Create":
          MainForm.Instance.Time32Create_ActionRequested();
          break;

        default:

          break;

      }

    }

    public static void WireUpCommandTriggers(Control startingControl) {

      foreach (Control control in startingControl.Controls) {

        if (control is Button) {

          if (control.Tag != null) control.Click += AnyCommandTrigger_Click;

        }

        if (control.Controls.Count != 0) WireUpCommandTriggers(control); // recusrsion 

      }

    }

    public void UidCreate_ActionRequested() {

      long uid = Snowflake44.Generate();

      UidTextBox.Text = uid.ToString();
    }

    private void AnyTextBox_TextChanged(object sender, EventArgs e) {
      Control senderControl = (Control)sender;
      foreach (Tuple<string, string> dependency in _RefreshingDependencies) {
        if (dependency.Item2 != senderControl.Name) continue;

        MethodInfo methodInfo = typeof(MainForm).GetMethod(
          "Refresh" + dependency.Item1,
          BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
        );

        methodInfo?.Invoke(this, null);
      }
    }

    private void RefreshDecodedDateTimeTextBox() {
      long numericValue;

      if (long.TryParse(UidTextBox.Text, out numericValue)) {
        DecodedDateTimeTextBox.Text = Snowflake44.DecodeDateTime(numericValue).ToString();
      } else {
        DecodedDateTimeTextBox.Text = "";
      }
    }

    private void RefreshEncodedTokenTextBox() {

      EncodingErrorLabel.Text = "";

      try {

        string incomingText = TokenTextBox.Text;

        if (incomingText.Length >= 1) {
          char u = Char.ToUpper(incomingText[0], System.Globalization.CultureInfo.InvariantCulture);
          if (incomingText[0] != u) {
            incomingText = u + incomingText.Substring(1);
            int cursor = TokenTextBox.SelectionStart;
            TokenTextBox.Text = incomingText;
            TokenTextBox.SelectionStart = cursor;
          }
        }

        string incomingToken = TokenEncoder.Encode(incomingText).ToString();

        if (EncodedTokenTextBox.Text != incomingToken) {
          if (!(incomingToken == "0" && EncodedTokenTextBox.Text == "")) {
            EncodedTokenTextBox.Text = incomingToken;
          }
        }

      } catch (Exception ex) {
        EncodingErrorLabel.Text = ex.Message;
      }

    }

    private void RefreshTokenTextBox() {
      long numericValue;

      string token = "";
      string rawToken = "";

      if (long.TryParse(EncodedTokenTextBox.Text, out numericValue)) {
        rawToken = TextToIntegerCodec.FromInt64(numericValue);
        token = TokenEncoder.Decode(numericValue);
      }

      if (TokenTextBox.Text != token) {
        TokenTextBox.Text = token;
        TokenTextBox.SelectionStart = TokenTextBox.Text.Length;
        TokenTextBox.SelectionLength = 0;
      }

      TokenRawTextBox.Text = rawToken;
    }

    public void Time32Create_ActionRequested() {

      Time32TextBox.Text = DateTimeUtility.ToInteger10SecondsResolution(DateTime.UtcNow).ToString();
    }

    private void RefreshTime32DecodedTextBox() {

      int inputInt;

      if (int.TryParse(Time32TextBox.Text, out inputInt)) {

        DateTime fromIntDateTime = DateTimeUtility.FromInteger10SecondsResolution(inputInt);

        Time32DecodedTextBox.Text = fromIntDateTime.ToString("yyyy-MM-dd HH:mm:ss");

      }
    }

    private void RefreshUidAsStringTextBox() {

      long uid = -1;

      if (long.TryParse(UidTextBox.Text, out uid)) {

        //string str = Convert.ToBase64String(BitConverter.GetBytes(uid)) ; // GetBytes() uses Heap => 25% slower

        Span<byte> bytes = stackalloc byte[8]; // Stack is 25% faster
        BitConverter.TryWriteBytes(bytes, uid);
        string str = Convert.ToBase64String(bytes);

        str = str.TrimEnd('=').Replace('+', '-').Replace('/', '_'); // Make the string URL-Safe

        UiAsdStringTextBox.Text = str;

      } else {
        UiAsdStringTextBox.Text = "";
      }

    }

    private void RefreshUidAsGuidTextBox() {

      long uid = -1;

      if (long.TryParse(UidTextBox.Text, out uid)) {

        UidAsGuidTextBox.Text = Snowflake44.ConvertToGuid(uid).ToString();

      } else {
        UiAsdStringTextBox.Text = "";
      }
    }

  }
}
