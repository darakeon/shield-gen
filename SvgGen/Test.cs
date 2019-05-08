using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvgGen.Image;

namespace SvgGen
{
	class Test
	{
		public Boolean LinesCross()
		{
			var c1 = new Coordinate(10, 0);
			var c2 = new Coordinate(30, 40);
			var l1 = new Line(c1, c2);
			
			var c3 = new Coordinate(0, 40);
			var c4 = new Coordinate(20, 0);
			var l2 = new Line(c3, c4);

			var cross = l1.Cross(l2);

			return cross.X == 15 && cross.Y == 10;
		}
	}
}
