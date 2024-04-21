using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Text.SmartStandards {

  [TestClass()]
  public class TokenEncoderTests {
  
    [TestMethod()]
    public void ToInt_PascalCaseInput_ReturnsPascalCaseOutput() {
      AssertEncodingAndDecodingOf("KnödelWürst", 742634033826132427L);
    }

    private static void AssertEncodingAndDecodingOf(string s, long l) {

      long id;

      string token;

      id = TokenEncoder.Encode(s);
      Assert.AreEqual(l, id, s);

      token = TokenEncoder.Decode(id);
      Assert.AreEqual(s, token);
    }
  }
}
