using System;
using System.Linq;

namespace MISOI.Core.Imaging.Clustering.Kmedian.Implementation
{
	public class MedianAlgorithm : ICenterDeterminingAlgorithm<double>
	{
		public double Calculate(double[] source)
		{
			double median;

			Array.Sort(source);

			if (source.Length % 2 == 0)
			{
				if (source.Length == 2)
				{
					median = source.Average();
				}
				else
				{
					int left = source.Length / 2;
					int right = source.Length / 2 + 1;
			
					median = (source[left] + source[right]) / 2;
				}
			}
			else
			{
				int medianIndex = source.Length / 2;
				median = source[medianIndex];
			}

			return median;
		}
	}
}
