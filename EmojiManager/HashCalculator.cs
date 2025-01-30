using System.Security.Cryptography;

namespace EmojiManager
{
    internal class HashCalculator
    {
        public static string ComputeFileHash_SHA256(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    return Convert.ToHexString(SHA256.HashData(fs));
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string ComputeFileHash_SHA1(string path)
        {
            try
            {
                using (FileStream fs = File.OpenRead(path))
                {
                    return Convert.ToHexString(SHA1.HashData(fs));
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string ComputeFileHash_MD5(string path)
        {
            try
            {
                using (FileStream fs = File.OpenRead(path))
                {
                    return Convert.ToHexString(MD5.HashData(fs));
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool ComputeFileHash_ALL(string path, out string sha256_data, out string sha1_data, out string md5_data)
        {
            try
            {
                sha256_data = ComputeFileHash_SHA256(path);
                sha1_data = ComputeFileHash_SHA1(path);
                md5_data = ComputeFileHash_MD5(path);
                return true;
            }
            catch (Exception)
            {
                sha256_data = null;
                sha1_data = null;
                md5_data = null;
                return false;
            }
        }
    }
}