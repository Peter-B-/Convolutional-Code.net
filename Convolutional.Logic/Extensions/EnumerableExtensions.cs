﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Convolutional.Logic.Extensions
{
    public static class EnumerableExtensions
    {
        public static T GetBestBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> scoreSelector, ScoreMethod method)
        {
            switch (method)
            {
                case ScoreMethod.Minimize:
                    return 
                        source.MinBy(scoreSelector)
                        .First();
                case ScoreMethod.Maximize:
                    return
                        source.MaxBy(scoreSelector)
                            .First();
                default:
                    throw new ArgumentOutOfRangeException(nameof(method), method, null);
            }
        }

        public static IEnumerable<T> NonUnique<T>(this IEnumerable<T> source)
        {
            return
                source.GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(y => y.Key)
                ;
        }
    }
}