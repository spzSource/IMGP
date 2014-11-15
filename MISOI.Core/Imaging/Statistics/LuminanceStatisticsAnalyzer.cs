using System.Drawing;

using MISOI.Core.Imaging.Extensions;
using MISOI.Core.Imaging.Unmanaged;

namespace MISOI.Core.Imaging.Statistics
{
	public class LuminanceStatisticsAnalyzer : ILuminanceStatisticsAnalyzer
	{
		public int[] Calculate(Bitmap bitmap)
		{
			int[] lumininances = new int[256];

			LockableBitmap lockableBitmap = new LockableBitmap(bitmap);
			lockableBitmap.LockBits();

			try
			{
				for (int i = 0; i < lockableBitmap.Width; i++)
				{
					for (int j = 0; j < lockableBitmap.Height; j++)
					{
						byte brightness = lockableBitmap.GetPixel(i, j).Brightness();
						lumininances[brightness]++;
					}
				}
			}
			finally
			{
				lockableBitmap.UnlockBits();
			}
			return lumininances;
		}
	}
}
