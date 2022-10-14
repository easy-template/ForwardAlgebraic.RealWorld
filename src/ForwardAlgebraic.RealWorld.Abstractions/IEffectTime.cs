namespace ForwardAlgebraic.RealWorld.Abstractions;

public interface IEffectTime : IDisposable
{
    DateTime Now { get; }
    DateTime UtcNow { get; }

    void IDisposable.Dispose() => GC.SuppressFinalize(this);
}
