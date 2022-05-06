using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace CyberWatcherTests
{
    [TestClass]
    public class NetworkTests
    {
        [TestMethod]
        public void GetIPAddressNotNull()
        {
            /*arrange*/
            //Act  
            string returnvalue = CyberWatcher.Helper.LocalAddress.GetLocalIPAddress();

            //assert

            Assert.IsNotNull(returnvalue);

        }
    }
}
