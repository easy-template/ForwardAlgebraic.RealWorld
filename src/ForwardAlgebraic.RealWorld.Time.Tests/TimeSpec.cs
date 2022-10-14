using ForwardAlgebraic.RealWorld.Abstractions;
using LanguageExt;
using LanguageExt.UnitsOfMeasure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ForwardAlgebraic.RealWorld.Time.Tests;

public class TimeSpec
{
    [Fact]
    public void CaseNow()
    {
        var now = DateTime.Now;

        var q = from __ in unitEff
                from _1 in Time<RT>.Now
                select _1;

        using var time = new FakeEffectTime(now);
        using var cts = new CancellationTokenSource();

        var r = q.Run(new(time, cts));

        Assert.Equal(now, r.ThrowIfFail());
    }

    [Fact]
    public async Task CaseGenericHostNow()
    {
        var now = DateTime.Now;

        var host = Host.CreateDefaultBuilder()
                       .UseEffectTime(() => new FakeEffectTime(now))
                       .Build();

        await host.StartAsync();

        var q = from __ in unitEff
                from _1 in Time<RT>.Now
                select _1;

        using var cts = new CancellationTokenSource();
        using var time = host.Services.GetRequiredService<Func<IEffectTime>>()();

        var r = q.Run(new(time, cts));

        Assert.Equal(now, r.ThrowIfFail());

        await host.StopAsync();
    }


    public readonly record struct RT(in IEffectTime Time,
                                     CancellationTokenSource CancellationTokenSource) :
        HasEffectTime<RT>
    {

        public RT LocalCancel => this;
        public CancellationToken CancellationToken => CancellationTokenSource.Token;
    }
}
