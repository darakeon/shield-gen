using System;

namespace SvgGen.Lines
{
	class Line
	{
		public Coordinate Start { get; }
		public Coordinate End { get; }
		public String Color { get; }

		public Line(Coordinate start, Coordinate end, String color)
		{
			Start = start;
			End = end;
			Color = color;
		}
	}
}
