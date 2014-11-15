using System.Drawing;

using MISOI.Core.Imaging.Clustering.Kmedian;
using MISOI.Core.Imaging.Extensions;
using MISOI.Core.Imaging.Scanning.Sequential;
using MISOI.Core.Imaging.Unmanaged;

using Region = MISOI.Core.Imaging.Scanning.Sequential.Region;


namespace MISOI.Core.Imaging.Clustering
{
	public class ClusteringProcessor : IClusteringProcessor
	{
		private readonly RandomColorGenerator colorGenerator = new RandomColorGenerator();

		public Bitmap Determine(
			Bitmap source, 
			int clustersNumber, 
			Region[] regions, 
			IClusteringAlgorithmFactory clusteringAlgorithmFactory)
		{
			LockableBitmap lockableBitmap = new LockableBitmap(source);
			lockableBitmap.LockBits();

			IClusteringAlgorithm<double> clusteringAlgorithm = 
				clusteringAlgorithmFactory.Create(clustersNumber, lockableBitmap);

			try
			{
				Region[] clusteredRegions = clusteringAlgorithm.Distribute(regions);

				foreach (Region region in clusteredRegions)
				{
					FillRegion(region, lockableBitmap);
				}
			}
			finally
			{
				lockableBitmap.UnlockBits();
			}

			return source;
		}

		private void FillRegion(
			Region region,
			LockableBitmap lockableBitmap)
		{
			Color color;

			if (!ColorMapper.TryMap(region.Number, out color))
			{
				color = colorGenerator.RandomColor();
			}

			foreach (ScanInfo info in region.Infos)
			{
				lockableBitmap.SetPixel(info.Coords.X, info.Coords.Y, color);
			}
		}
	}
}
