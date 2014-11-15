namespace MISOI.Core.NeuralNetworks.Hopfield
{
	public interface IActivationFunction<TObject>
	{
		TObject Calculate(TObject source);
	}
}
