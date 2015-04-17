using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhoneApp.Common
{
    public class JsonParser
    {
        
        /// <summary>
        /// 序列化Json对象(将对象转换为Json字符串)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ObjectToJson<T>(T t)
        {
            try
            {
                if (t == null)
                    return string.Empty;
                return JsonConvert.SerializeObject(t);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 反序列化Josn对象(将Json字符串反序列化对象)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(string data,T defaultValue)
        {
            if (string.IsNullOrEmpty(data))
                return defaultValue;
            try
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 反序列化Josn对象 通过JsonReader方法(将Json字符串反序列化对象)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T JsonToObjetByJosnReader<T>(string data,T defaultValue)
        {
            if (string.IsNullOrEmpty(data))
                return defaultValue;
            try
            {
                byte[] array = Encoding.UTF8.GetBytes(data);
                using (MemoryStream ms = new MemoryStream(array))
                using (StreamReader sr = new StreamReader(ms))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    return serializer.Deserialize<T>(reader);
                }
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
