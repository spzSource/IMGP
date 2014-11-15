namespace MISOI.Core.Imaging.Processors.PerItemProcessor
{
	public class PerItemProcessorParameters
	{
		private readonly byte left;
		private readonly byte right;
		private readonly byte threshold;

		public PerItemProcessorParameters(
			byte left,
			byte right,
			byte threshold)
		{
			this.left = left;
			this.right = right;
			this.threshold = threshold;
		}
	}
}
