using System;
using System.Collections;
using System.Collections.Generic;

using MISOI.Core.Imaging.Extensions;

namespace MISOI.Core.Imaging.Scanning.Sequential
{
	internal class SequentialEnumerator : IEnumerator<AbcMask>
	{
		private int x;
		private int y;

		private readonly LockableBitmapWrapper source;

		private readonly Func<int, int, int, int, LockableBitmapWrapper, ScanInfo> maskSelector =
			(sx, sy, x, y, source) =>
			{
				if (sx < 0 || sy < 0) return source.GetScanInfo(x, y);
				if (sx >= source.Width || sy >= source.Height) return source.GetScanInfo(x, y);
				return source.GetScanInfo(sx, sy);
			};

		public SequentialEnumerator(LockableBitmapWrapper source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.source = source;
		}

		public AbcMask Current
		{
			get;
			private set;
		}

		object IEnumerator.Current
		{
			get
			{
				return Current;
			}
		}

		private AbcMask GenerateNext()
		{
			AbcMask mask = new AbcMask
			{
				Top  = maskSelector.Invoke(x - 1, y,     x, y, source),
				Left = maskSelector.Invoke(x,     y - 1, x, y, source),
				Current = source.GetScanInfo(x, y)
			};

			mask.Current.IsBoundary = DetermineBoundary(x, y);

			return mask;
		}

		public bool MoveNext()
		{
			Current = GenerateNext();

			x++;
			if (x == source.Width)
			{
				y++;
				x = 0;
			}

			return x != source.Width && y != source.Height;
		}

		private bool DetermineBoundary(int cx, int cy)
		{
			ScanInfo left   = maskSelector.Invoke(cx - 1, cy,     cx, cy, source);
			ScanInfo right  = maskSelector.Invoke(cx + 1, cy,     cx, cy, source);
			ScanInfo top    = maskSelector.Invoke(cx,     cy - 1, cx, cy, source);
			ScanInfo bottom = maskSelector.Invoke(cx,     cy + 1, cx, cy, source);
			ScanInfo center = maskSelector.Invoke(cx,     cy,     cx, cy, source);

			return center.Color.IsWhite() &&
			       (!left.Color.IsWhite() 
						|| !right.Color.IsWhite() 
						|| !top.Color.IsWhite() 
						|| !bottom.Color.IsWhite());
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}

		public void Dispose()
		{
			if (source != null)
			{
				source.UnlockBits();
			}
		}
	}
}
