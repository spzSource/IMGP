using System;

using MISOI.Core.Imaging.Scanning.Sequential;

namespace MISOI.Core.Imaging.Clustering.Attributes.Implementation
{
	public class Density : ISign<double>
	{
		private readonly ISign<double> squareSign;
		private readonly ISign<double> perimeterSign;

		public Density(ISign<double> squareSign, ISign<double> perimeterSign)
		{
			if (squareSign == null)
			{
				throw new ArgumentNullException("squareSign");
			}
			if (perimeterSign == null)
			{
				throw new ArgumentNullException("perimeterSign");
			}

			this.squareSign = squareSign;
			this.perimeterSign = perimeterSign;
		}

		public double Calculate(Region region)
		{
			double square = squareSign.Calculate(region);
			double perimeter = perimeterSign.Calculate(region);

			return perimeter * perimeter / square;
		}
	}
}
