using System.Collections.Generic;

using MISOI.Core.Imaging.Clustering.Attributes;
using MISOI.Core.Imaging.Scanning.Sequential;

namespace MISOI.Core.Imaging.Clustering.Kmedian
{
	public interface IStartSignsVectorGenerator
	{
		List<double[]> GenerateVectors(int clusterNumber, ISign<double>[] signs, Region[] regions);

		double[] GenerateVector(int length);
	}
}
