using MISOI.Core.Imaging.Detectors.Implementation;
using MISOI.Core.Imaging.Unmanaged;

namespace MISOI.Core.Imaging.Detectors
{
	public interface IPointsDetector
	{
		СharacteristicPoint[] Detect(LockableBitmap source);
	}
}
