using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace System.SmartStandards {

  [TestClass]
  public class Snowflake44FilteringTests {

    [TestMethod()]
    public void TestSnowflake44Filtering1() {

      //PREP

      var entities = new List<MyEntity>();

      long uid1 = Snowflake44.EncodeDateTime(new DateTime(2024, 01, 01));
      long uid2 = Snowflake44.EncodeDateTime(new DateTime(2024, 01, 02));
      long uid3 = Snowflake44.EncodeDateTime(new DateTime(2024, 01, 03));

      entities.Add(new MyEntity { Uid = uid1 });
      entities.Add(new MyEntity { Uid = uid2 });
      entities.Add(new MyEntity { Uid = uid3 });

      //TEST

      DateTime dayToFilter = new DateTime(2024, 01, 02);

      var filterExpression = Snowflake44.BuildTimeRangeExpression<MyEntity>(
        (e) => e.Uid, dayToFilter
      ).Compile();

      MyEntity[] outcome1 = entities.Where(filterExpression).ToArray();

      Assert.AreEqual(1, outcome1.Length);
      Assert.AreEqual(uid2, outcome1[0].Uid);

    }

    [TestMethod()]
    public void TestSnowflake44Filtering2() {

      //PREP

      var uids = new List<long>();

      long uid1 = Snowflake44.EncodeDateTime(new DateTime(2024, 01, 01));
      long uid2 = Snowflake44.EncodeDateTime(new DateTime(2024, 01, 02));
      long uid3 = Snowflake44.EncodeDateTime(new DateTime(2024, 01, 03));

      uids.Add(uid1);
      uids.Add(uid2);
      uids.Add(uid3);

      //TEST

      DateTime dayToFilter = new DateTime(2024, 01, 02);

      long[] outcome1 = uids.FilterByDay(dayToFilter).ToArray();
      
      Assert.AreEqual(1, outcome1.Length);
      Assert.AreEqual(uid2, outcome1[0]);

    }

    public class MyEntity {
      public long Uid { get; set; }
    }

  }

}
