using System;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using System.IO;
using System.Windows.Resources;
using System.Net;
using System.Reflection;
using System.Windows.Controls;

namespace DYRuntime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string currentPath = AppDomain.CurrentDomain.BaseDirectory;
        string autoKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        string autoKeyName = "DYRuntime";
        string mydataKey = "SOFTWARE";
        string mydataKeyName = "DY";
        public MainWindow()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainWindow_Loaded);
            bt_启动.Click += new RoutedEventHandler(bt_启动_Click);
            bt_iisInstall.Click += new RoutedEventHandler(bt_iisInstall_Click);
            App.Current.Exit += new ExitEventHandler(Current_Exit);
            bt_selectPath.Click += new RoutedEventHandler(bt_selectPath_Click);
            tbk_logo.MouseLeftButtonDown += new MouseButtonEventHandler(tbk_logo_MouseLeftButtonDown);
            tgb_autorun.Click += new RoutedEventHandler(tgb_autorun_Click);
        }

        void tgb_autorun_Click(object sender, RoutedEventArgs e)
        {
            if (tgb_autorun.IsChecked.Value)
            {
                if (bt_启动.IsEnabled == false)
                {
                    other.GetOther().createKeySub(mydataKey, mydataKeyName, tb_ip.Text + "," + tb_port.Text + "," + tb_path.Text + "," + cb_mode.SelectedIndex.ToString() +"," +tb_sslPort.Text);
                    other.GetOther().createKeySub(autoKey, autoKeyName, currentPath + Assembly.GetExecutingAssembly().Location);
                }
                else
                {
                    tgb_autorun.IsChecked = false;
                    MessageBox.Show("此功能必须在本运行时启动服务后才可以设置!", "东雅软件 DYOK.NET", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                if (other.GetOther().IsRegeditExit(mydataKey, mydataKeyName))
                {
                    other.GetOther().DeleteRegist(mydataKey, mydataKeyName);
                }
                if (other.GetOther().IsRegeditExit(autoKey, autoKeyName))
                {
                    other.GetOther().DeleteRegist(autoKey, autoKeyName);
                }
            }
            refresh();
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (other.GetOther().IsRegeditExit(mydataKey, mydataKeyName))
            {
                tgb_autorun.IsChecked = true;
                var loaddata = other.GetOther().GetRegistData(mydataKey, mydataKeyName);
                var data = loaddata.Split(',');
                tb_ip.Text = data[0];
                tb_port.Text = data[1];
                tb_path.Text = data[2];
                cb_mode.SelectedIndex = int.Parse(data[3]);
                tb_sslPort.Text = data[4];
                bt_启动_Click(null, null);
            }
            else
            {
                tb_ip.Text = getIPAddress();
                tgb_autorun.IsChecked = false;
            }
            refresh();
            FileVersionInfo v = FileVersionInfo.GetVersionInfo("DYRuntime.exe");
            tbk_Ver.Text = "版本号：" + v.FileVersion; 
        }

        void refresh()
        {
            if (tgb_autorun.IsChecked.Value)
            {
                tgb_autorun.Content = "取消开机自动运行";
            }
            else
            {
                tgb_autorun.Content = "开机自动运行";
            }
        }

        void tbk_logo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("http://dyok.net");
        }

        void bt_selectPath_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();
            if (folderDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tb_path.Text = folderDlg.SelectedPath;
            }
        }

        void Current_Exit(object sender, ExitEventArgs e)
        {
            if (File.Exists(currentPath + "httpcfg.exe"))
            {
                File.Delete(currentPath + "httpcfg.exe");
            }

            if (File.Exists(currentPath + "iisexpress_1_11_x86_zh-CN.msi"))
            {
                File.Delete(currentPath + "iisexpress_1_11_x86_zh-CN.msi");
            }

            Process[] allProcess = Process.GetProcesses();
            foreach (Process process in allProcess)
            {
                if (process.ProcessName == "iisexpress")
                {
                    process.Kill();
                }
            }
        }

        void bt_iisInstall_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(currentPath + "iisexpress_1_11_x86_zh-CN.msi"))
            {
                createFile("iisexpress_1_11_x86_zh-CN.msi");
            }
            Process myNewProcess = new Process();
            myNewProcess.StartInfo.FileName = "iisexpress_1_11_x86_zh-CN.msi";
            myNewProcess.StartInfo.WorkingDirectory = currentPath;
            myNewProcess.Start();
        }

        Process myNewProcess;
        void bt_启动_Click(object sender, RoutedEventArgs e)
        {
            var pfpath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            //if (Directory.Exists(@"C:\Program Files (x86)"))
            //{
                if (!File.Exists(pfpath + @"\IIS Express\iisexpress.exe"))
                {
                    MessageBox.Show(@"您的系统还没有安装本程序依赖组件！请点击“安装运行依赖组件”按扭完成安装后再启动服务！");
                    return;
                }
            //}
            //else if (Directory.Exists(@"C:\Program Files"))
            //{
            //    if (!File.Exists(@"C:\Program Files\IIS Express\iisexpress.exe"))
            //    {
            //        MessageBox.Show(@"您的系统还没有安装本程序依赖组件！请点击“安装运行依赖组件”按扭完成安装后再启动服务！");
            //        return;
            //    }
            //}

            if (tb_ip.Text == string.Empty || tb_ip.Text.Contains(","))
            {
                MessageBox.Show("IP地址不是能");
                return;
            }

            if (tb_port.Text == string.Empty || tb_port.Text.Contains(","))
            {
                MessageBox.Show("端口不能空");
                return;
            }

            if (tb_sslPort.Text == string.Empty || tb_sslPort.Text.Contains(","))
            {
                MessageBox.Show("SSL端口不能空");
                return;
            }

            string cip = tb_ip.Text;
            string cpt = tb_port.Text;
            string sslCpt = tb_sslPort.Text;

            var myd = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var mydoc = myd + @"\IISExpress\config";
            if (!Directory.Exists(mydoc))
            {
                Directory.CreateDirectory(mydoc);
            }
            Uri uri = new Uri("/applicationhost1.txt", UriKind.Relative);
            StreamResourceInfo info = Application.GetResourceStream(uri);
            Stream s = info.Stream;
            byte[] buffer = new byte[s.Length];
            s.Read(buffer, 0, buffer.Length);
            string x = Encoding.UTF8.GetString(buffer);
            s.Close();

            if (x != null)
            {
                string pt = tb_path.Text;
                if (!Directory.Exists(pt))
                {
                    MessageBox.Show(@"本机不存在您所指定的程序目录,请重新选择程序目录", "东雅软件 DYOK.NET", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var astr = x.Replace("{dypp}", pt);
                var a1str = astr.Replace("{sslPort}", sslCpt);
                var bstr = a1str.Replace("{dyPort}", tb_port.Text);
                var bstr1 = bstr.Replace("{dyMode}", (cb_mode.SelectedItem as ComboBoxItem).Tag.ToString());
                var cstr = bstr1.Replace("{dyip}", tb_ip.Text);
                string dstr = string.Empty;
                //if (Directory.Exists(pfpath + @"\IIS Express"))
                //{
                    dstr = cstr.Replace(@"%IIS_USER_HOME%\config",
                        pfpath + @"\IIS Express\config\templates\PersonalWebServer");
                //}
                //else if (Directory.Exists(@"C:\Program Files\IIS Express"))
                //{
                //    dstr = cstr.Replace(@"%IIS_USER_HOME%\config",
                //        @"C:\Program Files\IIS Express\config\templates\PersonalWebServer");
                //}
                FileStream fsFile = new FileStream(mydoc + @"\applicationhost.config", FileMode.Create);
                byte[] byteData = Encoding.UTF8.GetBytes(dstr);
                fsFile.Seek(0, SeekOrigin.Begin);
                fsFile.Write(byteData, 0, byteData.Length);
                fsFile.Close();
                //if (Directory.Exists(@"C:\Program Files (x86)\IIS Express"))
                //{
                    myNewProcess = new Process();
                    myNewProcess.StartInfo.FileName = "iisexpress.exe";
                    myNewProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    myNewProcess.StartInfo.WorkingDirectory = pfpath + @"\IIS Express";
                    myNewProcess.Start();
                //}
                //else if (Directory.Exists(@"C:\Program Files\IIS Express"))
                //{
                //    myNewProcess = new Process();
                //    myNewProcess.StartInfo.FileName = "iisexpress.exe";
                //    myNewProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //    myNewProcess.StartInfo.WorkingDirectory = @"C:\Program Files\IIS Express";
                //    myNewProcess.Start();
                //}
                bt_启动.IsEnabled = false;
                cb_mode.IsEnabled = false;
                tb_ip.IsReadOnly = true;
                tb_port.IsReadOnly = true;
                tb_sslPort.IsReadOnly = true;
                bt_selectPath.IsEnabled = false;
                bt_iisInstall.IsEnabled = false;
                var osv = getOsV();
                if (osv == winOS.xp)
                {
                    string exeFile = "httpcfg.exe";
                    if (!File.Exists(currentPath + exeFile))
                    {
                        createFile(exeFile);
                    }
                    string cmd = string.Format(" set urlacl /u http://{0}:{1} /a D:(A;;GX;;;WD)", cip, cpt);
                    excute(exeFile, cmd);
                    cmd = string.Format(" set urlacl /u http://{0}:{1} /a D:(A;;GX;;;WD)", cip, sslCpt);
                    excute(exeFile, cmd);
                }
                else if (osv == winOS.win7)
                {
                    string exeFile = "cmd.exe";
                    string cmd = string.Format("netsh http add urlacl url=http://{0}:{1} user=everyone", cip, cpt);
                    excute(exeFile, cmd);
                    cmd = string.Format("netsh http add urlacl url=http://{0}:{1} user=everyone", cip, sslCpt);
                    excute(exeFile, cmd);
                }
            }
        }

        void excute(string exe, string cmd)
        {
            Process compiler = new Process();
            compiler.StartInfo.FileName = exe;
            compiler.StartInfo.Arguments = "/c " + cmd;
            compiler.StartInfo.WorkingDirectory = currentPath;
            compiler.StartInfo.UseShellExecute = false;
            compiler.StartInfo.CreateNoWindow = true;
            compiler.Start();
        }

        void createFile(string fileName)
        {
            Uri uri = new Uri("/"+fileName, UriKind.Relative);
            StreamResourceInfo info = Application.GetResourceStream(uri);
            Stream s = info.Stream;
            byte[] buffer = new byte[s.Length];
            s.Read(buffer, 0, buffer.Length);
            s.Close();
            FileStream fsFile = new FileStream(currentPath + fileName, FileMode.Create);
            fsFile.Seek(0, SeekOrigin.Begin);
            fsFile.Write(buffer, 0, buffer.Length);
            fsFile.Close();
            FileInfo fi = new FileInfo(currentPath + fileName);
            fi.Attributes = fi.Attributes | FileAttributes.Hidden;
        }

        winOS getOsV()
        {
            if (Environment.OSVersion.Version.Major < 6)
            {
                return winOS.xp;
            }
            return winOS.win7;
        }

        string getIPAddress()
        {
            System.Net.IPAddress addr;
            // 获得本机局域网IP地址
            addr = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
            return addr.ToString();
        }

        void RunCmd(string workDir, string command)
        {
            //Thread t = new Thread(new ThreadStart(() => {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";          
            p.StartInfo.Arguments = "/c " + command;  
            p.StartInfo.UseShellExecute = false;       
            p.StartInfo.RedirectStandardInput = true;   
            p.StartInfo.RedirectStandardOutput = true; 
            p.StartInfo.RedirectStandardError = true;   
            p.StartInfo.CreateNoWindow = true;         
            p.StartInfo.WorkingDirectory = workDir;
            p.Start();   
            //p.StandardInput.WriteLine(command);      
            //p.StandardInput.WriteLine("exit");       
            //p.Close();
            //}));
            //t.Start();
        }

    }

    enum winOS
    {
        xp, win7
    }
}
