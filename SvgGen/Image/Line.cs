using System;

namespace SvgGen.Image
{
	class Line
	{
		public Coordinate Start { get; }
		public Coordinate End { get; }
		public String Color { get; }

		public Line(Coordinate start, Coordinate end)
		{
			Start = start;
			End = end;
		}

		public Line(Coordinate start, Coordinate end, String color)
			: this(start, end)
		{
			Color = color;
		}

		public Coordinate Cross(Line other)
		{
			var coordinate = getCrossAnywhere(other);

			var belongsToBoth = belongs(coordinate)
				&& other.belongs(coordinate);
			
			return belongsToBoth ? coordinate : null;
		}

		private Coordinate getCrossAnywhere(Line other)
		{
			var x1 = (Decimal) Start.X;
			var x2 = (Decimal) End.X;
			var dx21 = x2 - x1;

			var x3 = (Decimal) other.Start.X;
			var x4 = (Decimal) other.End.X;
			var dx43 = x4 - x3;

			var y1 = (Decimal) Start.Y;
			var y2 = (Decimal) End.Y;
			var dy21 = y2 - y1;

			var y3 = (Decimal) other.Start.Y;
			var y4 = (Decimal) other.End.Y;
			var dy43 = y4 - y3;

			var x = getX(
				x1, y1, x2, y2, x3, y3, x4, y4,
				dx21, dy21, dx43, dy43
			);

			// Look at the getX formulas
			var y = dx21 != 0
				? y2 - (x2 - x) / dx21 * dy21
				: y4 - (x4 - x) / dx43 * dy43;

			return new Coordinate(x, y);
		}

		private static Decimal getX(
			Decimal x1, Decimal y1,
			Decimal x2, Decimal y2,
			Decimal x3, Decimal y3,
			Decimal x4, Decimal y4,
			Decimal dx21, Decimal dy21,
			Decimal dx43, Decimal dy43)
		{
			// if the difference is zero, X never changes

			if (dx21 == 0)
				return x1;

			if (dx43 == 0)
				return x3;

			/*
			 * (x2 - x) / (x2 - x1) = (y2 - y) / (y2 - y1)
			 * (x4 - x) / (x4 - x3) = (y4 - y) / (y4 - y3)
			 *
			 * (x2 - x) / dx21 = (y2 - y) / dy21
			 * (x4 - x) / dx43 = (y4 - y) / dy43
			 *
			 * (x2 - x) / dx21 * dy21 = y2 - y
			 * (x4 - x) / dx43 * dy43 = y4 - y
			 *
			 * y = y2 - (x2 - x) / dx21 * dy21
			 * y = y4 - (x4 - x) / dx43 * dy43
			 *
			 * y = y2 - (x2 - x) * dy21 / dx21
			 * y = y4 - (x4 - x) * dy43 / dx43
			 *
			 * y = (y2 * dx21 - (x2 - x) * dy21) / dx21
			 * y = (y4 * dx43 - (x4 - x) * dy43) / dx43
			 *
			 * y = (y2 * dx21 + (x - x2) * dy21) / dx21
			 * y = (y4 * dx43 + (x - x4) * dy43) / dx43
			 *
			 * y = (y2 * dx21 + x * dy21 - x2 * dy21) / dx21
			 * y = (y4 * dx43 + x * dy43 - x4 * dy43) / dx43
			 *
			 * y = (x * dy21 + y2 * dx21 - x2 * dy21) / dx21
			 * y = (x * dy43 + y4 * dx43 - x4 * dy43) / dx43
			 *
			 * (x * dy21 + y2 * dx21 - x2 * dy21) / dx21 =
			 * (x * dy43 + y4 * dx43 - x4 * dy43) / dx43
			 *
			 * (x * dy21 + y2 * dx21 - x2 * dy21) * dx43 =
			 * (x * dy43 + y4 * dx43 - x4 * dy43) * dx21
			 *
			 * x * dy21 * dx43 + (y2 * dx21 - x2 * dy21) * dx43 =
			 * x * dy43 * dx21 + (y4 * dx43 - x4 * dy43) * dx21
			 *
			 * x * dy21 * dx43 =
			 * x * dy43 * dx21 + (y4 * dx43 - x4 * dy43) * dx21 - (y2 * dx21 - x2 * dy21) * dx43
			 *
			 * x * dy21 * dx43 - x * dy43 * dx21 =
			 * (y4 * dx43 - x4 * dy43) * dx21 - (y2 * dx21 - x2 * dy21) * dx43
			 *
			 * x * [(dy21 * dx43) - (dy43 * dx21)] =
			 * (y4 * dx43 - x4 * dy43) * dx21 - (y2 * dx21 - x2 * dy21) * dx43
			 *
			 * x * [(dy21 * dx43) - (dy43 * dx21)] =
			 * (y4 * dx43 - x4 * dy43) * dx21 - (y2 * dx21 - x2 * dy21) * dx43
			 *
			 * x * [(dy21 * dx43) - (dy43 * dx21)] =
			 * (y4 * (x4 - x3) - x4 * (y4 - y3)) * dx21 - (y2 * (x2 - x1) - x2 * (y2 - y1)) * dx43
			 *
			 * x * [(dy21 * dx43) - (dy43 * dx21)] =
			 * (y4 * (x4 - x3) + x4 * (y3 - y4)) * dx21 - (y2 * (x2 - x1) + x2 * (y1 - y2)) * dx43
			 *
			 * x * [(dy21 * dx43) - (dy43 * dx21)] =
			 * (y4 * x4 - y4 * x3 + x4 * y3 - x4 * y4) * dx21 - (y2 * x2 - y2 * x1 + x2 * y1 - x2 * y2) * dx43
			 *
			 * x * [(dy21 * dx43) - (dy43 * dx21)] =
			 * (- y4 * x3 + x4 * y3) * dx21 - (- y2 * x1 + x2 * y1) * dx43
			 *
			 * x * [(dy21 * dx43) - (dy43 * dx21)] =
			 * (x4 * y3 - y4 * x3) * dx21 - (x2 * y1 - y2 * x1) * dx43
			 *
			 * e1 = [(dy21 * dx43) - (dy43 * dx21)];
			 * e2 = (x4 * y3 - y4 * x3) * dx21;
			 * e3 = (x2 * y1 - y2 * x1) * dx43;
			 *
			 * x * e1 = e2 - e3
			 *
			 * x = (e2 - e3) / e1
			 */

			var e1 = dy21 * dx43 - dy43 * dx21;
			var e2 = (x4 * y3 - y4 * x3) * dx21;
			var e3 = (x2 * y1 - y2 * x1) * dx43;
			var x = (e2 - e3) / e1;
			return x;
		}

		private Boolean belongs(Coordinate coordinate)
		{
			var maxX = Math.Max(Start.X, End.X);
			var minX = Math.Min(Start.X, End.X);
			var maxY = Math.Max(Start.Y, End.Y);
			var minY = Math.Min(Start.Y, End.Y);
			
			return coordinate.X >= minX
				&& coordinate.X <= maxX
				&& coordinate.Y >= minY
				&& coordinate.Y <= maxY;
		}

		public override string ToString()
		{
			return $"{Start}>{End}";
		}
	}
}
