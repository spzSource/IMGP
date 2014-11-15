namespace MISOI.Core.NeuralNetworks.Hopfield
{
	public interface INeuron<TObject>
	{
		TObject State
		{
			get;
			set;
		}
	}
}