namespace MISOI.Core.Imaging.Scanning.Sequential
{
	/// <summary>
	///		 C
	///      |
	/// B -- A
	/// </summary>
	internal class AbcMask
	{
		public ScanInfo Top
		{
			get;
			set;
		}

		public ScanInfo Current
		{
			get;
			set;
		}

		public ScanInfo Left
		{
			get;
			set;
		}
	}
}
