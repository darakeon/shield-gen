using System;
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
		}
	}
}
