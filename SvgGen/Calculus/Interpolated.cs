using System;
using System.Collections.Generic;
using System.Linq;

namespace SvgGen.Calculus
{
	class Interpolated : LineCalculator
	{
		public Interpolated(Boolean isFinal, UInt32 size) : base(isFinal, size) { }

		protected override IList<String> getColors(IList<String> mainColors)
		{
			var newColors = mainColors.ToList();
			newColors.AddRange(mainColors);
			newColors.AddRange(mainColors);
			return newColors;
		}
	}
}
