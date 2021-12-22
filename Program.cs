// See https://aka.ms/new-console-template for more information
// KittyKite was here
using System;
using Microsoft.Win32;
using System.Security.Principal;

bool isElevated;
using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
{
    WindowsPrincipal principal = new WindowsPrincipal(identity);
    isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
}

if (isElevated == false)
{
    Console.WriteLine("You need to run this as Administrator!");
    Console.WriteLine("Press Any Key to Exit");
    Console.ReadLine();
    System.Environment.Exit(0);
}


if (Registry.LocalMachine.OpenSubKey(@"SOFTWARE\OBS Studio").GetValue("") == null)
{
    Console.WriteLine("ERR- No Obs Found! -- Do you have the Latest Obs Studio installed?");
    Console.ReadLine();
    System.Environment.Exit(0);
}



string[] cameras = {
    @"SOFTWARE\Classes\CLSID\{860BB310-5D01-11d0-BD3B-00A0C911CE86}\Instance\{A3FCE0F5-3493-419F-958A-ABA1250EC20B}",
    @"SOFTWARE\Classes\CLSID\{860BB310-5D01-11d0-BD3B-00A0C911CE86}\Instance\{27B05C2D-93DC-474A-A5DA-9BBA34CB2A9C}",
    @"SOFTWARE\Classes\CLSID\{860BB310-5D01-11d0-BD3B-00A0C911CE86}\Instance\{27B05C2D-93DC-474A-A5DA-9BBA34CB2A9D}",
    @"SOFTWARE\Classes\CLSID\{860BB310-5D01-11d0-BD3B-00A0C911CE86}\Instance\{27B05C2D-93DC-474A-A5DA-9BBA34CB2A9E}",
    @"SOFTWARE\Classes\CLSID\{860BB310-5D01-11d0-BD3B-00A0C911CE86}\Instance\{27B05C2D-93DC-474A-A5DA-9BBA34CB2A9F}"
};

static int GetCams(string[] cameras) {
    int ac = 0;
    foreach (string cam in cameras)
    {
        RegistryKey camaddr = Registry.LocalMachine.OpenSubKey(cam, true);
        if (camaddr == null) { Console.WriteLine("ERR-- Camera Not found"); }
        else
        {
            Console.WriteLine($"Camera {ac} : " + camaddr.GetValue("FriendlyName"));
            ac++;
        }
    }
    return cameras.Length;
}



while (true) {
    GetCams(cameras);
    Console.WriteLine("\n");
    Console.Write("What Camera name do you want to change?:");
    int Cam2chng = Convert.ToUInt16(Console.ReadLine());

    Console.Clear();

    Console.WriteLine("Input new camera name:");
    string CamName = Console.ReadLine();
    Console.Clear();
    RegistryKey key = Registry.LocalMachine.OpenSubKey(cameras[Cam2chng], true);
    Console.Write("Original Camera Name: ");
    Console.Write(key.GetValue("FriendlyName"));
    key.SetValue("FriendlyName", CamName);
    Console.WriteLine("\n");
    Console.Write("New name Set to:");
    Console.Write(key.GetValue("FriendlyName"));
    Console.WriteLine("\n");
    key.Close();
    Console.Clear();


}


