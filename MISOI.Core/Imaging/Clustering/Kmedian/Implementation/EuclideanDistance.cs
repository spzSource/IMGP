using System;
using System.Linq;

using MISOI.Core.Imaging.Clustering.Attributes;
using MISOI.Core.Imaging.Scanning.Sequential;

namespace MISOI.Core.Imaging.Clustering.Kmedian.Implementation
{
	public class EuclideanDistance : IClusteringDistance<double>
	{
		public double Calculate(
			ISign<double>[] signs, 
			Region region,
			IClusteringCenter<double> center)
		{
			double[] regionSignsValues = signs.Select(sign => sign.Calculate(region)).ToArray();
			
			double sum = regionSignsValues
				.Zip(center.SignsValues, (first, second) => Math.Pow(first - second, 2))
				.Sum();

			return Math.Sqrt(sum);
		}
	}
}
