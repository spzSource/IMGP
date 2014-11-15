using System.Linq;

using MISOI.Core.Imaging.Extensions;
using MISOI.Core.Imaging.Scanning.Sequential;

namespace MISOI.Core.Imaging.Clustering.Attributes.Implementation
{
	public class BrightnessDispersion : ISign<double>
	{
		public double Calculate(Region region)
		{
			int pixelsCount = region.Infos.Count;
			int sum = region.Infos.Select(e => (int)e.Color.Brightness()).Sum();
			int sum2 = region.Infos.Select(e => e.Color.Brightness() * e.Color.Brightness()).Sum();

			return (sum2 - sum * sum / pixelsCount) / (double)(pixelsCount - 1);
		}
	}
}
