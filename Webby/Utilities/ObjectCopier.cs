using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Webby.Utilities
{
	public static class ObjectCopier
	{
		public static T DeepClone<T>(this T obj)
		{
			if (!typeof(T).IsSerializable)
			{
				throw new ArgumentException("Object provided is not serializable");
			}

			if (Object.ReferenceEquals(obj, null))
			{
				return default(T);
			}

			IFormatter formatter = new BinaryFormatter();
			using (Stream stream = new MemoryStream())
			{
				formatter.Serialize(stream, obj);
				stream.Seek(0, SeekOrigin.Begin);

				return (T)formatter.Deserialize(stream);
			}
		}
	}
}