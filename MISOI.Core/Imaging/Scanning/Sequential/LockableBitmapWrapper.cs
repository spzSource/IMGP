using System;
using System.Drawing;

using MISOI.Core.Imaging.Unmanaged;

namespace MISOI.Core.Imaging.Scanning.Sequential
{
	public class LockableBitmapWrapper
	{
		private readonly ScanInfo[,] infos; 
		private readonly LockableBitmap source;

		public int Width
		{
			get
			{
				return source.Width;
			}
		}

		public int Height
		{
			get
			{
				return source.Height;
			}
		}

		public LockableBitmapWrapper(LockableBitmap source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.source = source;

			infos = new ScanInfo[source.Width, source.Height];

			for (int y = 0; y < source.Height; y++)
			{
				for (int x = 0; x < source.Width; x++)
				{
					infos[x, y] = new ScanInfo(source.GetPixel(x, y), new Point(x, y));
				}
			}
		}
		
		public void LockBits()
		{
			source.LockBits();
		}

		public void UnlockBits()
		{
			source.UnlockBits();
		}

		public Color GetPixel(int x, int y)
		{
			return source.GetPixel(x, y);
		}

		public ScanInfo GetScanInfo(int x, int y)
		{
			return infos[x, y];
		}

		public void SetScanInfo(int x, int y, ScanInfo scanInfo)
		{
			infos[x, y] = scanInfo;
		}
	}
}
