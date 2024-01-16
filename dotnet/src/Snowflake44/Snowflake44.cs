using System;
using System.Collections.Generic;

namespace System {

  public partial class Snowflake44 {

    private static long _PreviousTimeFrame;

    private static readonly HashSet<int> _RandomsOfCurrentTimeFrame = new HashSet<int>();

    private static Random RandomGenerator { get; set; } = new Random();

    internal static DateTime CenturyBegin { get; set; } = new DateTime(1900, 1, 1);

    /// <summary>
    ///   Generates an id based on the current UTC time and a random part.
    /// </summary>
    /// <returns> An integer 64 id containing the encoded current UTC time. </returns>
    public static long Generate() {
      return EncodeDateTime(DateTime.UtcNow);
    }

    /// <summary>
    ///   Encodes a DateTime value to an integer 64 value.
    /// </summary>
    /// <param name="incomingDate"> A date value between 1900-01-01 and 2457-01-01 </param>
    public static long EncodeDateTime(DateTime incomingDate) {

      // 11
      // 98                 0
      // 1TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTRRRRRRRRRRRRRRRRRRR
      // [                 44                       ][        19       ]

      lock (RandomGenerator) {

        long elapsedMilliseconds = incomingDate.Ticks / 10000L - CenturyBegin.Ticks / 10000L;
        // ^ One tick is 100 nano seconds (There are 10,000 ticks in a millisecond)

        // 44 bit gives us 557 years date range resolved in a resolution of microseconds
        if (elapsedMilliseconds >= 17592186044416L) // We don't want to use the 45th bit
        {
          throw new Exception("Time stamp exceeds 44 bit!");
        }

        if (elapsedMilliseconds != _PreviousTimeFrame) {
          _RandomsOfCurrentTimeFrame.Clear();
        }

        _PreviousTimeFrame = elapsedMilliseconds;

        elapsedMilliseconds = elapsedMilliseconds << 19;

        int randomValue;
        bool collisionDetected;

        do {
          randomValue = RandomGenerator.Next(524287);
          collisionDetected = !_RandomsOfCurrentTimeFrame.Add(randomValue);
        }

        while (collisionDetected);

        long randomPart = randomValue;
        long uid = elapsedMilliseconds | randomPart;

        return uid;
      }

    }

    /// <summary>
    ///   Decodes a DateTime value from an encoded integer 64 value.
    /// </summary>
    public static DateTime DecodeDateTime(long uid64) {

      if (uid64 < 0L)
        uid64 = -uid64;

      long decodedMilliseconds = uid64 >> 19;
      long decodedTicks = decodedMilliseconds * 10000L;
      var ts = new TimeSpan(decodedTicks);
      var decodedDate = new DateTime(CenturyBegin.Ticks + decodedTicks);

      return decodedDate;
    }

  }

}
