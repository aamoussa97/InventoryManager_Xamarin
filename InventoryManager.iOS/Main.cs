using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Foundation;
using UIKit;

namespace InventoryManager.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            ServicePointManager
                        .ServerCertificateValidationCallback +=
                        (sender, cert, chain, sslPolicyErrors) => true;
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
