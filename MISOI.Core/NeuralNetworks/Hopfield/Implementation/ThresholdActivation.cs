namespace MISOI.Core.NeuralNetworks.Hopfield.Implementation
{
	public class ThresholdActivation : IActivationFunction<double>
	{
		public double Calculate(double source)
		{
			return source < 0 ? -1.0 : 1.0;
		}
	}
}
