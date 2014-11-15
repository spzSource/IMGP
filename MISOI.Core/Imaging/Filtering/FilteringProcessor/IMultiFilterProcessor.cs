using System.Drawing;

using MISOI.Core.Imaging.Filtering.Filters;

namespace MISOI.Core.Imaging.Filtering.FilteringProcessor
{
	public interface IMultiFilterProcessor
	{
		Bitmap Process(Bitmap source, IMultiFilter filter);
	}
}
