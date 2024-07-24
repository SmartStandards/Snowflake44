using System.Collections.Generic;
using System.Linq;

namespace System.SmartStandards {

  public static class Snowflake44FilteringExtensions {

    //////// for IEnumerable

    /// <summary>
    ///   Filters a given set of Snowflake44-Uids by the represented timestamp.
    /// </summary>
    /// <param name="extendee"></param>
    /// <param name="day">
    ///   NOTE: the time-portion of the given date will be ignorred!
    /// </param>
    /// <returns></returns>
    public static IEnumerable<long> FilterByDay(this IEnumerable<long> extendee, DateTime day) {
      return FilterByTimeRange(extendee, day.Date, day.Date.AddDays(1));
    }

    /// <summary>
    ///   Filters a given set of Snowflake44-Uids by the represented timestamp.  
    /// </summary>
    /// <param name="extendee"></param>
    /// <param name="from">
    ///   WARNING: this is time sensitive - consider to pass the date-portion only for correct filtering!
    /// </param>
    /// <param name="before">
    ///   WARNING: this is time sensitive - consider to pass the date-portion only for correct filtering
    ///   and this is representing the first value AFTER the selected range and will NOT be a match!
    /// </param>
    /// <returns></returns>
    public static IEnumerable<long> FilterByTimeRange(
      this IEnumerable<long> extendee, DateTime from, DateTime before
    ) {
      var expr = Snowflake44.BuildTimeRangeExpression(from, before);
      return extendee.Where(expr.Compile()); //<< The small difference betw. IEnumerable and IQuerable is the need of compilation!
    }

    //////// for IQueryable

    /// <summary>
    ///   Filters a given set of Snowflake44-Uids by the represented timestamp.
    /// </summary>
    /// <param name="extendee"></param>
    /// <param name="day">
    ///   NOTE: the time-portion of the given date will be ignorred!
    /// </param>
    /// <returns></returns>
    public static IQueryable<long> FilterByDay(
      this IQueryable<long> extendee, DateTime day
    ) {
      return FilterByTimeRange(extendee, day.Date, day.Date.AddDays(1));
    }

    /// <summary>
    ///   Filters a given set of Snowflake44-Uids by the represented timestamp.
    /// </summary>
    /// <param name="extendee"></param>
    /// <param name="from">
    ///   WARNING: this is time sensitive - consider to pass the date-portion only for correct filtering!
    /// </param>
    /// <param name="before">
    ///   WARNING: this is time sensitive - consider to pass the date-portion only for correct filtering
    ///   and this is representing the first value AFTER the selected range and will NOT be a match!
    /// </param>
    /// <returns></returns>
    public static IQueryable<long> FilterByTimeRange(
      this IQueryable<long> extendee, DateTime from, DateTime before
    ) {
      var expr = Snowflake44.BuildTimeRangeExpression(from, before);
      return extendee.Where(expr);
    }

  }

}
