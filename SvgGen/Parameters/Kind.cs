using System;
using System.Collections.Generic;
using System.Linq;

namespace SvgGen.Parameters
{
	internal enum Kind
	{
		None = 0,
		Grouped = 1,
		Interpolated = 2,
		Filled = 3,
	}

	class KindX
	{
		private static KindX @new(Kind kind)
		{
			var name = kind.ToString().ToLower();

			return new KindX
			{
				kind = kind,
				name = name,
				value = (Int32) kind,
				letter = name.Substring(0, 1)
			};
		}

		private Kind kind;
		private String name;
		private String letter;
		private Int32 value;

		private static readonly IList<KindX> all =
			Enum.GetValues(typeof(Kind))
				.Cast<Kind>()
				.Select(@new)
				.ToList();

		private static Kind? get(Func<KindX, Boolean> func)
		{
			return all.SingleOrDefault(func)?.kind;
		}

		public static Kind? GetByLetter(String letter)
		{
			return get(k => k.letter == letter);
		}

		public static Kind? GetByName(String name)
		{
			return get(k => k.name == name);
		}

		public static Kind? GetByValue(String value)
		{
			return get(k => k.value.ToString() == value);
		}

		public static Kind? Get(String text)
		{
			return GetByName(text)
				?? GetByLetter(text)
				?? GetByValue(text);
		}
	}
}
