using System.Collections.Generic;
using System.Drawing;
using MISOI.Core.Imaging.Processors.PerItemProcessor.Algorithms;

namespace MISOI.Core.Imaging.Processors
{
	/// <summary>
	/// 
	/// </summary>
	public interface ISourceImageProcessor
	{
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		Bitmap Process(Bitmap source, params IPerItemAlgorithm[] perItemAlgorithms);

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		IEnumerable<int> GenerateLuminancePoints(Bitmap source);
	}
}
