using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

using MISOI.Core.Imaging.Filtering.Filters.Implementation;
using MISOI.Core.Imaging.Unmanaged;

namespace MISOI.Core.Imaging.Filtering.FilteringProcessor.Implementation
{
	public class SobelImageEnumerator : IEnumerator<Aperture>
	{
		private int x;
		private int y;

		private readonly LockableBitmap source;

		private readonly Func<int, int, int, int, LockableBitmap, Color> apertureSelectorR =
			(sx, sy, x, y, source) =>
			{
				if (sx < 0 || sy < 0) return source.GetPixel(x, y);
				if (sx >= source.Width || sy >= source.Height) return source.GetPixel(x, y);
				return source.GetPixel(sx, sy);
			};

		public SobelImageEnumerator(LockableBitmap source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.source = source;
		}

		public Aperture Current
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

		private Aperture GenerateNext()
		{
			Aperture aperture = new Aperture
			{
				A1 = apertureSelectorR.Invoke(x - 1, y - 1, x, y, source),
				A2 = apertureSelectorR.Invoke(x,     y - 1, x, y, source),
				A3 = apertureSelectorR.Invoke(x + 1, y - 1, x, y, source),
				A4 = apertureSelectorR.Invoke(x - 1, y,     x, y, source),
									 
				A5 = apertureSelectorR.Invoke(x + 1, y,     x, y, source),
				A6 = apertureSelectorR.Invoke(x - 1, y + 1, x, y, source),
				A7 = apertureSelectorR.Invoke(x,     y + 1, x, y, source),
				A8 = apertureSelectorR.Invoke(x + 1, y + 1, x, y, source),

				CurrentX = x,
				CurrentY = y,
				CurrentColor = source.GetPixel(x, y)
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
