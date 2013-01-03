using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using SharedClasses;

namespace MonitorClipboardDebugAssertions
{
	public partial class MainForm : Form
	{
		const string settingsfileProcessname = "processname.fjset";
		const string settingsfileProcessarguments = "processarguments.fjset";
		const string settingsfileRootAlbionDirectory = "rootalbionfolder.fjset";
		//const string rootAlbionFolder = @"C:\devKiln\Albion\";

		public MainForm()
		{
			InitializeComponent();

			nextClipboardViewer = SetClipboardViewer(this.Handle);
			ClipboardChanged += new EventHandler<ClipboardChangedEventArgs>(Form1_ClipboardChanged);
		}

		private static void ResetCurrentDir()
		{
			var path = Environment.GetCommandLineArgs()[0].TrimEnd('\\');
			Environment.CurrentDirectory = Path.GetDirectoryName(path);
		}

		public string ProcessNameOrFullpath
		{
			get
			{
				ResetCurrentDir();
				if (File.Exists(settingsfileProcessname))
					return File.ReadAllText(settingsfileProcessname).Trim();
				else
				{
					var tmpstr = SharedClasses.DialogBoxStuff.InputDialog("Please enter the process name or fullpath, ie. notepad, notepad2, notepad++");
					if (tmpstr != "")
					{
						//ProcessNameOrFullpath = tmpstr;
						File.WriteAllText(settingsfileProcessname, tmpstr);
						return tmpstr;
					}
					else
					{
						UserMessages.ShowWarningMessage("No process name entered, will not be able to run");
						return "";
					}
				}
			}
			set
			{
				try
				{
					ResetCurrentDir();
					File.WriteAllText(settingsfileProcessname, value);
				}
				catch (Exception exc)
				{
					UserMessages.ShowErrorMessage("Error writing file: " + exc.Message);
				}
			}
		}
		public string ProcessArguments
		{
			get
			{
				ResetCurrentDir();
				if (File.Exists(settingsfileProcessarguments))
					return File.ReadAllText(settingsfileProcessarguments).Trim();
				else
				{
					var tmpstr = SharedClasses.DialogBoxStuff.InputDialog("Please enter the process arguments with {0} for linenumber and {1} for filepath, ie."
						+ Environment.NewLine
						+ "notepad2 = /g {0} \"{1}\""
						+ Environment.NewLine
						+ "notepad++ = -n{0}\"{1}\"").Trim();
					if (tmpstr != "")
					{
						//ProcessNameOrFullpath = tmpstr;
						File.WriteAllText(settingsfileProcessarguments, tmpstr);
						return tmpstr;
					}
					else
					{
						UserMessages.ShowWarningMessage("No process arguments entered, will not be able to run correctly.");
						return "";
					}
				}
			}
			set
			{
				try
				{
					ResetCurrentDir();
					File.WriteAllText(settingsfileProcessarguments, value);
				}
				catch (Exception exc)
				{
					UserMessages.ShowErrorMessage("Error writing file: " + exc.Message);
				}
			}
		}
		public string RootAlbionDirectory
		{
			get
			{
				ResetCurrentDir();
				if (File.Exists(settingsfileRootAlbionDirectory))
					return File.ReadAllText(settingsfileRootAlbionDirectory).Trim();
				else
				{
					var tmpstr = SharedClasses.DialogBoxStuff.InputDialog("Please enter the root directory for Albion, the one containing proj folder.").Trim();
					if (tmpstr != "")
					{
						//ProcessNameOrFullpath = tmpstr;
						if (!tmpstr.EndsWith("\\"))
							tmpstr += "\\";
						if (!Directory.Exists(tmpstr))
							UserMessages.ShowWarningMessage("Directory does not exist, this will cause issues on trying to open assertion source file.");
						File.WriteAllText(settingsfileRootAlbionDirectory, tmpstr);
						return tmpstr;
					}
					else
					{
						UserMessages.ShowWarningMessage("No albion directory entered, this will cause issues on trying to open assertion source file.");
						return "";
					}
				}
			}
			set
			{
				try
				{
					ResetCurrentDir();
					File.WriteAllText(settingsfileRootAlbionDirectory, value);
				}
				catch (Exception exc)
				{
					UserMessages.ShowErrorMessage("Error writing file: " + exc.Message);
				}
			}
		}

