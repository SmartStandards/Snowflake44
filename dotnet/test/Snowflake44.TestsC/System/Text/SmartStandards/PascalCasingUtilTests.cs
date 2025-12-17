using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Text.SmartStandards {

  [TestClass()]
  public class PascalCasingUtilTests {

    [TestMethod()]
    public void ToPascalCase_BorderCases_BehaveCorrectly() {
      string actual;

      actual = PascalCasingUtil.ToPascalCase(null);
      Assert.IsNull(actual);

      actual = PascalCasingUtil.ToPascalCase("");
      Assert.AreEqual("", actual);

      actual = PascalCasingUtil.ToPascalCase("_");
      Assert.AreEqual("", actual);

      actual = PascalCasingUtil.ToPascalCase(@"ÄÖÜß1234567890!§$%&/()=?\+-#'`´,.;:^°<>|@²³");

      actual = PascalCasingUtil.ToPascalCase(@"A2B");

      actual = PascalCasingUtil.ToPascalCase("A");
      Assert.AreEqual("A", actual);

      actual = PascalCasingUtil.ToPascalCase("a");
      Assert.AreEqual("A", actual);

      actual = PascalCasingUtil.ToPascalCase("_A");
      Assert.AreEqual("A", actual);

      actual = PascalCasingUtil.ToPascalCase("A_");
      Assert.AreEqual("A", actual);

      actual = PascalCasingUtil.ToPascalCase(" A");
      Assert.AreEqual("A", actual);

      actual = PascalCasingUtil.ToPascalCase("A ");
      Assert.AreEqual("A", actual);

      actual = PascalCasingUtil.ToPascalCase("aA");
      Assert.AreEqual("Aa", actual);

      actual = PascalCasingUtil.ToPascalCase("A A");
      Assert.AreEqual("AA", actual);

      actual = PascalCasingUtil.ToPascalCase("a A");
      Assert.AreEqual("AA", actual);

      actual = PascalCasingUtil.ToPascalCase("A a");
      Assert.AreEqual("AA", actual);

      actual = PascalCasingUtil.ToPascalCase("A_A");
      Assert.AreEqual("AA", actual);

      actual = PascalCasingUtil.ToPascalCase("A_A_");
      Assert.AreEqual("AA", actual);

      actual = PascalCasingUtil.ToPascalCase("hello42world_DuPont GmbH  Zwei__Trenner"); // Ja, aus "DuPont" wird "Dupont"!
      Assert.AreEqual("Hello42worldDupontGmbhZweiTrenner", actual);

      actual = PascalCasingUtil.ToPascalCase("Alles.außer-Buchstaben+und#Ziffern123456789@fliegt,raus!");
      Assert.AreEqual("AllesAußerBuchstabenUndZiffern123456789FliegtRaus", actual);

      // Parameter-Variationen

      actual = PascalCasingUtil.ToPascalCase("hällo_DuPont GmbH  Ätzend-+Trennerß DeVolo", new[] { "DuPont", "DeVolo" }, true);
      Assert.AreEqual("HaelloDuPontGmbhAetzendTrennerssDeVolo", actual);
    }

    [TestMethod()]
    public void ToPascalCase_GermanUmlauts_BehaveCorrectly() {
      string actual;

      actual = PascalCasingUtil.ToPascalCase("Ä");
      Assert.AreEqual("Ä", actual);

      actual = PascalCasingUtil.ToPascalCase("Ää");
      Assert.AreEqual("Ää", actual);

      actual = PascalCasingUtil.ToPascalCase("äÄ");
      Assert.AreEqual("Ää", actual);

      actual = PascalCasingUtil.ToPascalCase("Ö");
      Assert.AreEqual("Ö", actual);

      actual = PascalCasingUtil.ToPascalCase("Ü");
      Assert.AreEqual("Ü", actual);

      actual = PascalCasingUtil.ToPascalCase("ä");
      Assert.AreEqual("Ä", actual);

      actual = PascalCasingUtil.ToPascalCase("ö");
      Assert.AreEqual("Ö", actual);

      actual = PascalCasingUtil.ToPascalCase("ü");
      Assert.AreEqual("Ü", actual);

      actual = PascalCasingUtil.ToPascalCase("ß");
      Assert.AreEqual("ß", actual);

    }

    [TestMethod()]
    public void SplitFromPascalCaseTests() {
      string actual;

      // Default-Verhalten


      actual = PascalCasingUtil.SplitFromPascalCase("HelloGmbhTheAnswerIsValue42");



      actual = PascalCasingUtil.SplitFromPascalCase(null);
      Assert.IsNull(actual);

      actual = PascalCasingUtil.SplitFromPascalCase("");
      Assert.AreEqual("", actual);

      actual = PascalCasingUtil.SplitFromPascalCase("A");
      Assert.AreEqual("A", actual);

      actual = PascalCasingUtil.SplitFromPascalCase("a");
      Assert.AreEqual("a", actual);

      actual = PascalCasingUtil.SplitFromPascalCase("Aa");
      Assert.AreEqual("Aa", actual);

      actual = PascalCasingUtil.SplitFromPascalCase("aaA");
      Assert.AreEqual("aa A", actual);

      actual = PascalCasingUtil.SplitFromPascalCase("HalloWeltBlah");
      Assert.AreEqual("Hallo Welt Blah", actual);

      actual = PascalCasingUtil.SplitFromPascalCase("Hallo die_Welt camelCamel PascalPascal  Das waren vorher schon 2 Leerzeichen.");
      Assert.AreEqual("Hallo die_ Welt camel Camel Pascal Pascal  Das waren vorher schon 2 Leerzeichen.", actual);

      // Parameter "separator", "toUppercase"

      actual = PascalCasingUtil.SplitFromPascalCase("A", '_', true);
      Assert.AreEqual("A", actual);

      actual = PascalCasingUtil.SplitFromPascalCase("a", '_', true);
      Assert.AreEqual("A", actual);

      actual = PascalCasingUtil.SplitFromPascalCase("ä", '_', true);
      Assert.AreEqual("Ä", actual);

      actual = PascalCasingUtil.SplitFromPascalCase("Aa", '_', true);
      Assert.AreEqual("AA", actual);

      actual = PascalCasingUtil.SplitFromPascalCase("aaA", '_', true);
      Assert.AreEqual("AA_A", actual);

      actual = PascalCasingUtil.SplitFromPascalCase("AA", '_', true);
      Assert.AreEqual("A_A", actual);

      actual = PascalCasingUtil.SplitFromPascalCase("AAA", '_', true);
      Assert.AreEqual("A_A_A", actual);

      actual = PascalCasingUtil.SplitFromPascalCase("HalloWeltBlah", '_', true);
      Assert.AreEqual("HALLO_WELT_BLAH", actual);

      // Parameter "keepPascalCaseFor"

      actual = PascalCasingUtil.SplitFromPascalCase("HaelloDuPontGmbhAetzendTrennerssDeVolo", default, false, new[] { "DuPont", "DeVolo" });
      Assert.AreEqual("Haello DuPont Gmbh Aetzend Trennerss DeVolo", actual);
    }

  }

}
