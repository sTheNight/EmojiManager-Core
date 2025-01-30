/**
 * 项目介绍：这是个表情包管理系统的核心模块，用于管理表情包的加载、保存、分类等功能
 * 暂时没有做完 Emoji 文件的操作功能，待完善
 * 暂时没有做完 UI 界面，待完善
 */

using static EmojiManager.EmojiClass;
using Newtonsoft.Json;
namespace EmojiManager
{
    /*
     * Todo List
     * 
     * ~1. 应当定义一个 EmojiFilePath:string 用于存储保存存储Emoji文件的文件夹路径~
     * ~2. 将 Emoji 文件移动到对应目录的方法应当采用一个独立的静态类来实现，此类应当包含一个方法用于检查文件是否存在于 EmojiFilePath 中~
     * ~3. 此静态类可以合并 LoadSaveConfig 类，同时应当涵盖根据文件动态检查内存中的所有 Emoji 是否真实存在~
     * 
     * 应当搭配 UI 界面这样在需要使用emoji的时候会更加方便，但我还不会做，因此先把程序的核心功能写好，等以后学会做 UI 后再完善
     * 
     * 后续计划：
     * 
     * 1. 对于未注册的 Emoji 但存在于 EmojiFilePath/ClassName 的 Emoji 文件而言，应当做一个方法用于询问用户是否将其注册
     * 2. 学习线程相关知识，将 EmojiManager 作为一个后台线程运行，以便在 UI 界面中实现 Emoji 的动态加载
     * 
     */
    internal class Program
    {
        static void Main(string[] args)
        {
        }
        internal static void ProgramTest()
        {
            try
            {
                // File.Delete(GlobalVar.ConfigFilePath);
                // File.Delete(GlobalVar.EmojiClassListPath);
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
            GlobalVar.ClassList = new EmojiClassList();
            GlobalVar.ClassList.AddClass("默认分类");
            GlobalVar.ClassList.AddClass("测试分类");
            // 由于没法生成 emoji 的各种哈希值，因此用随机数代替
            Random random = new Random();
            Emoji emoji1 = Emoji.Create("testname", random.Next(0,200000).ToString(), random.Next(0, 200000).ToString(), random.Next(0, 200000).ToString());
            Emoji emoji2 = Emoji.Create("testname2", random.Next(0, 200000).ToString(), random.Next(0, 200000).ToString(), random.Next(0, 200000).ToString());
            Emoji emoji3 = Emoji.Create("testname3", random.Next(0, 200000).ToString(), random.Next(0, 200000).ToString(), random.Next(0, 200000).ToString());
            GlobalVar.ClassList.AddEmojiByClass(emoji1, "默认分类");
            GlobalVar.ClassList.AddEmojiByClass(emoji2, "默认分类");
            GlobalVar.ClassList.AddEmojiByClass(emoji3, "测试分类");
            LoadSaveConfig.SaveConfig();
        }
    }
}