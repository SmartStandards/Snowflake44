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
      bool withinDigits = false;

      for (int i = 0; i <= (rawToken.Length - 1); i++) {

        char c = rawToken[i];

        if (!char.IsLetterOrDigit(c)) {
          nextOneUp = true;
          withinDigits = (c == '#');
          continue;
        }

        if (withinDigits) {
          if (c == 'I') { sb.Append('1'); continue; }
          if (c == 'Z') { sb.Append('2'); continue; }
          if (c == 'E') { sb.Append('3'); continue; }
          if (c == 'H') { sb.Append('4'); continue; }
          if (c == 'S') { sb.Append('5'); continue; }
          if (c == 'G') { sb.Append('6'); continue; }
          if (c == 'L') { sb.Append('7'); continue; }
          if (c == 'B') { sb.Append('8'); continue; }
          if (c == 'P') { sb.Append('9'); continue; }
          if (c == 'O') { sb.Append('0'); continue; }
          // This would be an unexpected letter as Digit surrogate. We are robust and just continue => print the original letter.
          withinDigits = false;
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

      char separator = '_';

      char digitSurrogate = ' ';
      bool withinDigits = false;

      StringBuilder sb = new StringBuilder(token.Length * 2);

      for (int i = 0; i <= (token.Length - 1); i++) {

        char c = token[i];

        digitSurrogate = ' ';

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

        if (digitSurrogate != ' ') {
          if (!withinDigits) sb.Append('#');
          withinDigits = true;
          sb.Append(digitSurrogate);
          continue;
        }

        if ((i > 0) && ((char.IsUpper(c) || withinDigits) && token[i - 1] != separator)) sb.Append(separator);

        sb.Append(char.ToUpper(c));

        withinDigits = false;
      }

      return sb.ToString();
    }

  }

}

