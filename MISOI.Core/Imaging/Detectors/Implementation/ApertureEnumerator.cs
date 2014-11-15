using System;
using System.Collections;
using System.Collections.Generic;

namespace MISOI.Core.Imaging.Detectors.Implementation
{
	internal class ApertureEnumerator : IEnumerator<List<СharacteristicPoint>>
	{
		private int x;
		private int y;
		
		private readonly СharacteristicPoint[,] source;
		private readonly int apertureSize;
		private readonly int apertureCenter;

		private readonly Func<int, int, int, int, СharacteristicPoint[,], СharacteristicPoint> itemSelector =
			(sx, sy, x, y, source) =>
			{
				if (sx < 0 || sy < 0) return source[x, y];
				if (sx >= source.GetLength(0) || sy >= source.GetLength(1)) return source[x, y];
				return source[sx, sy];
			};

		public List<СharacteristicPoint> Current
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

		public ApertureEnumerator(СharacteristicPoint[,] source, int apertureSize)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.source = source;
			this.apertureSize = apertureSize;

			x = apertureSize;
			y = apertureSize;
			apertureCenter = apertureSize / 2;
			}

		private List<СharacteristicPoint> GenerateNext()
		{
			List<СharacteristicPoint> points = new List<СharacteristicPoint>();

			for (int i = 0; i < apertureSize; i++)
			{
				for (int j = 0; j < apertureSize; j++)
				{
					int indexX = x + (i - apertureCenter);
					int indexY = y + (j - apertureCenter);

					points.Add(itemSelector.Invoke(indexX, indexY, x, y, source));
				}
			}

			return points;
		}

		public bool MoveNext()
		{
			Current = GenerateNext();
			x += apertureSize;
			if (x >= source.GetLength(0))
			{
				y += apertureSize;
				x = apertureSize;
			}
			return x < source.GetLength(0) && y < source.GetLength(1);
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}

		public void Dispose()
		{
		}
	}
}
