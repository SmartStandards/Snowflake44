Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace Snowflake44Tests

  <TestClass>
  Public Class Snowflake44GeneratorTests

    <TestMethod()>
    Public Sub GenerateUid_CalledQuicklyAndOften_DoesNotCreateDuplicateIds()
      Dim uid As Long
      Dim previousUid As Long

      For i = 1 To 1000000

        previousUid = uid

        uid = Snowflake44.Generate()
        Assert.AreNotEqual(previousUid, uid)

      Next

    End Sub

    <TestMethod()>
    Public Sub DecodeDateTimeTest()

      Dim expectedDate1 As New Date(1973, 12, 9, 15, 33, 22)
      Dim saltedTimestamp1 As Long = Snowflake44.EncodeDateTime(expectedDate1)

      Dim expectedDate2 As Date = expectedDate1.AddMilliseconds(1)
      Dim saltedTimestamp2 As Long = Snowflake44.EncodeDateTime(expectedDate2)

      Dim actualDate1 As Date = Snowflake44.DecodeDateTime(saltedTimestamp1)
      Dim actualDate2 As Date = Snowflake44.DecodeDateTime(saltedTimestamp2)

      Assert.AreEqual(expectedDate1, actualDate1)
      Assert.AreEqual(expectedDate2, actualDate2)
      Assert.AreEqual(actualDate1.AddMilliseconds(1), actualDate2)

    End Sub

  End Class

End Namespace
