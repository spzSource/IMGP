using System.Drawing;

using Region = MISOI.Core.Imaging.Scanning.Sequential.Region;

namespace MISOI.Core.Imaging.Scanning
{
	public interface IScanProcessor
	{
		Region[] Process(Bitmap source, IScanner scanner, int threshold);

		Bitmap MarkRegions(Bitmap source, IScanner scanner, int threshold);
	}
}
