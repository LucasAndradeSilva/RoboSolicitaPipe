using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Encodings.Web;

namespace RoboSolicitaPipe
{
    public static class JsonHelper
    {

        public static T DeserializeJsonByTagNameArray<T>(this string json, string tagName)
        {
            T? instance = default;

            try
            {
                if (string.IsNullOrEmpty(json))
                    return instance;

                var options = OptionsJsonConvert();

                using (JsonDocument document = JsonDocument.Parse(json))
                {

                    var element = document.RootElement.GetProperty(tagName)[0];
                    return JsonSerializer.Deserialize<T>(element.GetRawText(), options);
                }
            }
            catch (Exception ex)
            {


                return instance;
            }
        }

        public static T DeserializeJsonByTagName<T>(this string json, string tagName)
        {
            T? instance = default;

            if (string.IsNullOrEmpty(json))
                return instance;

            var options = OptionsJsonConvert();

            try
            {
                using (JsonDocument document = JsonDocument.Parse(json))
                {

                    var element = document.RootElement.GetProperty(tagName);
                    return JsonSerializer.Deserialize<T>(element.GetRawText(), options);
                }
            }
            catch (Exception ex)
            {


                return instance;
            }
        }

        public static T DeserializeJson<T>(this string json)
        {
            T? instance = default;

            try
            {
                if (string.IsNullOrEmpty(json))
                    return instance;

                var options = OptionsJsonConvert();

                using (JsonDocument document = JsonDocument.Parse(json))
                {
                    var element = document.RootElement;
                    return JsonSerializer.Deserialize<T>(element.GetRawText(), options);
                }
            }
            catch (Exception ex)
            {


                return instance;
            }
        }

        public static T DeserializeJsonPath<T>(this string path, string mainTag)
        {
            T? instance = default;

            if (string.IsNullOrEmpty(path))
                return default(T);

            var options = OptionsJsonConvert();

            try
            {
                using (FileStream openStream = File.OpenRead(path))
                {
                    using (JsonDocument document = JsonDocument.Parse((Stream)openStream))
                    {
                        var root = document.RootElement;
                        var tag = root.GetProperty(mainTag);
                        return instance = JsonSerializer.Deserialize<T>(tag.GetRawText(), options);
                    }
                }
            }
            catch (Exception ex)
            {


                return instance;
            }
        }

        public static T DeserializeJsonPath<T>(this string path)
        {
            T? instance = default;

            if (string.IsNullOrEmpty(path))
                return default(T);

            var options = OptionsJsonConvert();

            try
            {
                using (FileStream openStream = File.OpenRead(path))
                {
                    using (JsonDocument document = JsonDocument.Parse((Stream)openStream))
                    {
                        var root = document.RootElement;
                        return instance = JsonSerializer.Deserialize<T>(root, options);
                    }
                }
            }
            catch (Exception ex)
            {


                return instance;
            }
        }

        public static string ReadJsonByPath(this string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            try
            {
                using (FileStream openStream = File.OpenRead(path))
                {
                    using (JsonDocument document = JsonDocument.Parse((Stream)openStream))
                    {
                        return document.RootElement.ToString();
                    }
                }
            }
            catch (Exception ex)
            {


                return string.Empty;
            }
        }

        public static T DeserializerObjectResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = OptionsJsonConvert();

            var json = responseMessage.Content.ReadAsStream();

            T obj = JsonSerializer.Deserialize<T>(json, options);

            return obj;
        }

        public static StringContent GetContent(this object data)
        {
            return new StringContent(
                JsonSerializer.Serialize<object>(data),
                Encoding.UTF8, "application/json"
                );
        }

        public static string SerializeObjectToJsonAsync(this object obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                JsonSerializer.Serialize(stream, obj);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        public static JsonSerializerOptions OptionsJsonConvert()
        {
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = false // Opcional: para formatar o JSON de maneira legível
            };

            //serializerOptions.Converters.Add(new IntToBoolConverter());
            //serializerOptions.Converters.Add(new JsonStringDecimalConverter());
            return serializerOptions;
        }
    }
}
