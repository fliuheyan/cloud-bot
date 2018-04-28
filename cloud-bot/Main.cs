using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Management;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace cloud_bot
{
    public partial class Main : Form
    {
        private static string D2Path = "D:\\game\\D2",
                              D2Exe = "D2loader.exe",
                              D2Args = "-w -noplugin",
                              D2BSDLL = String.Empty;
        private BindingList<ProcessWrapper> processes = new BindingList<ProcessWrapper>();

        private void refreshButton_Click(object sender, EventArgs e)
        {
            processes.Clear();
            foreach (Process process in Process.GetProcesses())
            {
                if (IsD2Window(process))
                {
                    Console.WriteLine("ok we have d2");
                    ProcessWrapper pw = new ProcessWrapper(process);
                    processes.Add(pw);

                }
            }
        }

        private void createButton_Click(object sender, EventArgs e)
        {

        }

        public Main(string[] args)
        {
            InitializeComponent();
            processes.RaiseListChangedEvents = true;
            Console.WriteLine("################222222");
            processListView.DataSource = processes;
            processListView.DisplayMember = "ProcessName";
        }

        //utils
        private static bool IsD2Window(Process p)
        {
            if (p == null)
                return false;
            string moduleName = p.ProcessName.ToLowerInvariant();
            return moduleName == "game.exe" || moduleName.Contains("d2loader") ||
                         moduleName.Contains("d2launcher");
        }
        private static string GetLCClassName(Process p) { return PInvoke.User32.GetClassNameFromProcess(p).ToLowerInvariant(); }

        private static string GetMainModuleFilepath(int processId)
        {
            string wmiQueryString = "SELECT ProcessId, ExecutablePath FROM Win32_Process WHERE ProcessId = " + processId;

            using (var searcher = new ManagementObjectSearcher(wmiQueryString))
            {
                using (var results = searcher.Get())
                {
                    ManagementObject mo = results.Cast<ManagementObject>().FirstOrDefault();
                    if (mo != null)
                    {
                        return mo["ExecutablePath"] == null ? "" : (string)mo["ExecutablePath"];
                    }
                }
            }
            return "";
        }
    }

    class ProcessWrapper
    {
        public bool Loaded { get; set; }
        public Process Process { get; internal set; }
        public string ProcessName
        {
            get
            {
                bool exited = false;
                try { exited = Process.HasExited; } catch { }
                if (exited)
                    return "Exited...";
                return Process.MainWindowTitle + " [" + Process.Id + "]" + (Loaded ? " *" : "");
            }
        }
        public ProcessWrapper(Process p) { Process = p; }
    }
}
