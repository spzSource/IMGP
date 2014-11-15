using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using MISOI.Core.Imaging.Clustering.Attributes;
using MISOI.Core.Imaging.Scanning.Sequential;

namespace MISOI.Core.Imaging.Clustering.Kmedian.Implementation
{
	public class ClusteringCenter : IClusteringCenter<double>
	{
		public ClusteringCenter(double[] startValues, int clusterNumber)
		{
			SignsValues = startValues;
			Number = clusterNumber;
		}

		public int Number
		{
			get;
			private set;
		}

		public double[] SignsValues
		{
			get;
			private set;
		}

		public void Recalculate(
			ISign<double>[] signs,
			Region[] includedRegions,
			ICenterDeterminingAlgorithm<double> centerDeterminingAlgorithm)
		{
			if (includedRegions.Length > 0)
			{
				ICollection<KeyValuePair<Region, double[]>> regionsSigns = 
					new Collection<KeyValuePair<Region, double[]>>();

				foreach (Region region in includedRegions)
				{
					regionsSigns.Add(new KeyValuePair<Region, double[]>(
							region, signs.Select(sign => sign.Calculate(region)).ToArray()));
				}

				SignsValues = new double[signs.Length];




				//for (int signIndex = 0; signIndex < signs.Length; signIndex++)
				//{
				//	SignsValues[signIndex] = centerDeterminingAlgorithm.Calculate(
				//		regionsSigns.Select(e => e[signIndex]).ToArray());
				//}
			}
		}
	}
}
