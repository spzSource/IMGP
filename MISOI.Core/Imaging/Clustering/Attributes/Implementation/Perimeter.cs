using System.Linq;

using MISOI.Core.Imaging.Scanning.Sequential;

namespace MISOI.Core.Imaging.Clustering.Attributes.Implementation
{
	public class Perimeter : ISign<double>
	{
		public double Calculate(Region region)
		{
			return region.Infos.Count(e => e.IsBoundary);
		}
	}
}
