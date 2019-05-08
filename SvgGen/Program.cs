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

			var calculators = Calculator.New(config.Size, config.Kind);

			foreach (var calc in calculators)
			{
				foreach (var line in calc.GetLines())
				{
					drawer.AddLine(line, config.Line);
				}

				foreach (var shape in calc.GetShapes())
				{
					drawer.AddShape(shape);
				}
			}

			drawer.Generate("test.svg", config.Size, config.Size);
		}
	}
}
