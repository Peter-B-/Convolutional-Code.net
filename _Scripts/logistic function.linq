<Query Kind="Expression" />

Enumerable.Range(-255, 512)
	.Select(i => i/ 255.0)
	.Select(i => new {
		Img = i ,
		Logistic = 1.0/(1.0 + Math.Exp(-12 * (i - 0.5))),
		SymmetricScore = -1.0 + 2.0 / (1.0 + Math.Exp(-6 * (i - 0.0)))
	})
	.Chart(i => i.Img)
	.AddYSeries(i => i.Logistic, LINQPad.Util.SeriesType.Line)
	.AddYSeries(i => i.SymmetricScore, LINQPad.Util.SeriesType.Line)
	
	
	
	
	