public class TokenEncoder {

  // CREDITS: http://vodi.de for https://github.com/SmartStandards/Snowflake44

  /**
   * Decodes a long to a token string (of maximum 12 characters).
   * The String will be PascalCase (e.g. "HelloWorld")
  */
  public static String decodeToken(long int64Value) {
    String rawToken = TokenEncoder.encodedTokenToRawToken(int64Value);
    String token = TokenEncoder.rawTokenToReadableToken(rawToken);
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
        unmappedCode = (byte)35; // #
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
  public static String rawTokenToReadableToken(String rawToken) {

    if (rawToken == null) return null;
    
    if (rawToken == "") return "";

    String pascalCaseString = "";

    boolean nextOneUp = true; // Der Erste immer Groß
    boolean withinDigits = false;

    for (int i = 0; i <= (rawToken.length() - 1); i++) {

      char c = rawToken.charAt(i);

      boolean isLetter = (Character.toLowerCase(c) != Character.toUpperCase(c));

      if (!isLetter) {
        nextOneUp = true;
        withinDigits = (c == '#');
        continue;
      }

      if (withinDigits) {
        if (c == 'I') { pascalCaseString += '1'; continue; }
        if (c == 'Z') { pascalCaseString += '2'; continue; }
        if (c == 'E') { pascalCaseString += '3'; continue; }
        if (c == 'H') { pascalCaseString += '4'; continue; }
        if (c == 'S') { pascalCaseString += '5'; continue; }
        if (c == 'G') { pascalCaseString += '6'; continue; }
        if (c == 'L') { pascalCaseString += '7'; continue; }
        if (c == 'B') { pascalCaseString += '8'; continue; }
        if (c == 'P') { pascalCaseString += '9'; continue; }
        if (c == 'O') { pascalCaseString += '0'; continue; }
        // This would be an unexpected letter as Digit surrogate. We are robust and just continue => print the original letter.
        withinDigits = false;
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
