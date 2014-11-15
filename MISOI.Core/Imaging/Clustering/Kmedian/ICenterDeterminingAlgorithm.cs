namespace MISOI.Core.Imaging.Clustering.Kmedian
{
	public interface ICenterDeterminingAlgorithm<TObject>
	{
		TObject Calculate(TObject[] source);
	}
}
