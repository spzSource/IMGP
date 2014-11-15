using System.Drawing;

namespace MISOI.Core.Imaging.Detectors
{
	public interface IPointsDetectProcessor
	{
		Bitmap Process(Bitmap source, IPointsDetector detector);
	}
}
