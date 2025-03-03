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

/// <summary>
///Simple filebrowser class
///parses all files and folders in the given path
///populates a context menu with results
///
/// uses unmanaged code of SHFILEINFO interface for retrieving file icons and other infos
/// Note: The first time a submenue opens may be SLOW... 
/// that's a problem of the OS/SHFILEINFO - I think so, but not really sure
/// 
/// right click opens shell context menu
/// 
/// double click opens selected file/folder
/// 
///</summary>

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Linq;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Tar;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Windows.Documents;
using System.Security.AccessControl;

namespace TaskBarFolder
{
    public class ToolStripDropDownItemEx : ToolStripDropDownItem
    {
        string _Path;

        public ToolStripDropDownItemEx():base()
        {
        }

        public string Path
        {
            get
            {
                return _Path;
            }
            set
            {
                _Path = value;
            }
        }
    }
    public class ToolStripMenuItemEx : ToolStripMenuItem
    {
        string _Path, _fileInfo;
        long _zipEntry;
        bool _compressed;

        public ToolStripMenuItemEx():base()
        {
        }
        public ToolStripMenuItemEx(string Name, Image i):base(Name,i)
        {
        }

        public string Path
        {
            get
            {
                return _Path;
            }
            set
            {
                _Path = value;
            }
        }
        public long zipEntry
        {
            get
            {
                return _zipEntry;
            }
            set
            {
                _zipEntry = value;
            }
        }
        public bool compressed
        {
            get
            {
                return _compressed;
            }
            set
            {
                _compressed = value;
            }
        }
    }

    class FileBrowser
    {
        Form1 f1;
        string rootDir;
        int xPos,yPos;
 //       List<string> lExt = new List<string>(new string[] { ".zip", "r.gz", ".tar", ".bz2", ".gz" });
        List<string> lExt = new List<string>(new string[] { ".zip"});

        public FileBrowser(ContextMenuStrip cms, int xPos, int yPos, Form1 form)
        {
            this.xPos = xPos;   //position of mouse above taskbaricon
            this.yPos = yPos;
        //    cMenue = f1.cmsFolderBrowser;
            cMenue = cms;
            f1 = form;
        }

