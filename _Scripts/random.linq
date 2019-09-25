<Query Kind="Program" />

void Main()
{
	var numbers = new Random(0).Indices(15);
	numbers.Dump();
}

static class RandomExtensions
{
	public static IReadOnlyList<int> Indices(this Random rng, int noOfIndices)
	{
		var list = new List<int>(noOfIndices);
		for (int i = 0; i < noOfIndices; i++)
			list.Add(i);
		
		
		int n = noOfIndices;
		while (n > 1)
		{
			int k = rng.Next(n--);
			(list[n], list[k]) = (list[k], list[n]);
		}
		
		return list;
	}
}