		void Form1_ClipboardChanged(object sender, MainForm.ClipboardChangedEventArgs e)
		{
			var textData = e.DataObject.GetData(DataFormats.Text);
			if (textData != null)
			//if (e.DataObject.GetDataPresent(DataFormats.Text))
			{
				string fullpath = null;
				int lineNum = -1;

				string clipboardText = textData.ToString();//e.DataObject.GetData(DataFormats.Text).ToString();
				if (
					clipboardText.IndexOf("Microsoft Visual C++ Debug Library", StringComparison.InvariantCultureIgnoreCase) != -1 &&
					clipboardText.IndexOf("Debug Assertion Failed!", StringComparison.InvariantCultureIgnoreCase) != -1 &&
					clipboardText.IndexOf("File: ", StringComparison.InvariantCultureIgnoreCase) != -1)
				{
					string[] lines = clipboardText.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
					string tmpFileColon = "File: ";
					string tmpLineColon = "Line: ";
					for (int i = 0; i < lines.Length; i++)
						if (lines[i].StartsWith(tmpFileColon, StringComparison.InvariantCultureIgnoreCase))
						{
							string relFilepath = lines[i].Substring(tmpFileColon.Length);
							fullpath = RootAlbionDirectory + relFilepath;//rootAlbionFolder + relFilepath;
						}
						else if (lines[i].StartsWith(tmpLineColon, StringComparison.InvariantCultureIgnoreCase))
						{
							if (!int.TryParse(lines[i].Substring(tmpLineColon.Length), out lineNum))
								lineNum = -1;
						}
				}
				else if (
					clipboardText.IndexOf("AdbAssert", StringComparison.InvariantCultureIgnoreCase) != -1 &&
					clipboardText.IndexOf("Adb assertion failed: ", StringComparison.InvariantCultureIgnoreCase) != -1)
				{
					string[] lines = clipboardText.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
					string tmpFailedColon = "Adb assertion failed: ";
					for (int i = 0; i < lines.Length; i++)
						if (lines[i].StartsWith(tmpFailedColon, StringComparison.InvariantCultureIgnoreCase))
						{
							int lastColonIndex = lines[i].LastIndexOf(':');
							string relFilePath = lines[i].Substring(tmpFailedColon.Length, lastColonIndex - tmpFailedColon.Length);
							if (File.Exists(relFilePath))
								fullpath = relFilePath;
							else
								fullpath = RootAlbionDirectory + relFilePath;
							if (!int.TryParse(lines[i].Substring(lastColonIndex + 2), out lineNum))
								lineNum = -1;
						}
					//Adb assertion failed: proj\adb\Wrap\ModTable.cpp: 155
				}

				if (fullpath != null && lineNum != -1)
				{
					if (!File.Exists(fullpath))
						UserMessages.ShowWarningMessage("Cannot find file: " + fullpath);
					else
					{
						try
						{
							var proc = Process.Start(
								ProcessNameOrFullpath,
								string.Format(ProcessArguments, lineNum, fullpath));
							//Process.Start("notepad",
							//string.Format("/g {0} \"{1}\"", lineNum, fullpath));
							//Process.Start("notepad++",
							//string.Format("-n{0}\"{1}\"", lineNum, fullpath));
							Win32Api.SetForegroundWindow(proc.MainWindowHandle);
						}
						catch (Exception Exception)
						{
							UserMessages.ShowErrorMessage("Error occurred opening file and then SetForegroundWindow: " + Exception.Message);
						}
					}
				}
				/*
				 ---------------------------
				AdbAssert
				---------------------------
				Adb assertion failed: proj\adb\Wrap\ModTable.cpp: 155
				---------------------------
				OK   
				--------------------------- 
				*/
				/*
				---------------------------
				Microsoft Visual C++ Debug Library
				---------------------------
				Debug Assertion Failed!

				Program: ...ln\Albion\tundra-output\win32-baked-debug-default\Wadiso6.exe
				File: proj\adb\Triggers\StdTriggers.cpp
				Line: 515

				For information on how your program can cause an assertion
				failure, see the Visual C++ documentation on asserts.

				(Press Retry to debug the application)
				---------------------------
				Abort   Retry   Ignore   
				---------------------------
				*/
			}
		}

		#region Extra stuff

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);
		public event EventHandler<ClipboardChangedEventArgs> ClipboardChanged;
		private IntPtr nextClipboardViewer;

		[DllImport("User32.dll")]
		protected static extern int SetClipboardViewer(int hWndNewViewer);

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

		private void Form1_Shown(object sender, EventArgs e)
		{
			this.Hide();

		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			ChangeClipboardChain(this.Handle, nextClipboardViewer);
		}

		private void changeProcessnameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			File.Delete(settingsfileProcessname);
			var t = ProcessNameOrFullpath;
		}

		private void changeProcessargumentsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			File.Delete(settingsfileProcessarguments);
			var t = ProcessArguments;
		}

		private void changerootAlbionDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			File.Delete(settingsfileRootAlbionDirectory);
			var t = RootAlbionDirectory;
		}

		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			// defined in winuser.h
			const int WM_DRAWCLIPBOARD = 0x308;
			const int WM_CHANGECBCHAIN = 0x030D;

			switch (m.Msg)
			{
				case WM_DRAWCLIPBOARD:
					OnClipboardChanged();
					SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
					break;

				case WM_CHANGECBCHAIN:
					if (m.WParam == nextClipboardViewer)
						nextClipboardViewer = m.LParam;
					else
						SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
					break;

				default:
					base.WndProc(ref m);
					break;
			}
		}

		void OnClipboardChanged()
		{
			try
			{
				IDataObject iData = Clipboard.GetDataObject();
				if (ClipboardChanged != null)
				{
					ClipboardChanged(this, new ClipboardChangedEventArgs(iData));
				}

			}
			catch (Exception e)
			{
				// Swallow or pop-up, not sure
				// Trace.Write(e.ToString());
				UserMessages.ShowErrorMessage(e.ToString());
			}
		}

		public class ClipboardChangedEventArgs : EventArgs
		{
			public readonly IDataObject DataObject;

			public ClipboardChangedEventArgs(IDataObject dataObject)
			{
				DataObject = dataObject;
			}
		}

		#endregion Extra stuff

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutWindow2.ShowAboutWindow(new System.Collections.ObjectModel.ObservableCollection<DisplayItem>()
			{
				new DisplayItem("Author", "Francois Hill"),
				new DisplayItem("Icon(s) obtained from", null)

			});
		}
	}
}
