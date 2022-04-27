using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberWatcher.Model
{
    public class MenuItems
    {
        public string MenuName { get; set; }
        public string MenuImage { get; set; }
    }

    // Home Page
    public class HomeItems
    {
        public string HomeName { get; set; }
        public string HomeImage { get; set; }
    }

    // PC Page
    public class PCItems
    {
        public string PCName { get; set; }
        public string PCImage { get; set; }
    }

    // Password Page
    public class PasswordItems
    {
        public string PasswordName { get; set; }
        public string PasswordImage { get; set; }
    }

    // UserAccount Page
    public class UserAccountItems
    {
        public string UserAccountName { get; set; }
        public string UserAccountImage { get; set; }
    }

    // Help Page
    public class HelpItems
    {
        public string HelpName { get; set; }
        public string HelpImage { get; set; }
    }

}
