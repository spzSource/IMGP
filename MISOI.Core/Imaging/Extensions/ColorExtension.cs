using System.Drawing;

namespace MISOI.Core.Imaging.Extensions
{
	public static class ColorExtension
	{
		public static byte Brightness(this Color color)
		{
			return (byte)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
		}

		public static byte Trunc(this int value)
		{
			byte result = 0;
			if (value < 0) result = 0;
			if (value > 255) result = 255;
			return result;
		}

		public static bool IsBlack(this Color color)
		{
			return color.ToArgb() == Color.Black.ToArgb();
		}

		public static bool IsWhite(this Color color)
		{
			return color.ToArgb() == Color.White.ToArgb();
		}
	}
}
