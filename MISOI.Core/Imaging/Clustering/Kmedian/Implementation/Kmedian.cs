using System;
using System.Linq;

using MISOI.Core.Imaging.Clustering.Attributes;
using MISOI.Core.Imaging.Scanning.Sequential;

namespace MISOI.Core.Imaging.Clustering.Kmedian.Implementation
{
	public class Kmedian : IClusteringAlgorithm<double>
	{
		private readonly int clustersNumber;
		private readonly ISign<double>[] signs;
		private readonly IClusteringDistance<double> distance;
		private readonly IStartSignsVectorGenerator startSignVectorGenerator;
		private readonly ICenterDeterminingAlgorithm<double> centerDeterminingAlgorithm;

		private IClusteringCenter<double>[] clusterCenters; 

		public Kmedian(
			int clustersNumber, 
			ISign<double>[] signs,
			IClusteringDistance<double> distance,
			IStartSignsVectorGenerator startSignVectorGenerator,
			ICenterDeterminingAlgorithm<double> centerDeterminingAlgorithm)
		{
			if (signs == null)
			{
				throw new ArgumentNullException("signs");
			}
			if (distance == null)
			{
				throw new ArgumentNullException("distance");
			}
			if (startSignVectorGenerator == null)
			{
				throw new ArgumentNullException("startSignVectorGenerator");
			}
			if (centerDeterminingAlgorithm == null)
			{
				throw new ArgumentNullException("centerDeterminingAlgorithm");
			}

			this.clustersNumber = clustersNumber;
			this.signs = signs;
			this.distance = distance;
			this.startSignVectorGenerator = startSignVectorGenerator;
			this.centerDeterminingAlgorithm = centerDeterminingAlgorithm;
		}

		private void InitializeClusterCenters(int clusterNumber, int signsLength, Region[] regions)
		{
			clusterCenters = new IClusteringCenter<double>[clusterNumber];

			var startVect = startSignVectorGenerator.GenerateVectors(clusterNumber, signs, regions);
			for (int i = 0; i < clusterNumber; i++)
			{
				if (i >= startVect.Count)
				{
					clusterCenters[i] = new ClusteringCenter(
						startSignVectorGenerator.GenerateVector(signsLength),
						clusterNumber: i);
				}
				else
				{
					clusterCenters[i] = new ClusteringCenter(
						startVect[i],
						clusterNumber: i);
				}
				
			}
		}

		private void InitializeClustersByRegions(int clusterNumber, Region[] regions)
		{
			if (clusterNumber >= regions.Length)
			{
				throw new ArgumentException("clusterNumber");
			}
			
			clusterCenters = new IClusteringCenter<double>[clusterNumber];

			for (int i = 0; i < clusterNumber; i++)
			{
				clusterCenters[i] = new ClusteringCenter(
					signs.Select(sign => sign.Calculate(regions[i])).ToArray(),
					clusterNumber: i);

				regions[i].IsCenter = true;
			}
		}

		public Region[] Distribute(Region[] sourceRegions)
		{
			InitializeClusterCenters(clustersNumber, signs.Length, sourceRegions);

			//InitializeClustersByRegions(clustersNumber, sourceRegions);

			bool clustersChanged = true;

			while (clustersChanged)
			{
				foreach (Region region in sourceRegions.Where(r => !r.IsCenter))
				{
					int newClusterNumber = DetermineClusterNumber(region);

					clustersChanged = newClusterNumber != region.Number;
					
					region.Number = newClusterNumber;
				}

				foreach (IClusteringCenter<double> clusterCenter in clusterCenters)
				{
					IClusteringCenter<double> center = clusterCenter;

					Region[] clustersRegions = sourceRegions
						.Where(region => region.Number == center.Number && !region.IsCenter)
						.ToArray();

					clusterCenter.Recalculate(signs, clustersRegions, centerDeterminingAlgorithm);

					foreach (Region region in sourceRegions)
					{
						region.IsCenter = false;
					}
				}
			}
			return sourceRegions;
		}

		private int DetermineClusterNumber(Region region)
		{
			return clusterCenters.Select(center => new
			{
				center.Number, 
				Distance = distance.Calculate(signs, region, center)
			}).OrderBy(e => e.Distance).First().Number;
		}
	}
}
