```

BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5247/22H2/2022Update)
11th Gen Intel Core i7-1165G7 2.80GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.400
  [Host]     : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-TYVRNG : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

IterationCount=5  LaunchCount=1  

```
| Method       | Mean       | Error      | StdDev    | Gen0   | Allocated |
|------------- |-----------:|-----------:|----------:|-------:|----------:|
| NpgslInsert  |   780.0 μs |   151.6 μs |  23.45 μs |      - |   4.71 KB |
| NpgslSelect  |   900.6 μs |   660.0 μs | 171.40 μs |      - |   3.76 KB |
| DapperInsert |   915.2 μs |   370.6 μs |  96.24 μs |      - |   5.01 KB |
| DapperSelect |   927.1 μs |   505.7 μs | 131.32 μs |      - |   4.49 KB |
| EfCoreSelect | 1,932.0 μs | 1,372.6 μs | 212.42 μs | 7.8125 |  56.63 KB |
| EfCoreInsert | 4,084.6 μs | 1,031.9 μs | 159.69 μs |      - |  70.93 KB |
