// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Bonebrake.Monads.Static.Benchmarks;

BenchmarkRunner.Run<MonadBenchmarks>();