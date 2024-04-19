using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Text.SmartStandards {

  [TestClass()]
  public class EncodedTokenExtensionsTests {
  
    [TestMethod()]
    public void ToInt_PascalCaseInput_ReturnsPascalCaseOutput() {
      AssertEncodingAndDecodingOf("KnödelWürst", 742634033826132427L);
    }

    private void AssertEncodingAndDecodingOf(string s, long l) {

      long id;

      string token;

      id = s.EncodeTokenToId();
      Assert.AreEqual(l, id, s);

      token = id.DecodeIdToToken();
      Assert.AreEqual(s, token);
    }
  }
}
