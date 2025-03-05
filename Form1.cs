//============================================================================
// TaskBarFolder 1.0
// Copyright © 2025 Stephanowicz
// 
//This file is part of TaskBarFolder.
//
//TaskBarFolder is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//TaskBarFolder is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with TaskBarFolder.  If not, see <http://www.gnu.org/licenses/>.
//
//============================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml;
using TaskBarFolder.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using IDataObject = System.Windows.Forms.IDataObject;
using Path = System.IO.Path;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Windows.Documents;
using Control = System.Windows.Forms.Control;

namespace TaskBarFolder
{
    public partial class Form1 : Form
    {
        FileBrowser fb;
        string filePath = Directory.GetCurrentDirectory();
        string settings;
        string iconPath = "";
        Icon TaskBarIcon;
        bool bToolTips = false, bFileExt = false, bHidden = false, bSmallIcons = false;
        public List<folderItems> folderItemsList = new List<folderItems>();

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        public Form1()
        {
            InitializeComponent();
            DropDownMenuScrollWheelHandler.Enable(true);
            settings = filePath + "\\settings.xml";
            loadXML();
            if (bSmallIcons)
            {
                cmsFolderBrowser.ImageList = imageListSmall;
                cmsFolderBrowser.ImageScalingSize = new Size(16, 16);
            }

            fb = new FileBrowser(cmsFolderBrowser, 0, 0, this);
            if (folderItemsList.Count > 0)
            {
                fb.populateList(folderItemsList);
            }
            if (iconPath.Length > 0 && File.Exists(iconPath))
            {
                TaskBarIcon = System.Drawing.Icon.ExtractAssociatedIcon(iconPath);
            }
            else
            {
                TaskBarIcon = (System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon"));
            }
            this.notifyIcon1.Icon = TaskBarIcon;
            btnIco.BackgroundImage = TaskBarIcon.ToBitmap();
            cbToolTips.Checked = bToolTips;
            cbFileExt.Checked = bFileExt;
            cbHidden.Checked = bHidden;
            cbSmallIcons.Checked = bSmallIcons;
        }

        #region notifyIcon
        [DllImport("User32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool SetForegroundWindow(HandleRef hWnd);
        private void notifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (cmsFolderBrowser.Items.Count > 0)
                {
                    SetForegroundWindow(new HandleRef(this, this.Handle));
                    this.cmsFolderBrowser.Show(this, this.PointToClient(Cursor.Position));
                }
                else
                {
                    settingsToolStripMenuItem_Click(null, null);
                }
            }
        }
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            //open folder if only one in list
            if (folderItemsList != null && folderItemsList.Count == 1 && Directory.Exists(folderItemsList[0].Path))
            {
                Process.Start(folderItemsList[0].Path);
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var x = Screen.GetBounds(this);
            this.WindowState = FormWindowState.Normal;
            this.Top = x.Height - this.Height - 100;
            this.Left = x.Width - this.Width - 100;
            this.Show();
        }

        #endregion notifyIcon 
        #region settings window
        public bool tooltips
        {
            get { return bToolTips; }
        }
        public bool fileExt
        {
            get { return bFileExt; }
        }
        public bool hidden
        {
            get { return bHidden; }
        }
        public bool smallIcons
        {
            get { return bSmallIcons; }
        }

        public ImageList imageList
        {
            get { return bSmallIcons ? imageListSmall : imageList1; }
        }

        private void cbToolTips_Click(object sender, EventArgs e)
        {
            bToolTips = cbToolTips.Checked;
            saveXML();
        }

        private void cbFileExt_Click(object sender, EventArgs e)
        {
            bFileExt = cbFileExt.Checked;
            fb.populateList(folderItemsList);
            saveXML();
        }

        private void cbHidden_Click(object sender, EventArgs e)
        {
            bHidden = cbHidden.Checked;
            fb.populateList(folderItemsList);
            saveXML();
        }
        private void cbSmallIcons_Click(object sender, EventArgs e)
        {
            bSmallIcons = cbSmallIcons.Checked;
            if (bSmallIcons)
            {
               cmsFolderBrowser.ImageList = imageListSmall;
                cmsFolderBrowser.ImageScalingSize = new Size(16, 16);
            }
            else
            {
                cmsFolderBrowser.ImageList = imageList1;
                cmsFolderBrowser.ImageScalingSize = new Size(20, 20);
            }
            fb.populateList(folderItemsList);
            saveXML();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void btnIco_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "ico | *.ico";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String filename = openFileDialog.FileName;
                string ext = System.IO.Path.GetExtension(filename).ToLower();
                string path = System.IO.Path.GetFullPath(filename);
                TaskBarIcon = System.Drawing.Icon.ExtractAssociatedIcon(path);
                this.notifyIcon1.Icon = TaskBarIcon;
                btnIco.BackgroundImage = TaskBarIcon.ToBitmap();
                iconPath = path;
                saveXML();
            }
        }
        private void btnIcoDefault_Click(object sender, EventArgs e)
        {
            btnIco.BackgroundImage = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon"))).ToBitmap();
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            saveXML();
        }
        #region gridView
        private void btnGridViewUp_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows[0].Index > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;

