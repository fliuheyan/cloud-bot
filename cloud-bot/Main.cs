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
        private static int LoadDelay = 1000;
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
            ProcessStartInfo psi = new ProcessStartInfo(Path.Combine(D2Path, D2Exe), D2Args)
            {
                UseShellExecute = false,
                WorkingDirectory = D2Path
            };
            Process diabloProcess = Process.Start(psi);
            System.Threading.Thread.Sleep(LoadDelay);
            ProcessWrapper pw = new ProcessWrapper(diabloProcess);
            processes.Add(pw);
        }

        private void injectButton_Click(object sender, EventArgs e)
        {
            if (processListView.SelectedIndex == -1)
            {
                SetStatus("No process selected!", Color.Blue);
                return;
            }
            ProcessWrapper pw = processListView.SelectedItem as ProcessWrapper;
            if (Attach(pw.Process))
            {
                SetStatus("Success!", Color.Green);
                pw.Loaded = true;
            }
            else
            {
                SetStatus("Failed!", Color.Red);
                pw.Loaded = false;
            }
        }

        public Main(string[] args)
        {
            InitializeComponent();
            processes.RaiseListChangedEvents = true;
            processListView.DataSource = processes;
            processListView.DisplayMember = "ProcessName";
        }

        //utils
        private delegate void StatusCallback(string status, System.Drawing.Color color);
        private void SetStatus(string status, Color color)
        {
            // ignore the call if we haven't init'd the window yet
            if (statusLabel == null)
                return;

            if (statusLabel.InvokeRequired)
            {
                StatusCallback cb = SetStatus;
                statusLabel.Invoke(cb, status, color);
                return;
            }
            statusLabel.ForeColor = color;
            statusLabel.Text = status;
        }

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

        //most important funciton
        private static bool Attach(Process p)
        {
            string js32 = Path.Combine(Application.StartupPath, "js32.dll"),
                  libnspr = Path.Combine(Application.StartupPath, "libnspr4.dll");

            return File.Exists(libnspr) && File.Exists(js32) &&
              PInvoke.Kernel32.LoadRemoteLibrary(p, libnspr) &&
              PInvoke.Kernel32.LoadRemoteLibrary(p, js32);
            /*
                        string js32 = Path.Combine(Application.StartupPath, "js32.dll"),
                               libnspr = Path.Combine(Application.StartupPath, "libnspr4.dll"),
                               d2bs = Path.Combine(Application.StartupPath, D2BSDLL);
                        return File.Exists(libnspr) && File.Exists(js32) && File.Exists(d2bs) &&
                                PInvoke.Kernel32.LoadRemoteLibrary(p, libnspr) &&
                                PInvoke.Kernel32.LoadRemoteLibrary(p, js32) &&
                                PInvoke.Kernel32.LoadRemoteLibrary(p, d2bs);
                                */
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
