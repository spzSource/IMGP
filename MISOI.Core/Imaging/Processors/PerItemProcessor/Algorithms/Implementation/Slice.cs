using System;
using System.Drawing;
using MISOI.Core.Imaging.Extensions;

namespace MISOI.Core.Imaging.Processors.PerItemProcessor.Algorithms.Implementation
{
	public class Slice : IPerItemAlgorithm
	{
		private readonly byte left;
		private readonly byte right;

		private readonly Func<byte, byte, byte, byte> action =
			(brightness, left, right) =>
			{
				byte result = 0;
				if (brightness <= left) result = 0;
				if (brightness > left && brightness <= right) result = 255;
				if (brightness > right) result = 0;
				return result;
			}; 


		public Slice(byte left, byte right)
		{
			if (left > right)
			{
				throw new ArgumentException("Left boundary should be less or equals than right.");
			}
			this.left = left;
			this.right = right;
		}

		public Color Execute(Color color)
		{
			byte brightness = color.Brightness();
			byte newBrightness = action.Invoke(brightness, left, right);

			return Color.FromArgb(newBrightness, newBrightness, newBrightness);
		}
	}
}
