using System.Drawing;

using Region = MISOI.Core.Imaging.Scanning.Sequential.Region;

namespace MISOI.Core.Imaging.Clustering
{
	public interface IClusteringProcessor
	{
		Bitmap Determine(
			Bitmap source,
			int clustersNumber, 
			Region[] regions,
			IClusteringAlgorithmFactory clusteringAlgorithmFactory);
	}
}
