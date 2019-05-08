using System;
using System.Collections.Generic;
using System.Linq;

namespace SvgGen.Lines
{
	class Interpolated : LineCalculator
	{
		public Interpolated(UInt32 size) : base(size) { }

		protected override IList<String> getColors(IList<String> mainColors)
		{
			var newColors = mainColors.ToList();
			newColors.AddRange(mainColors);
			newColors.AddRange(mainColors);
			return newColors;
		}
	}
}
