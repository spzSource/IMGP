using System;
using System.Drawing;
using MISOI.Core.Imaging.Extensions;

namespace MISOI.Core.Imaging.Processors.PerItemProcessor.Algorithms.Implementation
{
	public class Binarization : IPerItemAlgorithm
	{
		private readonly byte threshold;

		private readonly Func<byte, byte, byte> action = 
			(brightness, threshold) => (byte)(brightness > threshold ? 255 : 0); 

		public Binarization(byte threshold)
		{
			this.threshold = threshold;
		}

		public Color Execute(Color color)
		{
			byte brightness = color.Brightness();
			byte newBrightness = action.Invoke(brightness, threshold);
			
			return Color.FromArgb(newBrightness, newBrightness, newBrightness);
		}
	}
}
