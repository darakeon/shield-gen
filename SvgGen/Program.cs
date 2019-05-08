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

			var drawer = new Drawer();

			var calc = new Calculator(config.Size);

			foreach (var line in calc.Lines)
			{
				drawer.AddLine(
					line.Start.X, line.Start.Y,
					line.End.X, line.End.Y,
					"000000", config.Line
				);
			}
				
			drawer.Generate("test.svg", config.Size, config.Size);
		}
	}
}
