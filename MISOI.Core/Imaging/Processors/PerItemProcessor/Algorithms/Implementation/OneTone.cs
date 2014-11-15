using System.Drawing;
using MISOI.Core.Imaging.Extensions;

namespace MISOI.Core.Imaging.Processors.PerItemProcessor.Algorithms.Implementation
{
	public class OneTone : IPerItemAlgorithm
	{
		public Color Execute(Color color)
		{
			int brightness = color.Brightness();

			return Color.FromArgb(
				brightness,
				brightness,
				brightness);
		}
	}
}
