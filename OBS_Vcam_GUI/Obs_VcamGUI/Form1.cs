using Microsoft.Win32;

namespace Obs_VcamGUI
{
    public partial class Form1 : Form
    {
        readonly string[] cameras = {
                @"SOFTWARE\Classes\CLSID\{860BB310-5D01-11d0-BD3B-00A0C911CE86}\Instance\{A3FCE0F5-3493-419F-958A-ABA1250EC20B}",
                @"SOFTWARE\Classes\CLSID\{860BB310-5D01-11d0-BD3B-00A0C911CE86}\Instance\{27B05C2D-93DC-474A-A5DA-9BBA34CB2A9C}",
                @"SOFTWARE\Classes\CLSID\{860BB310-5D01-11d0-BD3B-00A0C911CE86}\Instance\{27B05C2D-93DC-474A-A5DA-9BBA34CB2A9D}",
                @"SOFTWARE\Classes\CLSID\{860BB310-5D01-11d0-BD3B-00A0C911CE86}\Instance\{27B05C2D-93DC-474A-A5DA-9BBA34CB2A9E}",
                @"SOFTWARE\Classes\CLSID\{860BB310-5D01-11d0-BD3B-00A0C911CE86}\Instance\{27B05C2D-93DC-474A-A5DA-9BBA34CB2A9F}"
            };


        public Form1()
        {
            string cam_nm = "";

            List<string> camera_nms = new List<string>();
            foreach (string cam in cameras)
            {
                RegistryKey camaddr = Registry.LocalMachine.OpenSubKey(cam, true);
                if (camaddr == null)
                {
                    cam_nm = "Kite says 'Wot camera?'";
                    camera_nms.Add(cam_nm);
                }

                else if (camaddr != null)
                {
                    cam_nm = Convert.ToString(camaddr.GetValue("FriendlyName"));
                    camera_nms.Add(cam_nm);
                }

               
            }



            InitializeComponent();
            listBox1.DataSource = camera_nms;
        }




        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(cameras[listBox1.SelectedIndex], true);
            if (key == null)
            {
                label1.Text = "ERR-- Key doesnt exist";
                return;
            }
            else if (key != null)
            {
                key.SetValue("FriendlyName", textBox1.Text);
                label1.Text = $"New Name: {key.GetValue("FriendlyName")}";
                key.Close();

                string cam_nm = "";

                List<string> camera_nms2 = new List<string>();
                foreach (string cam in cameras)
                {
                    RegistryKey camaddr = Registry.LocalMachine.OpenSubKey(cam, true);
                    if (camaddr == null)
                    {
                        cam_nm = "ERR-- Camera Not found";
                    }

                    else if (camaddr != null)
                    {
                        cam_nm = Convert.ToString(camaddr.GetValue("FriendlyName"));
                    }

                    camera_nms2.Add(cam_nm);
                }
                listBox1.DataSource = camera_nms2;


            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}