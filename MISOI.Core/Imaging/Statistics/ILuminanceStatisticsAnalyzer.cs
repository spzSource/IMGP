using System.Drawing;

namespace MISOI.Core.Imaging.Statistics
{
	public interface ILuminanceStatisticsAnalyzer
	{
		int[] Calculate(Bitmap bitmap);
	}
}
