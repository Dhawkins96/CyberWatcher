using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberWatcher.Model.Password_Manager
{
    public class PasswordGenerate
    {
        public string GivePassword(int length, bool numbers, bool low, bool uper, bool special)
        {
            if (length > 0 && numbers == true || low == true || uper == true || special == true)
            {
               
                string characters = "";

                if (numbers) characters += "1234567890";
                
                if (low) characters += "abcdefghijklmnopqrstuvwxyz";
                
                if (uper) characters += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                
                if (special) characters += "!@#$%^&*()_+=-";

                var res = new StringBuilder();
                
                var rnd = new Random();

                while (0 < length--)
                {
                    res.Append(characters[rnd.Next(characters.Length)]);
                }
                return res.ToString();
            }
            else return "";
        }
    }
}
