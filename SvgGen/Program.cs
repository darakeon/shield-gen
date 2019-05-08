using System;
using SvgGen.Lines;
using SvgGen.Parameters;

namespace SvgGen
{
	class Program
	{
		public static void Main(String[] args)
		{
			try
			{
				generate(args);
			}
			catch (SVGGException e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				Console.Read();
			}
		}

		private static void generate(String[] args)
		{
			var config = new Config(args);

			var s = config.Size / 3;
			var e = s * 2;

			new Drawer()
				.AddLine(s, s, e, e, "CC0000", config.Line)
				.Generate("test.svg", config.Size, config.Size);
		}
	}
}
