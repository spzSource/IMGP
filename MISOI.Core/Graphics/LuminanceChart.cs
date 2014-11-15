using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace MISOI.Core.Graphics
{
	public class LuminanceChart : IChart 
	{
		private readonly string name;
		private readonly Chart sourceChart;
		private readonly int[] sourcePoints;

		public LuminanceChart(
			string name,
			Chart sourceChart,
			IEnumerable<int> sourcePoints)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (sourceChart == null)
			{
				throw new ArgumentNullException("sourceChart");
			}
			if (sourcePoints == null)
			{
				throw new ArgumentNullException("sourcePoints");
			}

			this.name = name;
			this.sourceChart = sourceChart;
			this.sourcePoints = sourcePoints.ToArray();
		}

		public void Draw()
		{
			sourceChart.Series.Clear();

			Series series = sourceChart.Series.Add(name);
			ConfigureSeries(series);

			for (int i = 0; i < sourcePoints.Count(); i++)
			{
				series.Points.Add(sourcePoints[i]);
			}

			sourceChart.Show();
		}

		public void Clear()
		{
			sourceChart.Invalidate();
		}

		private void ConfigureSeries(Series series)
		{
			series.IsVisibleInLegend = false;
			series.Color = Color.Coral;
			series.BorderWidth = 3;
		}
	}
}
