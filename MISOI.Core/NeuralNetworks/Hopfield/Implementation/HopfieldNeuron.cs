namespace MISOI.Core.NeuralNetworks.Hopfield.Implementation
{
	public class HopfieldNeuron : INeuron<double>
	{
		public double State
		{
			get;
			set;
		}

		public HopfieldNeuron()
		{
		}

		public HopfieldNeuron(double state)
		{
			State = state;
		}
	}
}