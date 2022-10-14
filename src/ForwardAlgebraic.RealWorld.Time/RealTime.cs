using ForwardAlgebraic.RealWorld.Abstractions;
using LanguageExt.Effects.Traits;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ForwardAlgebraic.RealWorld.Time;

public readonly struct RealEffectTime : IEffectTime
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}

public static class HostExtension
{
    public static IHostBuilder UseEffectTime(this IHostBuilder host) =>
        UseEffectTime(host, () => new RealEffectTime());

    public static IHostBuilder UseEffectTime(this IHostBuilder host, Func<IEffectTime> factory)
    {
        host.ConfigureServices((ctx, services) =>
        {
            services.AddSingleton<Func<IEffectTime>>(factory);
        });

        return host;
    }        

}
