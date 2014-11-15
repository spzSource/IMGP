using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using MISOI.Core.Imaging.Detectors.Forstner;
using MISOI.Core.Imaging.Extensions;
using MISOI.Core.Imaging.Unmanaged;
using MISOI.Logging;

namespace MISOI.Core.Imaging.Detectors.Implementation
{
	public class PointsDetector : IPointsDetector
	{
		private double[,] dfx;
		private double[,] dfy;
		private double[,] dfxy;

		private readonly int[,] convolutionX =
		{
			{ -1, 0, 1 },
			{ -1, 0, 1 },
			{ -1, 0, 1 }
		};

		private readonly int[,] convolutionY =
		{
			{ -1, -1, -1 },
			{  0,  0,  0 },
			{  1,  1,  1 }
		};

		private readonly Func<int, int, int, int, LockableBitmap, byte> apertureSelector =
			(sx, sy, x, y, source) =>
			{
				if (sx < 0 || sy < 0) return source.GetPixel(x, y).Brightness();
				if (sx >= source.Width || sy >= source.Height) return source.GetPixel(x, y).Brightness();
				return source.GetPixel(sx, sy).Brightness();
			};

		private readonly IResponseAngle responseAngle;
		private readonly double threshold;
		private readonly int filteredApertureSize;

		public PointsDetector(IResponseAngle responseAngle, double threshold, int filteredApertureSize)
		{
            if (responseAngle == null)
            {
                throw new ArgumentNullException("responseAngle");
            }
			this.responseAngle = responseAngle;
			this.threshold = threshold;
			this.filteredApertureSize = filteredApertureSize;
		}

		private void InitializeGradients(LockableBitmap source)
		{
			dfx  = new double[source.Width, source.Height];
			dfy  = new double[source.Width, source.Height];
			dfxy = new double[source.Width, source.Height];

			for (int x = 0; x < source.Width; x++)
			{
				for (int y = 0; y < source.Height; y++)
				{
					CalculateGradientsForAperture(x, y, source);
				}
			}
		}

		private void CalculateGradientsForAperture(int x, int y, LockableBitmap source)
		{
			double dx = 0.0;
			double dy = 0.0;

			int centerX = convolutionX.GetLength(0) / 2;
			int centerY = convolutionY.GetLength(0) / 2;

			for (int i = 0; i < convolutionX.GetLength(0); i++)
			{
				for (int j = 0; j < convolutionY.GetLength(0); j++)
				{
					int indexX = x + (i - centerX);
					int indexY = y + (j - centerY);

					dx += apertureSelector.Invoke(indexX, indexY, x, y, source)
						* convolutionX[j, i];

					dy += apertureSelector.Invoke(indexX, indexY, x, y, source)
						* convolutionY[j, i];
				}
			}

			dfx [x, y] = dx;
			dfy [x, y] = dy;
			dfxy[x, y] = dx * dy;
		}

		public СharacteristicPoint[] Detect(LockableBitmap source)
		{
			InitializeGradients(source);

			СharacteristicPoint[,] responces = new СharacteristicPoint[source.Width, source.Height];

			for (int y = 0; y < source.Height; y++)
			{
				for (int x = 0; x < source.Width; x++)
				{
					double response = responseAngle.Calculate(dfx[x, y], dfy[x, y], dfxy[x, y]);

					response = response > threshold ? response : default(double);

					responces[x, y] = new СharacteristicPoint(
						new Point(x, y), 
						response);
				}
			}

			List<СharacteristicPoint> filteredPoints = DetectLocalMaximumPoints(responces);

			return filteredPoints.ToArray();
		}

		private List<СharacteristicPoint> DetectLocalMaximumPoints(СharacteristicPoint[,] responces)
		{
			List<СharacteristicPoint> filteredPoints = new List<СharacteristicPoint>();

			try
			{
				ApertureEnumerator enumerator = new ApertureEnumerator(responces, filteredApertureSize);

				while (enumerator.MoveNext())
				{
					СharacteristicPoint localMaxResponcePoint = 
						enumerator.Current.OrderByDescending(e => e.ResponseAngle).First();

					if (localMaxResponcePoint.ResponseAngle > default(double))
					{
						filteredPoints.Add(localMaxResponcePoint);		
					}
				}
			}
			catch (Exception ex)
			{
				Log.Current.Error(ex);
				throw;
			}

			return filteredPoints;
		}
	}
}
