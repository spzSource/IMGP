using System;
using System.Drawing;

using MISOI.Core.Imaging.Extensions;

namespace MISOI.Core.Imaging.Filtering.Filters.Implementation
{
	public class SobelFilter : IFilter
	{
		public Color Execute(Color color, ref Aperture aperture)
		{
			int dx = (aperture.A1.Brightness() + 2 * aperture.A8.Brightness() + aperture.A7.Brightness()) -
				(aperture.A3.Brightness() + 2 * aperture.A4.Brightness() + aperture.A5.Brightness());

			int dy = (aperture.A7.Brightness() + 2 * aperture.A6.Brightness() + aperture.A5.Brightness()) -
				(aperture.A1.Brightness() + 2 * aperture.A2.Brightness() + aperture.A3.Brightness());

			byte gradient = (byte)Math.Sqrt(dx * dx + dy * dy);
		
			return Color.FromArgb(
				gradient,
				gradient,
				gradient);
		}
	}
}
