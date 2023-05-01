Imports System.Runtime.CompilerServices
Imports System.Collections.Generic
Imports System

Namespace IdHandling

  Public Class Uid64Generator

    Private Shared _PreviousTimeFrame As Long

    Private Shared ReadOnly _RandomsOfCurrentTimeFrame As New HashSet(Of Integer)

    Private Shared Property RandomGenerator As Random = New Random()

    Friend Shared Property CenturyBegin As DateTime = New DateTime(1900, 1, 1)

    ''' <summary>
    '''   Generates an id based on the current UTC time and a random part.
    ''' </summary>
    ''' <returns> An integer 64 id containing the encoded current UTC time. </returns>
    Public Shared Function GenerateUid() As Long
      Return Uid64Generator.EncodeDateTime(DateTime.UtcNow)
    End Function

    ''' <summary>
    '''   Encodes a DateTime value to an integer 64 value.
    ''' </summary>
    ''' <param name="incomingDate"> A date value between 1900-01-01 and 2457-01-01 </param>
    Public Shared Function EncodeDateTime(incomingDate As Date) As Long

      '                                             11
      '                                             98                 0
      ' 1TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTRRRRRRRRRRRRRRRRRRR
      '  [                 44                       ][        19       ]

      SyncLock Uid64Generator.RandomGenerator

        Dim elapsedMilliseconds As Long = incomingDate.Ticks \ 10000 - CenturyBegin.Ticks \ 10000
        ' ^ One tick is 100 nano seconds (There are 10,000 ticks in a millisecond)

        ' 44 bit gives us 557 years date range resolved in a resolution of microseconds
        If (elapsedMilliseconds >= 17592186044416) Then ' We don't want to use the 45th bit
          Throw New Exception("Time stamp exceeds 44 bit!")
        End If

        If (elapsedMilliseconds <> _PreviousTimeFrame) Then
          _RandomsOfCurrentTimeFrame.Clear()
        End If

        _PreviousTimeFrame = elapsedMilliseconds

        elapsedMilliseconds = elapsedMilliseconds << 19

        Dim randomValue As Integer
        Dim collisionDetected As Boolean

        Do
          randomValue = Uid64Generator.RandomGenerator.Next(524287)
          collisionDetected = Not _RandomsOfCurrentTimeFrame.Add(randomValue)

        Loop While collisionDetected

        Dim randomPart As Long = randomValue
        Dim uid As Long = (elapsedMilliseconds Or randomPart)

        Return uid
      End SyncLock

    End Function

    ''' <summary>
    '''   Decodes a DateTime value from an encoded integer 64 value.
    ''' </summary>
    Public Shared Function DecodeDateTime(dateAsLong As Long) As Date

      If (dateAsLong < 0) Then dateAsLong = -dateAsLong

      Dim decodedMilliseconds As Long = (dateAsLong >> 19)
      Dim decodedTicks As Long = decodedMilliseconds * 10000
      Dim ts = New TimeSpan(decodedTicks)
      Dim decodedDate = New Date(Uid64Generator.CenturyBegin.Ticks + decodedTicks)

      Return decodedDate
    End Function


  End Class

End Namespace
