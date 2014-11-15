using System.Drawing;
using System.Linq;

using MISOI.Core.Imaging.Extensions;

namespace MISOI.Core.Imaging.Filtering.Filters.Implementation
{
	/// <summary>
	/// 
	/// </summary>
	public class Aperture
	{
		public Color A1 { get; set; }
		public Color A2 { get; set; }
		public Color A3 { get; set; }
		public Color A4 { get; set; }
		public Color A5 { get; set; }
		public Color A6 { get; set; }
		public Color A7 { get; set; }
		public Color A8 { get; set; }
		public Color F { get; set; }

		public int CurrentX { get; set; }
		public int CurrentY { get; set; }

		public Color CurrentColor { get; set; }

		public Color GetMedian()
		{
			Color[] apertureColors = { A1, A2, A3, A4, A5, A6, A7, A8, CurrentColor };
			Color[] sortedColors = apertureColors.OrderBy(e => e.Brightness()).ToArray();

			// NOTE: element with index equal to 5 is the median
			return sortedColors[5];
		}
	}
}
