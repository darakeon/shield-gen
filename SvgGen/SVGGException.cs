using System;

namespace SvgGen
{
	internal class SVGGException : Exception
	{
		public SVGGException(String message)
			: base(message) { }
	}
}
