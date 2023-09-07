using LitJson;
using UnityEngine;

namespace Tianbo.Wang
{
    public static class Wtb_JsonDataEx
    {
        public static string ToStringEx(this JsonData jsonData)
        {
            return System.Text.RegularExpressions.Regex.Unescape(jsonData.ToJson()).Trim('"');
        }

        public static bool HasKey(this JsonData jsonData, string keyName)
        {
            bool isContain = false;
            try
            {
                jsonData[keyName].ToJson();
                isContain = true;
            }
            catch
            {
                isContain = false;
            }
            return isContain;
        }

        public static bool HasValue(this JsonData jsonData, string keyName, string value)
        {
            bool isContain = false;
            for (int i = 0; i < jsonData.Count; i++)
            {
                if (HasKey(jsonData[i], keyName) && jsonData[i][keyName].ToStringEx() == value)
                {
                    isContain = true;
                    break;
                }
            }
            return isContain;
        }


        /// <summary>
        /// 获取物体的transform
        /// </summary>
        public static void GetTrans(this JsonData jsonData, out Vector3 position, out Quaternion rotation, out Vector3 scale)
        {
            SetSelectObjTransMethod(jsonData["position"], jsonData["rotation"], jsonData["scale"], out position, out rotation, out scale);
        }
        static void SetSelectObjTransMethod(LitJson.JsonData tempPosition, LitJson.JsonData tempRotation, LitJson.JsonData tempScale, out Vector3 position, out Quaternion rotation, out Vector3 scale)
        {
            position = new Vector3(float.Parse(tempPosition["x"].ToString()), float.Parse(tempPosition["y"].ToStringEx()), float.Parse(tempPosition["z"].ToStringEx()));

            rotation = new Quaternion(float.Parse(tempRotation["x"].ToStringEx()), float.Parse(tempRotation["y"].ToStringEx()), float.Parse(tempRotation["z"].ToStringEx()), float.Parse(tempRotation["w"].ToStringEx()));

            scale = new Vector3(float.Parse(tempScale["x"].ToStringEx()), float.Parse(tempScale["y"].ToStringEx()), float.Parse(tempScale["z"].ToStringEx()));
        }

        /// <summary>
        /// 删除固定索引的值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static JsonData RemoveAt(this JsonData data, int index)
        {
            if (data.GetJsonType() == JsonType.Array)
            {
                JsonData tempData = new JsonData();
                for (int i = 0; i < data.Count; i++)
                {
                    tempData.Add(data[i]);
                }
                data.Clear();
                for (int i = 0; i < tempData.Count; i++)
                {
                    if (i != index)
                    {
                        data.Add(tempData[i]);
                    }
                }
            }
            return data;
        }

        /// <summary>
        /// 打印出格式
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToPrettyStr(this JsonData data)
        {
            JsonWriter jsonWriter = new JsonWriter();
            jsonWriter.PrettyPrint = true;
            data.ToJson(jsonWriter);
            return jsonWriter.ToString();
        }

    }
}