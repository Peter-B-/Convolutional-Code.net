<Query Kind="Statements">
  <Reference Relative="..\Convolutional.Logic\bin\Debug\netstandard2.0\Convolutional.Logic.dll">D:\Projekte\Open Source\Convolutional-Code.net\Convolutional.Logic\bin\Debug\netstandard2.0\Convolutional.Logic.dll</Reference>
  <Namespace>Convolutional.Logic</Namespace>
  <Namespace>Convolutional.Logic.Extensions</Namespace>
  <Namespace>Convolutional.Logic.Scores</Namespace>
</Query>

var config = CodeConfig.Size7_6d_4f;

var input = 15003.GetBools(14)
	.Concat(Enumerable.Repeat(false, 6));
//var input = "11101010011011".ParseBools();

var encoder = new Convolutional.Logic.Encoder(config, terminateCode: false);
var encoded = encoder.Encode(input);

var transmitted =
	encoded
	.Select(e => e ? 255.0 : 0);
	
encoded.Dump();	
transmitted.Dump();

var decoder = new Viterbi<double>(
	config.EnumerateTransitions(),
	SymmetricScore.Range_0_255.CalculateScore,
	new ViterbiConfig()
	{
		InitialState = State.Zero(config.NoOfStateRegisters),
		ScoreMethod = ScoreMethod.Maximize,
		TerminationState = State.Zero(config.NoOfStateRegisters)
	});
var res = decoder.Solve(transmitted);


Enumerable.Zip(input, res.Message, (a, b) => a == b).All(b => b).Dump();
input.Format(groupSize: 2).Dump();
res.Message.Format(groupSize: 2).Dump();
res.Dump();