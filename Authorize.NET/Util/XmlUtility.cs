using System;
using System.IO;
using System.Xml.Serialization;

namespace AuthorizeNet.Util
{
#pragma warning disable 1591
	public static class XmlUtility
	{

		private static readonly Log Logger = LogFactory.GetLog(typeof(XmlUtility));

		public static string GetXml<T>(T entity) //where T: object //MarshalByRefObject //Serializable
		{
			string xmlString;

			var requestType = typeof(T);
			try
			{
				var serializer = new XmlSerializer(requestType);
				using var writer = new Utf8StringWriter();

				serializer.Serialize(writer, entity);
				xmlString = writer.ToString();
			}
			catch (Exception e)
			{
				LogHelper.Error(Logger, "Error:'{0}' when serializing object:'{1}' to xml", e.Message, requestType);
				throw;
			}

			return xmlString;
		}

		public static T Create<T>(string xml) //where T: object //MarshalByRefObject
		{
			var entity = default(T);
			//make sure we have not null and not-empty string to de-serialize
			if (null != xml && 0 != xml.Trim().Length)
			{
				var responseType = typeof(T);
				try
				{
					object deSerializedobject;
					var serializer = new XmlSerializer(responseType);
					using (var reader = new StringReader(xml))
					{
						deSerializedobject = serializer.Deserialize(reader);
					}

					if (deSerializedobject is T t)
					{
						entity = t;
					}
				}
				catch (Exception e)
				{
					LogHelper.Error(Logger, "Error:'{0}' when deserializing the into object:'{1}' from xml:'{2}'", e.Message, responseType, xml);
					throw;
				}
			}

			return entity;
		}
	}

	public sealed class Utf8StringWriter : StringWriter
	{
		public override System.Text.Encoding Encoding { get { return System.Text.Encoding.UTF8; } }
	}
#pragma warning disable 1591
}