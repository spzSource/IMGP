using System.Drawing;

namespace MISOI.Core.Imaging.Scanning.Sequential
{
	/// <summary>
	/// 
	/// </summary>
	public class ScanInfo
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="color"></param>
		/// <param name="coords"></param>
		public ScanInfo(Color color, Point coords)
		{
			Color = color;
			Coords = coords;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="coords"></param>
		/// <param name="color"></param>
		/// <param name="regionNumber"></param>
		/// <param name="isLabeled"></param>
		public ScanInfo(Point coords, Color color, int regionNumber, bool isLabeled)
		{
			Coords = coords;
			Color = color;
			RegionNumber = regionNumber;
			IsLabeled = isLabeled;
		}

		/// <summary>
		/// 
		/// </summary>
		public Point Coords
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		public Color Color
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		public int RegionNumber
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		public bool IsLabeled
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		public bool IsBoundary
		{
			get;
			set;
		}
	}
}
