// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Security.Cryptography;

var summary = BenchmarkRunner.Run(typeof(Program).Assembly);

[SimpleJob(launchCount: 3, warmupCount: 10, targetCount: 30, invocationCount: 1000)]
[MinColumn, MaxColumn, BaselineColumn]
public class Md5VsSha256
{
    private const int N = 10000;
    private readonly byte[] data;

    private readonly SHA256 sha256 = SHA256.Create();
    private readonly MD5 md5 = MD5.Create();

    public Md5VsSha256()
    {
        data = new byte[N];
        new Random(42).NextBytes(data);
    }

    [Benchmark]
    public byte[] Sha256() => sha256.ComputeHash(data);

    [Benchmark(Baseline = true)]
    public byte[] Md5() => md5.ComputeHash(data);
}