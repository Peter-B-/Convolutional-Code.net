<Query Kind="Program">
  <Reference Relative="..\Convolutional.Logic\bin\Debug\netstandard2.0\Convolutional.Logic.dll">D:\Projekte\OpenSource\Convolutional-Code.net\Convolutional.Logic\bin\Debug\netstandard2.0\Convolutional.Logic.dll</Reference>
  <Namespace>Convolutional.Logic</Namespace>
  <Namespace>Convolutional.Logic.Extensions</Namespace>
  <Namespace>Convolutional.Logic.Scores</Namespace>
  <Namespace>Convolutional.Logic.Interleaver</Namespace>
  <Namespace>System.Drawing</Namespace>
  <Namespace>System.Runtime.InteropServices</Namespace>
  <Namespace>System.Drawing.Imaging</Namespace>
</Query>

void Main()
{
	var config = CodeConfig.Size7_6d_4f;
	
	var interleaver = new BlockInterleaver(40);
	
	var input = 15003.GetBools(14)
		.Concat(Enumerable.Repeat(false, 6));
	//var input = "11101010011011".ParseBools();
	
	var encoder = new Convolutional.Logic.Encoder(config, terminateCode: false);
	var encoded = encoder.Encode(input);
	
	var transmitted =
		interleaver.Interleave(
			encoded
		.Select(e => e ? 255.0 : 0).ToList());
		
	DirectBitmap.FromGrayscaleArray(transmitted.Select(t => (byte)t)).Dump();
	
		
	var decoder = new Viterbi<double>(
		config.EnumerateTransitions(),
		SymmetricScore.Range_0_255.CalculateScore,
		new ViterbiConfig()
		{
			InitialState = State.Zero(config.NoOfStateRegisters),
			ScoreMethod = ScoreMethod.Maximize,
			TerminationState = State.Zero(config.NoOfStateRegisters)
		});
		
	var deinterleaved = interleaver.Deinterleave(transmitted);	
	var res = decoder.Solve(deinterleaved);
	
	
	Enumerable.Zip(input, res.Message, (a, b) => a == b).All(b => b).Dump();
	input.Format(groupSize: 2).Dump();
	res.Message.Format(groupSize: 2).Dump();
	res.Dump();
}

public class DirectBitmap : IDisposable
{
	public Bitmap Bitmap { get; private set; }
	public Int32[] Bits { get; private set; }
	public bool Disposed { get; private set; }
	public int Height { get; private set; }
	public int Width { get; private set; }

	protected GCHandle BitsHandle { get; private set; }

	public DirectBitmap(int width, int height)
	{
		Width = width;
		Height = height;
		Bits = new Int32[width * height];
		BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
		Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
	}

	public void SetPixel(int x, int y, Color colour)
	{
		int index = x + (y * Width);
		int col = colour.ToArgb();

		Bits[index] = col;
	}

	public void SetPixel(int x, int y, byte gray)
	{
		int index = x + (y * Width);
		int col = ((255 << 24) | (gray << 16) | (gray << 8) | gray);

		Bits[index] = col;
	}

	public Color GetPixel(int x, int y)
	{
		int index = x + (y * Width);
		int col = Bits[index];
		Color result = Color.FromArgb(col);

		return result;
	}

	public Bitmap GetScaled(int factor)
	{
		var resized = new Bitmap(Width * factor, Height * factor);
		using (Graphics graphics = Graphics.FromImage(resized))
		{
			graphics.Clear(Color.Transparent);
			graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			graphics.DrawImage(Bitmap, 0, 0, resized.Width, resized.Height);
		}
		return resized;
	}

	public void Dispose()
	{
		if (Disposed) return;
		Disposed = true;
		Bitmap.Dispose();
		BitsHandle.Free();
	}
	
	public static Bitmap FromGrayscaleArray(IEnumerable<byte> data, int size = 40)
	{
		var array = data.ToArray();
		using var db = new DirectBitmap(array.Length, 1);
		for (int i = 0; i < array.Length; i++)
			db.SetPixel(i, 0, array[i]);

		return db.GetScaled(size);
	}
}
