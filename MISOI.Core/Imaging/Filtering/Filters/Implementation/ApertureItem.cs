using System.Drawing;

namespace MISOI.Core.Imaging.Filtering.Filters.Implementation
{
	public class ApertureItem
	{
		public Color Color
		{
			get;
			set;
		}

		public Point Coords
		{
			get;
			set;
		}

		public ApertureItem(Point coords, Color color)
		{
			Coords = coords;
			Color = color;
		}
	}
}
