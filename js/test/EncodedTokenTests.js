// ENCODING OF THIS FILE IS NOT UTF-8, BUT ISO 8859-1 (aka Windows-1252)!

class EncodedTokenTests {

  static runTests(onPrint) {

    var runTestsCount = 0;
    var failedTestsCount = 0;

    var methodNames = Object.getOwnPropertyNames(EncodedTokenTests);

    for (var methodName of methodNames) {

      var count = 0;
      for (var c of methodName) if (c=='_') count++;
        
      if (count == 2) {

        runTestsCount++;

        var outcome = 'OK';
        var errorMessage = '';

        try{
          EncodedTokenTests[methodName]();
        } catch (error) {
          outcome = "FAILED";
          errorMessage = error;
          failedTestsCount++;
        }
        onPrint(outcome + ': ' + methodName + '() ' + errorMessage);
      } // dynamically invoke the refresh function;
    }
    onPrint('Tests run: ' + runTestsCount + ' failed: ' + failedTestsCount);
  }    
  
  static readableTokenToRawToken_TestPatterns_ReturnExpextedOutput() {

    // All numbers

    EncodedTokenTests.assertReadableToRaw("1234567890", "#IZEHSGLBPO");

    // All escape duets

    EncodedTokenTests.assertReadableToRaw("#", "##");

    EncodedTokenTests.assertReadableToRaw("+", "#A");

    EncodedTokenTests.assertReadableToRaw(",", "#C");

    EncodedTokenTests.assertReadableToRaw(".", "#D");

    EncodedTokenTests.assertReadableToRaw("?", "#F");

    EncodedTokenTests.assertReadableToRaw("(", "#J");

    EncodedTokenTests.assertReadableToRaw(")", "#K");

    EncodedTokenTests.assertReadableToRaw("-", "#M");

    EncodedTokenTests.assertReadableToRaw("&", "#N");

    EncodedTokenTests.assertReadableToRaw("@", "#Q");

    EncodedTokenTests.assertReadableToRaw(":", "#R");

    EncodedTokenTests.assertReadableToRaw("\'", "#T");

    EncodedTokenTests.assertReadableToRaw("\\", "#U");

    EncodedTokenTests.assertReadableToRaw("/", "#V");

    EncodedTokenTests.assertReadableToRaw("=", "#W");

    EncodedTokenTests.assertReadableToRaw("*", "#X");

    EncodedTokenTests.assertReadableToRaw("!", "#Y");

    EncodedTokenTests.assertReadableToRaw("]", "#Ä");

    EncodedTokenTests.assertReadableToRaw("[", "#Ö");

    EncodedTokenTests.assertReadableToRaw("%", "#Ü");

    EncodedTokenTests.assertReadableToRaw("^", "#ß");

    // Inline Escape Chars

    EncodedTokenTests.assertReadableToRaw("(Unknown)", "#JUNKNOWN#K");

    EncodedTokenTests.assertReadableToRaw("R&V", "R#NV");

    EncodedTokenTests.assertReadableToRaw("AT&T", "A_T#NT");

    EncodedTokenTests.assertReadableToRaw("me@there.com", "_ME#Q_THERE#D_COM");

    // Numbers followed by escaped chars

    EncodedTokenTests.assertReadableToRaw("1+-2=3*4^10!", "#IAMZWEXHßIOY");

    EncodedTokenTests.assertReadableToRaw("1,200.34Usd", "#ICZOODEH_USD");

    // Miscellaneous Cases

    EncodedTokenTests.assertReadableToRaw("Mambo5", "MAMBO#S");

    EncodedTokenTests.assertReadableToRaw("Mambo#5", "MAMBO###S");

    EncodedTokenTests.assertReadableToRaw("Mambo5#", "MAMBO#S#");

    EncodedTokenTests.assertReadableToRaw("Zimmer12A", "ZIMMER#IZ_A");

    EncodedTokenTests.assertReadableToRaw("Gasse13bf", "GASSE#IE__BF");
    
  }

  static assertReadableToRaw(readable, expectedRaw) {
    var actual = TokenEncoder.readableToRaw(readable);
    EncodedTokenTests.assertAreEqual(expectedRaw, actual, readable);
  }

  static assertAreEqual(expected, actual, hint) {
    if (actual == expected) return;
    throw new Error('AreEqual failed for "' + hint + '" Expected: ' + expected + ' Actual: ' + actual);
  }

}