global using LanguageExt;
global using static LanguageExt.Prelude;
using LanguageExt.Attributes;
using LanguageExt.Effects.Traits;

namespace ForwardAlgebraic.RealWorld.Abstractions;

[Typeclass("*")]
public interface HasEffectTime<RT> : HasCancel<RT>
    where RT : struct, HasEffectTime<RT>
{
    IEffectTime Time { get; }

    Eff<RT, IEffectTime> Eff => Eff<RT, IEffectTime>(static rt => rt.Time);
}
