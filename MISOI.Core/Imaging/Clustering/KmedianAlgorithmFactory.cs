using MISOI.Core.Imaging.Clustering.Attributes;
using MISOI.Core.Imaging.Clustering.Attributes.Implementation;
using MISOI.Core.Imaging.Clustering.Kmedian;
using MISOI.Core.Imaging.Clustering.Kmedian.Implementation;
using MISOI.Core.Imaging.Unmanaged;

namespace MISOI.Core.Imaging.Clustering
{
	public class KmedianAlgorithmFactory : IClusteringAlgorithmFactory
	{
		public IClusteringAlgorithm<double> Create(
			int clustersNumber,
			LockableBitmap source)
		{
			ISign<double>[] signs =
			{
				//new AverageBrightness(source), 
				//new AverageColor(source), 
				//new BrightnessDispersion(), 
				new Density(new Square(), new Perimeter()),
				new Elongation(), 
				new PrincipalAxisOrientation(), 
				new Square(), 
				new Perimeter(), 
			};

			return new Kmedian.Implementation.Kmedian(
				clustersNumber,
				signs,
				new EuclideanDistance(),
				new StartSignsVectorGenerator(),
				new MedianAlgorithm());
		}
	}
}
