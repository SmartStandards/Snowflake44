namespace System.Text.SmartStandards {

  public static class TokenEncoder {

    /// <summary>
    ///   Creates an int64 ID from a textual token.
    /// </summary>
    /// <param name="token">
    ///   A string following these conventions:
    ///   Allowed characters:  Letters, german umlauts (äöüß), dot. No digits, no spaces, nothing else.
    ///   True PascalCase (first letter uppercase)
    ///   Max. 12 characters (each uppercase character occupies 2 places). 
    /// </param>
    /// <returns> An int64 ID. </returns>
    public static long Encode(string token) {
      string rawRepresentation = ReadableToRaw(token);
      return TextToIntegerCodec.ToInt64(rawRepresentation);
    }

    /// <summary>
    ///   Decodes a textual token back from an encoded int64 ID.
    /// </summary>
    /// <param name="id"> An int64 ID that has been created from a textual token before. </param>
    /// <returns> The textual token. </returns>
    public static string Decode(long id) {
      string rawRepresentation = TextToIntegerCodec.FromInt64(id);
      return RawToReadable(rawRepresentation);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rawToken"></param>
    /// <returns></returns>
    /// <remarks>
    ///   Digits are mapped like this:
    ///   1234567890
    ///   IZEHSGLBPO
    /// </remarks>
    public static string RawToReadable(string rawToken) {

      if (rawToken == null) return null;

      if (rawToken == "") return "";

      StringBuilder sb = new StringBuilder(rawToken.Length * 2);

      bool nextOneUp = true; // Der Erste immer Groß
      bool escaping = false;
      bool withinDigits = false;

      for (int i = 0; i <= (rawToken.Length - 1); i++) {

        char c = rawToken[i];

        if (withinDigits) {
          if (c == '_') {
            withinDigits = false;
            escaping = false;
            nextOneUp = true;
            continue;
          }
        }

        if (escaping) {
          if (c == 'I') { sb.Append('1'); withinDigits = true; continue; }
          if (c == 'Z') { sb.Append('2'); withinDigits = true; continue; }
          if (c == 'E') { sb.Append('3'); withinDigits = true; continue; }
          if (c == 'H') { sb.Append('4'); withinDigits = true; continue; }
          if (c == 'S') { sb.Append('5'); withinDigits = true; continue; }
          if (c == 'G') { sb.Append('6'); withinDigits = true; continue; }
          if (c == 'L') { sb.Append('7'); withinDigits = true; continue; }
          if (c == 'B') { sb.Append('8'); withinDigits = true; continue; }
          if (c == 'P') { sb.Append('9'); withinDigits = true; continue; }
          if (c == 'O') { sb.Append('0'); withinDigits = true; continue; }

          if (c == '#') { sb.Append('#'); }
          if (c == 'A') { sb.Append('+'); }
          if (c == 'C') { sb.Append(','); }
          if (c == 'D') { sb.Append('.'); }
          if (c == 'F') { sb.Append('?'); }
          if (c == 'J') { sb.Append('('); }
          if (c == 'K') { sb.Append(')'); }
          if (c == 'M') { sb.Append('-'); }
          if (c == 'N') { sb.Append('&'); }
          if (c == 'Q') { sb.Append('@'); }
          if (c == 'R') { sb.Append(':'); }
          if (c == 'T') { sb.Append('\''); }
          if (c == 'U') { sb.Append('\\'); }
          if (c == 'V') { sb.Append('/'); }
          if (c == 'W') { sb.Append('='); }
          if (c == 'X') { sb.Append('*'); }
          if (c == 'Y') { sb.Append('!'); }
          if (c == 'Ä') { sb.Append(']'); }
          if (c == 'Ö') { sb.Append('['); }
          if (c == 'Ü') { sb.Append('%'); }
          if (c == 'ß') { sb.Append('^'); }

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
          sb.Append(char.ToUpperInvariant(c));
        } else {
          sb.Append(char.ToLowerInvariant(c));
        }

        nextOneUp = false;
      }
      return sb.ToString();
    }

    public static string ReadableToRaw(string token) {

      if ((token) == null) return null;

      if ((token == "")) return "";

      char digitSurrogate = ' ';
      char escapedChar = ' ';
      bool withinEscapeSequence = false;
      bool upperCaseExpected = true;

      StringBuilder sb = new StringBuilder(token.Length * 2);

      for (int i = 0; i <= (token.Length - 1); i++) {

        char c = token[i];

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
          sb.Append('_');
          upperCaseExpected = true;
        }

        if (digitSurrogate != ' ') {
          if (!withinEscapeSequence) sb.Append('#');
          withinEscapeSequence = true;
          sb.Append(digitSurrogate);
          continue;
        }

        if (escapedChar != ' ') {
          if (!withinEscapeSequence) {
            sb.Append('#');
            upperCaseExpected = true;
          }
          sb.Append(escapedChar);
          continue;
        }

        char upperC = char.ToUpper(c);

        ulong mappedCode = TextToIntegerCodec.GetMappedCode(upperC);

        if (mappedCode > 31) {
          throw new ArgumentException($"Unsupported Character '{c}' in \"{token}\"!", nameof(token));
        }

        if (char.IsUpper(c) != upperCaseExpected) sb.Append('_');

        sb.Append(upperC);

        upperCaseExpected = false;
      } // next

      return sb.ToString();
    }

  }

}

