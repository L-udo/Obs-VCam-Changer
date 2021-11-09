// See https://aka.ms/new-console-template for more information

using System;
using Microsoft.Win32;
using System.Security.Principal;

bool isElevated;
using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
{
    WindowsPrincipal principal = new WindowsPrincipal(identity);
    isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
}

if (isElevated)
{
    Console.WriteLine("Input new camera name:");
    string CamName = Console.ReadLine();
    Console.Clear();
    RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes\CLSID\{860BB310-5D01-11d0-BD3B-00A0C911CE86}\Instance\{A3FCE0F5-3493-419F-958A-ABA1250EC20B}", true);
    Console.Write("Original Camera Name: ");
    Console.Write(key.GetValue("FriendlyName"));
    key.SetValue("FriendlyName", CamName);
    Console.WriteLine("\n");
    Console.Write("New name Set to:");
    Console.Write(key.GetValue("FriendlyName"));
    Console.WriteLine("\n");
    key.Close();
    Console.WriteLine("Done! Press any key to exit....");
    Console.ReadLine();
}
else {
    Console.WriteLine("You need to run this as Administrator!");
    Console.ReadLine();

}

