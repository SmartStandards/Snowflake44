namespace System.Text.SmartStandards {

  public class TextToIntegerCodec {

    // CREDITS: http://vodi.de for https://github.com/SmartStandards/Snowflake44

    /// <summary> Decodes an Integer to a string (of maximum 6 characters). </summary>
    /// <remarks> The String will be pascal case or CAPITALS. </remarks>
    public static string FromInt64(long int64Value) {
      long bitmask = 31;
      int offset = 0;
      byte mappedCode;
      byte unmappedCode;
      char[] decoded = new char[12];

      if ((int64Value < 0)) throw new ArgumentException($"Negative values like {int64Value} are not supported!", nameof(int64Value));

      for (int i = 0; i <= 11; i++) {

        mappedCode = Convert.ToByte((int64Value & bitmask) >> offset);

        switch (mappedCode) {

          case 0: {
            unmappedCode = 32; // Leerzeichen, vorübergehend, damit wir später einfach Trim schreiben können, danach wird's Underscore
            break;
          }
          case object _ when 1 <= mappedCode && mappedCode <= 26: {
            unmappedCode = (byte)(mappedCode | Convert.ToByte(64));
            break;
          }
          case 27: {
            unmappedCode = 196; // Ä
            break;
          }
          case 28: {
            unmappedCode = 214; // Ö
            break;
          }
          case 29: {
            unmappedCode = 220; // Ü
            break;
          }
          case 30: {
            unmappedCode = 223; // ß
            break;
          }
          case 31: {
            unmappedCode = 35; // #
            break;
          }
          default: {
            unmappedCode = 32; // Space
            break;
          }
        }
        decoded[i] = Convert.ToChar(unmappedCode);
        bitmask <<= 5;
        offset += 5;
      }

      return new string(decoded).TrimEnd().Replace(' ', '_'); // 32 => 95
    }

    public static string FromInt32(int int32Value) {
      if (int32Value < 0) int32Value *= -1;
      long int64Value = int32Value;
      return FromInt64(int64Value);
    }

    /// <summary> Encodes an uppercase String (of maximum 12 characters) into a Long. </summary>
    /// <remarks> 
    ///   Allowed Chars: A-Z, ÄÖÜ, dot, underscore (the string must not end with underscore).
    ///   Casing:
    ///   All forms of casing will be lost except CAPITALS.
    ///   The decoder will create Pascal Casing or CAPITALS.
    /// </remarks>
    public static long ToInt64(string rawToken, bool shouldThrowException = true) {

      if (rawToken.Length > 12) {
        if (shouldThrowException) {
          throw new ArgumentException(string.Format("Maximum length of 12 characters was exceeded by \"{0}\"!", rawToken), nameof(rawToken));
        } else {
          return -1;
        }
      }

      if (rawToken.EndsWith("_")) {
        if (shouldThrowException) {
          throw new ArgumentException(string.Format("String must not end with underscore: \"{0}\"!", rawToken), nameof(rawToken));
        } else {
          return -1;
        }
      }

      string toEncode = rawToken.ToUpperInvariant();

      if (toEncode.Length < 12) toEncode = toEncode.PadRight(12, '_');

      ulong encodedValue = 0;
      int offset = 0;
      ulong mappedCode;

      for (int i = 0; i <= 11; i++) {

        mappedCode = GetMappedCode(toEncode[i]);

        if (mappedCode > 31) {
          if (shouldThrowException) {
            throw new ArgumentException($"Unsupported Character '{toEncode[i]}' in \"{rawToken}\"!", nameof(rawToken));
          } else {
            return -1;
          }
        }
        encodedValue |= (mappedCode << offset);
        offset += 5;
      }
      return Convert.ToInt64(encodedValue);
    }

    public static int ToInt32(string stringValue) {
      if ((stringValue.Length > 6)) {
        throw new ArgumentException(string.Format("Maximum length of 6 characters was exceeded by \"{0}\"!", stringValue), nameof(stringValue));
      }
      return Convert.ToInt32(TextToIntegerCodec.ToInt64(stringValue));
    }

    public static ulong GetMappedCode(char c) {

      byte unmappedCode = Convert.ToByte(c); // Convert.ToByte() default is ISO 8859-1 (aka Windows-1252)

      if (unmappedCode == 95) { // _
        return 0;
      } else if (65 <= unmappedCode && unmappedCode <= 90) { // A-Z
        return (ulong)(unmappedCode - 64);
      } else if (unmappedCode == 35) { // #
        return 31;
      } else if (unmappedCode == 196) { // Ä,ä
        return 27;
      } else if (unmappedCode == 214) { // Ö,ö
        return 28;
      } else if (unmappedCode == 220) { // Ü,ü
        return 29;
      } else if (unmappedCode == 223) { // ß
        return 30;
      } else {
        return 32;
      }
    }

  }
}
