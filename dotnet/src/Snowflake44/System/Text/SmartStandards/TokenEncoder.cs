namespace System.Text.SmartStandards {

  public static class TokenEncoder {

    /// <summary>
    ///   Creates an int64 ID from a textual token.
    /// </summary>
    /// <param name="token">
    ///   A string following these conventions:
    ///   Only letters (no digits).
    ///   True PascalCase (first letter uppercase, no spaces, underscores, hyphens, etc.)
    ///   Max. 12 characters (each uppercase character occupies 2 places).    ///   
    /// </param>
    /// <returns> An int64 ID. </returns>
    public static long Encode(string token) {
      string internalRepresentation = PascalCasingUtil.SplitFromPascalCase(token, '_', true);
      return TextToIntegerCodec.ToInt64(internalRepresentation);
    }

    /// <summary>
    ///   Decodes a textual token back from an encoded int64 ID.
    /// </summary>
    /// <param name="id"> An int64 ID that has been created from a textual token before. </param>
    /// <returns> The textual token. </returns>
    public static string Decode(long id) {
      string internalRepresentation = TextToIntegerCodec.FromInt64(id);
      return PascalCasingUtil.ToPascalCase(internalRepresentation);
    }

  }

}

