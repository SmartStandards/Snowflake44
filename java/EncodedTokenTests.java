import java.lang.reflect.Method;
import java.util.Objects;
//import java.util.function.Consumer;

public class EncodedTokenTests {

  public static void main() {

    int runTestsCount = 0;
    int failedTestsCount = 0;

    Method[] declaredMethods = EncodedTokenTests.class.getDeclaredMethods();

    for (Method method : declaredMethods) {

      String methodName = method.getName();      

      if (EncodedTokenTests.countMatchesOfChar(methodName, '_') == 2){

        runTestsCount ++;

        String outcome = "OK";
        String errorMessage = "";

        try {          
          method.invoke(null, (Object[]) null);          
        } catch (Exception e) {

          outcome = "FAILED";

          Exception innerException = (Exception)e.getCause();          
          errorMessage = (innerException != null) ? innerException.getMessage() :  e.getMessage();
          
          failedTestsCount ++;
        }
        System.out.println(outcome + ": " + methodName + "() " + errorMessage);
      }
    }    

    System.out.println("Tests run: " + runTestsCount + " failed: " + failedTestsCount);
  }

  public static void Decode_ErrorCases_BehaveAsDesigned() throws Exception {
    try {
      TokenEncoder.decode(-1L);
    } catch (Exception e){
      return;
    }
    throw new RuntimeException("Expected exception was not thrown");
  }

  public static void EncodeAndDecodeRaw_ForthAndBack_ReturnsOriginal() throws Exception {

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("", 0);

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("A", 1);// A ist 1

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("#", 31);// Hashtag ist das letzte mögliche Zeichen (alle 5 Bit gesetzt)

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("_A", 32); // Überlauf: Underscore ist 0 und A an zweiter Stelle => 6. Bit gesetzt

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("aaaaaa", 34636833, "AAAAAA");  // lower case wird zu upper case (robustes Design)
    EncodedTokenTests.assertEncodingAndDecodingOfRaw("AAAAAA", 34636833);  // upper case liefert den selben Integer-Wert

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("xÄÖÜß", 32437112, "XÄÖÜß");

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("Xäöüß", 32437112, "XÄÖÜß");

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("ÄäÄ", 28539, "ÄÄÄ");

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("ÖöÖ", 29596, "ÖÖÖ");

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("ÜüÜ", 30653, "ÜÜÜ");

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("Hello", 16134312, "HELLO");

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("EMEAX", 25204133);

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("_Hello", 516297984, "_HELLO");

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("THE#ID", 144676116);

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("Ab_Cde", 172064833, "AB_CDE");

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("######", 1073741823);

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("______A", 1073741824L);

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("___________A", 36028797018963968L);

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("############", 1152921504606846975L);

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("AAAAAAAAAAAA", 37191016277640225L);

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("KNÖDEL_WÜRST", 742634033826132427L);

    EncodedTokenTests.assertEncodingAndDecodingOfRaw("WÜRßTKNÖDÄL", 14466152471153591L);

  }

