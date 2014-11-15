using System.Drawing;

using MISOI.Core.Imaging.Filtering.Filters.Implementation;

namespace MISOI.Core.Imaging.Filtering.Filters
{
	public interface IMultiFilter
	{
		Color Execute(ApertureItem[,] aperture);
	}
}
