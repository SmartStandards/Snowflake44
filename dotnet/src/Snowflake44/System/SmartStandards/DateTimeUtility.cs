namespace System.SmartStandards {

  public static class DateTimeUtility {

    /// <summary>
    ///   Converts a DateTime value to integer.
    /// </summary>
    /// <returns> Number of 10-second-frames after 1900-01-01 00:00</returns>
    /// <remarks> 
    ///   Because we use only positive signed integer values, we've got only 31 bit.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException"> When date is after 2500-01-01 23:59:59. </exception>
    public static int ToInteger10SecondsResolution(this DateTime extendee) {
      var centuryBegin = new DateTime(1900, 1, 1);
      long elapsed10Seconds = (extendee.Ticks - centuryBegin.Ticks) / 100000000L;
      // ^ One tick is 100 nano seconds (There are 10,000 ticks in a millisecond)
      return Convert.ToInt32(elapsed10Seconds);
    }

    /// <summary>
    ///   Converts an integer value to DateTime.
    /// </summary>
    /// <remarks> 
    ///   The integer must represent the number of 10-second-frames after 1900-01-01 00:00.
    ///   The maximum date would be 2500-01-01 23:59:59.
    /// </remarks>
    public static DateTime FromInteger10SecondsResolution(int dateAsInteger) {
      var centuryBegin = new DateTime(1900, 1, 1);
      long elapsedTicks = dateAsInteger * 100000000L;
      return new DateTime(centuryBegin.Ticks + elapsedTicks);
    }

  }

}
