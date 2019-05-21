using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InternetConnectivity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (WebRequestTest(textBox1.Text))
                ellipse1.Fill = System.Windows.Media.Brushes.Green;
            else
                ellipse1.Fill = System.Windows.Media.Brushes.Red;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (TcpSocketTest(textBox1.Text))
                ellipse2.Fill = System.Windows.Media.Brushes.Green;
            else
                ellipse2.Fill = System.Windows.Media.Brushes.Red;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (PingTest(textBox1.Text))
                ellipse3.Fill = System.Windows.Media.Brushes.Green;
            else
                ellipse3.Fill = System.Windows.Media.Brushes.Red;
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (DnsTest(textBox1.Text))
                ellipse4.Fill = System.Windows.Media.Brushes.Green;
            else
                ellipse4.Fill = System.Windows.Media.Brushes.Red;
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
        }


        /*
         |-------------------------------------------------------- 
         |  Functions 
         |--------------------------------------------------------
         */
        public static bool WebRequestTest(string strUrl)
        {
            try
            {
                System.Net.WebRequest myRequest = System.Net.WebRequest.Create(strUrl);
                System.Net.WebResponse myResponse = myRequest.GetResponse();
            }
            catch (System.Net.WebException)
            {
                return false;
            }
            return true;
        }
        public static bool TcpSocketTest(string strUrl)
        {
            try
            {
                System.Net.Sockets.TcpClient client =
                    new System.Net.Sockets.TcpClient(strUrl, 80);
                client.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool PingTest(string strUrl)
        {
            try
            {
                System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                System.Net.NetworkInformation.PingReply pingStatus = ping.Send(strUrl, 1000);
                //System.Net.NetworkInformation.PingReply pingStatus = ping.Send(strUrl, 1000);
                //System.Net.NetworkInformation.PingReply pingStatus = ping.Send(IPAddress.Parse("208.69.34.231"), 1000);
                if (pingStatus.Status == System.Net.NetworkInformation.IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public static bool DnsTest(string strUrl)
        {
            try
            {
                System.Net.IPHostEntry ipHe =
                    System.Net.Dns.GetHostByName(strUrl);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /*
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int connDescription, int ReservedValue);

        //check if a connection to the Internet can be established 
        public static bool IsConnectionAvailable()
        {
            int Desc;
            return InternetGetConnectedState(out connDesc, 0);
        }
         * */
    }
}
