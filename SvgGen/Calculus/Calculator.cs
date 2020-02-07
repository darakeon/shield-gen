using System;
using System.Collections.Generic;
using System.Linq;
using SvgGen.Image;
using SvgGen.Parameters;

namespace SvgGen.Calculus
{
	abstract class Calculator
	{
		public static IList<Calculator> New(UInt32 size, Kind kind)
		{
			return @new(size, kind).ToList();
		}

		private static IEnumerable<Calculator> @new(UInt32 size, Kind kind)
		{
			if (kind == Kind.None)
				throw new SVGGException("Choose some kind of shield!");

			if (kind.HasFlag(Kind.Filled))
				yield return new Filled(size);

			if (kind.HasFlag(Kind.Grouped))
				yield return new Grouped(size);

			if (kind.HasFlag(Kind.Interpolated))
				yield return new Interpolated(size);
		}

		protected Calculator(UInt32 size)
		{
			this.size = size;

			var width = (Decimal)size;
			var height = (Decimal)(size * Math.Sin(60 * Math.PI / 180));

			coordinates[0, 0, 0] = new Coordinate(width * 3 / 8, height * 1 / 4);
			coordinates[0, 0, 1] = new Coordinate(width * 2 / 8, height * 2 / 4);
			coordinates[0, 0, 2] = new Coordinate(width * 1 / 8, height * 3 / 4);

			coordinates[0, 1, 0] = new Coordinate(width * 2 / 8, height * 4 / 4);
			coordinates[0, 1, 1] = new Coordinate(width * 4 / 8, height * 4 / 4);
			coordinates[0, 1, 2] = new Coordinate(width * 6 / 8, height * 4 / 4);

			coordinates[0, 2, 0] = new Coordinate(width * 7 / 8, height * 3 / 4);
			coordinates[0, 2, 1] = new Coordinate(width * 6 / 8, height * 2 / 4);
			coordinates[0, 2, 2] = new Coordinate(width * 5 / 8, height * 1 / 4);

			coordinates[1, 0, 0] = new Coordinate(width * 4 / 8, height * 0 / 4);
			coordinates[1, 0, 1] = new Coordinate(width * 2 / 8, height * 2 / 4);
			coordinates[1, 0, 2] = new Coordinate(width * 0 / 8, height * 4 / 4);

			coordinates[0, 1, 0] = new Coordinate(width * 0 / 8, height * 4 / 4);
			coordinates[0, 1, 1] = new Coordinate(width * 4 / 8, height * 4 / 4);
			coordinates[0, 1, 2] = new Coordinate(width * 8 / 8, height * 4 / 4);

			coordinates[0, 2, 0] = new Coordinate(width * 8 / 8, height * 4 / 4);
			coordinates[0, 2, 1] = new Coordinate(width * 6 / 8, height * 2 / 4);
			coordinates[0, 2, 2] = new Coordinate(width * 4 / 8, height * 0 / 4);
		}

		protected const Int32 sides = 3;
		protected const Int32 dots = 3;
		protected readonly UInt32 size;

		protected readonly Coordinate[,,] coordinates = new Coordinate[2,sides,dots];

		public virtual IList<Line> GetLines() => new List<Line>();
		public virtual IList<Shape> GetShapes() => new List<Shape>();
	}
}
