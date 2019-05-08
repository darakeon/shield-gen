using System;
using System.Collections.Generic;
using SvgGen.Image;

namespace SvgGen.Calculus
{
	internal class Filled : Calculator
	{
		public Filled(UInt32 size) : base(size) { }

		public override IList<Shape> GetShapes()
		{
			var shapes = new List<Shape>();

			oneColor(shapes, "993333", 0);
			oneColor(shapes, "333333", 1);
			oneColor(shapes, "993399", 2);

			twoColors(shapes, "663333", 0);
			twoColors(shapes, "663366", 1);
			twoColors(shapes, "993366", 2);

			var middle = new Shape("773355");
			threeColors(middle, 0);
			threeColors(middle, 1);
			threeColors(middle, 2);
			shapes.Add(middle);

			return shapes;
		}

		private void oneColor(IList<Shape> shapes, String color, Int32 index)
		{
			var c = index; // current
			var n = (index + 1) % 3; // next
			var p = (index + 2) % 3; // previous

			var right = new Shape(color);
			right.Add(coordinates[c, 0]);
			right.Add(getCross(c, 0, n, 2, c, 1, p, 2));
			right.Add(getCross(c, 0, n, 0, c, 1, p, 2));
			shapes.Add(right);

			var center = new Shape(color);
			center.Add(coordinates[c, 2]);
			center.Add(getCross(c, 2, p, 0, c, 1, n, 0));
			center.Add(getCross(c, 2, p, 2, c, 1, n, 0));
			shapes.Add(center);

			var left = new Shape(color);
			left.Add(coordinates[c, 1]);
			left.Add(getCross(c, 1, n, 0, c, 2, p, 2));
			left.Add(getCross(c, 2, p, 2, c, 1, n, 1));
			left.Add(getCross(c, 1, n, 1, c, 2, p, 1));
			left.Add(getCross(c, 2, p, 1, c, 0, n, 1));
			left.Add(getCross(c, 0, n, 1, c, 1, p, 1));
			left.Add(getCross(c, 1, p, 1, c, 0, n, 0));
			left.Add(getCross(c, 0, n, 0, c, 1, p, 2));
			shapes.Add(left);
		}

		private void twoColors(IList<Shape> shapes, String color, Int32 index)
		{
			var c = index; // current
			var n = (index + 1) % 3; // next
			var p = (index + 2) % 3; // previous

			var shape = new Shape(color);

			shape.Add(getCross(c, 1, n, 2, n, 1, c, 0));
			shape.Add(getCross(n, 1, c, 0, c, 2, p, 1));
			shape.Add(getCross(c, 2, p, 1, c, 1, n, 1));
			shape.Add(getCross(c, 1, n, 1, c, 2, p, 2));
			shape.Add(getCross(c, 2, p, 2, c, 1, n, 0));
			shape.Add(getCross(c, 1, n, 0, c, 2, p, 0));
			shape.Add(getCross(c, 2, p, 0, c, 2, n, 0));
			shape.Add(getCross(c, 2, n, 0, n, 0, p, 2));
			shape.Add(getCross(n, 0, p, 2, c, 2, n, 1));
			shape.Add(getCross(c, 2, n, 1, n, 0, p, 0));
			shape.Add(getCross(n, 0, p, 0, n, 1, c, 1));
			shape.Add(getCross(n, 1, c, 1, n, 0, p, 1));
			shape.Add(getCross(n, 0, p, 1, c, 1, n, 2));

			shapes.Add(shape);
		}

		private void threeColors(Shape shape, Int32 index)
		{
			var c = index; // current
			var n = (index + 1) % 3; // next
			var p = (index + 2) % 3; // previous

			shape.Add(getCross(c, 1, p, 0, p, 1, c, 2));
			shape.Add(getCross(p, 1, c, 2, c, 0, n, 1));
		}

		private Coordinate getCross(Int32 x1, Int32 y1, Int32 x2, Int32 y2, Int32 x3, Int32 y3, Int32 x4, Int32 y4)
		{
			var line1 = new Line(coordinates[x1, y1], coordinates[x2, y2]);
			var line2 = new Line(coordinates[x3, y3], coordinates[x4, y4]);
			return line1.Cross(line2);
		}
	}
}
