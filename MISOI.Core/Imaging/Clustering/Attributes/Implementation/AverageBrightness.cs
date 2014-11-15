using System;
using System.Linq;

using MISOI.Core.Imaging.Extensions;
using MISOI.Core.Imaging.Scanning.Sequential;
using MISOI.Core.Imaging.Unmanaged;

namespace MISOI.Core.Imaging.Clustering.Attributes.Implementation
{
	public class AverageBrightness : ISign<double>
	{
		private readonly LockableBitmap source;

		public AverageBrightness(LockableBitmap source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			this.source = source;
		}

		public double Calculate(Region region)
		{
			return region.Infos.Select(
					e => (int)source.GetPixel(e.Coords.X, e.Coords.Y).Brightness())
				.Average();
		}
	}
}
