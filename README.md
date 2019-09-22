# Convolutional-Code.net

The aim of this project is, to provide a .Net implementation for encoding and decoding [convolutional codes](https://en.wikipedia.org/wiki/Convolutional_code). The decoder is implemented using a Viterbi algorithm as described in the wiki book [A Basic Convolutional Coding Example](https://en.wikibooks.org/wiki/A_Basic_Convolutional_Coding_Example).

The project is still at it's beginnings, but there is a test program included, that can already encode and decode convolutional codes.

If you have any thoughts on the code or even a PR, please let me know.

```csharp
var input = "10110".ParseBools();

var config = CodeConfig.Default3;
var encoder = new Encoder(config, true);
var viterbi = Viterbi.CreateWithHammingDistance(config);

var output = encoder.Encode(input);
var restored = viterbi.Solve(output);

Console.WriteLine("Input: " + input.Format());
Console.WriteLine("Encoded: " + output.Format());
Console.WriteLine("Restored: " + restored.Format());
```