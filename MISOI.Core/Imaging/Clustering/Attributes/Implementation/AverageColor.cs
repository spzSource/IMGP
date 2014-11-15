using System;
using System.Collections.Generic;
using System.Linq;

using MISOI.Core.Imaging.Unmanaged;

using Region = MISOI.Core.Imaging.Scanning.Sequential.Region;

namespace MISOI.Core.Imaging.Clustering.Attributes.Implementation
{
	public class AverageColor : ISign<double>
	{
		private readonly LockableBitmap source;

		public AverageColor(LockableBitmap source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			this.source = source;
		}

		public double Calculate(Region region)
		{
			IEnumerable<int> averageColor = region.Infos.Select(e => new
			{
				source.GetPixel(e.Coords.X, e.Coords.Y).R,
				source.GetPixel(e.Coords.X, e.Coords.Y).G,
				source.GetPixel(e.Coords.X, e.Coords.Y).B
			}).Aggregate(new[] { 0, 0, 0 }, (tuple, element) =>
			{
				tuple[0] += element.R;
				tuple[1] += element.G;
				tuple[2] += element.B;
				return tuple;
			}).Select(e => e / region.Infos.Count);

			return averageColor.Average();
		}
	}
}
