namespace MISOI.Core.NeuralNetworks.Hopfield
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public interface INeuralNetwork<TObject>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pattern"></param>
		void Teach(IPattern<TObject> pattern);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		TObject[] Determine(IPattern<TObject> item);
	}
}