using System;
using System.Collections.Generic;

namespace SvgGen.Image
{
	class Shape
	{
		public Shape(String color)
		{
			Color = color;
			Coordinates = new List<Coordinate>();
		}

		public IList<Coordinate> Coordinates { get; }
		public String Color { get; }

		public void Add(Coordinate coordinate)
		{
			Coordinates.Add(coordinate);
		}
	}
}
