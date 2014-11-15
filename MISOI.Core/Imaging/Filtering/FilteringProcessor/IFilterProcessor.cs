using System.Drawing;

using MISOI.Core.Imaging.Filtering.Filters;

namespace MISOI.Core.Imaging.Filtering.FilteringProcessor
{
	public interface IFilterProcessor
	{
		Bitmap Process(Bitmap source, IFilter filter);
	}
}
