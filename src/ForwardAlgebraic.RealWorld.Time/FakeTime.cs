using ForwardAlgebraic.RealWorld.Abstractions;

namespace ForwardAlgebraic.RealWorld.Time;

public readonly record struct FakeEffectTime(DateTime FakeNow) : IEffectTime
{
    public DateTime Now => FakeNow;
    public DateTime UtcNow => FakeNow.ToUniversalTime();
}
