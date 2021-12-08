using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AuthorizeNet.Util
{
	public class SensitiveDataTextLogger : TextWriterTraceListener
	{
		private static string[] cardPatterns;
		private static string[] tagPatterns;
		private static string[] tagReplacements;

		static SensitiveDataTextLogger()
		{
			LoadSensitiveDataConfig();
		}
		public SensitiveDataTextLogger(System.IO.Stream stream) : base(stream)
		{
		}

		public SensitiveDataTextLogger(System.IO.Stream stream, string name) : base(stream, name)
		{
		}

		public SensitiveDataTextLogger(string FileName, string name) : base(FileName, name)
		{
		}

		public SensitiveDataTextLogger(string FileName) : base(FileName)
		{
		}

		public SensitiveDataTextLogger(System.IO.TextWriter writer, string name) : base(writer, name)
		{
		}

		public SensitiveDataTextLogger(System.IO.TextWriter writer) : base(writer)
		{
		}

		private static void LoadSensitiveDataConfig()
		{
			cardPatterns = SensitiveDataConfigType.sensitiveStringRegexes;

			int noOfSensitiveTags = SensitiveDataConfigType.sensitiveTags.Length;
			tagPatterns = new string[noOfSensitiveTags];
			tagReplacements = new string[noOfSensitiveTags];

			for (int i = 0; i < noOfSensitiveTags; i++)
			{
				String tagName = SensitiveDataConfigType.sensitiveTags[i].TagName;
				String pattern = SensitiveDataConfigType.sensitiveTags[i].Pattern;
				String replacement = SensitiveDataConfigType.sensitiveTags[i].Replacement;

				if (!string.IsNullOrEmpty(pattern))
					tagPatterns[i] = "<" + tagName + ">" + pattern + "</" + tagName + ">";
				else
					tagPatterns[i] = "<" + tagName + ">" + ".+" + "</" + tagName + ">";
				tagReplacements[i] = "<" + tagName + ">" + replacement + "</" + tagName + ">";
			}
		}

		public override void Write(string Msg)
		{
			base.Write(Msg);
			base.Flush();
		}
		public override void WriteLine(string Msg)
		{
			string maskXmlMessage = MaskSensitiveXmlString(Msg);
			base.WriteLine(MaskCreditCards(maskXmlMessage));
			base.Flush();
		}

		public static String MaskCreditCards(String str)
		{
			for (int i = 0; i < cardPatterns.Length; i++)
			{
				str = Regex.Replace(str, cardPatterns[i], "XXXX");
			}
			return str;
		}

		public static String MaskSensitiveXmlString(String str)
		{
			for (int i = 0; i < tagPatterns.Length; i++)
			{
				str = Regex.Replace(str, tagPatterns[i], tagReplacements[i]);
			}
			return str;
		}
	}

}
