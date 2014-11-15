using System.Collections.Generic;
using System.Drawing;

using MISOI.Core.Imaging.Scanning.Sequential;

namespace MISOI.Core.Imaging.Scanning
{
	/// <summary>
	/// 
	/// </summary>
	public interface IScanner
	{
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		IDictionary<int, List<ScanInfo>> Scan(Bitmap source);
	}
}
