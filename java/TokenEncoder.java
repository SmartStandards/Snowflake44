public class TokenEncoder {

  // CREDITS: http://vodi.de for https://github.com/SmartStandards/Snowflake44

  /**
   * Decodes a long to a token string (of maximum 12 characters).
   * The String will be PascalCase (e.g. "HelloWorld")
  */
  public static String decodeToken(long int64Value) {
    String rawToken = TokenEncoder.encodedTokenToRawToken(int64Value);
    String token = TokenEncoder.rawTokenToPascalCase(rawToken);
    return token;
  }

  /**
   * Decodes a long to a raw token string (of maximum 12 characters).
   * The String will be CAPITALS and underscores (e.g. "HELLO_WORLD")
  */
  public static String encodedTokenToRawToken(long int64Value) {
    long bitmask = 31;
    int offset = 0;
    byte mappedCode;
    byte unmappedCode;
    char[]decoded = new char[12];

    if (int64Value < 0)
      int64Value *= -1;

    for (int i = 0; i <= 11; i++) {
      mappedCode = (byte)((int64Value & bitmask) >> offset);

      if (mappedCode >= 1 && mappedCode <= 26) {
        unmappedCode = (byte)(mappedCode | 64);
      } else if (mappedCode == 27) {
        unmappedCode = (byte)196; // Ä (Windows-1252 aka ANSI)
      } else if (mappedCode == 28) {
        unmappedCode = (byte)214; // Ö (Windows-1252 aka ANSI)
      } else if (mappedCode == 29) {
        unmappedCode = (byte)220; // Ü (Windows-1252 aka ANSI)
      } else if (mappedCode == 30) {
        unmappedCode = (byte)223; // ß (Windows-1252 aka ANSI)
      } else if (mappedCode == 31) {
        unmappedCode = (byte)46; // .
      } else {
        unmappedCode = (byte)32; // Space
      }
      decoded[i] = (char)(unmappedCode & 0xFF);
      bitmask <<= 5;
      offset += 5;
    }

    return new String(decoded).replaceAll("\\s+$", "").replace(' ', '_');
  }

  /**
   * Transforms a string containing separate words into a PascalCase formatted string.
   * E.g. "HELLO_WORLD" becomes "HelloWorld"
  */
  public static String rawTokenToPascalCase(String rawString) {

    if (rawString == null)
      return null;

    if (rawString == "")
      return "";

    String pascalCaseString = "";
    boolean nextOneUp = true; // Der Erste immer Groß

    for (int i = 0; i <= (rawString.length() - 1); i++) {

      char c = rawString.charAt(i);

      boolean isLetter = (Character.toLowerCase(c) != Character.toUpperCase(c));

      if (!isLetter) {
        nextOneUp = true;
        continue;
      }

      if (nextOneUp) {
        c = Character.toUpperCase(c);
      } else {
        c = Character.toLowerCase(c);
      }
      pascalCaseString += c;
      nextOneUp = false;
    }
    return pascalCaseString;
  }
}
