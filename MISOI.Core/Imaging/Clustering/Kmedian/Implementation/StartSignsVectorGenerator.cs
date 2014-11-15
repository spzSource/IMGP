using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

using MISOI.Core.Imaging.Clustering.Attributes;
using MISOI.Core.Imaging.Scanning.Sequential;

namespace MISOI.Core.Imaging.Clustering.Kmedian.Implementation
{
	public class StartSignsVectorGenerator : IStartSignsVectorGenerator
	{
		private readonly Random rand = new Random(DateTime.Now.Millisecond);

		public List<double[]> GenerateVectors(int clusterNumber, ISign<double>[] signs, Region[] regions)
		{
			List<double[]> results = new List<double[]>();

			List<double[]> signsValues = regions
				.Select(t => signs.Select(sign => sign.Calculate(t)).ToArray())
				.ToList();

			List<double[]> sorted = signsValues.OrderBy(e => e[1]).ToList();

			results.Add(sorted[0]);
			double[] prev = sorted[0];
			foreach (double[] value in sorted)
			{
				if (Math.Abs(value[1] - prev[1]) > 2 && Math.Abs(value[0] - prev[0]) > 2)
				{
					prev = value;
					results.Add(value);
				}
			}

			return results;
		}

		public double[] GenerateVector(int length)
		{
			double[] res = new double[length];

			for (int i = 0; i < length; i++)
			{
				res[i] = rand.Next(-100, 100);
			}

			return res;
		}
	}
}
