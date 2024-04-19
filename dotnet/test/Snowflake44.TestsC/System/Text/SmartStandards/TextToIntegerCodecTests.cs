using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Text.SmartStandards {

  public  class TextToIntegerCodecTests {

    [TestMethod()]
    public void ToInt_DifferentPatterns_ReturnExpectedResults() {

      int encodedInteger;

      string decoded;

      encodedInteger = TextToIntegerCodec.ToInt32("");
      Assert.AreEqual(0, encodedInteger);
      decoded = TextToIntegerCodec.FromInt32(encodedInteger);
      Assert.AreEqual("", decoded);

      encodedInteger = TextToIntegerCodec.ToInt32("A"); // A ist 1
      Assert.AreEqual(1, encodedInteger);
      decoded = TextToIntegerCodec.FromInt32(encodedInteger);
      Assert.AreEqual("A", decoded);

      encodedInteger = TextToIntegerCodec.ToInt32("."); // Der Puknt ist das letzte mögliche Zeichen (alle 5 Bit gesetzt)
      Assert.AreEqual(31, encodedInteger);
      decoded = TextToIntegerCodec.FromInt32(encodedInteger);
      Assert.AreEqual(".", decoded);

      encodedInteger = TextToIntegerCodec.ToInt32("_A"); // Überlauf: Underscore ist 0 und A an zweiter Stelle => 6. Bit gesetzt
      Assert.AreEqual(32, encodedInteger);
      decoded = TextToIntegerCodec.FromInt32(encodedInteger);
      Assert.AreEqual("_A", decoded);

      encodedInteger = TextToIntegerCodec.ToInt32("aaaaaa"); // lower case wird zu upper case (by Design)
      Assert.AreEqual(34636833, encodedInteger);
      decoded = TextToIntegerCodec.FromInt32(encodedInteger);
      Assert.AreEqual("AAAAAA", decoded);

      encodedInteger = TextToIntegerCodec.ToInt32("AAAAAA"); // upper case liefert den selben Integer-Wert
      Assert.AreEqual(34636833, encodedInteger);
      decoded = TextToIntegerCodec.FromInt32(encodedInteger);
      Assert.AreEqual("AAAAAA", decoded);

      encodedInteger = TextToIntegerCodec.ToInt32("xÄÖÜß");
      Assert.AreEqual(32437112, encodedInteger);
      decoded = TextToIntegerCodec.FromInt32(encodedInteger);
      Assert.AreEqual("XÄÖÜß", decoded);

      encodedInteger = TextToIntegerCodec.ToInt32("Xäöüß");
      Assert.AreEqual(32437112, encodedInteger);
      decoded = TextToIntegerCodec.FromInt32(encodedInteger);
      Assert.AreEqual("XÄÖÜß", decoded);

      encodedInteger = TextToIntegerCodec.ToInt32("ÄäÄ");
      Assert.AreEqual(28539, encodedInteger);
      decoded = TextToIntegerCodec.FromInt32(encodedInteger);
      Assert.AreEqual("ÄÄÄ", decoded);

      encodedInteger = TextToIntegerCodec.ToInt32("ÖöÖ");
      Assert.AreEqual(29596, encodedInteger);
      decoded = TextToIntegerCodec.FromInt32(encodedInteger);
      Assert.AreEqual("ÖÖÖ", decoded);

      encodedInteger = TextToIntegerCodec.ToInt32("ÜüÜ");
      Assert.AreEqual(30653, encodedInteger);
      decoded = TextToIntegerCodec.FromInt32(encodedInteger);
      Assert.AreEqual("ÜÜÜ", decoded);

      encodedInteger = TextToIntegerCodec.ToInt32("Hello");
      Assert.AreEqual(16134312, encodedInteger);
      decoded = TextToIntegerCodec.FromInt32(encodedInteger);
      Assert.AreEqual("HELLO", decoded);

      encodedInteger = TextToIntegerCodec.ToInt32("EMEAX");
      Assert.AreEqual(25204133, encodedInteger);
      decoded = TextToIntegerCodec.FromInt32(encodedInteger);
      Assert.AreEqual("EMEAX", decoded);

      encodedInteger = TextToIntegerCodec.ToInt32("_Hello");
      Assert.AreEqual(516297984, encodedInteger);
      decoded = TextToIntegerCodec.FromInt32(encodedInteger);
      Assert.AreEqual("_HELLO", decoded);

      encodedInteger = TextToIntegerCodec.ToInt32("THE.ID");
      Assert.AreEqual(144676116, encodedInteger);
      decoded = TextToIntegerCodec.FromInt32(encodedInteger);
      Assert.AreEqual("THE.ID", decoded);

      encodedInteger = TextToIntegerCodec.ToInt32("Ab_Cde");
      Assert.AreEqual(172064833, encodedInteger);
      decoded = TextToIntegerCodec.FromInt32(encodedInteger);
      Assert.AreEqual("AB_CDE", decoded);

      encodedInteger = TextToIntegerCodec.ToInt32("......");
      Assert.AreEqual(1073741823, encodedInteger);
      decoded = TextToIntegerCodec.FromInt32(encodedInteger);
      Assert.AreEqual("......", decoded);

      long encodedLong; // Ab hier die Long-Tests

      encodedLong = TextToIntegerCodec.ToInt64("______A");
      Assert.AreEqual(1073741824L, encodedLong);
      decoded = TextToIntegerCodec.FromInt64(encodedLong);
      Assert.AreEqual("______A", decoded);

      encodedLong = TextToIntegerCodec.ToInt64("___________A");
      Assert.AreEqual(36028797018963968L, encodedLong);
      decoded = TextToIntegerCodec.FromInt64(encodedLong);
      Assert.AreEqual("___________A", decoded);

      encodedLong = TextToIntegerCodec.ToInt64("............");
      Assert.AreEqual(1152921504606846975L, encodedLong);
      decoded = TextToIntegerCodec.FromInt64(encodedLong);
      Assert.AreEqual("............", decoded);

      encodedLong = TextToIntegerCodec.ToInt64("AAAAAAAAAAAA");
      Assert.AreEqual(37191016277640225L, encodedLong);
      decoded = TextToIntegerCodec.FromInt64(encodedLong);
      Assert.AreEqual("AAAAAAAAAAAA", decoded);

      encodedLong = TextToIntegerCodec.ToInt64("KNÖDEL_WÜRST");
      Assert.AreEqual(742634033826132427L, encodedLong);
      decoded = TextToIntegerCodec.FromInt64(encodedLong);
      Assert.AreEqual("KNÖDEL_WÜRST", decoded);
    }

  }

}
