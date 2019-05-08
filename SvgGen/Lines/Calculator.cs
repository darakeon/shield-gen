using System;
using System.Collections.Generic;

namespace SvgGen.Lines
{
	class Calculator
	{
		public Calculator(UInt32 size)
		{
			var width = (Double)size;
			var height = size * Math.Sin(60 * Math.PI / 180);

			coordinates[0, 0] = new Coordinate(width * 3 / 8, height / 4);
			coordinates[0, 1] = new Coordinate(width / 4, height * 1 / 2);
			coordinates[0, 2] = new Coordinate(width / 8, height * 3 / 4);

			coordinates[1, 0] = new Coordinate(width / 4, height);
			coordinates[1, 1] = new Coordinate(width / 2, height);
			coordinates[1, 2] = new Coordinate(width * 3 / 4, height);

			coordinates[2, 0] = new Coordinate(width * 7 / 8, height * 3 / 4);
			coordinates[2, 1] = new Coordinate(width * 3 / 4, height * 1 / 2);
			coordinates[2, 2] = new Coordinate(width * 5 / 8, height / 4);

			Lines = new List<Line>();

			for (var s = 0; s < sides; s++)
			{
				for (var d = 0; d < dots; d++)
				{
					var start = coordinates[s, d];
					addLinesFor(s, start);
				}
			}
		}

		private void addLinesFor(Int32 ignore, Coordinate start)
		{
			for (var s = 0; s < sides; s++)
			{
				if (s == ignore) continue;

				for (var d = 0; d < dots; d++)
				{
					var projection = coordinates[s, d];
					var end = start.Average(projection);

					var line = new Line(start, end);

					Lines.Add(line);
				}
			}
		}

		private const Int32 sides = 3;
		private const Int32 dots = 3;
		private readonly Coordinate[,] coordinates = new Coordinate[3,3];

		public List<Line> Lines { get; }
	}
}
