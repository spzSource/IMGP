using MISOI.Core.Imaging.Scanning.Sequential;

namespace MISOI.Core.Imaging.Clustering.Attributes.Implementation
{
	public class Square : ISign<double>
	{
		public double Calculate(Region region)
		{
			return region.Infos.Count;
		}
	}
}
