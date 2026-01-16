using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Text.SmartStandards {

  [TestClass()]
  public class TokenEncoderTests {

    [TestMethod()]
    public void Encode_ErrorCases_BehaveAsDesigned() {

      long id;

      // Incoming null => Exception

      try {
        id = TokenEncoder.Encode(null);
      } catch (Exception ex) {
        Assert.IsTrue(ex.GetType() == typeof(NullReferenceException));
      }

      // Incoming containing space => Exception

      try {
        id = TokenEncoder.Encode(" ");
      } catch (Exception ex) {
        Assert.IsTrue(ex.GetType() == typeof(ArgumentException));
      }

      try {
        id = TokenEncoder.Encode("A ");
      } catch (Exception ex) {
        Assert.IsTrue(ex.GetType() == typeof(ArgumentException));
      }

      try {
        id = TokenEncoder.Encode(" B");
      } catch (Exception ex) {
        Assert.IsTrue(ex.GetType() == typeof(ArgumentException));
      }

      try {
        id = TokenEncoder.Encode("A B");
      } catch (Exception ex) {
        Assert.IsTrue(ex.GetType() == typeof(ArgumentException));
      }

      // Incoming ending with underscore space => Exception

      try {
        id = TokenEncoder.Encode("_");
      } catch (Exception ex) {
        Assert.IsTrue(ex.GetType() == typeof(ArgumentException));
      }

      try {
        id = TokenEncoder.Encode("A_");
      } catch (Exception ex) {
        Assert.IsTrue(ex.GetType() == typeof(ArgumentException));
      }

      // Longer than 12 => Exception

      try {
        id = TokenEncoder.Encode("AAAAAAAAAAAAA");
      } catch (Exception ex) {
        Assert.IsTrue(ex.GetType() == typeof(ArgumentException));
      }

    }

    [TestMethod()]
    public void EncodeAndDecode_ForthAndBack_ReturnsOriginal() {

      AssertEncodingAndDecodingOf("", 0);

      // All chars of our alphabet

      AssertEncodingAndDecodingOf("Abcdefghijkl", 445092485129178177L);
      AssertEncodingAndDecodingOf("Mnopqrstuvwx", 891384680460860877L);
      AssertEncodingAndDecodingOf("Yz", 857L);
      AssertEncodingAndDecodingOf("Würßtknödäl", 14466152471153591L);

      // All numbers

      AssertEncodingAndDecodingOf("0123456789", 18098222586996223);

      // All escape duets

      AssertEncodingAndDecodingOf("#", 1023L);
      AssertEncodingAndDecodingOf("+", 63L);
      AssertEncodingAndDecodingOf(",", 127L);
      AssertEncodingAndDecodingOf(".", 159L);
      AssertEncodingAndDecodingOf("?", 223L);
      AssertEncodingAndDecodingOf("(", 351L);
      AssertEncodingAndDecodingOf(")", 383L);
      AssertEncodingAndDecodingOf("-", 447L);
      AssertEncodingAndDecodingOf("&", 479L);
      AssertEncodingAndDecodingOf("@", 575L);
      AssertEncodingAndDecodingOf(":", 607L);
      AssertEncodingAndDecodingOf("\'", 671L);
      AssertEncodingAndDecodingOf("\\", 703L);
      AssertEncodingAndDecodingOf("/", 735L);
      AssertEncodingAndDecodingOf("=", 767L);
      AssertEncodingAndDecodingOf("*", 799L);
      AssertEncodingAndDecodingOf("!", 831L);
      AssertEncodingAndDecodingOf("]", 895L);
      AssertEncodingAndDecodingOf("[", 927L);
      AssertEncodingAndDecodingOf("%", 959L);
      AssertEncodingAndDecodingOf("^", 991L);

      // Inline Escape Chars

      AssertEncodingAndDecodingOf("(Unknown)", 13491814534698335);

      AssertEncodingAndDecodingOf("R&V", 736242);

      AssertEncodingAndDecodingOf("AT&T", 686804993);

      AssertEncodingAndDecodingOf("a@b.com", 485368583758085152);

      // Numerical and Escaping Kung-Fu

      AssertEncodingAndDecodingOf("1+-2=3*4^10", 551630212503602495);

      AssertEncodingAndDecodingOf("1%=3!", 844887359);

      AssertEncodingAndDecodingOf("1,200.34Eu", 762243209639038271);

      // Casing Kung-Fu

      AssertEncodingAndDecodingOf("HelloWorld", 4946143410008232L);
      AssertEncodingAndDecodingOf("helloWorld", 158276589120263424L);
      AssertEncodingAndDecodingOf("Zimmer12A", 1154830342468922L);
      AssertEncodingAndDecodingOf("Gasse13b", 2251982322125863L);

    }

    [TestMethod()]
    public void RawToReadable_TestPatterns_ReturnExpextedOutput() {

      // This scenario is implicitely covered by EncodeAndDecode_ForthAndBack_ReturnsOriginal
      // So we just add some border cases here      

      string actual;

      actual = TokenEncoder.RawToReadable("MAMBO#S");
      Assert.AreEqual("Mambo5", actual);

      actual = TokenEncoder.RawToReadable("#HZ_IS_THE_ANSWER");
      Assert.AreEqual("42IsTheAnswer", actual);

      actual = TokenEncoder.RawToReadable("#IZEHSGLBPO");
      Assert.AreEqual("1234567890", actual);

      actual = TokenEncoder.RawToReadable("#X");
      Assert.AreEqual("*", actual);

    }

    public void RawToReadable_ErrorCases_BehaveAsDesigned() {

      string actual;

      actual = TokenEncoder.RawToReadable("#");
      Assert.AreEqual("", actual);

      actual = TokenEncoder.RawToReadable("_");
      Assert.AreEqual("", actual);

    }

    [TestMethod()]
    public void ReadableToRaw_TestPatterns_ReturnExpextedOutput() {
      string actual;

      // All numbers

      actual = TokenEncoder.ReadableToRaw("1234567890");
      Assert.AreEqual("#IZEHSGLBPO", actual);

      // All escape duets

      actual = TokenEncoder.ReadableToRaw("#");
      Assert.AreEqual("##", actual);

      actual = TokenEncoder.ReadableToRaw("+");
      Assert.AreEqual("#A", actual);

      actual = TokenEncoder.ReadableToRaw(",");
      Assert.AreEqual("#C", actual);

      actual = TokenEncoder.ReadableToRaw(".");
      Assert.AreEqual("#D", actual);

      actual = TokenEncoder.ReadableToRaw("?");
      Assert.AreEqual("#F", actual);

      actual = TokenEncoder.ReadableToRaw("(");
      Assert.AreEqual("#J", actual);

      actual = TokenEncoder.ReadableToRaw(")");
      Assert.AreEqual("#K", actual);

      actual = TokenEncoder.ReadableToRaw("-");
      Assert.AreEqual("#M", actual);

      actual = TokenEncoder.ReadableToRaw("&");
      Assert.AreEqual("#N", actual);

      actual = TokenEncoder.ReadableToRaw("@");
      Assert.AreEqual("#Q", actual);

      actual = TokenEncoder.ReadableToRaw(":");
      Assert.AreEqual("#R", actual);

      actual = TokenEncoder.ReadableToRaw("\'");
      Assert.AreEqual("#T", actual);

      actual = TokenEncoder.ReadableToRaw("\\");
      Assert.AreEqual("#U", actual);

      actual = TokenEncoder.ReadableToRaw("/");
      Assert.AreEqual("#V", actual);

      actual = TokenEncoder.ReadableToRaw("=");
      Assert.AreEqual("#W", actual);

      actual = TokenEncoder.ReadableToRaw("*");
      Assert.AreEqual("#X", actual);

      actual = TokenEncoder.ReadableToRaw("!");
      Assert.AreEqual("#Y", actual);

      actual = TokenEncoder.ReadableToRaw("]");
      Assert.AreEqual("#Ä", actual);

      actual = TokenEncoder.ReadableToRaw("[");
      Assert.AreEqual("#Ö", actual);

      actual = TokenEncoder.ReadableToRaw("%");
      Assert.AreEqual("#Ü", actual);

      actual = TokenEncoder.ReadableToRaw("^");
      Assert.AreEqual("#ß", actual);

      // Inline Escape Chars

      actual = TokenEncoder.ReadableToRaw("(Unknown)");
      Assert.AreEqual("#JUNKNOWN#K", actual);

      actual = TokenEncoder.ReadableToRaw("R&V");
      Assert.AreEqual("R#NV", actual);

      actual = TokenEncoder.ReadableToRaw("AT&T");
      Assert.AreEqual("A_T#NT", actual);

      actual = TokenEncoder.ReadableToRaw("me@there.com");
      Assert.AreEqual("_ME#Q_THERE#D_COM", actual);

      // Numbers followed by escaped chars

      actual = TokenEncoder.ReadableToRaw("1+-2=3*4^10!");
      Assert.AreEqual("#IAMZWEXHßIOY", actual);

      actual = TokenEncoder.ReadableToRaw("1,200.34Usd");
      Assert.AreEqual("#ICZOODEH_USD", actual);

      // Miscellaneous Cases

      actual = TokenEncoder.ReadableToRaw("Mambo5");
      Assert.AreEqual("MAMBO#S", actual);

      actual = TokenEncoder.ReadableToRaw("Mambo#5");
      Assert.AreEqual("MAMBO###S", actual);

      actual = TokenEncoder.ReadableToRaw("Mambo5#");
      Assert.AreEqual("MAMBO#S#", actual);

      actual = TokenEncoder.ReadableToRaw("Zimmer12A");
      Assert.AreEqual("ZIMMER#IZ_A", actual);

      actual = TokenEncoder.ReadableToRaw("Gasse13bf");
      Assert.AreEqual("GASSE#IE__BF", actual);

    }

    [TestMethod()]
    public void ReadableToRaw_ErrorCases_BehaveAsDesigned() {

      string actual;

      // Unsupported Chars

      try {
        actual = TokenEncoder.ReadableToRaw("_");
      } catch (Exception ex) {
        Assert.IsTrue(ex.GetType() == typeof(ArgumentException));
      }

      try {
        actual = TokenEncoder.ReadableToRaw("~");
      } catch (Exception ex) {
        Assert.IsTrue(ex.GetType() == typeof(ArgumentException));
      }

      try {
        actual = TokenEncoder.ReadableToRaw("$");
      } catch (Exception ex) {
        Assert.IsTrue(ex.GetType() == typeof(ArgumentException));
      }

      try {
        actual = TokenEncoder.ReadableToRaw("\"");
      } catch (Exception ex) {
        Assert.IsTrue(ex.GetType() == typeof(ArgumentException));
      }

    }

    private static void AssertEncodingAndDecodingOf(string originalToken, long expectedId) {

      long actualId;

      string decodedToken;

      actualId = TokenEncoder.Encode(originalToken);
      Assert.AreEqual(expectedId, actualId, originalToken);

      decodedToken = TokenEncoder.Decode(actualId);
      Assert.AreEqual(originalToken, decodedToken);
    }
  }
}
