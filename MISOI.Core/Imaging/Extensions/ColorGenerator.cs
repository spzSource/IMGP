using System;
using System.Drawing;

namespace MISOI.Core.Imaging.Extensions
{
	public class RandomColorGenerator
	{
		readonly Random random = new Random(DateTime.Now.Millisecond);

		public Color RandomColor()
		{
			byte red = (byte)random.Next(0, 255);
			byte green = (byte)random.Next(0, 255);
			byte blue = (byte)random.Next(0, 255);

			return Color.FromArgb(red, green, blue);
		}
	}
}
