namespace MISOI.Core.NeuralNetworks.Hopfield
{
	public interface IPattern<TObject>
	{
		TObject[] Values
		{
			get;
			set;
		}

		INeuron<TObject>[] ToNeurons();
	}
}