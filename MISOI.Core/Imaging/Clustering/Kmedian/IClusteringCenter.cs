using MISOI.Core.Imaging.Clustering.Attributes;
using MISOI.Core.Imaging.Scanning.Sequential;

namespace MISOI.Core.Imaging.Clustering.Kmedian
{
	public interface IClusteringCenter<out TObject>
	{
		int Number
		{
			get;
		}

		TObject[] SignsValues
		{
			get;
		}

		void Recalculate(
			ISign<double>[] signs,
			Region[] includedRegions,
			ICenterDeterminingAlgorithm<double> centerDeterminingAlgorithm);
	}
}
