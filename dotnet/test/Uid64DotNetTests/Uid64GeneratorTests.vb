Imports IdHandling.IdHandling
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace IdHandling

  <TestClass>
  Public Class Uid64GeneratorTests

    <TestMethod()>
    Public Sub GenerateUid_CalledQuicklyAndOften_DoesNotCreateDuplicateIds()
      Dim uid As Long
      Dim previousUid As Long

      For i = 1 To 1000000

        previousUid = uid

        uid = Uid64Generator.GenerateUid()
        Assert.AreNotEqual(previousUid, uid)

      Next

    End Sub

    <TestMethod()>
    Public Sub DecodeDateTimeTest()

      Dim expectedDate1 As New Date(1973, 12, 9, 15, 33, 22)
      Dim saltedTimestamp1 As Long = Uid64Generator.EncodeDateTime(expectedDate1)

      Dim expectedDate2 As Date = expectedDate1.AddMilliseconds(1)
      Dim saltedTimestamp2 As Long = Uid64Generator.EncodeDateTime(expectedDate2)

      Dim actualDate1 As Date = Uid64Generator.DecodeDateTime(saltedTimestamp1)
      Dim actualDate2 As Date = Uid64Generator.DecodeDateTime(saltedTimestamp2)

      Assert.AreEqual(expectedDate1, actualDate1)
      Assert.AreEqual(expectedDate2, actualDate2)
      Assert.AreEqual(actualDate1.AddMilliseconds(1), actualDate2)

    End Sub

  End Class

End Namespace

