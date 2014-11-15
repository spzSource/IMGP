using System;
using System.Drawing;

using MISOI.Core.Imaging.Filtering.Filters;
using MISOI.Core.Imaging.Filtering.Filters.Implementation;
using MISOI.Core.Imaging.Unmanaged;
using MISOI.Logging;

namespace MISOI.Core.Imaging.Filtering.FilteringProcessor.Implementation
{
	public class FilterProcessor : IFilterProcessor
	{
		public Bitmap Process(Bitmap source, IFilter filter)
		{
			Bitmap copiedSource = (Bitmap)source.Clone();
			LockableBitmap outBitmap = new LockableBitmap(copiedSource);
			outBitmap.LockBits();

			LockableBitmap processedBitmap = new LockableBitmap(source);
			processedBitmap.LockBits();

			try
			{	
				SobelImageEnumerator enumerator = new SobelImageEnumerator(processedBitmap);
				
				while (enumerator.MoveNext())
				{
					try
					{
						ExecuteStep(enumerator.Current, filter, processedBitmap, outBitmap);
					}
					catch (Exception ex)
					{
						Log.Current.Error(ex);
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

		protected virtual void ExecuteStep(Aperture aperture, IFilter filter, LockableBitmap processed, LockableBitmap outBitmap)
		{
			Color filteredColor = filter.Execute(aperture.CurrentColor, ref aperture);

			ProcessOutput(aperture, outBitmap, filteredColor);
		}

		protected static void ProcessOutput(Aperture aperture, LockableBitmap outBitmap, Color filteredColor)
		{
			outBitmap.SetPixel(aperture.CurrentX, aperture.CurrentY, filteredColor);
		}
	}
}
