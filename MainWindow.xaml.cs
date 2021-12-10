using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace desktop_integration_test2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            txt_op_sys.Text = Environment.OSVersion.ToString();
            txt_processor.Text = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER").ToString();
            using (var searcher = new ManagementObjectSearcher("select * from Win32_VideoController"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    txt_gpu.Text = obj["Name"].ToString();
                }
            }
            string DomainName = System.Environment.UserDomainName;
            string AccountName = System.Environment.UserName.ToLower();
            SelectQuery query = new SelectQuery("select FullName from Win32_UserAccount where domain='" + DomainName + "' and name='" + AccountName + "'");
            ManagementObjectSearcher s = new ManagementObjectSearcher(query);
            foreach (ManagementBaseObject disk in s.Get())
            {
               txt_name.Text = disk["FullName"].ToString();
            }
            txt_username.Text = Environment.UserName.ToString();
            int total_width = (int)SystemParameters.PrimaryScreenWidth;
            this.Left = total_width - 450;
            this.Top = 100;
            DispatcherTimer _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(2000);
            _timer.Tick += new EventHandler(delegate (object s, EventArgs a)
            {
                txt_date.Text = string.Format("Timer Ticked: {0}ms", Environment.TickCount);
            });
            _timer.Start();
        }
        private void txt_gpu_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
