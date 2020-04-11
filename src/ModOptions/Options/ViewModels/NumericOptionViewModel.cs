using TaleWorlds.Engine.Options;
using TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions;

namespace ModOptions {

  public abstract class NumericOptionViewModel : NumericOptionDataVM {

    protected NumericOptionViewModel(string optionNameId, OptionsVM optsVm, INumericOptionData data)
      : base(optsVm, data,
        optionNameId.Localized(),
        $"{optionNameId}Description".Localized(true)) {
    }

  }

}