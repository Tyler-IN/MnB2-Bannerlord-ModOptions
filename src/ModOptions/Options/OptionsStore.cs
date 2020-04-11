using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

    [PublicAPI]
    public object GetMetadata(Option option, string metadataType)
      => GetOptionMetadata(option.Namespace, option.Name, metadataType);

    [PublicAPI]
    public virtual object GetOptionMetadata(string ns, string key, string metadataType) => null;

    [PublicAPI]
    public virtual IEnumerable<Option> GetKnownOptions() {
      foreach (var mi in GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance)) {
        switch (mi) {
          case FieldInfo fi: {
            if (typeof(Option).IsAssignableFrom(fi.FieldType))
              yield return (Option) fi.GetValue(this);

            break;
          }
          case PropertyInfo pi: {
            if (typeof(Option).IsAssignableFrom(pi.PropertyType))
              yield return (Option) pi.GetValue(this);

            break;
          }
        }
      }
    }

    [PublicAPI]
    public abstract void Save();

    [PublicAPI]
    public abstract void Set<T>(string key, T value);

    [PublicAPI]
    public abstract void Set<T>(string ns, string key, T value);

    [PublicAPI]
    public abstract T Get<T>(string key);

    [PublicAPI]
    public abstract T Get<T>(string ns, string key);

    [PublicAPI]
    public OptionNamespace GetNamespace(string ns)
      => new OptionNamespace(this, ns);

    [PublicAPI]
    public Option<TOption> GetOption<TOption>([NotNull] string key) where TOption : unmanaged, IEquatable<TOption>
      => GetOption<TOption>(null, key);

    [PublicAPI]
    public Option<TOption> GetOption<TOption>([CanBeNull] string ns, [NotNull] string key) where TOption : unmanaged, IEquatable<TOption>
      => new Option<TOption>(this, ns, key);

    [PublicAPI]
    public StringOption GetStringOption([NotNull] string key)
      => GetStringOption(null, key);

    [PublicAPI]
    public StringOption GetStringOption([CanBeNull] string ns, [NotNull] string key)
      => new StringOption(this, ns, key);

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