using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CyberWatcher.Helper
{
    public class Encrypt
    {
        //Encrypting password for users registrating/validates stored passwords
        public static string HashString(string passwordString)
        {
            StringBuilder sb = new StringBuilder();
            //Changes hashed password to new string looping through the hashed values and changing each byte to a 3 character hexadecimal string
            foreach (byte b in GetHash(passwordString))
                sb.Append(b.ToString("X3"));
            return sb.ToString();
        }

        public static byte[] GetHash(string passwordString)
        {
            //Hashes password with SHA256 and passes the bytes back to HashString
            using (HashAlgorithm algorithm = SHA256.Create())
                //Computes the hash value for the specified byte array
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(passwordString));
        }
    }
}
