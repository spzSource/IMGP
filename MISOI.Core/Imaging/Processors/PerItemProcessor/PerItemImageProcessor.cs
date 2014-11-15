using System;
using System.Collections.Generic;
using System.Drawing;

using MISOI.Core.Imaging.Processors.PerItemProcessor.Algorithms;
using MISOI.Core.Imaging.Statistics;
using MISOI.Core.Imaging.Unmanaged;

namespace MISOI.Core.Imaging.Processors.PerItemProcessor
{
	public class PerItemImageProcessor : ISourceImageProcessor
	{
		private readonly ILuminanceStatisticsAnalyzer analyzer;

		public PerItemImageProcessor(
			ILuminanceStatisticsAnalyzer analyzer)
		{
			if (analyzer == null)
			{
				throw new ArgumentNullException("analyzer");
			}

			this.analyzer = analyzer;
		}

		public Bitmap Process(Bitmap source, params IPerItemAlgorithm[] perItemAlgorithms)
		{
			LockableBitmap lockableBitmap = new LockableBitmap(source);
			lockableBitmap.LockBits();

			try
			{
				foreach (IPerItemAlgorithm algorithm in perItemAlgorithms)
				{
					for (int x = 0; x < lockableBitmap.Width; x++)
					{
						for (int y = 0; y < lockableBitmap.Height; y++)
						{
							Color processedColor = algorithm.Execute(lockableBitmap.GetPixel(x, y));
							lockableBitmap.SetPixel(x, y, processedColor);
						}
					}
				}	
			}
			finally
			{
				lockableBitmap.UnlockBits();
			}

			return source;
		}

		public IEnumerable<int> GenerateLuminancePoints(Bitmap source)
		{
			return analyzer.Calculate(source);
		}
	}
}
