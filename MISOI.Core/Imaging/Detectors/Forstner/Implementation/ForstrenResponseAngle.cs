namespace MISOI.Core.Imaging.Detectors.Forstner.Implementation
{
	public class ForstrenResponseAngle : IResponseAngle
	{
		public double Calculate(double dx, double dy, double dxy)
		{
			double a = dx * dx;
			double b = dy * dy;

			double det = a * b;
			double trace = a + b;

			return det / trace;
		}
	}
}
