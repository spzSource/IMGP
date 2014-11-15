using System;

using MathNet.Numerics.LinearAlgebra;

namespace MISOI.Core.NeuralNetworks.Hopfield.Implementation
{
	public class HopfieldNetwork : INeuralNetwork<double>
	{
		private Matrix<double> weights;
		private const double EpsState = 0.0000001;

		private readonly int neuronsCount;
		private readonly int iterationLimit;
		private readonly IActivationFunction<double> activationFunction;

		private readonly Func<double, double> binaryToBipolar = 
			source => Math.Abs(source - 1) < EpsState ? -1.0 : 1.0;
		private readonly Func<double, double> bipolarToBinary =
			source => source < 0 ? 1.0 : 0.0;
		
		public HopfieldNetwork(
			int neuronsCount,
			int iterationLimit,
			IActivationFunction<double> activationFunction)
		{
			if (activationFunction == null)
			{
				throw new ArgumentNullException("activationFunction");
			}
			this.neuronsCount = neuronsCount;
			this.iterationLimit = iterationLimit;
			this.activationFunction = activationFunction;

			weights = Matrix<double>.Build.Dense(neuronsCount, neuronsCount);
		}

		public void Teach(IPattern<double> pattern)
		{
			Vector<double> patternVector = ConvertToVector(pattern)
				.Map(binaryToBipolar, Zeros.Include);

			Matrix<double> rowMatrix = patternVector.ToRowMatrix();
			Matrix<double> columnMatrix = patternVector.ToColumnMatrix();
			Matrix<double> contributionMatrix = columnMatrix.Multiply(rowMatrix);

			weights = weights.Add(contributionMatrix);
				
			weights.SetDiagonal(
				Vector<double>.Build.Dense(neuronsCount, default(double)));
		}

		private Vector<double> ConvertToVector(IPattern<double> pattern)
		{
			Vector<double> patternVector = Vector<double>.Build.Dense(pattern.Values)
				.Map(binaryToBipolar);

			return patternVector;
		}

		public double[] Determine(IPattern<double> item)
		{
			int iterationCount = 0;
			
			bool stateChanged = true;
			
            Vector<double> patternVector = ConvertToVector(item);

			while (stateChanged)
			{
				Vector<double> newPatternVector = patternVector.Clone();

				stateChanged = ProcessPattern(patternVector, newPatternVector);
				
				patternVector = newPatternVector;

				if (++iterationCount > iterationLimit)
					break;
			}

			return patternVector.Map(e => bipolarToBinary(e), Zeros.Include).ToArray();
		}

		private bool ProcessPattern(Vector<double> patternVector, Vector<double> newPatternVector)
		{
			bool result = false;

			for (int neuronIndex = 0; neuronIndex < neuronsCount; neuronIndex++)
			{
				double newState = CalculateNewState(neuronIndex, patternVector);

				bool changed = IsStateChanged(patternVector[neuronIndex], newState);

				result = result | changed;

				newPatternVector[neuronIndex] = newState;
			}

			return result;
		}

		private bool IsStateChanged(double oldState, double newState)
		{
			return Math.Abs(oldState - newState) > EpsState;
		}

		private double CalculateNewState(int neuronIndex, Vector<double> patternVector)
		{
			Vector<double> weightsRow = weights.Row(neuronIndex);
			Vector<double> newNeuronStateVector = weightsRow
				.PointwiseMultiply(patternVector);

			return activationFunction.Calculate(newNeuronStateVector.Sum());
		}
	}
}
