using System.Drawing;

namespace MISOI.Core.Imaging.Detectors.Implementation
{
	public class СharacteristicPoint
	{
		public Point Coord
		{
			get;
			set;
		}

		public double ResponseAngle
		{
			get;
			set;
		}

		public СharacteristicPoint(Point coord, double responseAngle)
		{
			Coord = coord;
			ResponseAngle = responseAngle;
		}
	}
}
