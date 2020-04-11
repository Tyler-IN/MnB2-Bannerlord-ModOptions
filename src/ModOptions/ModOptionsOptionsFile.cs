using System.Collections.Generic;

namespace ModOptions {

  public sealed class ModOptionsOptionsFile : OptionsFile {

    public override string Name => nameof(ModOptions);

    public ModOptionsOptionsFile() : base(nameof(ModOptions) + ".txt") {
    }

  }

}