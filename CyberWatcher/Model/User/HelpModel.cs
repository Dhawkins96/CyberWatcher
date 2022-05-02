using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberWatcher.Model.User
{
    public class HelpModel
    {

        public string Title { get; set; }
        public string Description { get; set; }

    }

    public class HelpChoices
    {
        public static List<HelpModel> ListHelpValues => new List<HelpModel>()
        {
            new HelpModel{ Title = "How To Use Cyber Watcher",
                Description = "Hello and Welcome to Cyber Watcher! \n Cyber Watcher is an application for home users to monitor their home network, with an added bonus of a password manager - " +
                "For those passwords you want to keep extra safe. \n \n If you are seeing this message you have aready accomplished Step One - Creating an account! Yay \n" +
                "Step Two - Running you first scan \n As you can see on your homepage it's pretty empty right now. To fix this we shall run our first scan. To do this all you have to do is:" +
                "\n \t - Click on the type of scan you would like to run, Local Network Scan or Port Scan.\n \n \t \t -Local Network Scan is the faster scan. If you select this scan you will" +
                "be scanning for all the live hosts on your network (which is any device connected to your internet from your phone to print), this scan will also scan 1000 ports of the live hosts." +
                "\n \t \t Proving you with all the information you need for an overview of your network." +
                "\n \n \t \t -Port Scan is the slower scan. This scan can take upto 20 mins if you have alot of devices connected to your internet but if you have the time it is worth it. \n" +
                "\t \t The port scan will do everything the Local Network Scan does the only difference is it will scan all 65,535 ports on the live hosts - Which is why the scan takes longer. " +
                "Having all the ports scanned would give you a more in depth look at you local network. \n Once you hace chosen your scan type all you have to do is wait for the information to be inputed below" +
                "\n It's as easy as that to run a scan using Cyber Watcher! \n \n Password Manager \n \n Using the password manager in Cyber Watcher is so easy. When getting started all " +
                "you have to do is add the password information into the textboxed. Then this information can be seen in the table as its stored in a local database. "},
                
            new HelpModel{ Title = "What is NMAP?",
                Description = "Overview what is NMAP? \n Nmap is a network mapper that has emerged as one of the most popular, free network discovery tools on the market. Nmap is now one of the core tools used by network administrators to map their networks." +
                " The program can be used to find live hosts on a network, perform port scanning, ping sweeps, OS detection, and version detection." +
                "\n What does NMAP do? \n At a practical level, Nmap is used to provide detailed, real-time information on your networks, and on the devices connected to them." +
                "The primary uses of NMAP is a program that gives you detailed information on your network as a whole. It can be used to provide a list of live hosts and open ports, as well as identifying the OS of every connected device. " +
                "This allows you to check whether a host is being used by a legitimate service, or by an external attacker. \n \n How to use NMAP? \n The program is most commonly used via a command-line interface (though GUI front-ends are also available)" +
                " and is available for many different operating systems such as Linux, Free BSD, and Gentoo. Its popularity has also been bolstered by an active and enthusiastic user support community." +
                "\n This is where Cyber Watcher comes in to play, as a GUI for NMAP. Cyber Watcher has the commands NMAP needs to run and then prints out the output in a user friendly view."},
            
            new HelpModel{ Title = "Top Tips To Improve Your Cyber Security",
                Description = "1. Keep Your Software Up to Date \n As we saw from the stats above, ransomware attacks were a major attack vector of 2017 for both businesses and consumers. One of the most important cyber security tips to mitigate " +
                "ransomware is patching outdated software, both operating system, and applications. This helps remove critical vulnerabilities that hackers use to access your devices. " +
                "Here are a few quick tips to get you started: Turn on automatic system updates for your device. Make sure your desktop web browser uses automatic security updates. Keep your web browser plugins like Flash, Java, etc. updated" +
                "\n 2. Use Anti-Virus Protection & Firewall \n Anti-virus (AV) protection software has been the most prevalent solution to fight malicious attacks. AV software blocks malware and other malicious viruses from entering your device and compromising your data." +
                " Use anti-virus software from trusted vendors and only run one AV tool on your device. Using a firewall is also important when defending your data against malicious attacks. A firewall helps screen out hackers, viruses, and other malicious activity that occurs over the Internet and determines what traffic is allowed to enter your device. " +
                "Windows and Mac OS X comes with their respective firewalls, aptly named Windows Firewall and Mac Firewall. Your router should also have a firewall built in to prevent attacks on your network. \n 3. Use Strong Passwords & Use a Password Management Tool \n " +
                "You’ve probably heard that strong passwords are critical to online security. The truth is passwords are important in keeping hackers out of your data! According to the National Institute of Standards and Technology’s (NIST) 2017 new password policy framework, you should consider:" +
                "\n \t -Dropping the crazy, complex mixture of upper case letters, symbols, and numbers. Instead, opt for something more user-friendly but with at least eight characters and a maximum length of 64 characters." +
                "\n \t -Don’t use the same password twice. \n \t -The password should contain at least one lowercase letter, one uppercase letter, one number, and four symbols but not the following &%#@_." +
                "\n \t -Choose something that is easy to remember and never leave a password hint out in the open or make it publicly available for hackers to see" +
                "\n \t Reset your password when you forget it. But, change it once per year as a general refresh." +
                "\n 4. Use Two-Factor or Multi-Factor Authentication \n Two-factor or multi-factor authentication is a service that adds additional layers of security to the standard password method of online identification. " +
                "Without two-factor authentication, you would normally enter a username and password. But, with two-factor, you would be prompted to enter one additional authentication method such as a Personal Identification Code, another password or even fingerprint. With multi-factor authentication," +
                " you would be prompted to enter more than two additional authentication methods after entering your username and password." +
                "\n 5. Learn about Phishing Scams – be very suspicious of emails, phone calls, and flyers \n We recently blogged that phishing scams are nastier than ever this year. In a phishing scheme attempt, " +
                "the attacker poses as someone or something the sender is not to trick the recipient into divulging credentials, clicking a malicious link, or opening an attachment that infects the user’s system with malware, trojan, or zero-day vulnerability exploit." +
                " This often leads to a ransomware attack. In fact, 90% of ransomware attacks originate from phishing attempts. \n A few important cyber security tips to remember about phishing schemes include:" +
                "\n \t -Bottom line – Don’t open email from people you don’t know \n \t -Know which links are safe and which are not – hover over a link to discover where it directs to" +
                "\n \t Be suspicious of the emails sent to you in general – look and see where it came from and if there are grammatical errors \n \t -Malicious links can come from friends who have been infected too. So, be extra careful! \n " +
                "Use these tips to protect your information and security."},
            
            new HelpModel{ Title = "What Information is Stored About You",
                Description = "Cyber Watcher stores your: \n Username \n Email Address \n Hashed User Password \n " +
                "Encrypted Data Added Within The Password Manager \n NMAP Scan File Name"},
                
            new HelpModel{ Title = "About Cyber Watcher",
                Description = "Cyber Watcher is a project made my Daisy Hawkins, as part of my final year project at Plymouth University, completeling my BSc(hons) Cyber Security degree." +
                "\n Cyber Watcher is an application for home users to monitor their home network, with an added bonus of a password manager - " +
                "For those passwords you want to keep extra safe. \n I am very proud of this project and everything I have accomplished during my time university and now I am looking forwrd" +
                "to what my future holds \n Thank you for using my project and enjoy what Cyber Watcher has to offer!"}
            };


    }



}
