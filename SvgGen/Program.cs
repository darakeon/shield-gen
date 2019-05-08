using System;
using SvgGen.Calculus;
using SvgGen.Image;
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

			var calc = Calculator.New(config.Size, config.Kind);

			foreach (var line in calc.GetLines())
			{
				drawer.AddLine(line, config.Line);
			}
				
			drawer.Generate("test.svg", config.Size, config.Size);
		}
	}
}
