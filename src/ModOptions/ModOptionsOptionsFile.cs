using System.Collections.Generic;

namespace ModOptions {

  public sealed class ModOptionsOptionsFile : OptionsFile {

    public override string Name => nameof(ModOptions);

    public ModOptionsOptionsFile(string fileName) : base(fileName) {
    }

    // essentially this is the option file's schema
    public static readonly Dictionary<string, object> Metadata
      = new Dictionary<string, object> {
      };

    public override object GetOptionMetadata(string ns, string key, string metadataType)
      => Metadata.TryGetValue(ns == null ? $"{key}.{metadataType}" : $"{ns}:{key}.{metadataType}", out var result) ? result : null;

    public override IEnumerable<Option> GetKnownOptions() {
      yield break;
    }

  }

}