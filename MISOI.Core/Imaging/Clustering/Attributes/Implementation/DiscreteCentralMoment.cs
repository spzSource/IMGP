using System;
using System.Drawing;
using System.Linq;

using Region = MISOI.Core.Imaging.Scanning.Sequential.Region;

namespace MISOI.Core.Imaging.Clustering.Attributes.Implementation
{
	public class DiscreteCentralMoment : ISign<double>
	{
		private readonly int i;
		private readonly int j;
		private readonly ISign<Point> centerOfMass;

		public DiscreteCentralMoment(int i, int j, ISign<Point> centerOfMass)
		{
			if (centerOfMass == null)
			{
				throw new ArgumentNullException("centerOfMass");
			}

			this.i = i;
			this.j = j;
			this.centerOfMass = centerOfMass;
		}

		public double Calculate(Region region)
		{
			Point center = centerOfMass.Calculate(region);

			return region.Infos.Select(pixel => 
					Math.Pow(pixel.Coords.X - center.X, i) * Math.Pow(pixel.Coords.Y - center.Y, j))
				.Sum();
		}
	}
}