  public static void EncodeAndDecode_ForthAndBack_ReturnsOriginal() throws Exception {

    EncodedTokenTests.assertEncodingAndDecodingOf("", 0);

    // All chars of our alphabet

    EncodedTokenTests.assertEncodingAndDecodingOf("Abcdefghijkl", 445092485129178177L);
    EncodedTokenTests.assertEncodingAndDecodingOf("Mnopqrstuvwx", 891384680460860877L);
    EncodedTokenTests.assertEncodingAndDecodingOf("Yz", 857L);
    EncodedTokenTests.assertEncodingAndDecodingOf("Würßtknödäl", 14466152471153591L);

    // All numbers

    EncodedTokenTests.assertEncodingAndDecodingOf("0123456789", 18098222586996223L);

    // All escape duets

    EncodedTokenTests.assertEncodingAndDecodingOf("#", 1023L);
    EncodedTokenTests.assertEncodingAndDecodingOf("+", 63L);
    EncodedTokenTests.assertEncodingAndDecodingOf(",", 127L);
    EncodedTokenTests.assertEncodingAndDecodingOf(".", 159L);
    EncodedTokenTests.assertEncodingAndDecodingOf("?", 223L);
    EncodedTokenTests.assertEncodingAndDecodingOf("(", 351L);
    EncodedTokenTests.assertEncodingAndDecodingOf(")", 383L);
    EncodedTokenTests.assertEncodingAndDecodingOf("-", 447L);
    EncodedTokenTests.assertEncodingAndDecodingOf("&", 479L);
    EncodedTokenTests.assertEncodingAndDecodingOf("@", 575L);
    EncodedTokenTests.assertEncodingAndDecodingOf(":", 607L);
    EncodedTokenTests.assertEncodingAndDecodingOf("\'", 671L);
    EncodedTokenTests.assertEncodingAndDecodingOf("\\", 703L);
    EncodedTokenTests.assertEncodingAndDecodingOf("/", 735L);
    EncodedTokenTests.assertEncodingAndDecodingOf("=", 767L);
    EncodedTokenTests.assertEncodingAndDecodingOf("*", 799L);
    EncodedTokenTests.assertEncodingAndDecodingOf("!", 831L);
    EncodedTokenTests.assertEncodingAndDecodingOf("]", 895L);
    EncodedTokenTests.assertEncodingAndDecodingOf("[", 927L);
    EncodedTokenTests.assertEncodingAndDecodingOf("%", 959L);
    EncodedTokenTests.assertEncodingAndDecodingOf("^", 991L);

    // Inline Escape Chars

    EncodedTokenTests.assertEncodingAndDecodingOf("(Unknown)", 13491814534698335L);

    EncodedTokenTests.assertEncodingAndDecodingOf("R&V", 736242);

    EncodedTokenTests.assertEncodingAndDecodingOf("AT&T", 686804993);

    EncodedTokenTests.assertEncodingAndDecodingOf("a@b.com", 485368583758085152L);

    // Numerical and Escaping Kung-Fu

    EncodedTokenTests.assertEncodingAndDecodingOf("1+-2=3*4^10", 551630212503602495L);

    EncodedTokenTests.assertEncodingAndDecodingOf("1%=3!", 844887359);

    EncodedTokenTests.assertEncodingAndDecodingOf("1,200.34Eu", 762243209639038271L);

    // Casing Kung-Fu

    EncodedTokenTests.assertEncodingAndDecodingOf("HelloWorld", 4946143410008232L);
    EncodedTokenTests.assertEncodingAndDecodingOf("helloWorld", 158276589120263424L);
    EncodedTokenTests.assertEncodingAndDecodingOf("Zimmer12A", 1154830342468922L);
    EncodedTokenTests.assertEncodingAndDecodingOf("Gasse13b", 2251982322125863L);

  }

  public static void ReadableTokenToRawToken_TestPatterns_ReturnExpextedOutput() throws Exception {
   
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

  public static void assertReadableToRaw(String readable, String expectedRaw) throws Exception {
    String actual = TokenEncoder.readableToRaw(readable);
    EncodedTokenTests.assertAreEqual(expectedRaw, actual, readable);
  }

  public static void assertAreEqual(String expected, String actual, String... hint ) throws Exception{
    if (Objects.equals(actual, expected)) return;
    String hintArgument = (hint.length > 0) ? hint[0] : "";
    throw new Exception("AreEqual failed for \"" + hintArgument + "\" Expected: " + expected + " Actual: " + actual);
  }

  public static void assertAreEqual(long expected, long actual, String... hint ) throws Exception{
    if (actual == expected) return;
    String hintArgument = (hint.length > 0) ? hint[0] : "";
    throw new Exception("AreEqual failed for \"" + hintArgument + "\" Expected: " + expected + " Actual: " + actual);
  }

  private static void assertEncodingAndDecodingOf(String originalToken, long expectedId) throws Exception {

    long actualId;

    String decodedToken;

    actualId = TokenEncoder.encode(originalToken);
    EncodedTokenTests.assertAreEqual(expectedId, actualId, originalToken);

    decodedToken = TokenEncoder.decode(actualId);
    EncodedTokenTests.assertAreEqual(originalToken, decodedToken, originalToken);
  }

  private static void assertEncodingAndDecodingOfRaw(String rawToken, long expectedEncodedId, String... deviantExpectedToken) throws Exception {
    long actualEncodedId = TokenEncoder.rawToInt64(rawToken);
    EncodedTokenTests.assertAreEqual(expectedEncodedId, actualEncodedId, rawToken);
    String decodedRawToken = TokenEncoder.int64ToRaw(actualEncodedId);      
    String expectedToken = (deviantExpectedToken.length > 0) ? deviantExpectedToken[0] : rawToken;
    EncodedTokenTests.assertAreEqual(expectedToken, decodedRawToken, rawToken);
  }

  /*
  private static void assertThrowsException(Consumer<Void> action) throws Exception {
    try {
      action.accept(null);
    } catch (Exception e){
      return;
    }
    throw new RuntimeException("Expected exception was not thrown");
  }
  */

  public static int countMatchesOfChar(String haystack, Character needle){
    int count = 0;
    for (int i = 0; i < haystack.length(); i++){
      char c = haystack.charAt(i);        
      if (c==needle) count++;      
    }
    return count;
  }

}
