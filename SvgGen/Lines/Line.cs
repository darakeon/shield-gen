namespace SvgGen.Lines
{
	class Line
	{
		public Coordinate Start { get; }
		public Coordinate End { get; }

		public Line(Coordinate start, Coordinate end)
		{
			Start = start;
			End = end;
		}
	}
}
