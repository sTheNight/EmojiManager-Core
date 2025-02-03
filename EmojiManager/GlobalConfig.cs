using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmojiManager.EmojiClass;

namespace EmojiManager
{
    internal class GlobalConfig
    {
        // SupportedExtensions 用于保存支持的文件扩展名，暂未使用
        public List<string> SupportedExtensions = new List<string>();
        public static GlobalConfig Config;
        public static EmojiClassList ClassList;
        public static string CurrentDirectory = Environment.CurrentDirectory;
        public static string ConfigFilePath = CurrentDirectory + @"\config.json";
        public static string EmojiClassListPath = CurrentDirectory + @"\emojiClassList.json";
        public static string EmojiFilePath = CurrentDirectory + @"\emojis";
    }
}