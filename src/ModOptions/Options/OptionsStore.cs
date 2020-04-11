using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace ModOptions {

  [PublicAPI]
  public abstract class OptionsStore : IEquatable<OptionsStore>, IComparable<OptionsStore> {

    private static readonly ISet<OptionsStore> RegisteredForGui
      = new HashSet<OptionsStore>();

    private static readonly ICollection<OptionsStore> RegisteredForGuiSequence
      = new LinkedList<OptionsStore>();

    public static bool RegisterForGui(OptionsStore options) {
      if (!RegisteredForGui.Add(options))
        return false;

      RegisteredForGuiSequence.Add(options);
      return true;
    }

    internal static IEnumerable<OptionsStore> GetRegisteredForGui() {
      foreach (var options in RegisteredForGuiSequence)
        yield return options;
    }

    public abstract string Name { get; }

    public object GetMetadata(Option option, string metadataType)
      => GetOptionMetadata(option.Namespace, option.Name, metadataType);

    public abstract object GetOptionMetadata(string ns, string key, string metadataType);

    public abstract IEnumerable<Option> GetKnownOptions();

    public abstract void Save();

    public abstract void Set<T>(string key, T value);

    public abstract void Set<T>(string ns, string key, T value);

    public abstract T Get<T>(string key);

    public abstract T Get<T>(string ns, string key);

    public OptionNamespace GetNamespace(string ns)
      => new OptionNamespace(this, ns);

    public Option<TOption> GetOption<TOption>([NotNull] string key) where TOption : unmanaged, IEquatable<TOption>
      => GetOption<TOption>(null, key);

    public Option<TOption> GetOption<TOption>([CanBeNull] string ns, [NotNull] string key) where TOption : unmanaged, IEquatable<TOption>
      => new Option<TOption>(this, ns, key);

    public abstract bool Equals(OptionsStore other);

    public abstract int CompareTo(OptionsStore other);

    public static bool operator <(OptionsStore left, OptionsStore right)
      => left.CompareTo(right) < 0;

    public static bool operator >(OptionsStore left, OptionsStore right)
      => left.CompareTo(right) > 0;

    public static bool operator <=(OptionsStore left, OptionsStore right)
      => left.CompareTo(right) <= 0;

    public static bool operator >=(OptionsStore left, OptionsStore right)
      => left.CompareTo(right) >= 0;

  }

}