using System;
using System.Drawing;

namespace MISOI.Core.Imaging.Processors.PerItemProcessor.Algorithms.Implementation
{
	public class LogarithmicCorrection : IPerItemAlgorithm
	{
		private readonly byte[] gammaRamp = new byte[256];

		private readonly Func<int, double, byte> make = 
			(index, gamma) => (byte)(255 * Math.Pow(index / 255.0, 1 / gamma));

		private readonly Func<double, byte> trunc =
			(source) =>
			{
				if (source < 0) source = 0;
				if (source > 255) source = 255;
				return (byte) source;
			};

		public LogarithmicCorrection(double gamma)
		{
			for (int i = 0; i < 256; i++)
			{
				gammaRamp[i] = trunc(make(i, gamma));
			}
		}

		public Color Execute(Color color)
		{
			return Color.FromArgb(
				gammaRamp[color.R], 
				gammaRamp[color.G], 
				gammaRamp[color.B]);
		}
	}
}
