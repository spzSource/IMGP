using System;
using System.Collections.Generic;
using System.Drawing;

using MISOI.Core.Imaging.Unmanaged;
using MISOI.Logging;

namespace MISOI.Core.Imaging.Detectors.Implementation
{
	public class PointsDetectProcessor : IPointsDetectProcessor
	{
		public Bitmap Process(Bitmap source, IPointsDetector detector)
		{
			LockableBitmap processedBitmap = new LockableBitmap(source);
			processedBitmap.LockBits();

			try
			{
				СharacteristicPoint[] characteristicsPoints = null;
				try
				{
					characteristicsPoints = detector.Detect(processedBitmap);
				}
				finally
				{
					processedBitmap.UnlockBits();
				}

				ProcessOutput(source, characteristicsPoints);
			}
			catch (Exception ex)
			{
				Log.Current.Error(ex);
			}

			return source;
		}

		private static void ProcessOutput(Bitmap source,
			IEnumerable<СharacteristicPoint> characteristicsPoints)
		{
			using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(source))
			{
				using (Brush b = new SolidBrush(Color.Red))
				{
					foreach (СharacteristicPoint characteristicsPoint in characteristicsPoints)
					{
						g.FillEllipse(b, characteristicsPoint.Coord.X - 3, characteristicsPoint.Coord.Y - 3, 6, 6);
					}
				}
			}
			
		}
	}
}
