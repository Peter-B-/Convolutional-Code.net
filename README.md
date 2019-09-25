# Convolutional-Code.net

The aim of this project is, to provide a .Net implementation for encoding and decoding [convolutional codes](https://en.wikipedia.org/wiki/Convolutional_code). The decoder is implemented using a Viterbi algorithm as described in the wiki book [A Basic Convolutional Coding Example](https://en.wikibooks.org/wiki/A_Basic_Convolutional_Coding_Example).

The project is still at it's beginnings, but there is a test program included, that can already encode and decode convolutional codes.

If you have any thoughts on the code or even a PR, please let me know.

```csharp
var config = CodeConfig.Size3_7_5;

var input = "10110".ParseBools();

var encoder = new Encoder(config, terminateCode: false);
var output = encoder.Encode(input);

var viterbi = Viterbi.CreateWithHammingDistance(config);
var restored = viterbi.Solve(output);

Console.WriteLine("Configuration: " + config);
Console.WriteLine();
Console.WriteLine("Input:    " + input.Format());
Console.WriteLine("Encoded:  " + output.Format());
Console.WriteLine("Restored: " + restored.Format());
Console.WriteLine();
```

This will produce:
```
Input:    10110
Encoded:  1110000101
Restored: 10110
```

The logic is written as a .Net Standard 2.0 dll and should be easy to reference from any project.