        //Create and populate the contextmenu with files/folders of selected directory
        ContextMenuStrip cMenue;
        public void populateList(List<folderItems> folderItem)
        {
            int k = 0;
            cMenue.Items.Clear();
            ToolStripMenuItemEx[] tsMenDirs = new ToolStripMenuItemEx[0]; //new ToolStripMenuItemEx[sPaths.Length];
            string sPath;
            foreach (folderItems x in folderItem)
            {
                sPath = x.Path;
                Image img = x.Icon != "" && File.Exists(x.Icon) ? Image.FromFile(x.Icon) : f1.imageList.Images[0];
                DirectoryInfo nodeDirInfo = new DirectoryInfo(sPath);
                if (nodeDirInfo.Exists)
                {
                    //    rootDir = sPath;
                    int i = 0;
                    FileInfo[] fileInf = nodeDirInfo.GetFiles();
                    DirectoryInfo[] dirInf = nodeDirInfo.GetDirectories();
                    ToolStripMenuItemEx[] tsMenDir = new ToolStripMenuItemEx[dirInf.Length];
                    ToolStripMenuItemEx[] tsMenFile = new ToolStripMenuItemEx[fileInf.Length];
                    tsMenDirs = tsMenDirs.Append(new ToolStripMenuItemEx(x.Name, img)).ToArray();
                    tsMenDirs[k].Path = sPath;
                    tsMenDirs[k].MouseUp += new MouseEventHandler(DropDown_MouseClick);
                    tsMenDirs[k].DropDownOpening += new EventHandler(iItem_DropDownOpening);
                    tsMenDirs[k].DoubleClickEnabled = true;
                    tsMenDirs[k].DoubleClick += new EventHandler(DropDown_DoubleClick);
                    foreach (DirectoryInfo dir in dirInf)
                    {
                        if (f1.hidden | ((dir.Attributes & FileAttributes.Hidden) == 0))
                        {
                            if (CheckFolderPermission(dir.FullName))
                                tsMenDir[i] = new ToolStripMenuItemEx(dir.Name, f1.imageList.Images[0]);
                            else
                                tsMenDir[i] = new ToolStripMenuItemEx(dir.Name, f1.imageList.Images[3]);
                            tsMenDir[i].Path = dir.FullName;
                            if (f1.tooltips)
                            {
                                if (System.Threading.Thread.CurrentThread.CurrentUICulture.ToString() == "de-DE")
                                    tsMenDir[i].ToolTipText = de[3]; //"Erstellt: "
                                else
                                    tsMenDir[i].ToolTipText = en[3]; //"created: "  - means the local time when the file was first seen in this system ;) not necessarly the real creation time...
                                tsMenDir[i].ToolTipText += dir.CreationTime.ToString();
                            }
                            tsMenDir[i].compressed = false;
                            tsMenDir[i].DropDownItems.Add("...");
                            tsMenDir[i].DropDownOpening += new EventHandler(iItem_DropDownOpening);
                            Console.Write(tsMenDir[i].Name + "\n");
                            i++;
                        }
                    }
                    tsMenDir = tsMenDir.Where(c => c != null).ToArray();
                    tsMenDirs[k].DropDownItems.AddRange(tsMenDir);
/*
                    // Integration of SharpZipLib for compressed folders
                    var query = (from fileExt in fileInf
                                 where lExt.Contains(fileExt.Extension)
                                 select fileExt)
                             .ToList();

                    i = 0;
                    if (query.Count > 0)
                    {
                        tsMenDir = new ToolStripMenuItemEx[query.Count];
                        foreach (FileInfo fi in query)
                        {
                            tsMenDir[i] = new ToolStripMenuItemEx(fi.Name, f1.imageList.Images[2]);
                            tsMenDir[i].Path = fi.FullName;
                            tsMenDir[i].compressed = true;
                            tsMenDir[i].DropDownItems.Add("...");
                            tsMenDir[i].DropDownOpening += new EventHandler(ziItem_DropDownOpening);
                            i++;
                        }
                        tsMenDirs[k].DropDownItems.AddRange(tsMenDir);
                    }
                    // *************************************************************
*/
                    i = 0;
                    foreach (FileInfo file in fileInf)
                    {
                        if (f1.hidden | ((file.Attributes & FileAttributes.Hidden) == 0))
                        {
                            if (lExt.Contains(file.Extension))
                            {
                                if (CheckFilePermission(file.FullName))
                                    tsMenFile[i] = new ToolStripMenuItemEx(file.Name, f1.imageList.Images[2]);
                                else
                                    tsMenFile[i] = new ToolStripMenuItemEx(file.Name, f1.imageList.Images[3]);
                                tsMenFile[i].Path = file.FullName;
                                tsMenFile[i].compressed = true;
                                tsMenFile[i].DropDownItems.Add("...");
                                tsMenFile[i].DropDownOpening += new EventHandler(ziItem_DropDownOpening);
                            }
                            else
                            {
                                tsMenFile[i] = fileInfo(file, tsMenFile.Length);
                            }
                            i++;
                        }
                    }
                    tsMenFile = tsMenFile.Where(c => c != null).ToArray();
                    tsMenDirs[k].DropDownItems.AddRange(tsMenFile);
                    k++;
                }
            }
            if (tsMenDirs.Length == 1)
            {
                ToolStripMenuItemEx[] tsMenDir = new ToolStripMenuItemEx[tsMenDirs[0].DropDownItems.Count];
                for (int i = 0; i < tsMenDirs[0].DropDownItems.Count; i++)
                {
                    ToolStripMenuItemEx item = (ToolStripMenuItemEx)tsMenDirs[0].DropDownItems[i];
                    item.MouseUp += new MouseEventHandler(DropDown_MouseClick);
                    item.DropDownOpening += new EventHandler(iItem_DropDownOpening);
                    item.DoubleClickEnabled = true;
                    item.DoubleClick += new EventHandler(DropDown_DoubleClick);
                    tsMenDir[i] = item;
                }
                cMenue.Items.AddRange(tsMenDir);
            }
            else cMenue.Items.AddRange(tsMenDirs);
        }
       void iItem_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItemEx iParentItem = (ToolStripMenuItemEx)sender;
            string DirTemp = iParentItem.Path;
            DirectoryInfo nodeDirInfo = new DirectoryInfo(DirTemp);
            int i = 0;
            if (nodeDirInfo.Exists)
            {

                rootDir = DirTemp;
                iParentItem.DropDownItems.Clear();

                DirectoryInfo[] dir = null;
                try
                {
                    dir = nodeDirInfo.GetDirectories();
                }
                catch (UnauthorizedAccessException)
                {
                     ; ;
                }
                ToolStripMenuItemEx[] iItem;

                if (dir != null)
                {
                    iItem = new ToolStripMenuItemEx[dir.Length];
                    i = 0;
                    foreach (DirectoryInfo dDir in dir)
                    {
                        if (f1.hidden | ((dDir.Attributes & FileAttributes.Hidden) == 0))
                        {
                            if(CheckFolderPermission(dDir.FullName))
                                iItem[i] = new ToolStripMenuItemEx(dDir.Name, f1.imageList.Images[0]);
                            else
                                iItem[i] = new ToolStripMenuItemEx(dDir.Name, f1.imageList.Images[3]);
                            iItem[i].Path = dDir.FullName;
                            iItem[i].compressed = false;
                            iItem[i].DropDownItems.Add("...");
                            iItem[i].DropDownOpening += new EventHandler(iItem_DropDownOpening);
                            i++;
                        }
                    }
                    iItem = iItem.Where(c => c != null).ToArray();
                    iParentItem.DropDownItems.AddRange(iItem);
                    iParentItem.DropDown.DoubleClick += new EventHandler(DropDown_DoubleClick);
                    iParentItem.DropDown.MouseClick += new MouseEventHandler(DropDown_MouseClick);
                }
                FileInfo[] fFile = null;
                try
                {
                    fFile = nodeDirInfo.GetFiles();
                }
                catch (UnauthorizedAccessException)
                {
                     ; ;
                }
                i = 0;
               if (fFile != null)
                {
                    // Integration of SharpZipLib for compressed folders
/*
                     var query = (from fileExt in fFile
                                 where lExt.Contains(fileExt.Extension)
                                 select fileExt)
                             .ToList();


                    if (query.Count > 0)
                    {
                        iItem = new ToolStripMenuItemEx[query.Count];
                        foreach (FileInfo fi in query)
                        {
                            if(f1.hidden | ((fi.Attributes & FileAttributes.Hidden) == 0)) 
                            {
                                if (CheckFilePermission(fi.FullName))
                                    iItem[i] = new ToolStripMenuItemEx(fi.Name, f1.imageList.Images[2]);
                                else
                                    iItem[i] = new ToolStripMenuItemEx(fi.Name, f1.imageList.Images[3]);
                                iItem[i].Path = fi.FullName;
                                iItem[i].DropDownItems.Add("...");
                                iItem[i].DropDownOpening += new EventHandler(ziItem_DropDownOpening);
                                iItem[i].MouseDown += new MouseEventHandler(iItem_Click);
                                i++;
                            }
                        }
                        iParentItem.DropDownItems.AddRange(iItem);
                    }
                    //********************************************************

                    i = 0;
                    iItem = new ToolStripMenuItemEx[fFile.Length - query.Count];
                    foreach (FileInfo file in fFile)
                    {
                        if(!lExt.Contains(file.Extension))
                        {
                            if (f1.hidden | ((file.Attributes & FileAttributes.Hidden) == 0))
                            {
                                iItem[i] = fileInfo(file, fFile.Length);
                                i++;
                            }
                        }
                    }
                    iItem = iItem.Where(c => c != null).ToArray();
                    iParentItem.DropDownItems.AddRange(iItem);
*/
                    i = 0;
                    iItem = new ToolStripMenuItemEx[fFile.Length];
                    foreach (FileInfo file in fFile)
                    {
                        if (f1.hidden | ((file.Attributes & FileAttributes.Hidden) == 0))
                        {
                            if(lExt.Contains(file.Extension))
                            {
                                if (CheckFilePermission(file.FullName))
                                    iItem[i] = new ToolStripMenuItemEx(file.Name, f1.imageList.Images[2]);
                                else
                                    iItem[i] = new ToolStripMenuItemEx(file.Name, f1.imageList.Images[3]);
                                iItem[i].Path = file.FullName;
                                iItem[i].DropDownItems.Add("...");
                                iItem[i].compressed = true;
                                iItem[i].DropDownOpening += new EventHandler(ziItem_DropDownOpening);
                                iItem[i].MouseDown += new MouseEventHandler(iItem_Click);
                            }
                            else
                            {
                                iItem[i] = fileInfo(file, fFile.Length);
                            }
                            i++;
                        }
                    }
                    iItem = iItem.Where(c => c != null).ToArray();
                    iParentItem.DropDownItems.AddRange(iItem);
                }
            }
        }
        void ziItem_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItemEx iParentItem = (ToolStripMenuItemEx)sender;
            string DirTemp = iParentItem.Path;
            DirectoryInfo nodeDirInfo = new DirectoryInfo(DirTemp);
            int i = 0;
           // if (iParentItem.compressed)
            {
                switch (DirTemp.Substring(DirTemp.Length - 4, 4))
                {
                    case ".zip":
                        using (var zipFile = new ZipFile(DirTemp))
                        {
                            i = 0;
                            ToolStripMenuItemEx[] iItem = new ToolStripMenuItemEx[zipFile.Count];
                            foreach (ZipEntry entry in zipFile)
                            {
                               iItem[i] = zipInfo(entry);
                               iItem[i].Path = DirTemp;
                               i++;
                            }
                            iParentItem.DropDownItems.Clear();
                            iParentItem.DropDownItems.AddRange(iItem);
                        }
                        break;
                    case ".tar":
                        break;
                    case ".bz2":
                        break;
                    case "r.gz":
                        break;
                    default:
                        break;
                }   
            }
        }

        void DropDown_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
              //  Point location = new Point(f1.Location.X, f1.Location.Y + yPos);
                string dirSelected, dirRoot;
                if (sender.GetType() == typeof(ToolStripMenuItemEx))
                    rootDir = ((ToolStripMenuItemEx)sender).Path;
                DirectoryInfo nodeDirInfo = new DirectoryInfo(rootDir);

                if (nodeDirInfo.Exists)
                {
                    if (nodeDirInfo.Parent != null)
                    {
                        dirRoot = nodeDirInfo.Parent.FullName.ToString();
                        dirSelected = nodeDirInfo.Name.ToString();
                        IShellContextMenu contextmen = new IShellContextMenu();
                        contextmen.iContextMenu(dirRoot, dirSelected,false);
                    }
                }
            }
        }

        void iItem_Click(object sender, MouseEventArgs e)
        {
        //    Point location = new Point(f1.Location.X, f1.Location.Y + yPos);
            if (e.Button == MouseButtons.Right)
            {
                IShellContextMenu contextmen = new IShellContextMenu();
                ToolStripMenuItemEx mItem = (ToolStripMenuItemEx)sender;
                string root = mItem.Path.Substring(0, mItem.Path.LastIndexOf("\\")+1);
                string sItem = Path.GetFileName(mItem.Path);
                contextmen.iContextMenu(root, sItem, false);
            }
            if (e.Button == MouseButtons.Left)
                iItem_DoubleClick(sender, e);
        }

        void iItem_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "rundll32.exe";
            ToolStripMenuItemEx mItem = (ToolStripMenuItemEx)sender;
            if (mItem.compressed)
            {
                try
                {
                    using (var fs = new FileStream(mItem.Path, FileMode.Open, FileAccess.Read))
                    using (var zf = new ZipFile(fs))
                    {
                        ZipEntry ze = zf.GetEntry(sender.ToString());
                        if (ze != null)
                        {
                            System.IO.Stream s = zf.GetInputStream(ze);
                            if (ze.IsFile)
                            {
                                using (BinaryReader reader = new BinaryReader(s))
                                {

                                    String filename = ze.Name;
                                    if (filename.Contains("/"))
                                        filename = Path.GetFileName(filename);

                                    string filepath = Path.GetTempPath() + filename;

                                    using (BinaryWriter writer = new BinaryWriter(File.Open(filepath, FileMode.Create)))
                                    {

                                        byte[] buffer = new byte[8 * 1024];
                                        int len;
                                        while ((len = reader.Read(buffer, 0, buffer.Length)) > 0)
                                        {
                                            writer.Write(buffer, 0, len);
                                        }
                                        proc.StartInfo.Arguments = " shell32.dll,ShellExec_RunDLL " + filepath;
                                    }
                                }
                            }
                        }

                    }

                }
                catch (Exception)
                {

                    ;
                }

            }
            else
                proc.StartInfo.Arguments = " shell32.dll,ShellExec_RunDLL " + ((ToolStripMenuItemEx)sender).Path; 
