using System.Drawing;

namespace MISOI.Core.Imaging.Filtering.Filters.Implementation
{
	public class MedianFilter : IFilter
	{
		public Color Execute(Color color, ref Aperture aperture)
		{
			return aperture.GetMedian();
		}
	}
}
