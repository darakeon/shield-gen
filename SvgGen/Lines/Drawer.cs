using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SvgGen.Lines
{
	class Drawer
	{
		private IList<String> lines = new List<String>();

		public Drawer AddLine(
			UInt32 x1, UInt32 y1, UInt32 x2, UInt32 y2,
			String color, UInt32 line
		)
		{
			lines.Add(
				$"<line x1='{x1}' y1='{y1}' " +
				$"x2='{x2}' y2='{y2}' " +
				$"stroke='#{color}' " +
				$"stroke-width='{line}' " +
				"stroke-linecap='round'/>"
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
