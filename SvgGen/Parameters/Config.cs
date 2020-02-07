using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SvgGen.Parameters
{
	internal class Config
	{
		public UInt32 Size { get; private set; }
		public UInt32 Line { get; private set; }
		public Kind Kind { get; private set; }
		public Boolean Final { get; private set; }

		private IDictionary<String, Action<String>> byLetter;
		private IDictionary<String, Action<String>> byWord;

		public Config(String[] args)
		{
			Final = true;
			processArgs(args);
		}

		private void processArgs(String[] args)
		{
			if (args.Length % 2 == 1)
				throw new SVGGException("Wrong number of arguments");

			initDictionaries();

			for (var a = 0; a < args.Length; a+=2)
			{
				processName(args[a])(args[a + 1]);
			}
		}

		private void initDictionaries()
		{
			byLetter =
				new Dictionary<String, Action<String>>
				{
					{ "s", SetSize },
					{ "l", SetLine },
					{ "k", SetKind },
					{ "f", SetFinal },
				};

			byWord =
				new Dictionary<String, Action<String>>
				{
					{ "size", SetSize },
					{ "line", SetLine },
					{ "kind", SetKind },
					{ "final", SetFinal },
				};
		}

		private Action<String> processName(String arg)
		{
			var regex = new Regex(@"(\-(\w)|\-\-(\w+))");
			var match = regex.Match(arg);

			if (!match.Success)
				throw argException(arg);

			var letter = match.Groups[2].Value.ToLower();

			if (letter != String.Empty)
			{
				if (!byLetter.ContainsKey(letter))
					throw argException(arg);

				return byLetter[letter];
			}

			var word = match.Groups[3].Value.ToLower();

			if (word != String.Empty)
			{
				if (!byWord.ContainsKey(word))
					throw argException(arg);

				return byWord[word];
			}

			throw argException(arg);
		}

		private static Exception argException(String arg)
		{
			return new SVGGException(
				$"{{{arg}}} is not a valid argument"
			);
		}

		public void SetSize(String size)
		{
			Size = getInteger(size);
		}

		public void SetLine(String line)
		{
			Line = getInteger(line);
		}

		public void SetKind(String kind)
		{
			Kind = KindX.GetByName(kind)
			       ?? KindX.GetByLetter(kind)
				   ?? getKind(kind)
				   ?? throw valueException(kind);
		}
		
		public void SetKind(String kind)
		{
			Kind = KindX.GetByName(kind)
			       ?? KindX.GetByLetter(kind)
				   ?? getKind(kind)
				   ?? throw valueException(kind);
		}

		private static Kind? getKind(String kind)
		{
			return (Kind?) getInteger(kind);
		}

		public void SetFinal(String final)
		{
			var isFinalByNumber = UInt32.TryParse(text, out var integer)
				&& integer == 1;
			var firstLetter = final.ToLower()[0]

			Final = isFinalByNumber
				|| firstLetter == 'y';
		}

		private static UInt32 getInteger(String text)
		{
			var parsed = UInt32.TryParse(text, out var integer);

			if (!parsed || integer == 0)
				throw valueException(text);

			return integer;
		}

		private static Exception valueException(String text)
		{
			return new SVGGException(
				$"{{{text}}} is not valid value for the parameter"
			);
		}
	}
}
