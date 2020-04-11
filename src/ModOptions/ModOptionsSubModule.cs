using HarmonyLib;
using JetBrains.Annotations;
using TaleWorlds.MountAndBlade;

namespace ModOptions {

  [PublicAPI]
  public partial class ModOptionsSubModule : MBSubModuleBase {

    internal static readonly Harmony Harmony = new Harmony(nameof(ModOptions));

    protected override void OnSubModuleLoad() {
      Harmony.PatchAll();
      base.OnSubModuleLoad();
    }

  }

}