//                proc.StartInfo.Arguments = " shell32.dll,ShellExec_RunDLL " + rootDir + "\\" + sender.ToString();
            try
            {
                if (proc.StartInfo.Arguments != "")
                    proc.Start();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Application not found" || ex.Message == "Anwendung nicht gefunden") //open "OpenAs" filedialog
                {
                    proc.StartInfo.FileName = "rundll32.exe";
                    proc.StartInfo.Arguments = " shell32.dll, OpenAs_RunDLL " + rootDir + "\\" + sender.ToString();
                    proc.Start();
                }
            }
        }
        void DropDown_DoubleClick(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(ToolStripMenuItemEx))
                rootDir = ((ToolStripMenuItemEx)sender).Path;
            System.Diagnostics.Process.Start(rootDir);
        }

        ToolStripMenuItemEx fileInfo(FileInfo file, int iLength)
        {
            uint ICON = (uint)(f1.smallIcons ? 0x1 : 0x0);
            ToolStripMenuItemEx iItem = new ToolStripMenuItemEx();
            ShellExtensions.SHFILEINFO shinfo = new ShellExtensions.SHFILEINFO();
            IntPtr hImgSmall = ShellExtensions.Win32.SHGetFileInfo(file.FullName, ShellExtensions.Win32.FILE_ATTRIBUTE_NORMAL, ref shinfo,
               (uint)Marshal.SizeOf(shinfo),
//                ShellExtensions.Win32.SHGFI_ICON |
                ICON |
                ShellExtensions.Win32.SHGFI_TYPENAME |
                ShellExtensions.Win32.SHGFI_USEFILEATTRIBUTES);
            System.Drawing.Icon shellIcon;
            if (shinfo.hIcon == IntPtr.Zero) shellIcon = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
            else shellIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon);
            Image iImage = shellIcon.ToBitmap();

            ShellExtensions.Win32.DestroyIcon(shinfo.hIcon);
            if (!CheckFilePermission(file.FullName))
                iImage = f1.imageList.Images[3];
            if(f1.fileExt)
                iItem = new ToolStripMenuItemEx(file.Name, iImage);
            else
                iItem = new ToolStripMenuItemEx(Path.GetFileNameWithoutExtension(file.Name), iImage);
            iItem.DoubleClick += new EventHandler(iItem_DoubleClick);
            iItem.MouseDown += new MouseEventHandler(iItem_Click);
            iItem.Path = file.FullName;
            iItem.compressed = false;
            string[] sCultureText;
            if (System.Threading.Thread.CurrentThread.CurrentUICulture.ToString() == "de-DE")
                sCultureText = de;
            else
                sCultureText = en;
            if (f1.tooltips)
            {
                iItem.ToolTipText = sCultureText[0] + shinfo.szTypeName + "\n";
                iItem.ToolTipText += sCultureText[1] + CalcSize(file.Length) + "\n";
                iItem.ToolTipText += sCultureText[2] + file.LastWriteTime + "\n";
                iItem.ToolTipText += sCultureText[3] + file.CreationTime + "\n";
            }
            return iItem;
        }

        ToolStripMenuItemEx zipInfo(ZipEntry entry)
        {
            ToolStripMenuItemEx iItem = new ToolStripMenuItemEx();
            iItem = new ToolStripMenuItemEx(entry.Name, f1.imageList.Images[1]);
            iItem.DoubleClick += new EventHandler(iItem_DoubleClick);
            iItem.MouseDown += new MouseEventHandler(iItem_Click);
            iItem.Path = entry.ZipFileIndex.ToString();
            iItem.zipEntry = entry.ZipFileIndex;
            iItem.compressed = true;
            string[] sCultureText;
            if (System.Threading.Thread.CurrentThread.CurrentUICulture.ToString() == "de-DE")
                sCultureText = de_zip;
            else
                sCultureText = en_zip;

            if (f1.tooltips)
            {
                iItem.ToolTipText = sCultureText[0] + CalcSize(entry.CompressedSize) + "\n";
                iItem.ToolTipText += sCultureText[1] + CalcSize(entry.Size) + "\n";
                iItem.ToolTipText += sCultureText[2] + entry.DateTime.ToString() + "\n";
                iItem.ToolTipText += sCultureText[3] + entry.Comment + "\n";
            }
            return iItem;
        }
        string CalcSize(float lVal)
        {
            if (lVal < 1024)
                return lVal.ToString() + " Bytes";
            else
            {
                lVal /= 1024;
                if (lVal < 1024)
                    return lVal.ToString("##.##") + " KB";
                else
                {
                    lVal /= 1024;
                    if (lVal < 1024)
                        return lVal.ToString("##.##") + " MB";
                    else
                    {
                        lVal /= 1024;
                        return lVal.ToString("##.##") + " GB";
                    }

                }
            }
        }
        public bool CheckFolderPermission(string folderPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(folderPath);
            try
            {
                DirectorySecurity dirAC = dirInfo.GetAccessControl(AccessControlSections.Access);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
        public bool CheckFilePermission(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            try
            {
                FileSecurity fileSecurity = fileInfo.GetAccessControl(AccessControlSections.Access);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        string[] de = { "Typ: ", "Größe: ", "Geändert: ", "Erstellt: " };
        string[] en = { "type: ", "size: ", "changed: ", "created: " };
        string[] de_zip = { "kompr. Größe: ", "Größe: ", "Datum: ", "Kommentar: " };
        string[] en_zip = { "compr. size: ", "size: ", "date: ", "comment: " };

        public static string GetInternetShortcut(string filePath)
        {
            string url = "";

            using (TextReader reader = new StreamReader(filePath))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("URL="))
                    {
                        string[] splitLine = line.Split('=');
                        if (splitLine.Length > 0)
                        {
                            url = splitLine[1];
                            break;
                        }
                    }
                }
            }

            return url;
        }
    }
}
