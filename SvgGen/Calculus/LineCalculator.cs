using System;
using System.Collections.Generic;
using SvgGen.Image;

namespace SvgGen.Calculus
{
	abstract class LineCalculator : Calculator
	{
		protected LineCalculator(Boolean isFinal, UInt32 size) : base(isFinal, size) { }

		public override IList<Line> GetLines()
		{
			var lines = new List<Line>();

			var colors = getColors(mainColors);

			for (var s = 0; s < sides; s++)
			{
				for (var d = 0; d < dots; d++)
				{
					var start = coordinates[s, d];
					var color = colors[s * sides + d];

					lines.AddRange(getLinesFor(s, start, color));
				}
			}

			return lines;
		}

		private IList<Line> getLinesFor(Int32 ignore, Coordinate start, String color)
		{
			var lines = new List<Line>();

			for (var s = 0; s < sides; s++)
			{
				if (s == ignore) continue;

				for (var d = 0; d < dots; d++)
				{
					var projection = coordinates[s, d];
					var end = start.Average(projection);

					var line = new Line(start, end, color);

					lines.Add(line);
				}
			}

			return lines;
		}

		protected abstract IList<String> getColors(IList<String> mainColors);

		private List<String> mainColors = new List<String>
		{
			"990000", "000000", "990099"
		};
	}
}
