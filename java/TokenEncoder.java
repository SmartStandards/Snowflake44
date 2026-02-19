public class TokenEncoder {

  // CREDITS: http://vodi.de for https://github.com/SmartStandards/Snowflake44

  /**
   * Decodes a long to a token string (of maximum 12 characters).
   * The String will be PascalCase (e.g. "HelloWorld")
  */
  public static String decode(long int64Value) throws Exception {
    String rawToken = TokenEncoder.int64ToRaw(int64Value);
    String token = TokenEncoder.rawToReadable(rawToken);
    return token;
  }

  public static long encode(String token) throws Exception {
    String rawToken = TokenEncoder.readableToRaw(token);
    return TokenEncoder.rawToInt64(rawToken);
  }

  /**
   * Decodes a long to a raw token string (of maximum 12 characters).
   * The String will be CAPITALS and underscores (e.g. "HELLO_WORLD")
  */
  public static String int64ToRaw(long int64Value) throws Exception {
    long bitmask = 31;
    int offset = 0;
    byte mappedCode;
    byte unmappedCode;
    char[]decoded = new char[12];

    if (int64Value < 0) throw new Exception("Negative values like " + int64Value + " are not supported!");

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
      decoded[i] = (char)(unmappedCode & 0xFF); // & 0xFF ist the conversion from signed byte to unsigned
      bitmask <<= 5;
      offset += 5;
    }

    return new String(decoded).stripTrailing().replace(' ', '_');
  }

  public static long rawToInt64(String rawToken) throws Exception { 

    if (rawToken.length() > 12) {
      throw new Exception("Maximum length of 12 characters was exceeded by \"" + rawToken + "\"!");
    }

    if (rawToken.endsWith("_")) {
      throw new Exception("String must not end with underscore: \"" + rawToken + "\"!");
    }

    String toEncode = TokenEncoder.toUpperCaseFixed(rawToken);

    if (toEncode.length() < 12) toEncode = TokenEncoder.padRight(toEncode,12, '_');

    long encodedValue = 0;
    int offset = 0;
    long mappedCode;

    for (var i = 0; i <= 11; i++) {

      mappedCode = TokenEncoder.getMappedCode(toEncode.charAt(i));

      if (mappedCode > 31) {
        throw new Exception("Unsupported Character '" + toEncode.charAt(i) + "' in \"" + rawToken + "\"!");
      }
      encodedValue |= (mappedCode << offset);
      offset += 5;
    }
    return encodedValue;
  }

  public static String readableToRaw(String token) throws Exception {

    if ((token) == null) return null;

      if ((token == "")) return "";

      char digitSurrogate = ' ';
      char escapedChar = ' ';
      boolean withinEscapeSequence = false;
      boolean upperCaseExpected = true;

      StringBuilder sb = new StringBuilder(token.length() * 2);

      for (int i = 0; i <= (token.length() - 1); i++) {

        char c = token.charAt(i);

        digitSurrogate = ' ';
        escapedChar = ' ';

        if (c == '1') { digitSurrogate = 'I'; }
        if (c == '2') { digitSurrogate = 'Z'; }
        if (c == '3') { digitSurrogate = 'E'; }
        if (c == '4') { digitSurrogate = 'H'; }
        if (c == '5') { digitSurrogate = 'S'; }
        if (c == '6') { digitSurrogate = 'G'; }
        if (c == '7') { digitSurrogate = 'L'; }
        if (c == '8') { digitSurrogate = 'B'; }
        if (c == '9') { digitSurrogate = 'P'; }
        if (c == '0') { digitSurrogate = 'O'; }

        if (c == '#') { escapedChar = '#'; }
        if (c == '+') { escapedChar = 'A'; }
        if (c == ',') { escapedChar = 'C'; }
        if (c == '.') { escapedChar = 'D'; }
        if (c == '?') { escapedChar = 'F'; }
        if (c == '(') { escapedChar = 'J'; }
        if (c == ')') { escapedChar = 'K'; }
        if (c == '-') { escapedChar = 'M'; }
        if (c == '&') { escapedChar = 'N'; }
        if (c == '@') { escapedChar = 'Q'; }
        if (c == ':') { escapedChar = 'R'; }
        if (c == '\'') { escapedChar = 'T'; }
        if (c == '\\') { escapedChar = 'U'; }
        if (c == '/') { escapedChar = 'V'; }
        if (c == '=') { escapedChar = 'W'; }
        if (c == '*') { escapedChar = 'X'; }
        if (c == '!') { escapedChar = 'Y'; }
        if (c == ']') { escapedChar = 'Ä'; }
        if (c == '[') { escapedChar = 'Ö'; }
        if (c == '%') { escapedChar = 'Ü'; }
        if (c == '^') { escapedChar = 'ß'; }

        // Terminate escaping state

        if (withinEscapeSequence && digitSurrogate == ' ' && (escapedChar == ' ' || escapedChar == '_')) {
          withinEscapeSequence = false;
          sb.append('_');
          upperCaseExpected = true;
        }

        if (digitSurrogate != ' ') {
          if (!withinEscapeSequence) sb.append('#');
          withinEscapeSequence = true;
          sb.append(digitSurrogate);
          continue;
        }

        if (escapedChar != ' ') {
          if (!withinEscapeSequence) {
            sb.append('#');
            upperCaseExpected = true;
          }
          sb.append(escapedChar);
          continue;
        }

        char upperC = Character.toUpperCase(c);

        long mappedCode = TokenEncoder.getMappedCode(upperC);

        if (mappedCode > 31) {
          throw new Exception("Unsupported Character '" + c + "' in \"" + token + "\"!");
        }

        if (Character.isUpperCase(c) != upperCaseExpected) sb.append('_');

        sb.append(upperC);

        upperCaseExpected = false;
      } // next

      return sb.toString();
  }

  /**
   * Transforms a string containing separate words into a PascalCase formatted string.
   * E.g. "HELLO_WORLD" becomes "HelloWorld"
  */
  public static String rawToReadable(String rawToken) {

    if (rawToken == null) return null;

      if (rawToken == "") return "";

      StringBuilder sb = new StringBuilder(rawToken.length() * 2);

      boolean nextOneUp = true; // Der Erste immer Groß
      boolean escaping = false;
      boolean withinDigits = false;

      for (int i = 0; i <= (rawToken.length() - 1); i++) {

        char c = rawToken.charAt(i);

        if (withinDigits) {
          if (c == '_') {
            withinDigits = false;
            escaping = false;
            nextOneUp = true;
            continue;
          }
        }

        if (escaping) {
          if (c == 'I') { sb.append('1'); withinDigits = true; continue; }
          if (c == 'Z') { sb.append('2'); withinDigits = true; continue; }
          if (c == 'E') { sb.append('3'); withinDigits = true; continue; }
          if (c == 'H') { sb.append('4'); withinDigits = true; continue; }
          if (c == 'S') { sb.append('5'); withinDigits = true; continue; }
          if (c == 'G') { sb.append('6'); withinDigits = true; continue; }
          if (c == 'L') { sb.append('7'); withinDigits = true; continue; }
          if (c == 'B') { sb.append('8'); withinDigits = true; continue; }
          if (c == 'P') { sb.append('9'); withinDigits = true; continue; }
          if (c == 'O') { sb.append('0'); withinDigits = true; continue; }

          if (c == '#') { sb.append('#'); }
          if (c == 'A') { sb.append('+'); }
          if (c == 'C') { sb.append(','); }
          if (c == 'D') { sb.append('.'); }
          if (c == 'F') { sb.append('?'); }
          if (c == 'J') { sb.append('('); }
          if (c == 'K') { sb.append(')'); }
          if (c == 'M') { sb.append('-'); }
          if (c == 'N') { sb.append('&'); }
          if (c == 'Q') { sb.append('@'); }
          if (c == 'R') { sb.append(':'); }
          if (c == 'T') { sb.append('\''); }
          if (c == 'U') { sb.append('\\'); }
          if (c == 'V') { sb.append('/'); }
          if (c == 'W') { sb.append('='); }
          if (c == 'X') { sb.append('*'); }
          if (c == 'Y') { sb.append('!'); }
          if (c == 'Ä') { sb.append(']'); }
          if (c == 'Ö') { sb.append('['); }
          if (c == 'Ü') { sb.append('%'); }
          if (c == 'ß') { sb.append('^'); }

          if (!withinDigits) {
            escaping = false;
            nextOneUp = true;
          }
          continue;
        }

        if (c == '#') {
          escaping = true;
          continue;
        }

        if (c == '_') {
          nextOneUp = !nextOneUp;
          continue;
        }

        if (nextOneUp) {
          sb.append(Character.toUpperCase(c));
        } else {
          sb.append(Character.toLowerCase(c));
        }

        nextOneUp = false;
      }
      return sb.toString();
  }

  public static long getMappedCode(char c) {

    byte unmappedCode = (byte)c; // byte casting uses ISO 8859-1 (aka Windows-1252) mapping

    // int unsignedRepresentation = unmappedCode & 0xff;

    if (unmappedCode == 95) { // _
      return 0;
    } else if (65 <= unmappedCode && unmappedCode <= 90) { // A-Z
      return (long)(unmappedCode - 64);
    } else if (unmappedCode == 35) { // #
      return 31;
    } else if (unmappedCode == -60) { // Ä,ä (unsigned representation 196)
      return 27;
    } else if (unmappedCode == -42) { // Ö,ö (unsigned representation 214)
      return 28;
    } else if (unmappedCode == -36 ) { // Ü,ü (unsigned representation 220)
      return 29;
    } else if (unmappedCode == -33 ) { // ß (unsigned representation 223)
      return 30;
    } else {
      // System.out.println(c + ": " +  unmappedCode + " " + unsignedRepresentation);
      return 32;
    }
  }

  public static String padRight(String input, int length, char paddingChar) {
    if (input.length() >= length) {
        return input;
    }
    StringBuilder sb = new StringBuilder(input);
    while (sb.length() < length) {
        sb.append(paddingChar);
    }
    return sb.toString();
  }

  public static String toUpperCaseFixed(String incoming){
    char[] chars = incoming.toCharArray();
    for (int i = 0; i < chars.length; i++) {
      chars[i] = Character.toUpperCase(chars[i]);
    }
    return new String(chars);
  } 

}
