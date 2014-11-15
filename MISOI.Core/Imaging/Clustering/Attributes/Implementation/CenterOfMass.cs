using System.Drawing;
using System.Linq;

using Region = MISOI.Core.Imaging.Scanning.Sequential.Region;

namespace MISOI.Core.Imaging.Clustering.Attributes.Implementation
{
	public class CenterOfMass : ISign<Point>
	{
		public Point Calculate(Region region)
		{
			int square = region.Infos.Count;
			
			int coordsSumByX = region.Infos.Sum(e => e.Coords.X);
			int coordsSumByY = region.Infos.Sum(e => e.Coords.Y);

			int centerX = coordsSumByX / square;
			int centerY = coordsSumByY / square;

			return new Point(centerX, centerY);
		}
	}
}