                var row = dataGridView1.SelectedRows[0];
                if (row != null && row.Index > 0)
                {
                    var swapRow = dataGridView1.Rows[row.Index - 1];
                    object[] values = new object[swapRow.Cells.Count];

                    foreach (DataGridViewCell cell in swapRow.Cells)
                    {
                        values[cell.ColumnIndex] = cell.Value;
                        cell.Value = row.Cells[cell.ColumnIndex].Value;
                    }

                    foreach (DataGridViewCell cell in row.Cells)
                        cell.Value = values[cell.ColumnIndex];


                    folderItems x = folderItemsList[index - 1];
                    folderItemsList[index - 1] = folderItemsList[index];
                    folderItemsList[index] = x;
                    fb.populateList(folderItemsList);
                    saveXML();
                    dataGridView1.Rows[row.Index - 1].Selected = true;//have the selection follow the moving cell
                }
            }
        }
        private void btnGridViewDown_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows[0].Index > -1)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                var row = dataGridView1.SelectedRows[0];
                if (row != null && row.Index > -1)
                {
                    var swapRow = dataGridView1.Rows[row.Index + 1];
                    object[] values = new object[swapRow.Cells.Count];

                    foreach (DataGridViewCell cell in swapRow.Cells)
                    {
                        values[cell.ColumnIndex] = cell.Value;
                        cell.Value = row.Cells[cell.ColumnIndex].Value;
                    }

                    foreach (DataGridViewCell cell in row.Cells)
                        cell.Value = values[cell.ColumnIndex];

                    folderItems x = folderItemsList[index + 1];
                    folderItemsList[index + 1] = folderItemsList[index];
                    folderItemsList[index] = x;
                    fb.populateList(folderItemsList);
                    saveXML();
                    dataGridView1.Rows[index + 1].Selected = true;
                }
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int column = e.ColumnIndex;

            if (dataGridView1.Rows.Count >= row)
            {
                if (column == 0)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "ico | *.ico";
                    if (folderItemsList[row].Icon != "" && System.IO.File.Exists(folderItemsList[row].Icon))
                        openFileDialog.InitialDirectory = folderItemsList[row].Icon;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        String filename = openFileDialog.FileName;
                        string path = System.IO.Path.GetFullPath(filename);
                        folderItemsList[row].Icon = path;
                        dataGridView1.Rows[row].Cells["dgvIcon"].Value = System.Drawing.Image.FromFile(path);
                        fb.populateList(folderItemsList);
                        saveXML();
                    }
                }
                else if (column == 2)
                {
                    FolderBrowserDialogEx folderBrowserDialogEx = new FolderBrowserDialogEx
                    {
                        Description = "Select a new folder:",
                        ShowNewFolderButton = false,
                        ShowEditBox = true,
                        //NewStyle = false,
                        SelectedPath = folderItemsList[row].Path,
                        ShowFullPathInEditBox = true,
                    };
                    if (folderBrowserDialogEx.ShowDialog() == DialogResult.OK)
                    {
                        string path = folderBrowserDialogEx.SelectedPath;
                        dataGridView1.Rows[row].Cells["dgvPath"].Value = path;
                        //    dataGridView1.Rows[row].Cells["dgvName"].Value = name;
                        dataGridView1.Rows[row].Cells["dgvPath"].Style.ForeColor = System.Drawing.Color.Black;
                        folderItemsList[row].Path = path;
                        fb.populateList(folderItemsList);
                        saveXML();
                    }




                    //folderBrowserDialog1 = new FolderBrowserDialog();
                    //folderBrowserDialog1.SelectedPath = folderItemsList[row].Path;
                    //if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    //{
                    //    string path = folderBrowserDialog1.SelectedPath;
                    //    string name = new DirectoryInfo(path).Name;
                    //    dataGridView1.Rows[row].Cells["dgvPath"].Value = path;
                    //    //    dataGridView1.Rows[row].Cells["dgvName"].Value = name;
                    //    dataGridView1.Rows[row].Cells["dgvPath"].Style.ForeColor = System.Drawing.Color.Black;
                    //    folderItemsList[row].Path = path;
                    //    fb.populateList(folderItemsList);
                    //    saveXML();
                    //}
                }
            }
        }
        private void AddFolder(string path)
        {
            string name = new DirectoryInfo(path).Name;
            dataGridView1.Rows.Add(new object[] { imageList1.Images[0], name, path });
            folderItemsList.Add(new folderItems(name, path, ""));
            fb.populateList(folderItemsList);
            saveXML();
        }
        private void btnGridViewAdd_Click(object sender, EventArgs e)
        {
            FolderBrowserDialogEx folderBrowserDialogEx = new FolderBrowserDialogEx{
                Description = "Select a folder to add to the folderlist:",
                ShowNewFolderButton = false,
                ShowEditBox = true,
                //NewStyle = false,
                SelectedPath = "",
                ShowFullPathInEditBox = true,
            };
            if (folderBrowserDialogEx.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowserDialogEx.SelectedPath;
                AddFolder(path);
            }

            //folderBrowserDialog1 = new FolderBrowserDialog();
            //if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    string path = folderBrowserDialog1.SelectedPath;
            //    AddFolder(path);
            //}
        }

        private void btnGridViewDelete_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedRows[0].Index;
            dataGridView1.Rows.RemoveAt(index);
            folderItemsList.RemoveAt(index);
            fb.populateList(folderItemsList);
            saveXML();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count >0 && dataGridView1.SelectedRows[0].Index > -1)
            {
                btnGridViewDelete.Enabled = true;
                if(dataGridView1.SelectedRows[0].Index > 0) btnGridViewUp.Enabled = true;
                else btnGridViewUp.Enabled = false;
                if(dataGridView1.SelectedRows[0].Index < dataGridView1.Rows.Count-1) btnGridViewDown.Enabled = true;
                else btnGridViewDown.Enabled = false;
            }
            else btnGridViewDelete.Enabled = false;
            btnGridViewDefaultIcon.Enabled = false;
            btnGridViewName.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > -1)
            {
                if (dataGridView1.SelectedCells[0].ColumnIndex == 0)
                {
                    btnGridViewDefaultIcon.Enabled = true;
                }
                else btnGridViewDefaultIcon.Enabled = false;
                if (dataGridView1.SelectedCells[0].ColumnIndex == 1)
                {
                    tbFolderName.Text = dataGridView1.SelectedCells[0].Value.ToString();
                    btnGridViewName.Enabled = true;
                }
                else btnGridViewName.Enabled = false;
            }
            else {
                btnGridViewDefaultIcon.Enabled = false;
                btnGridViewName.Enabled = false;
            }
        }
        private void btnGridViewName_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells[0].ColumnIndex == 1)
            {
                if (tbFolderName.Text != "")
                {
                    int index = dataGridView1.SelectedCells[0].RowIndex;
                    folderItemsList[index].Name = tbFolderName.Text;
                    dataGridView1.SelectedCells[0].Value = tbFolderName.Text;

                    fb.populateList(folderItemsList);
                    saveXML();
                }
            }
        }
        private void btnGridViewDefaultIcon_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedCells[0].RowIndex;
            folderItemsList[index].Icon = "";
            dataGridView1.SelectedCells[0].Value = imageList1.Images[0];
            fb.populateList(folderItemsList);
            saveXML();
        }

        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            DragDropEffects effects = DragDropEffects.None;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                if (Directory.Exists(path))
                {
                    effects = DragDropEffects.Copy;
                }
            }
            e.Effect = effects;
        }
        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                if (Directory.Exists(path))
                {
                    AddFolder(path);
                }
            }
        }
        #endregion gridView

        #endregion settings window

        #region xml
        public void saveXML()
        {
            XmlNode node, subNode,childNode;
            XmlDocument doc = new XmlDocument();
            if (System.IO.File.Exists(settings))
            {
                doc.Load("settings.xml");
            }
            else
            {
                doc.InsertBefore(doc.CreateXmlDeclaration("1.0", "iso8859-1", null), doc.DocumentElement);
                node = doc.CreateElement("TaskBarFolder");
                doc.AppendChild(node);
            }
            node = doc.SelectSingleNode("//TaskBarFolder/toolTips");
            if (node == null)
            {
                node = doc.SelectSingleNode("//TaskBarFolder");
                node = node.AppendChild(doc.CreateElement("toolTips"));
            }
            node.InnerText = bToolTips.ToString();

            node = doc.SelectSingleNode("//TaskBarFolder/fileExt");
            if (node == null)
            {
                node = doc.SelectSingleNode("//TaskBarFolder");
                node = node.AppendChild(doc.CreateElement("fileExt"));
            }
            node.InnerText = bFileExt.ToString();

            node = doc.SelectSingleNode("//TaskBarFolder/hidden");
            if (node == null)
            {
                node = doc.SelectSingleNode("//TaskBarFolder");
                node = node.AppendChild(doc.CreateElement("hidden"));
            }
            node.InnerText = bHidden.ToString();

            node = doc.SelectSingleNode("//TaskBarFolder/small");
            if (node == null)
            {
                node = doc.SelectSingleNode("//TaskBarFolder");
                node = node.AppendChild(doc.CreateElement("small"));
            }
            node.InnerText = bSmallIcons.ToString();

            node = doc.SelectSingleNode("//TaskBarFolder/TaskBarIcon");
            if (node == null)
            {
                node = doc.SelectSingleNode("//TaskBarFolder");
                node = node.AppendChild(doc.CreateElement("TaskBarIcon"));
            }
            node.InnerText = iconPath;

            node = doc.SelectSingleNode("//TaskBarFolder/folders");
            if (node == null)
            {
                node = doc.SelectSingleNode("//TaskBarFolder");
                node = node.AppendChild(doc.CreateElement("folders"));
            }
            node.RemoveAll();
            foreach(folderItems x in folderItemsList)
            {
                XmlElement el = doc.CreateElement("folder");
                el.SetAttribute("name", x.Name);
                node.AppendChild(el);
                subNode = node.SelectSingleNode("folder[@name='" + x.Name + "']");
                childNode = editCreateNode(doc, subNode, "path");
                childNode.InnerText = x.Path;
                childNode = editCreateNode(doc, subNode, "icon");
                childNode.InnerText = x.Icon;
            }
            doc.Save("settings.xml");
        }

        public void loadXML()
        {
            XmlNode node;
            XmlDocument doc = new XmlDocument();
            String path, icon;
            if (System.IO.File.Exists(settings))
            {
                doc.Load("settings.xml");
                node = doc.SelectSingleNode("//TaskBarFolder/toolTips");
                if (node != null)
                {
                    bToolTips = Convert.ToBoolean(node.InnerText);
                }
                node = doc.SelectSingleNode("//TaskBarFolder/fileExt");
                if (node != null)
                {
                    bFileExt = Convert.ToBoolean(node.InnerText);
                }
                node = doc.SelectSingleNode("//TaskBarFolder/hidden");
                if (node != null)
                {
                    bHidden = Convert.ToBoolean(node.InnerText);
                }
                node = doc.SelectSingleNode("//TaskBarFolder/small");
                if (node != null)
                {
                    bSmallIcons = Convert.ToBoolean(node.InnerText);
                }
                node = doc.SelectSingleNode("//TaskBarFolder/TaskBarIcon");
                if (node != null)
                {
                    iconPath = node.InnerText;
                }
                node = doc.SelectSingleNode("//TaskBarFolder/folders");
                if (node != null)
                {
                    XmlNodeList list = node.SelectNodes("folder");
                    if (list != null)
                    {
                        foreach (XmlNode x in list)
                        {
                            path = x.SelectSingleNode("path").InnerText;
                            icon = x.SelectSingleNode("icon").InnerText;
                            folderItemsList.Add(new folderItems(x.Attributes[0].Value, path, icon));
                            System.Drawing.Image img;
                            if (icon != "" && System.IO.File.Exists(icon))
                            {
                                img = System.Drawing.Image.FromFile(icon);
                            }
                            else
                            {
                                img = imageList1.Images[0];
                            }
                            dataGridView1.Rows.Add(new object[] { img,x.Attributes[0].Value, path });
                            if (!Directory.Exists(path))
                            {
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["dgvPath"].Style.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
            }
        }

        XmlNode editCreateNode(XmlDocument doc, XmlNode parent, string sNode)
        {
            XmlNode subNode = parent.SelectSingleNode(sNode);
            if (subNode == null)
                subNode = parent.AppendChild(doc.CreateElement(sNode));
            return subNode;
        }

        #endregion xml
    }

    public class folderItems
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }

        public folderItems(string name, string path, string icon)
        {
            Name = name;
            Path = path;
            Icon = icon;
        }
    }

    //<summary>Get the rectangle of the visible Taskbar-Icon</summary>
    //<summary>The idea was to enable drag&drop by creating an invisible form above the icon...</summary>
    sealed class NotifyIconHelper
    {

        public static System.Drawing.Rectangle GetIconRect(NotifyIcon icon)
        {
            RECT rect = new RECT();
            NOTIFYICONIDENTIFIER notifyIcon = new NOTIFYICONIDENTIFIER();

            notifyIcon.cbSize = Marshal.SizeOf(notifyIcon);
            //use hWnd and id of NotifyIcon instead of guid is needed
            notifyIcon.hWnd = GetHandle(icon);
            notifyIcon.uID = GetId(icon);

            int hresult = Shell_NotifyIconGetRect(ref notifyIcon, out rect);
            //rect now has the position and size of icon
            if (hresult == 0)
                return new System.Drawing.Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
            else return new System.Drawing.Rectangle();
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public Int32 left;
            public Int32 top;
            public Int32 right;
            public Int32 bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct NOTIFYICONIDENTIFIER
        {
            public Int32 cbSize;
            public IntPtr hWnd;
            public Int32 uID;
            public Guid guidItem;
        }

        [DllImport("shell32.dll", SetLastError = true)]
        private static extern int Shell_NotifyIconGetRect([In] ref NOTIFYICONIDENTIFIER identifier, [Out] out RECT iconLocation);

        private static FieldInfo windowField = typeof(NotifyIcon).GetField("window", BindingFlags.NonPublic | BindingFlags.Instance);
        private static IntPtr GetHandle(NotifyIcon icon)
        {
            if (windowField == null) throw new InvalidOperationException("[Useful error message]");
            NativeWindow window = windowField.GetValue(icon) as NativeWindow;

            if (window == null) throw new InvalidOperationException("[Useful error message]");  // should not happen?
            return window.Handle;
        }

        private static FieldInfo idField = typeof(NotifyIcon).GetField("id", BindingFlags.NonPublic | BindingFlags.Instance);
        private static int GetId(NotifyIcon icon)
        {
            if (idField == null) throw new InvalidOperationException("[Useful error message]");
            return (int)idField.GetValue(icon);
        }

        public static bool InRect(Point point, System.Drawing.Rectangle rect)
         => point.X < rect.Right && point.X > rect.Left && point.Y < rect.Bottom && point.Y > rect.Top;


    }

    //<summary>Enable mousewheel scrolling</summary>
    //<summary>https://stackoverflow.com/a/27390000</summary>
    public class DropDownMenuScrollWheelHandler : System.Windows.Forms.IMessageFilter
    {
        private static DropDownMenuScrollWheelHandler Instance;
        public static void Enable(bool enabled)
        {
            if (enabled)
            {
                if (Instance == null)
                {
                    Instance = new DropDownMenuScrollWheelHandler();
                    Application.AddMessageFilter(Instance);
                }
            }
            else
            {
                if (Instance != null)
                {
                    Application.RemoveMessageFilter(Instance);
                    Instance = null;
                }
            }
        }
        private IntPtr activeHwnd;
        private ToolStripDropDown activeMenu;

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x200 && activeHwnd != m.HWnd) // WM_MOUSEMOVE
            {
                activeHwnd = m.HWnd;
                this.activeMenu = Control.FromHandle(m.HWnd) as ToolStripDropDown;
            }
            else if (m.Msg == 0x20A && this.activeMenu != null) // WM_MOUSEWHEEL
            {
                int delta = (short)(ushort)(((uint)(ulong)m.WParam) >> 16);
                handleDelta(this.activeMenu, delta);
                return true;
            }
            return false;
        }

        private static readonly Action<ToolStrip, int> ScrollInternal
            = (Action<ToolStrip, int>)Delegate.CreateDelegate(typeof(Action<ToolStrip, int>),
                typeof(ToolStrip).GetMethod("ScrollInternal",
                    System.Reflection.BindingFlags.NonPublic
                    | System.Reflection.BindingFlags.Instance));

        private void handleDelta(ToolStripDropDown ts, int delta)
        {
            if (ts.Items.Count == 0)
                return;
            var firstItem = ts.Items[0];
            var lastItem = ts.Items[ts.Items.Count - 1];
            if (lastItem.Bounds.Bottom < ts.Height && firstItem.Bounds.Top > 0)
                return;
            delta = delta / -4;
            if (delta < 0 && firstItem.Bounds.Top - delta > 9)
            {
                delta = firstItem.Bounds.Top - 9;
            }
            else if (delta > 0 && delta > lastItem.Bounds.Bottom - ts.Height + 9)
            {
                delta = lastItem.Bounds.Bottom - ts.Height + 9;
            }
            if (delta != 0)
                ScrollInternal(ts, delta);
        }
    }
}
