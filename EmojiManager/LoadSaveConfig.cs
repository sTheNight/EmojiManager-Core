using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmojiManager.EmojiClass;

namespace EmojiManager
{
    internal static class LoadSaveConfig
    {
        internal static bool LoadConfig()
        {
            try
            {
                if (File.Exists(GlobalConfig.ConfigFilePath) && File.Exists(GlobalConfig.EmojiClassListPath))
                {
                    GlobalConfig.Config = JsonConvert.DeserializeObject<GlobalConfig>(File.ReadAllText(GlobalConfig.ConfigFilePath));
                    GlobalConfig.ClassList = new EmojiClassList();
                    // 由于 ClassList 内部的 Emoji 无法直接序列化，因此需要手动添加
                    JObject jobject = JObject.Parse(File.ReadAllText(GlobalConfig.EmojiClassListPath));
                    // 通过 JSON 的 emojiClasses 键值对来载入 EmojiClassList
                    JArray emojiClasses = (JArray)jobject["emojiClasses"];
                    foreach (JObject item in emojiClasses)
                    {
                        GlobalConfig.ClassList.AddClass(item["ClassName"].ToString());
                        EmojiClass emojiClass = GlobalConfig.ClassList.FindClass(item["ClassName"].ToString());
                        JArray emojiList = (JArray)item["emojiList"];
                        foreach (var emoji in emojiList)
                        {
                            emojiClass.AddEmoji(Emoji.Create(emoji["EmojiName"].ToString(), emoji["uniqueIdentification"].ToString()));
                        }
                    }
                }
                else
                {
                    GlobalConfig.Config = new GlobalConfig();
                    string[] normalExtension = { ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".webp" };
                    GlobalConfig.Config.SupportedExtensions.AddRange(normalExtension);
                    GlobalConfig.ClassList = new EmojiClassList();
                    GlobalConfig.ClassList.AddClass("默认分类");
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        internal static bool SaveConfig()
        {
            try
            {
                string Config_json = JsonConvert.SerializeObject(GlobalConfig.Config, Formatting.Indented);
                string ClassList_json = JsonConvert.SerializeObject(GlobalConfig.ClassList, Formatting.Indented);
                File.WriteAllText(GlobalConfig.ConfigFilePath, Config_json);
                File.WriteAllText(GlobalConfig.EmojiClassListPath, ClassList_json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}