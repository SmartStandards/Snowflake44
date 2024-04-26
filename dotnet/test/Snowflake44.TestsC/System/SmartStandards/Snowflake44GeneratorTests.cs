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

      DateTime expectedDate1 = new DateTime(1973, 12, 9, 15, 33, 22);
      long saltedTimestamp1 = Snowflake44.EncodeDateTime(expectedDate1);

      DateTime expectedDate2 = expectedDate1.AddMilliseconds(1d);
      long saltedTimestamp2 = Snowflake44.EncodeDateTime(expectedDate2);

      DateTime actualDate1 = Snowflake44.DecodeDateTime(saltedTimestamp1);
      DateTime actualDate2 = Snowflake44.DecodeDateTime(saltedTimestamp2);

      Assert.AreEqual(expectedDate1, actualDate1);
      Assert.AreEqual(expectedDate2, actualDate2);
      Assert.AreEqual(actualDate1.AddMilliseconds(1d), actualDate2);

    }

  }

}
