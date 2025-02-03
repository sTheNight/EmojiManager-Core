/*
 * Todo List
 * 
 * 1. ~全局配置被拆分为了两个类，较为混乱，或许应该将 GlobalConfig 与 GlobalConfig合并~ ✓
 * 2. 将 Emoji 文件移动到对应目录的方法应当采用一个独立的静态类来实现，此类应当包含一个方法用于检查文件是否存在于 EmojiFilePath 中
 * 3. 此静态类可以合并 LoadSaveConfig 类，同时应当涵盖根据文件动态检查内存中的所有 Emoji 是否真实存在
 * 4. 对于未注册的 Emoji 但存在于 EmojiFilePath/ClassName 的 Emoji 文件而言，应当做一个方法用于询问用户是否将其注册
 * 5. 动态监控表情包文件的变化
 * 
 */

/**
 * 项目介绍：这是个表情包管理系统的核心模块，用于管理表情包的加载、保存、分类等功能
 * 
 * 目前没有很好的思路将 AddEmoji 与将文件移动到对应目录的功能联系起来，因此在用户需要添加 Emoji 时应当同时执行这两个操作，RemoveEmoji 也同理
 * 对于报错的处理过于简单，待优化
 * 
 */

using static EmojiManager.EmojiClass;
using Newtonsoft.Json;
namespace EmojiManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("程序入口没有制作，且暂不准备制作");
            ProgramTest();
        }
        internal static void ProgramTest()
        {
            try
            {
                // File.Delete(GlobalConfig.ConfigFilePath);
                // File.Delete(GlobalConfig.EmojiClassListPath);
            }
            catch (Exception)
            {
            }
            // 加载配置文件的方法由于无需初始化内容因此直接调用以测试
            LoadSaveConfig.LoadConfig();
            SaveEmojiClassListTest();
        }
        internal static void SaveEmojiClassListTest()
        {
            // 用于测试保存 EmojiClassList，应当创建几个默认文件和类用于测试
            GlobalConfig.ClassList = new EmojiClassList();
            GlobalConfig.ClassList.AddClass("默认分类");
            GlobalConfig.ClassList.AddClass("测试分类");
            // 由于没法生成 emoji 的各种哈希值，因此用随机数代替
            Random random = new Random();
            Emoji emoji1 = Emoji.Create("testname", random.Next(0,200000).ToString(), random.Next(0, 200000).ToString(), random.Next(0, 200000).ToString());
            Emoji emoji2 = Emoji.Create("testname2", random.Next(0, 200000).ToString(), random.Next(0, 200000).ToString(), random.Next(0, 200000).ToString());
            Emoji emoji3 = Emoji.Create("testname3", random.Next(0, 200000).ToString(), random.Next(0, 200000).ToString(), random.Next(0, 200000).ToString());
            GlobalConfig.ClassList.AddEmojiByClass(emoji1, "默认分类");
            GlobalConfig.ClassList.AddEmojiByClass(emoji2, "默认分类");
            GlobalConfig.ClassList.AddEmojiByClass(emoji3, "测试分类");
            LoadSaveConfig.SaveConfig();
        }
    }
}