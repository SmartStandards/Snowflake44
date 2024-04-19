namespace System.Text.SmartStandards {

  public class PascalCasingUtil {

    public static string ToPascalCase(string extendee, string[] keepCasingFor = null, bool replaceUmlauts = false) {

      if (extendee == null) return null;

      if (extendee == "") return "";

      StringBuilder sb = new(extendee.Length * 2);

      if (keepCasingFor != null) {
        sb.Append(extendee);
        foreach (string caseToKeep in keepCasingFor)
          sb.Replace(caseToKeep, SplitFromPascalCase(caseToKeep));
        extendee = sb.ToString();
        sb.Clear();
      }

      bool nextOneUp = true; // Der Erste immer Groß
      char umlautSurrogate = default(Char);

      for (int i = 0; i <= (extendee.Length - 1); i++) {
        char c = extendee[i];
        if (!char.IsLetterOrDigit(c)) {
          nextOneUp = true;
          continue;
        } else {
          if (replaceUmlauts) {
            switch (c) {
              case 'Ä':
              case 'ä': {
                c = 'a';
                umlautSurrogate = 'e';
                break;
              }

              case 'Ö':
              case 'ö': {
                c = 'o';
                umlautSurrogate = 'e';
                break;
              }

              case 'Ü':
              case 'ü': {
                c = 'u';
                umlautSurrogate = 'e';
                break;
              }

              case 'ß': {
                c = 's';
                umlautSurrogate = 's';
                break;
              }
            }
          }

          if (nextOneUp) {
            sb.Append(char.ToUpperInvariant(c));
          } else {
            sb.Append(char.ToLowerInvariant(c));
          }

          if (umlautSurrogate != default(Char)) {
            sb.Append(umlautSurrogate);
          }
        }
        nextOneUp = false;
        umlautSurrogate = default(Char);
      }
      return sb.ToString();
    }

    /// <summary>
    ///   Wandelt einen PascalCase-String in UPPERCASE_MIT_UNDERSCORES.
    ///  </summary>
    /// <param name="camelOrPascalCaseString">
    ///   Darf keine Leerzeichen oder Underscores enthalten. Nothing gibt Nothing zurück. Leerstring gibt Leerstring zurück.
    /// </param>
    /// <returns> Aus "HalloDuWelt" wird "HALLO_DU_WELT". </returns>
    /// <remarks>
    ///   Jedem Großbuchstaben im Eingangsstring wird ein Separator vorangestellt (außer an Index 0).
    ///   Doppelte Separatoren werden vermieden (außer sie waren schon im Eingangsstring vorhanden).
    /// </remarks>
    public static string SplitFromPascalCase(
      string camelOrPascalCaseString, char separator = ' ', bool toUppercase = false, string[] keepPascalCaseFor = null
    ) {
      if ((camelOrPascalCaseString) == null) return null;

      if ((camelOrPascalCaseString == "")) return "";

      if ((separator == default(Char))) separator = ' ';

      StringBuilder sb = new StringBuilder(camelOrPascalCaseString.Length * 2);
      if (toUppercase)
        sb.Append(char.ToUpper(camelOrPascalCaseString[0]));
      else
        sb.Append(camelOrPascalCaseString[0]);

      for (int i = 1; i <= (camelOrPascalCaseString.Length - 1); i++) {
        char c = camelOrPascalCaseString[i];
        if ((char.IsUpper(c) && camelOrPascalCaseString[i - 1] != separator))
          sb.Append(separator);
        if (toUppercase)
          sb.Append(char.ToUpper(c));
        else
          sb.Append(c);
      }

      if (keepPascalCaseFor != null) {
        foreach (string caseToForce in keepPascalCaseFor) {
          sb.Replace(SplitFromPascalCase(caseToForce, separator, toUppercase), caseToForce);
        }
      }

      return sb.ToString();
    }
  }
}

