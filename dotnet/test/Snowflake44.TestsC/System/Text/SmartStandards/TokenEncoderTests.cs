using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Text.SmartStandards {

  [TestClass()]
  public class TokenEncoderTests {
  
    [TestMethod()]
    public void EncodeAndDecode_TestPatterns_ReturnExpextedOutput() {
      AssertEncodingAndDecodingOf("KnödelWürst", 742634033826132427L);
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
