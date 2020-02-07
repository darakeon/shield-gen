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
			if (kind == Kind.None)
				throw new SVGGException("Choose some kind of shield!");

			return @new(size, kind).ToList();
		}

		private static IEnumerable<Calculator> @new(UInt32 size, Kind kind)
		{
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

			width = size;
			height = size * Math.Sin(60 * Math.PI / 180);

			coordinates[0, 0, 0] = generate(3, 1);
			coordinates[0, 0, 1] = generate(2, 2);
			coordinates[0, 0, 2] = generate(1, 3);

			coordinates[0, 1, 0] = generate(2, 4);
			coordinates[0, 1, 1] = generate(4, 4);
			coordinates[0, 1, 2] = generate(6, 4);

			coordinates[0, 2, 0] = generate(7, 3);
			coordinates[0, 2, 1] = generate(6, 2);
			coordinates[0, 2, 2] = generate(5, 1);

			coordinates[1, 0, 0] = generate(4, 0);
			coordinates[1, 0, 1] = generate(2, 2);
			coordinates[1, 0, 2] = generate(0, 4);

			coordinates[0, 1, 0] = generate(0, 4);
			coordinates[0, 1, 1] = generate(4, 4);
			coordinates[0, 1, 2] = generate(8, 4);

			coordinates[0, 2, 0] = generate(8, 4);
			coordinates[0, 2, 1] = generate(6, 2);
			coordinates[0, 2, 2] = generate(4, 0);
		}
		
		private Coordinate generate(Int32 gridX, Int32 gridY)
		{
			return new Coordinate(width * gridX / 8, height * gridY / 4);
		}

		protected const Int32 states = 2;
		protected const Int32 sides = 3;
		protected const Int32 dots = 3;
		protected readonly UInt32 size;
		protected readonly Decimal width;
		protected readonly Decimal height;

		protected readonly Coordinate[,,] coordinates = new Coordinate[states,sides,dots];

		public virtual IList<Line> GetLines() => new List<Line>();
		public virtual IList<Shape> GetShapes() => new List<Shape>();
	}
}
