using System.Collections.Generic;

namespace MISOI.Core.Imaging.Scanning.Sequential
{
	public class Region
	{
		public Region(int number, List<ScanInfo> infos)
		{
			Number = number;
			Infos = infos;
		}

		public int Number
		{
			get;
			set;
		}

		public bool IsCenter
		{
			get;
			set;
		}

		public List<ScanInfo> Infos
		{
			get;
			set;
		}
	}
}
