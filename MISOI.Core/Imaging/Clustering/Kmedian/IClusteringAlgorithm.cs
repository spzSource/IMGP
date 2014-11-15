using MISOI.Core.Imaging.Scanning.Sequential;

namespace MISOI.Core.Imaging.Clustering.Kmedian
{
	public interface IClusteringAlgorithm<in TObject>
	{
		Region[] Distribute(Region[] sourceRegions);
	}
}
