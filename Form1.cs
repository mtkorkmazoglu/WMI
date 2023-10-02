using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetAccount();
            GetProcessorInfo();
            GetDiskInfo();
            GetRamInfo();
            GetCpuUsage();
            GetMemoryUsage();
        }

        private void GetAccount()
        {

            System.Management.ManagementClass wmi = new System.Management.ManagementClass("Win32_ComputerSystem");
            var providers = wmi.GetInstances();
            foreach (var provider in providers)
            {
                string systemSku = provider["SystemSkuNumber"].ToString();
                textBox2.Text = systemSku.ToString();
            }
        }

        private void GetProcessorInfo()
        {
            System.Management.ManagementClass wmi = new System.Management.ManagementClass("Win32_Processor");
            var providers = wmi.GetInstances();
            foreach (var provider in providers)
            {
                int procFamily = Convert.ToInt16(provider["Family"]);
                int procSpeed = Convert.ToInt32(provider["CurrentClockSpeed"]);
                string procStatus = provider["status"].ToString();

                textBox3.Text = procFamily.ToString();
                textBox4.Text = procSpeed.ToString() + " " + "Mhz";
                textBox5.Text = procStatus.ToString();
            }
        }
        private void GetDiskInfo()
        {
            System.Management.ManagementClass wmi = new System.Management.ManagementClass("Win32_LogicalDisk");
            var providers = wmi.GetInstances();
            foreach (var provider in providers)
            {

                string diskSize = provider["Size"].ToString();
                textBox6.Text = Math.Round(Convert.ToInt64(diskSize) / Math.Pow(1024, 3)) + " GB";

                string freeSPace = provider["FreeSpace"].ToString();
                textBox7.Text = Math.Round(Convert.ToInt64(freeSPace) / Math.Pow(1024, 3)) + " GB";
            }
        }

        private void GetRamInfo()
        {
            System.Management.ManagementClass wmi = new System.Management.ManagementClass("Win32_PhysicalMemory");
            var providers = wmi.GetInstances();
            foreach (var provider in providers)
            {
                string ramSize = provider["Capacity"].ToString();
                textBox8.Text = Math.Round(Convert.ToInt64(ramSize) / Math.Pow(1024, 3)) + " GB";
            }
        }

        private void GetCpuUsage()
        {

            System.Management.ManagementClass wmi = new System.Management.ManagementClass("Win32_Processor");
            var providers = wmi.GetInstances();
            foreach (var provider in providers)
            {
                string systemSku = provider["LoadPercentage"].ToString();
                textBox9.Text = systemSku.ToString();
            }
        }

        private void GetMemoryUsage()
        {

            System.Management.ManagementClass wmi = new System.Management.ManagementClass("Win32_Process");
            var providers = wmi.GetInstances();
            foreach (var provider in providers)
            {
                string memoryUsage = provider["WorkingSetSize"].ToString();
                textBox10.Text = Math.Round(Convert.ToInt64(memoryUsage) / Math.Pow(1024, 1)) + " KB";
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-9A7HLLK;Initial Catalog=TestDb;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into Table_1 (SystemSku, ProcessorFamily, CurrentClockSpeed, ProcessorStatus, DiskSize, FreeSpace, RamSize, CpuUsage, MemoryUsage) values (@SystemSku, @ProcessorFamily, @CurrentClockSpeed, @ProcessorStatus, @DiskSize, @FreeSpace, @RamSize, @CpuUsage, @MemoryUsage)", conn);
            //cmd.Parameters.AddWithValue("@ID", int.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@SystemSku", (textBox2.Text));
            cmd.Parameters.AddWithValue("@ProcessorFamily", (textBox3.Text));
            cmd.Parameters.AddWithValue("@CurrentClockSpeed", (textBox4.Text));
            cmd.Parameters.AddWithValue("@ProcessorStatus", (textBox5.Text));
            cmd.Parameters.AddWithValue("@DiskSize", (textBox6.Text));
            cmd.Parameters.AddWithValue("@FreeSpace", (textBox7.Text));
            cmd.Parameters.AddWithValue("@RamSize", (textBox8.Text));
            cmd.Parameters.AddWithValue("@CpuUsage", (textBox9.Text));
            cmd.Parameters.AddWithValue("@MemoryUsage", (textBox10.Text));

            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Successfull");

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            GetAccount();
            GetProcessorInfo();
            GetDiskInfo();
            GetRamInfo();
            GetCpuUsage();
            GetMemoryUsage();
            button1_Click(sender, e);
        }
    }
}

