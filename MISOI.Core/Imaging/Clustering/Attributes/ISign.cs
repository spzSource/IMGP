using MISOI.Core.Imaging.Scanning.Sequential;

namespace MISOI.Core.Imaging.Clustering.Attributes
{
	public interface ISign<out T>
	{
		T Calculate(Region region);
	}
}
