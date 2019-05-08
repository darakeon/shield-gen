using System;
using System.Collections.Generic;
using System.Linq;

namespace SvgGen.Lines
{
	class Grouped : LineCalculator
	{
		public Grouped(UInt32 size) : base(size) { }

		protected override IList<String> getColors(IList<String> mainColors)
		{
			return mainColors.Select(
				c => new List<String> { c, c, c }
			).SelectMany(c => c).ToList();
		}
	}
}
