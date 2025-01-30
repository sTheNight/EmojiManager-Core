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
                if (File.Exists(GlobalVar.ConfigFilePath) && File.Exists(GlobalVar.EmojiClassListPath))
                {
                    GlobalVar.Config = JsonConvert.DeserializeObject<GlobalConfig>(File.ReadAllText(GlobalVar.ConfigFilePath));
                    GlobalVar.ClassList = new EmojiClassList();
                    // 由于 ClassList 内部的 Emoji 无法直接序列化，因此需要手动添加
                    JObject jobject = JObject.Parse(File.ReadAllText(GlobalVar.EmojiClassListPath));
                    // 通过 JSON 的 emojiClasses 键值对来载入 EmojiClassList
                    JArray emojiClasses = (JArray)jobject["emojiClasses"];
                    foreach (JObject item in emojiClasses)
                    {
                        GlobalVar.ClassList.AddClass(item["ClassName"].ToString());
                        EmojiClass emojiClass = GlobalVar.ClassList.FindClass(item["ClassName"].ToString());
                        JArray emojiList = (JArray)item["emojiList"];
                        foreach (var emoji in emojiList)
                        {
                            emojiClass.AddEmoji(Emoji.Create(emoji["EmojiName"].ToString(), emoji["uniqueIdentification"].ToString()));
                        }
                    }
                }
                else
                {
                    GlobalVar.Config = new GlobalConfig();
                    string[] normalExtension = { ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".webp" };
                    GlobalVar.Config.SupportedExtensions.AddRange(normalExtension);
                    GlobalVar.ClassList = new EmojiClassList();
                    GlobalVar.ClassList.AddClass("默认分类");
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
                string Config_json = JsonConvert.SerializeObject(GlobalVar.Config, Formatting.Indented);
                string ClassList_json = JsonConvert.SerializeObject(GlobalVar.ClassList, Formatting.Indented);
                File.WriteAllText(GlobalVar.ConfigFilePath, Config_json);
                File.WriteAllText(GlobalVar.EmojiClassListPath, ClassList_json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}