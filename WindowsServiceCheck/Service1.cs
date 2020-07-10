using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsServiceCheck
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        const string _serviceName = "wuauserv";
        public Service1()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is started at " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000; //number in milisecinds  
            timer.Enabled = true;
        }
        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now);
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            if (ServiceExists(_serviceName))
            {
                if (!ServiceIsRunning(_serviceName))
                    WriteToFile(StartService(_serviceName));

            }
            WriteToFile("Service is recall at " + DateTime.Now);
        }
        public void WriteToFile(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(message);
                }
            }
        }
        public bool ServiceExists(string serviceName)
        {
            return ServiceController.GetServices().Any(serviceController => serviceController.ServiceName.Equals(serviceName));
        }
        public bool ServiceIsRunning(string serviceName)
        {
            ServiceController sc = new ServiceController();
            sc.ServiceName = serviceName;

            if (sc.Status == ServiceControllerStatus.Running)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string StartService(string serviceName)
        {
            StringBuilder sb = new StringBuilder();
            ServiceController sc = new ServiceController();
            sc.ServiceName = serviceName;

            sb.Append("The ");
            sb.Append(serviceName);
            sb.Append(" service status is currently set to ");
            sb.Append(sc.Status.ToString());
            sb.Append("\n");

            if (sc.Status == ServiceControllerStatus.Stopped)
            {
                // Start the service if the current status is stopped.
                sb.Append("Starting the "); 
                sb.Append(serviceName);
                sb.Append(" service ...");
                sb.Append("\n");
                try
                {
                    // Start the service, and wait until its status is "Running".
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running);

                    // Display the current service status.
                    sb.Append("The ");
                    sb.Append(serviceName);
                    sb.Append(" status is now set to ");
                    sb.Append(sc.Status.ToString());
                    sb.Append("\n");
                }
                catch (InvalidOperationException e)
                {
                    sb.Append("Could not start the ");
                    sb.Append(serviceName);
                    sb.Append(" service.");
                    sb.Append("\n");
                    sb.Append(e.Message);
                }
            }
            else
            {
                sb.Append("Service ");
                sb.Append(serviceName);
                sb.Append(" already running.");
                sb.Append("\n");
            }
            return sb.ToString();
        }
        public string stopService(string serviceName)
        {
            StringBuilder sb = new StringBuilder();
            ServiceController sc = new ServiceController();
            sc.ServiceName = serviceName;

            sb.Append("The ");
            sb.Append(serviceName);
            sb.Append(" service status is currently set to ");
            sb.Append(sc.Status.ToString());
            sb.Append("\n");

            if (sc.Status == ServiceControllerStatus.Running)
            {
                // Stopping the service if the current status is running.
                sb.Append("Stopping the ");
                sb.Append(serviceName);
                sb.Append(" service ...");
                sb.Append("\n");
                try
                {
                    // Stop the service, and wait until its status is "Stopping".
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped);

                    // Display the current service status.
                    sb.Append("The ");
                    sb.Append(serviceName);
                    sb.Append(" status is now set to ");
                    sb.Append(sc.Status.ToString());
                    sb.Append("\n");
                }
                catch (InvalidOperationException e)
                {
                    sb.Append("Could not stop the ");
                    sb.Append(serviceName);
                    sb.Append(" service.");
                    sb.Append("\n");
                    sb.Append(e.Message);
                }
            }
            else
            {
                sb.Append("Cannot stop service ");
                sb.Append(serviceName);
                sb.Append(" because it's already inactive.");
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}
