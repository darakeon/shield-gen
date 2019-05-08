using System;

namespace SvgGen.Lines
{
	class Coordinate
	{
		public UInt32 X { get; }
		public UInt32 Y { get; }

		public Coordinate(UInt32 x, UInt32 y)
		{
			X = x;
			Y = y;
		}

		public Coordinate(Double x, Double y)
			: this((UInt32)x, (UInt32)y) { }

		public Coordinate Average(Coordinate other)
		{
			return new Coordinate(
				(X + other.X) / 2,
				(Y + other.Y) / 2
			);
		}
	}
}
