namespace System.Text.SmartStandards {

  public static class EncodedTokenExtensions {

    public static long EncodeTokenToId(this string extendee) {
      string internalRepresentation = PascalCasingUtil.SplitFromPascalCase(extendee, '_', true);
      return TextToIntegerCodec.ToInt64(internalRepresentation);
    }

    public static string DecodeIdToToken(this long int64Value) {
      string internalRepresentation = TextToIntegerCodec.FromInt64(int64Value);
      return PascalCasingUtil.ToPascalCase(internalRepresentation);
    }

  }

}
