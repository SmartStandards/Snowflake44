using System.Collections.Generic;

namespace System.SmartStandards {

  public partial class Snowflake44 {

    private static long _PreviousTimeFrame;

    private static readonly HashSet<int> _RandomsOfCurrentTimeFrame = new HashSet<int>();

    private static Random RandomGenerator { get; set; } = new Random();

    internal static DateTime CenturyBegin { get; set; } = new DateTime(1900, 1, 1);

    /// <summary>
    ///   Generates an id based on the current UTC time (44 bit) and a random part (19 bit).
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
      return EncodeDateTime(incomingDate, false);
    }

    /// <summary>
    ///   Encodes a DateTime value to an integer 64 value.
    /// </summary>
    /// <param name="incomingDate"> A date value between 1900-01-01 and 2457-01-01 </param>
    /// <param name="skipRandomSaltAndHistory">
    ///   opt-out the addition of the random-salt portion, 
    ///   which will result in the lowest possible long value for the given timestamp (for filtering purpose)
    /// </param>
    internal static long EncodeDateTime(DateTime incomingDate, bool skipRandomSaltAndHistory) {

      // 11
      // 98                 0
      // 1TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTRRRRRRRRRRRRRRRRRRR
      // [                 44                       ][        19       ]

      lock (RandomGenerator) {

        long elapsedMilliseconds = incomingDate.Ticks / 10000L - CenturyBegin.Ticks / 10000L;
        // ^ One tick is 100 nano seconds (There are 10,000 ticks in a millisecond)

        // 44 bit gives us 557 years date range resolved in a resolution of microseconds
        if (elapsedMilliseconds >= 17592186044416L){ // We don't want to use the 45th bit
          throw new Exception("Time stamp exceeds 44 bit!");
        }

        lock (_RandomsOfCurrentTimeFrame) {

          if (elapsedMilliseconds != _PreviousTimeFrame) {
            _RandomsOfCurrentTimeFrame.Clear();
          }

          _PreviousTimeFrame = elapsedMilliseconds;

          elapsedMilliseconds <<= 19;

          int randomValue = 0;

          if (!skipRandomSaltAndHistory) {

            bool collisionDetected;

            do {
              randomValue = RandomGenerator.Next(524287);
              collisionDetected = !_RandomsOfCurrentTimeFrame.Add(randomValue);
            } while (collisionDetected);

          }

          long randomPart = randomValue;
          long uid = elapsedMilliseconds | randomPart;

          return uid;
        }

      }

    }

    /// <summary>
    ///   Decodes a DateTime value from an encoded integer 64 value.
    /// </summary>
    public static DateTime DecodeDateTime(long uid64) {

      if (uid64 < 0L) uid64 = -uid64;

      long decodedMilliseconds = uid64 >> 19;
      long decodedTicks = decodedMilliseconds * 10000L;
      var decodedDate = new DateTime(CenturyBegin.Ticks + decodedTicks);

      return decodedDate;
    }

    /// <param name="from"></param>
    /// <param name="before"></param>
    /// <param name="fromUid"></param>
    /// <param name="beforeUid">
    ///  WARNING: this is time sensitive - consider to pass the date-portion only for correct mapping
    ///  and this is representing the first value AFTER the selected range and will NOT be a match!
    /// </param>
    public static void GetTimeRange(
      DateTime from, DateTime before,
      out long fromUid, out long beforeUid
    ) {

      fromUid = Snowflake44.EncodeDateTime(from, skipRandomSaltAndHistory: true);
      beforeUid = Snowflake44.EncodeDateTime(before, skipRandomSaltAndHistory: true);

    }

    /// <param name="extendee"></param>
    /// <param name="from"></param>
    /// <param name="before">
    ///  WARNING: this is time sensitive - consider to pass the date-portion only for correct mapping
    ///  and this is representing the first value AFTER the selected range and will NOT be a match!
    /// </param>
    public static bool IsInTimeRange(long extendee, DateTime from, DateTime before) {

      Snowflake44.GetTimeRange(
       from, before,
       out long fromUid, out long untilUid
     );

      return (extendee >= fromUid && extendee < untilUid);
    }

  }

}
