using System.Linq.Expressions;
using System.Reflection;

namespace System.SmartStandards {

  public partial class Snowflake44 {

    /// <summary>
    ///   Builds an Expressen to filter a set of entities with an (snowflake44)long-uid property by a given day.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="selector"></param>
    /// <param name="day">
    ///   NOTE: the time-portion of the given date will be ignorred!
    /// </param>
    public static Expression<Func<TEntity, bool>> BuildTimeRangeExpression<TEntity>(
      Expression<Func<TEntity, long>> selector, DateTime day
    ) {
      return BuildTimeRangeExpression(selector, day.Date, day.Date.AddDays(1));
    }

    /// <summary>
    ///   Builds an Expressen to filter a set of entities with an (snowflake44)long-uid property by a given timerange.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="selector"></param>
    /// <param name="from">
    ///   WARNING: this is time sensitive - consider to pass the date-portion only for correct filtering!
    /// </param>
    /// <param name="before">
    ///   WARNING: this is time sensitive - consider to pass the date-portion only for correct filtering
    ///   and this is representing the first value AFTER the selected range and will NOT be a match!
    /// </param>
    public static Expression<Func<TEntity, bool>> BuildTimeRangeExpression<TEntity>(
      Expression<Func<TEntity, long>> selector, DateTime from, DateTime before
    ) {

      //selector zerlegen
      PropertyInfo prop;
      if (selector.Body is MemberExpression) {
        var mex = selector.Body as MemberExpression;
        prop = (mex.Member as PropertyInfo);
      }
      else {
        var uex = selector.Body as UnaryExpression;
        if (uex.Operand is MemberExpression) {
          var mex = uex.Operand as MemberExpression;
          prop = (mex.Member as PropertyInfo);
        }
        else {
          throw new Exception("No valid Property selected");
        }
      }

      Expression field = selector.Body;
      var param = selector.Parameters[0];
      field = Expression.Property(param, prop);

      Snowflake44.GetTimeRange(
        from, before,
        out long fromUid, out long untilUid
      );

      var body = Expression.AndAlso(
        Expression.GreaterThanOrEqual(
           field, Expression.Constant(fromUid)
         ),
        Expression.LessThan(
           field, Expression.Constant(untilUid)
         )
       );

      return Expression.Lambda<Func<TEntity, bool>>(body, param);

    }

    /// <summary>
    ///   Builds an Expressen to filter a set of (snowflake44)long-uids by a given timerange.
    /// </summary>
    /// <param name="from">
    ///   WARNING: this is time sensitive - consider to pass the date-portion only for correct filtering!
    /// </param>
    /// <param name="before">
    ///   WARNING: this is time sensitive - consider to pass the date-portion only for correct filtering
    ///   and this is representing the first value AFTER the selected range and will NOT be a match!
    /// </param>
    public static Expression<Func<long, bool>> BuildTimeRangeExpression(DateTime from, DateTime before) {

      var param = Expression.Parameter(typeof(long), "uid");

      Snowflake44.GetTimeRange(
        from, before,
        out long fromUid, out long untilUid
      );

      var body = Expression.AndAlso(
        Expression.GreaterThanOrEqual(
           param, Expression.Constant(fromUid)
         ),
         Expression.LessThan(
           param, Expression.Constant(untilUid)
         )
       );

      return Expression.Lambda<Func<long, bool>>(body, param);

    }

  }

}
