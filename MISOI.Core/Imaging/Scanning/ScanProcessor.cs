using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using MISOI.Core.Imaging.Extensions;
using MISOI.Core.Imaging.Scanning.Sequential;
using MISOI.Core.Imaging.Unmanaged;

using Region = MISOI.Core.Imaging.Scanning.Sequential.Region;

namespace MISOI.Core.Imaging.Scanning
{
	public class ScanProcessor : IScanProcessor
	{
		private readonly RandomColorGenerator colorGenerator = new RandomColorGenerator();

		public Region[] Process(Bitmap source, IScanner scanner, int threshold)
		{
			return scanner.Scan(source)
				.Where(region => region.Value.Count >= threshold)
				.Select(e => new Region(0, e.Value))
				.ToArray();
		}

		public Bitmap MarkRegions(Bitmap source, IScanner scanner, int threshold)
		{
			IDictionary<int, List<ScanInfo>> regions = scanner.Scan(source);

			LockableBitmap lockableBitmap = new LockableBitmap(source);
			lockableBitmap.LockBits();

			try
			{
				int regionIndex = 1;
			
				foreach (KeyValuePair<int, List<ScanInfo>> region in 
					regions.Where(region => region.Value.Count >= threshold))
				{
					FillRegion(regionIndex, region, lockableBitmap);

					regionIndex++;
				}
			}
			finally
			{
				lockableBitmap.UnlockBits();
			}

			return source;
		}

		private void FillRegion(
			int regionIndex, 
			KeyValuePair<int, List<ScanInfo>> region, 
			LockableBitmap lockableBitmap)
		{
			Color color;

			if (!ColorMapper.TryMap(regionIndex, out color))
			{
				color = colorGenerator.RandomColor();
			}

			foreach (ScanInfo info in region.Value)
			{
				lockableBitmap.SetPixel(info.Coords.X, info.Coords.Y, color);
			}
		}
	}
}
