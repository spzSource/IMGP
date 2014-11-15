using System.Drawing;

namespace MISOI.Core.Imaging.Processors.PerItemProcessor.Algorithms
{
	public interface IPerItemAlgorithm
	{
		Color Execute(Color color);
	}
}
