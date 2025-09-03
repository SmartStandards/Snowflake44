using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.SmartStandards {

  [TestClass]
  public class Snowflake44GeneratorTests {

    [TestMethod()]
    public void GenerateUid_CalledQuicklyAndOften_DoesNotCreateDuplicateIds() {

      long uid = 0;
      long previousUid;

      for (int i = 1; i <= 1000000; i++) {

        previousUid = uid;

        uid = Snowflake44.Generate();
        Assert.AreNotEqual(previousUid, uid);

      }

    }

    [TestMethod()]
    public void DecodeDateTimeTest() {

      DateTime expectedDate1 = new DateTime(1973, 12, 9, 15, 33, 22, DateTimeKind.Utc);
      long saltedTimestamp1 = Snowflake44.EncodeDateTime(expectedDate1);

      DateTime expectedDate2 = expectedDate1.AddMilliseconds(1d);
      long saltedTimestamp2 = Snowflake44.EncodeDateTime(expectedDate2);

      DateTime actualDate1 = Snowflake44.DecodeDateTime(saltedTimestamp1);
      DateTime actualDate2 = Snowflake44.DecodeDateTime(saltedTimestamp2);

      Assert.AreEqual(expectedDate1, actualDate1);
      Assert.AreEqual(expectedDate2, actualDate2);
      Assert.AreEqual(actualDate1.AddMilliseconds(1d), actualDate2);

    }

    [TestMethod()]
    public void GuidConversionTest() {

      const long inputUid = 2079256415714739899L;
      const string expectedGuidString = "00000000-0000-0000-1cdb-00b197a106bb";

      Guid guid = Snowflake44.ConvertToGuid(inputUid);
      Assert.AreEqual(expectedGuidString, guid.ToString());

      // check the endian correctness of the conversion
      string uidAsHex = inputUid.ToString("x16");
      string expectedLast16HexChars = expectedGuidString.Replace("-", "").Substring(16, 16);
      Assert.AreEqual(expectedLast16HexChars, uidAsHex);

      long reConvertedUid = Snowflake44.ConvertFromGuid(guid);
      Assert.AreEqual(inputUid, reConvertedUid);

    }

  }

}
