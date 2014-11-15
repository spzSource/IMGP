using System;

using MISOI.Core.Imaging.Scanning.Sequential;

namespace MISOI.Core.Imaging.Clustering.Attributes.Implementation
{
	public class PrincipalAxisOrientation : ISign<double>
	{
		private readonly DiscreteCentralMoment m20Moment =
			new DiscreteCentralMoment(2, 0, new CenterOfMass());

		private readonly DiscreteCentralMoment m02Moment =
			new DiscreteCentralMoment(0, 2, new CenterOfMass());

		private readonly DiscreteCentralMoment m11Moment =
			new DiscreteCentralMoment(1, 1, new CenterOfMass());

		public double Calculate(Region region)
		{
			double m20 = m20Moment.Calculate(region);
			double m02 = m02Moment.Calculate(region);
			double m11 = m11Moment.Calculate(region);

			return Math.Atan(2 * m11 / (double)(m20 - m02)) / 2;
		}
	}
}
