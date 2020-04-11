#if EXAMPLE
namespace ModOptions {
  public sealed class ModOptionsOptionsFile : OptionsFile {
    public override string Name => nameof(ModOptions);
    public ModOptionsOptionsFile() : base(nameof(ModOptions) + ".txt") {}
    public StringOption ExampleStringOption;
    public Option<float> ExampleSliderOption;
  }
}
#endif