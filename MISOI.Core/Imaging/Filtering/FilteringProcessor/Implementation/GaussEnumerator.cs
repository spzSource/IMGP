using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

using MISOI.Core.Imaging.Filtering.Filters.Implementation;
using MISOI.Core.Imaging.Unmanaged;

namespace MISOI.Core.Imaging.Filtering.FilteringProcessor.Implementation
{
	public class GaussEnumerator : IEnumerator<ApertureItem[,]>
	{
		private int x;
		private int y;

		private readonly LockableBitmap source;

		private readonly Func<int, int, int, int, LockableBitmap, ApertureItem> apertureSelectorR =
			(sx, sy, x, y, source) =>
			{
				if (sx < 0 || sy < 0) return new ApertureItem(new Point(x, y), source.GetPixel(x, y));
				if (sx >= source.Width || sy >= source.Height) return new ApertureItem(new Point(x, y), source.GetPixel(x, y));
				return new ApertureItem(new Point(sx, sy), source.GetPixel(sx, sy));
			};

		public GaussEnumerator(LockableBitmap source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.source = source;
		}

		public ApertureItem[,] Current
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

		private ApertureItem[,] GenerateNext()
		{
			ApertureItem[,] aperture =
			{
				{
					apertureSelectorR.Invoke(x - 1, y - 1, x, y, source),
					apertureSelectorR.Invoke(x,     y - 1, x, y, source),
					apertureSelectorR.Invoke(x + 1, y - 1, x, y, source)
				},
				{
					apertureSelectorR.Invoke(x - 1, y,     x, y, source),
					apertureSelectorR.Invoke(x    , y    , x, y, source),					 
					apertureSelectorR.Invoke(x + 1, y,     x, y, source)
				},
				{
					apertureSelectorR.Invoke(x - 1, y + 1, x, y, source),
					apertureSelectorR.Invoke(x,     y + 1, x, y, source),
					apertureSelectorR.Invoke(x + 1, y + 1, x, y, source)
				}
			};
			return aperture;
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
