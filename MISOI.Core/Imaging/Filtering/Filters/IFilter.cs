using System.Drawing;

using MISOI.Core.Imaging.Filtering.Filters.Implementation;

namespace MISOI.Core.Imaging.Filtering.Filters
{
	public interface IFilter
	{
		Color Execute(Color color, ref Aperture aperture);
	}
}
