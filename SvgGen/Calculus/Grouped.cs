using System;
using System.Collections.Generic;
using System.Linq;

namespace SvgGen.Calculus
{
	class Grouped : LineCalculator
	{
		public Grouped(Boolean isFinal, UInt32 size) : base(isFinal, size) { }

		protected override IList<String> getColors(IList<String> mainColors)
		{
			return mainColors.Select(
				c => new List<String> { c, c, c }
			).SelectMany(c => c).ToList();
		}
	}
}
