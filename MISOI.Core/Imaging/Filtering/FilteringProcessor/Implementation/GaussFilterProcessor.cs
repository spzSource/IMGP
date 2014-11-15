using System;
using System.Drawing;

using MISOI.Core.Imaging.Filtering.Filters;
using MISOI.Core.Imaging.Filtering.Filters.Implementation;
using MISOI.Core.Imaging.Unmanaged;
using MISOI.Logging;

namespace MISOI.Core.Imaging.Filtering.FilteringProcessor.Implementation
{
	public class GaussFilterProcessor : IMultiFilterProcessor
	{
		public Bitmap Process(Bitmap source, IMultiFilter filter)
		{
			Bitmap copiedSource = (Bitmap)source.Clone();
			LockableBitmap outBitmap = new LockableBitmap(copiedSource);
			outBitmap.LockBits();

			LockableBitmap processedBitmap = new LockableBitmap(source);
			processedBitmap.LockBits();

			try
			{
				GaussEnumerator enumerator = new GaussEnumerator(processedBitmap);

				while (enumerator.MoveNext())
				{
					try
					{
						Color processedColor = filter.Execute(enumerator.Current);

						ApertureItem processedApertureItem =
							enumerator.Current[enumerator.Current.GetLength(0) / 2, enumerator.Current.GetLength(0) / 2];

						ProcessOutput(processedApertureItem, processedColor, outBitmap);
					}
					catch (Exception exception)
					{
						Log.Current.Error(exception);
					}
				}
			}
			finally
			{
				processedBitmap.UnlockBits();
				outBitmap.UnlockBits();
			}

			return copiedSource;
		}

		private void ProcessOutput(ApertureItem processedApertureItem, Color processedColor, LockableBitmap outBitmap)
		{
			outBitmap.SetPixel(
				processedApertureItem.Coords.X,
				processedApertureItem.Coords.Y,
				processedColor);
		}
	}
}
