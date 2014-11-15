using System;
using System.Linq;

namespace MISOI.Core.NeuralNetworks.Hopfield.Implementation
{
	public class Pattern : IPattern<double>
	{
		public Pattern(int size)
		{
			Values = new double[size];
		}

		public Pattern(double[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			Values = values;
		}

		public double[] Values
		{
			get;
			set;
		}

		public INeuron<double>[] ToNeurons()
		{
			return Values.Select(e => new HopfieldNeuron(e)).ToArray();
		}
	}
}