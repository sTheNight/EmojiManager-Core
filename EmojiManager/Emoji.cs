using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmojiManager
{
    /**
     * Emoji 结构
     * EmojiClassList 内包含 EmojiClass，EmojiClass 内包含 Emoji
     * 
     * 说明
     * EmojiClassList 用于管理所有 EmojiClass 分组，EmojiClass 保存该分组的名称信息且管理属于此分组的 Emoji
     * EmojiClassList 可以通过 Add/RemoveClass 方法来添加或删除 EmojiClass，可以通过 FindClass 来查找分组
     * EmojiClassList 还可以通过 AddEmojiByClass 方法来添加 Emoji，但是需要传入 Emoji 与 ClassName，此方法实际调用的是对应的 EmojiClass 的 AddEmoji 方法
     * 
     * EmojiClass 用于管理 Emoji，EmojiClass 内部包含一个 List<Emoji> 用于保存 Emoji
     * EmojiClass 可以通过 Add/RemoveEmoji 方法来添加或删除 Emoji，可以通过 FindEmoji 来查找 Emoji
     * 
     * Emoji 用于保存 Emoji 的信息，包括 EmojiName 与 uniqueIdentification
     * 目前设计用 uniqueIdentification 作为文件名，但是未来可能会改变
     * uniqueIdentification 的生成方式过于简陋，未来可能会优化
     * 
     */
    public class Emoji
    {
        private string emojiName;
        public string uniqueIdentification;
        private Emoji(string emojiName, string uniqueIdentification)
        {
            this.emojiName = emojiName;
            this.uniqueIdentification = uniqueIdentification;
        }
        public static Emoji Create(string emojiName, string sha256, string sha1, string md5)
        {
            string uniqueIdentification = sha256 + sha1 + md5;
            return new Emoji(emojiName, uniqueIdentification);
        }
        // 此重载方法应常用于反序列化，后续应当在反序列化时自动检查文件是否存在且计算其哈希值来填充 uniqueIdentification*
        public static Emoji Create(string emojiName, string uniqueIdentification)
        {
            return new Emoji(emojiName, uniqueIdentification);
        }
        public string EmojiName { get => emojiName; set => emojiName = value; }
    }
    public class EmojiClass
    {
        private string className;
        public List<Emoji> emojiList = new List<Emoji>();
        public string ClassName { get => className; set => className = value; }
        public EmojiClass(string className)
        {
            this.className = className;
        }
        public Emoji FindEmoji(string uniqueIdentification)
        {
            foreach (var emoji in emojiList)
            {
                if (emoji.uniqueIdentification == uniqueIdentification)
                {
                    return emoji;
                }
            }
            return null;
        }
        public bool AddEmoji(Emoji emoji)
        {
            // EmojiClass 需要判断将要添加的 Emoji 是否已与 Class 内部的某个 Emoji 重合
            if (this.FindEmoji(emoji.uniqueIdentification) == null)
            {
                this.emojiList.Add(emoji);
                return true;
            }
            return false;
        }
        public bool RemoveEmoji(Emoji emoji)
        {
            // 或许应该做一个传入 emojiname 的重载*
            if (this.FindEmoji(emoji.uniqueIdentification) != null)
            {
                this.emojiList.Remove(emoji);
                return true;
            }
            return false;
        }
        public class EmojiClassList
        {
            // 由于全局只需要一个唯一的 EmojiClassList，所以使用单例模式
            private static List<EmojiClass> emojiClassList = new List<EmojiClass>();
            [Obsolete("此方法暂时废除，因为同一个 Emoji 可能存在于不同的 Class 之中，目前暂未处理此逻辑",true)]
            public Emoji FindEmoji(string uniqueIdentification)
            {
                // 由于同一个 Emoji 可能存在于不同的 Class 之中，此方法暂时废除

                // 第一层遍历 ClassList 的 Class
                foreach (var emojiclass in emojiClassList)
                {
                    // 第二层遍历 Class 内部的 Emoji
                    foreach (var emoji in emojiclass.emojiList)
                    {
                        if (emoji.uniqueIdentification == uniqueIdentification)
                        {
                            return emoji;
                        }
                    }
                }
                return null;
            }
            public List<EmojiClass> emojiClasses { get => emojiClassList; }
            public EmojiClass FindClass(string className)
            {
                foreach (var emojiClass in emojiClassList)
                {
                    if (emojiClass.ClassName == className)
                    {
                        return emojiClass;
                    }
                }
                return null;
            }
            public bool AddClass(string className)
            {
                if (FindClass(className) == null)
                {
                    emojiClassList.Add(new EmojiClass(className));
                    return true;
                }
                return false;
            }
            public bool AddEmojiByClass(Emoji emoji, string className)
            {
                // EmojiClassList 的 AddEmoji 不需要在刚开始就判断 Emoji 是否存在，因为尚不明确用户想要添加到哪个 Class 之中。
                var targetClass = FindClass(className);
                if (targetClass != null)
                {
                    // 直接调用 EmojiClass 的 AddEmoji 方法
                    if (targetClass.AddEmoji(emoji))
                        return true;
                }
                return false;
            }
            public bool RemoveClass(string className)
            {
                var targetClass = FindClass(className);
                if (targetClass != null)
                {
                    emojiClassList.Remove(targetClass);
                    return true;
                }
                return false;
            }
        }
    }
}