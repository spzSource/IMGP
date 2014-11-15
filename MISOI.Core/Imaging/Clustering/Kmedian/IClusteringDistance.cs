using MISOI.Core.Imaging.Clustering.Attributes;
using MISOI.Core.Imaging.Scanning.Sequential;

namespace MISOI.Core.Imaging.Clustering.Kmedian
{
	public interface IClusteringDistance<TObject>
	{
		TObject Calculate(
			ISign<TObject>[] signs, 
			Region region, 
			IClusteringCenter<TObject> center);
	}
}
