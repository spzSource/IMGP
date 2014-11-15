using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using MISOI.Core.Imaging.Extensions;
using MISOI.Core.Imaging.Unmanaged;
using MISOI.Logging;

namespace MISOI.Core.Imaging.Scanning.Sequential
{
	public class SequentialScanner : IScanner
	{
		private readonly IDictionary<int, List<ScanInfo>> regions =
			new Dictionary<int, List<ScanInfo>>();

		public IDictionary<int, List<ScanInfo>> Scan(Bitmap source)
		{
			LockableBitmap lockableBitmap = new LockableBitmap(source);
			lockableBitmap.LockBits();
			LockableBitmapWrapper lockableBitmapWrapper = new LockableBitmapWrapper(lockableBitmap);

			try
			{
				SequentialEnumerator enumerator = new SequentialEnumerator(lockableBitmapWrapper);
				while (enumerator.MoveNext())
				{
					AbcMask mask = enumerator.Current;
					if (mask.Current.Color.IsWhite())
					{
						if (!mask.Left.IsLabeled && !mask.Top.IsLabeled)
						{
							if (regions.Count > 0)
							{
								mask.Current.RegionNumber = regions.Keys.Max() + 1;
							}
							else
							{
								mask.Current.RegionNumber = 0;
							}

							AddRegionAndMove(
								regionNumber: mask.Current.RegionNumber, 
								info: mask.Current);
						}
						else if (!mask.Left.IsLabeled && mask.Top.IsLabeled)
						{
							mask.Current.RegionNumber = mask.Top.RegionNumber;

							Move(regionNumber: mask.Current.RegionNumber, info: mask.Current);
						}
						else if (mask.Left.IsLabeled && !mask.Top.IsLabeled)
						{
							mask.Current.RegionNumber = mask.Left.RegionNumber;

							Move(regionNumber: mask.Current.RegionNumber, info: mask.Current);
						}
						else if (mask.Left.IsLabeled && mask.Top.IsLabeled)
						{
							if (mask.Left.RegionNumber == mask.Top.RegionNumber)
							{
								mask.Current.RegionNumber = mask.Left.RegionNumber;

								Move(regionNumber: mask.Current.RegionNumber, info: mask.Current);
							}
							else
							{
								ReAllocate(mask);
							}
						}
						mask.Current.IsLabeled = true;
					}
				}
			}
			finally
			{
				lockableBitmap.UnlockBits();
			}

			return regions;
		}

		private void AddRegionAndMove(int regionNumber, ScanInfo info)
		{
			List<ScanInfo> region = new List<ScanInfo> { info };
			if (!regions.ContainsKey(regionNumber))
			{
				regions.Add(regionNumber, region);
			}
			else
			{
				Log.Current.Error("Region with specified number has already been added.");
			}
		}

		private void Move(int regionNumber, ScanInfo info)
		{
			regions[regionNumber].Add(info);
		}

		private void ReAllocate(AbcMask mask)
		{
			if (mask.Left.RegionNumber > mask.Top.RegionNumber)
			{
				int indexToRemove = mask.Left.RegionNumber;

				regions[mask.Top.RegionNumber].AddRange(regions[mask.Left.RegionNumber]);

				foreach (ScanInfo info in regions[mask.Left.RegionNumber])
				{
					info.RegionNumber = mask.Top.RegionNumber;
				}

				regions.Remove(indexToRemove);


				mask.Current.RegionNumber = mask.Top.RegionNumber;
				Move(mask.Current.RegionNumber, mask.Current);
			}
			else
			{
				int indexToRemove = mask.Top.RegionNumber;

				regions[mask.Left.RegionNumber].AddRange(regions[mask.Top.RegionNumber]);
				
				foreach (ScanInfo info in regions[mask.Top.RegionNumber])
				{
					info.RegionNumber = mask.Left.RegionNumber;
				}

				regions.Remove(indexToRemove);


				mask.Current.RegionNumber = mask.Left.RegionNumber;
				Move(mask.Current.RegionNumber, mask.Current);
			}
		}
	}
}
