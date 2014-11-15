using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using MISOI.Core.Imaging.Processors.PerItemProcessor.Algorithms;
using MISOI.Core.Imaging.Unmanaged;
using MISOI.Core.NeuralNetworks.Hopfield;
using MISOI.Core.NeuralNetworks.Hopfield.Implementation;

namespace MISOI.Core.NeuralNetworks.Common.Implementation
{
	public class HopfieldNeuralNetworkProcessor : INeuralNetworkProcessor
	{
		private readonly IPerItemAlgorithm perItemAlgorithm;
		private readonly INeuralNetwork<double> hopfieldNeuralNetwork;

		public HopfieldNeuralNetworkProcessor(
			IPerItemAlgorithm perItemAlgorithm,
			INeuralNetwork<double> hopfieldNeuralNetwork)
		{
			if (perItemAlgorithm == null)
			{
				throw new ArgumentNullException("perItemAlgorithm");
			}
			if (hopfieldNeuralNetwork == null)
			{
				throw new ArgumentNullException("hopfieldNeuralNetwork");
			}
			this.perItemAlgorithm = perItemAlgorithm;
			this.hopfieldNeuralNetwork = hopfieldNeuralNetwork;
		}

		public void AddPattern(Bitmap source)
		{
			LockableBitmap lockableBitmap = new LockableBitmap(source);
			lockableBitmap.LockBits();

			try
			{
				IPattern<double> pattern = RetrievePatternValue(lockableBitmap);
				hopfieldNeuralNetwork.Teach(pattern);
			}
			finally
			{
				lockableBitmap.UnlockBits();
			}
		}

		private IPattern<double> RetrievePatternValue(LockableBitmap lockableBitmap)
		{
			List<double> patternValues = new List<double>();

			for (int x = 0; x < lockableBitmap.Width; x++)
			{
				for (int y = 0; y < lockableBitmap.Height; y++)
				{
					Color processedColor = perItemAlgorithm
						.Execute(lockableBitmap.GetPixel(x, y));

					patternValues.Add(
						ColorToBinary(processedColor));
				}
			}

			return new Pattern(patternValues.ToArray());
		}

		private double ColorToBinary(Color color)
		{
			return color.ToArgb().Equals(Color.Black.ToArgb()) ? 1 : 0;
		}

		private Color BinaryToColor(double source)
		{
			return Math.Abs(source - 1) < 0.0000001 ? Color.Black : Color.White;
		}

		public Bitmap Process(Bitmap sourceToProcess)
		{
			Bitmap copiedSource = (Bitmap)sourceToProcess.Clone();

			LockableBitmap outBitmap = new LockableBitmap(copiedSource);
			outBitmap.LockBits();

			LockableBitmap lockableBitmap = new LockableBitmap(sourceToProcess);
			lockableBitmap.LockBits();

			try
			{
				IPattern<double> pattern = RetrievePatternValue(lockableBitmap);
				double[] processedPattern = hopfieldNeuralNetwork.Determine(pattern);

				ApplyPatternValues(processedPattern, outBitmap);
			}
			finally
			{
				lockableBitmap.UnlockBits();
				outBitmap.UnlockBits();
			}

			return copiedSource;
		}

		private void ApplyPatternValues(IEnumerable<double> processedPattern, LockableBitmap source)
		{
			Color[] colors = processedPattern.Select(BinaryToColor).ToArray();

			int colorIndex = 0;

			for (int x = 0; x < source.Width; x++)
			{
				for (int y = 0; y < source.Height; y++)
				{
					source.SetPixel(x, y, colors[colorIndex]);
	
					colorIndex++;
				}
			}
		}
	}
}