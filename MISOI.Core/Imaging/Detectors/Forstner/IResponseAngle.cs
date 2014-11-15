namespace MISOI.Core.Imaging.Detectors.Forstner
{
	public interface IResponseAngle
	{
		double Calculate(double dx, double dy, double dxy);
	}
}
