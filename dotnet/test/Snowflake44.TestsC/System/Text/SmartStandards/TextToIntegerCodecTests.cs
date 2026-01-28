using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Text.SmartStandards {

  [TestClass()]
  public class TextToIntegerCodecTests {

    [TestMethod()]
    public void ToInt64_DifferentPatterns_ReturnExpectedResults() {

      AssertEncodingAndDecodingOfRaw("", 0);

      AssertEncodingAndDecodingOfRaw("A", 1);// A ist 1

      AssertEncodingAndDecodingOfRaw("#", 31);// Hashtag ist das letzte mögliche Zeichen (alle 5 Bit gesetzt)

      AssertEncodingAndDecodingOfRaw("_A", 32); // Überlauf: Underscore ist 0 und A an zweiter Stelle => 6. Bit gesetzt

      AssertEncodingAndDecodingOfRaw("aaaaaa", 34636833, "AAAAAA");  // lower case wird zu upper case (robustes Design)
      AssertEncodingAndDecodingOfRaw("AAAAAA", 34636833);  // upper case liefert den selben Integer-Wert

      AssertEncodingAndDecodingOfRaw("xÄÖÜß", 32437112, "XÄÖÜß");

      AssertEncodingAndDecodingOfRaw("Xäöüß", 32437112, "XÄÖÜß");

      AssertEncodingAndDecodingOfRaw("ÄäÄ", 28539, "ÄÄÄ");

      AssertEncodingAndDecodingOfRaw("ÖöÖ", 29596, "ÖÖÖ");

      AssertEncodingAndDecodingOfRaw("ÜüÜ", 30653, "ÜÜÜ");

      AssertEncodingAndDecodingOfRaw("Hello", 16134312, "HELLO");

      AssertEncodingAndDecodingOfRaw("EMEAX", 25204133);

      AssertEncodingAndDecodingOfRaw("_Hello", 516297984, "_HELLO");

      AssertEncodingAndDecodingOfRaw("THE#ID", 144676116);

      AssertEncodingAndDecodingOfRaw("Ab_Cde", 172064833, "AB_CDE");

      AssertEncodingAndDecodingOfRaw("######", 1073741823);

      AssertEncodingAndDecodingOfRaw("______A", 1073741824L);

      AssertEncodingAndDecodingOfRaw("___________A", 36028797018963968L);

      AssertEncodingAndDecodingOfRaw("############", 1152921504606846975L);

      AssertEncodingAndDecodingOfRaw("AAAAAAAAAAAA", 37191016277640225L);

      AssertEncodingAndDecodingOfRaw("KNÖDEL_WÜRST", 742634033826132427L);

      AssertEncodingAndDecodingOfRaw("WÜRßTKNÖDÄL", 14466152471153591L);

    }

    private static void AssertEncodingAndDecodingOfRaw(string rawToken, long expectedEncodedId, string deviantExpectedToken = "") {
      long actualEncodedId = TextToIntegerCodec.ToInt64(rawToken);
      Assert.AreEqual(expectedEncodedId, actualEncodedId, rawToken);
      string decodedRawToken = TextToIntegerCodec.FromInt64(actualEncodedId);
      String expectedToken = (deviantExpectedToken != "") ? deviantExpectedToken : rawToken;
      Assert.AreEqual(expectedToken, decodedRawToken, rawToken);
    }

  }

}
