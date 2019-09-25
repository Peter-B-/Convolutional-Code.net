<Query Kind="Expression" />

Enumerable.Range(0, 256)
	.Select(i => new {
		Img = i,
		Score = 1.0/(1.0 + Math.Exp(-0.05 * (i - 128.0)))
	})
	.Chart(i => i.Img, i => i.Score, LINQPad.Util.SeriesType.Line)