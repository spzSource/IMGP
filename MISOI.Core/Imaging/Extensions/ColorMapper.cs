using System.Collections.Generic;
using System.Drawing;

namespace MISOI.Core.Imaging.Extensions
{
	internal class ColorMapper
	{
		private readonly static IDictionary<int, Color> mappings =
			new Dictionary<int, Color>
			{
				{0, Color.DodgerBlue},
				{1, Color.Coral},
				{2, Color.Brown},
				{3, Color.DarkCyan},
				{4, Color.DarkMagenta},
				{5, Color.DarkSlateBlue},
				{6, Color.DarkTurquoise},
				{7, Color.Gold},
				{8, Color.Fuchsia},
				{9, Color.LightGreen},
				{10, Color.DarkGray},
				{11, Color.Beige},
				{12, Color.Blue},
				{13, Color.Crimson},
				{14, Color.Tomato}
			};

		public static bool TryMap(int index, out Color color)
		{
			return mappings.TryGetValue(index, out color);
		}
	}
}
