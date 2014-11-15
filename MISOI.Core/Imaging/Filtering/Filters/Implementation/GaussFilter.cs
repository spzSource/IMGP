using System;
using System.Drawing;

namespace MISOI.Core.Imaging.Filtering.Filters.Implementation
{
	public class GaussFilter : IMultiFilter
	{
		private readonly double sigma;
		private readonly int apertureSize;
		private readonly double[,] gaussAperture;

		public GaussFilter(int apertureSize, double sigma)
		{
			this.apertureSize = apertureSize;
			this.sigma = sigma;

			gaussAperture = new double[apertureSize, apertureSize];

			InitializeGaussAperture();
		}

		public Color Execute(ApertureItem[,] aperture)
		{
			byte r = 0;
			byte g = 0;
			byte b = 0;

			for (int i = 0; i < apertureSize; i++)
			{
				for (int j = 0; j < apertureSize; j++)
				{
					r += (byte)(gaussAperture[i, j] * aperture[i, j].Color.R);
					g += (byte)(gaussAperture[i, j] * aperture[i, j].Color.G);
					b += (byte)(gaussAperture[i, j] * aperture[i, j].Color.B);
				}
			}

			return Color.FromArgb(r, g, b);
		}

		private void InitializeGaussAperture()
		{
			int center = apertureSize / 2;

			double total = 0;

			for (int x = 0; x < apertureSize; x++)
			{
				for (int y = 0; y < apertureSize; y++)
				{
					gaussAperture[x, y] = Math.Exp(-1 * (Math.Pow(x - center, 2) + Math.Pow(y - center, 2)) /
							(2 * Math.Pow(sigma, 2))) /
						(2 * Math.PI * Math.Pow(sigma, 2));

					total += gaussAperture[x, y];
				}
			}

			Normalize(total);
		}

		private void Normalize(double total)
		{
			for (int x = 0; x < apertureSize; x++)
			{
				for (int y = 0; y < apertureSize; y++)
				{
					gaussAperture[x, y] = gaussAperture[x, y] / total;
				}
			}
		}
	}
}
