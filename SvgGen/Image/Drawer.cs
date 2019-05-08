using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SvgGen.Image
{
	class Drawer
	{
		private readonly IList<String> lines = new List<String>();

		public Drawer AddLine(Line line, UInt32 weight)
		{
			lines.Add(
				$"<line x1='{line.Start.X}' " +
				$"y1='{line.Start.Y}' " +
				$"x2='{line.End.X}' " +
				$"y2='{line.End.Y}' " +
				$"stroke='#{line.Color}' " +
				$"stroke-width='{weight}' " +
				"stroke-linecap='round'/>"
			);

			return this;
		}

		public Drawer AddShape(Shape shape)
		{
			var points = String.Join(
				", ",
				shape.Coordinates
					.Select(c => $"{c.X} {c.Y}")
			);

			lines.Add(
				$"<polygon points='{points}' " +
				$"fill='#{shape.Color}' " +
				$"stroke='#{shape.Color}' />"
			);

			return this;
		}

		public void Generate(String path, UInt32 width, UInt32 height)
		{
			var fix = "xmlns='http://www.w3.org/2000/svg' version='1.1'";

			var svg = new List<String>
			{
				$"<svg {fix} width='{width}' height='{height}'>"
			};

			svg.AddRange(lines.Select(l => "\t" + l));

			svg.Add("</svg>");

			File.WriteAllLines(path, svg);
		}
	}
}
