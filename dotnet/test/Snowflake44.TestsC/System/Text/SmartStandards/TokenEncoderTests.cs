using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Text.SmartStandards {

  [TestClass()]
  public class TokenEncoderTests {

    [TestMethod()]
    public void EncodeAndDecode_TestPatterns_ReturnExpextedOutput() {
      AssertEncodingAndDecodingOf("KnödelWürst", 742634033826132427L);
    }

    [TestMethod()]
    public void RawToReadable_TestPatterns_ReturnExpextedOutput() {

      string actual;

      actual = TokenEncoder.RawToReadable("MAMBO#S");
      Assert.AreEqual("Mambo5", actual);

      actual = TokenEncoder.RawToReadable("#HZ_IS_THE_ANSWER");
      Assert.AreEqual("42IsTheAnswer", actual);

      actual = TokenEncoder.RawToReadable("#IZEHSGLBPO");
      Assert.AreEqual("1234567890", actual);

      actual = TokenEncoder.RawToReadable("#X");
      Assert.AreEqual("X", actual);

      actual = TokenEncoder.RawToReadable("#");
      Assert.AreEqual("", actual);

    }

    [TestMethod()]
    public void ReadableToRaw_TestPatterns_ReturnExpextedOutput() {
      string actual;

      actual = TokenEncoder.ReadableToRaw("Mambo5");
      Assert.AreEqual("MAMBO#S", actual);

      actual = TokenEncoder.ReadableToRaw("1234567890");
      Assert.AreEqual("#IZEHSGLBPO", actual);

      actual = TokenEncoder.ReadableToRaw("1forYou");
      Assert.AreEqual("#I_FOR_YOU", actual);

    }

    private static void AssertEncodingAndDecodingOf(string originalToken, long l) {

      long id;

      string decodedToken;

      id = TokenEncoder.Encode(originalToken);
      Assert.AreEqual(l, id, originalToken);

      decodedToken = TokenEncoder.Decode(id);
      Assert.AreEqual(originalToken, decodedToken);
    }
  }
}
