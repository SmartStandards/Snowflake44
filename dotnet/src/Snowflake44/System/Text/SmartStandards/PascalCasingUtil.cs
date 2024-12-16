namespace System.Text.SmartStandards {

  public class PascalCasingUtil {

    /// <summary>
    ///   Transforms a string containing separate words into a PascalCase formatted string.
    /// </summary>
    /// <param name="originalString"> The string to be pascalized. </param>
    /// <param name="keepCasingFor"> 
    ///   Array of exceptional words, the casing of which should be preserved (e.g. company or product names).
    ///   Example: Normally "Hello ExampleCompany" would be transformed to "HelloExamplecompany". By passing "ExampleCompany" as exception,
    ///   the result would be "HelloExampleCompany".
    /// </param>
    /// <param name="replaceUmlauts"> 
    ///   If true, german umlauts will be replaced by regular (ASCII) characters:
    ///   ä to ae, ö to oe, ü to ue ß to ss.
    /// </param>
    /// <returns> A pascal case formatted string. </returns>
    public static string ToPascalCase(string originalString, string[] keepCasingFor = null, bool replaceUmlauts = false) {

      if (originalString == null) return null;

      if (originalString == "") return "";

      StringBuilder sb = new StringBuilder(originalString.Length * 2);

      if (keepCasingFor != null) {
        sb.Append(originalString);
        foreach (string caseToKeep in keepCasingFor)
          sb.Replace(caseToKeep, SplitFromPascalCase(caseToKeep));
        originalString = sb.ToString();
        sb.Clear();
      }

      bool nextOneUp = true; // Der Erste immer Groß
      char umlautSurrogate = default;

      for (int i = 0; i <= (originalString.Length - 1); i++) {
        char c = originalString[i];
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
        umlautSurrogate = default;
      }
      return sb.ToString();
    }

    /// <summary>
    ///   Transforms a PascalCase string into a string with separated words.
    ///  </summary>
    /// <param name="camelOrPascalCaseString">
    ///   Must not contain any spaces or underscores. Null will return null, empty string will return empty string.
    /// </param>
    /// <param name="separator"> Will be used to separate words. Default: Space.</param>
    /// <param name="toUppercase"> If true, output will be uppercase. </param>
    /// <param name="keepPascalCaseFor"></param>
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

      if (toUppercase) {
        sb.Append(char.ToUpper(camelOrPascalCaseString[0]));
      } else {
        sb.Append(camelOrPascalCaseString[0]);
      }

      for (int i = 1; i <= (camelOrPascalCaseString.Length - 1); i++) {
        char c = camelOrPascalCaseString[i];

        if ((char.IsUpper(c) && camelOrPascalCaseString[i - 1] != separator)) sb.Append(separator);

        if (toUppercase) {
          sb.Append(char.ToUpper(c));
        } else {
          sb.Append(c);
        }
      }

      // Post-processing: "Repair" unwanted split results by replacing the split varinat by the original variant

      if (keepPascalCaseFor != null) {
        foreach (string caseToForce in keepPascalCaseFor) {
          sb.Replace(SplitFromPascalCase(caseToForce, separator, toUppercase), caseToForce);
        }
      }

      return sb.ToString();
    }
  }
}

