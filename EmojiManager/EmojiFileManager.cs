using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmojiManager
{
    // 目前遇到错误会直接 Return false，未来应当考虑添加异常处理
    internal static class EmojiFileManager
    {
        // destinationPath 应当传入 EmojiFilePath + ClassName
        internal static bool MoveEmojiFile(string originPath, string destinationPath, Emoji emoji)
        {
            try
            {
                if (!Directory.Exists(destinationPath))
                {
                    Directory.CreateDirectory(destinationPath);
                }
                File.Move(originPath, $"{destinationPath}/{emoji.uniqueIdentification}.{Path.GetExtension(originPath)}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        internal static bool RemoveEmojiFile(EmojiClass emojiClass, Emoji emoji,string extension)
        {
            try
            {
                File.Delete($"{GlobalConfig.EmojiFilePath}/{emojiClass.ClassName}/{emoji.uniqueIdentification}.{extension}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
