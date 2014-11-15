using System.Drawing;

namespace MISOI.Core.NeuralNetworks.Common
{
	/// <summary>
	/// 
	/// </summary>
	public interface INeuralNetworkProcessor
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		void AddPattern(Bitmap source);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sourceToProcess"></param>
		Bitmap Process(Bitmap sourceToProcess);
	}
}