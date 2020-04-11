using System.Text.RegularExpressions;
using JetBrains.Annotations;
using TaleWorlds.Localization;

namespace ModOptions {

  [PublicAPI]
  public static class LocalizationHelpers {

    [PublicAPI]
    public static TextObject Localized(this string id, bool blankIsDefault = false)
      => id.Localized(blankIsDefault ? "" : id.ToSentenceCase());

    [PublicAPI]
    public static TextObject Localized(this string id, TextObject fallback) {
      var localized = id.Localized();
      return localized.ToString() == "" ? fallback : localized;
    }

    [PublicAPI]
    public static TextObject Localized(this string id, string fallback)
      => new TextObject($"{{={id}}}{LocalizedTextManager.GetTranslatedText("English", id) ?? fallback}");

    [PublicAPI]
    public static string ToSentenceCase(this string str)
      => Regex.Replace(str, "[a-z][A-Z]", m => $"{m.Value[0]} {char.ToLower(m.Value[1])}");

    [PublicAPI]
    public static string ToTitleCase(this string str)
      => Regex.Replace(str, "[a-z][A-Z]", m => $"{m.Value[0]} {char.ToLower(m.Value[1])}");

  }

}