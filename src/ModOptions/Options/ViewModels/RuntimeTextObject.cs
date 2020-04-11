using System;
using TaleWorlds.Localization;

namespace ModOptions {

  public sealed class RuntimeTextObject : TextObject {

    private readonly Func<string> _resolver;

    public RuntimeTextObject(Func<string> resolve)
      => _resolver = resolve;

    public override string ToString()
      => _resolver();

  }

}