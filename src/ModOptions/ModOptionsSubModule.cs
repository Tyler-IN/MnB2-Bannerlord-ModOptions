using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;
using HarmonyLib;
using JetBrains.Annotations;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using Module = TaleWorlds.MountAndBlade.Module;

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