using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Diagnostics;

using Microsoft.VisualBasic.FileIO;

using Ionic.Zip;
using Ionic.Zlib;

using NUnrar.Reader;
using NUnrar.Archive;

namespace SimCityMODManager
{
    public partial class Form1 : Form
    {
        private string modDir;
        private string gameExe;
        public Form1()
        {
            InitializeComponent();
        }
        public void compressZIP(string modPath, string destPath)
        {
            if (File.Exists(destPath)) { return; }
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.CompressionLevel = CompressionLevel.BestSpeed;
                    zip.AddFile(modPath);
                    zip.Save(destPath);
                }
            }
            catch { }
        }
        public void compressZIP(string[] modPaths, string destPath)
        {
            if (File.Exists(destPath)) { return; }
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.CompressionLevel = CompressionLevel.BestSpeed;
                    foreach(string modPath in modPaths)
                    {
                        File.Copy(modPath, Path.GetFileName(modPath), true);
                        zip.AddFile(Path.GetFullPath(modPath));
                        File.Delete(Path.GetFileName(modPath));
                    }
                    zip.Save(destPath);
                }
            }
            catch { }
        }
        public void decompressMOD(string name)
        {
            if(File.Exists(@"Mods\"+name+".zip"))
            {
                decompress(@"Mods\"+name+".zip","temp");
            }
            else if (File.Exists(@"Mods\" + name + ".rar"))
            {
                decompress(@"Mods\" + name + ".rar", "temp");
            }
        }
        public void decompress(string srcPath,string destPath)
        {
            if(Path.GetExtension(srcPath) == ".zip")
            {
                decompressZIP(srcPath,destPath);
            }
            if(Path.GetExtension(srcPath) == ".rar")
            {
                decompressRAR(srcPath, destPath);
            }
        }
        public void decompressRAR(string srcPath,string destPath)
        {
            RarArchive.WriteToDirectory(srcPath,destPath);
        }
        public void decompressZIP(string srcPath,string destPath)
        {
            using(ZipFile zip = ZipFile.Read(srcPath))
            {
                zip.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
                zip.ExtractAll(destPath);
            }
        }
        public void deleteMod(string modName)
        {
            if(File.Exists(@"Mods\"+modName+".zip"))
            {
                File.Delete(@"Mods\" + modName + ".zip");
            }
            else if(File.Exists(@"Mods\"+modName+".rar"))
            {
                File.Delete(@"Mods\" + modName + ".rar");
            }
        }
        public static Boolean isEnglish()
        {
            return !File.Exists("JPN.lang");
        }
        public static string getPrevDir(string dir)
        {
            string str;
            if(dir[dir.Length - 1] == '\\')
            {
                dir = dir.Remove(dir.Length - 1);
            }
            str = dir.Remove(dir.LastIndexOf("\\"));
            return str + "\\";
        }
        public static string getText(string JPNtext,string ENGtext)
        {
            return isEnglish() ? ENGtext : JPNtext;
        }
        public string[] getAllModsName()
        {
            return Directory.GetFiles("Mods", "*.zip").Concat(Directory.GetFiles("Mods", "*.rar")).ToArray();
        }
        public string[] getAllInstalledMods()
        {
            List<string> list = new List<string>();
            string[] mods = getAllModsName();
            foreach(string mod in mods)
            {
                if (isModInstalled(mod)) { list.Add(mod); }
            }
            return list.ToArray();
        }
        public string[] getModFiles(string ModName)
        {
            List<string> ret = new List<string>();
            Directory.CreateDirectory("temp_dec");
            if(File.Exists("Mods\\"+ModName+".zip"))
            {
                decompressZIP("Mods\\" + ModName + ".zip", "temp_dec");
                string[] files = Directory.GetFiles("temp_dec","*.package",System.IO.SearchOption.AllDirectories);
                foreach(string f in files)
                {
                    ret.Add(Path.GetFileName(f));
                }
            }
            else if(File.Exists("Mods\\"+ModName+".rar"))
            {
                decompressRAR("Mods\\" + ModName + ".rar", "temp_dec");
                string[] files = Directory.GetFiles("temp_dec", "*.package",System.IO.SearchOption.AllDirectories);
                foreach (string f in files)
                {
                    ret.Add(Path.GetFileName(f));
                }
            }
            FileSystem.DeleteDirectory("temp_dec",UIOption.OnlyErrorDialogs,RecycleOption.DeletePermanently,UICancelOption.DoNothing);
            return ret.ToArray();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!isEnglish())
            {
                Inst.Text = "インストール";
                Uninstall.Text = "アンインストール";
                InstALL.Text = "全てインストール";
                uninstallALL.Text = "全てアンインストール";
                AddMod.Text = "MODをModsフォルダに追加...";
                DeleteMod.Text = "MODをModsフォルダから削除";
                Launch.Text = "SimCityを起動";
            }
            listView1.GridLines = true;
            ColumnHeader columnInstalled = new ColumnHeader();
            columnInstalled.Text = "Installed";
            ColumnHeader columnPath = new ColumnHeader();
            columnPath.Text = "Mod Name";
            listView1.Columns.Add(columnInstalled);
            listView1.Columns.Add(columnPath);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            if (!Directory.Exists("Mods"))
            {
                Directory.CreateDirectory("Mods");
            }
            if (!File.Exists(@"Mods\installed.txt"))
            {
                File.CreateText(@"Mods\installed.txt");
            }
            if(!File.Exists("SimcityExe.cfg"))
            {
                DialogResult drs = MessageBox.Show(this, getText("SimCityの実行ファイルを選択してください", "Please select SimCity.exe file"), "SCMM", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if(drs == DialogResult.Cancel)
                {
                    Close();
                }

                DialogResult dr = SimCityLocation.ShowDialog();
                while (dr == DialogResult.Cancel)
                {
                    MessageBox.Show(getText("SimCityの実行ファイルを選択してください", "Please select SimCity.exe file"));
                    dr = SimCityLocation.ShowDialog();
                }
            }
            else
            {
                gameExe = File.ReadAllText("SimcityExe.cfg").Trim().Replace("\t","").Replace("\r","").Replace("\n","");
                if (File.Exists(gameExe) && Path.GetFileName(gameExe) == "SimCity.exe" && Directory.Exists(getPrevDir(Path.GetDirectoryName(gameExe))+"SimCityData\\"))
                {
                    modDir = getPrevDir(Path.GetDirectoryName(gameExe)) + "SimCityData\\";
                    refreshModItems();
                }
                else
                {
                    MessageBox.Show(getText("SimcityExe.cfgで指定されたファイルは適切ではありません", "The file written in SimcityExe.cfg is incorrect"));
                    Close();
                }
            }
        }

        private void SimCityLocation_FileOk(object sender, CancelEventArgs e)
        {

            if (File.Exists(SimCityLocation.FileName) && Directory.Exists(getPrevDir(Path.GetDirectoryName(SimCityLocation.FileName)) + "SimCityData\\"))
            {
                gameExe = SimCityLocation.FileName;
                modDir = getPrevDir(Path.GetDirectoryName(SimCityLocation.FileName)) + "SimCityData\\";
                refreshModItems();
            }
            else
            {
                MessageBox.Show(getText("指定されたファイルは適切ではありません", "The selected file is incorrect"));
                Close();
            }
            File.WriteAllText("SimcityExe.cfg",SimCityLocation.FileName);
        }
        private void dialogMultiple(bool uninstall,string[] modNames)
        {
            string names = "";
            foreach(string name in modNames)
            {
                names += name+",";
            }
            if(names.Length == 0)
            {
                return;
            }
            names = names.Remove(names.Length - 1);
            if(isEnglish())
            {
                MessageBox.Show("The mod"+(modNames.Length > 0 ? "s were "  : " was ")+"successfully "+(uninstall ? "un" : "")+"installed :"+names);
            }
            else
            {
                MessageBox.Show("MODは正常に"+(uninstall ? "アン":"")+"インストールされました;"+names);
            }
        }
        private void refreshModItems()
        {
            List<int> indecies = new List<int>();
            if(listView1.SelectedIndices.Count > 0)
            {
                foreach(int id in listView1.SelectedIndices)
                {
                    indecies.Add(id);
                }
            }
            listView1.Items.Clear();
            string installed = File.ReadAllText(@"Mods\installed.txt");
            foreach (string name in getAllModsName())
            {
                string fn = Path.GetFileNameWithoutExtension(name);
                if (installed.Contains(fn))
                {
                    listView1.Items.Add(new ListViewItem(new string[] { "Yes", fn }));
                }
                else
                {
                    listView1.Items.Add(new ListViewItem(new string[] { "No", fn }));
                }
            }
            try
            {
                if (indecies.Count > 0)
                {
                    foreach (int id in indecies)
                    {
                        ListViewItem item = listView1.Items[id];
                        item.Focused = true;
                        item.Selected = true;
                    }
                }
            }
            catch { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (!Directory.Exists("Mods"))
            {
                Directory.CreateDirectory("Mods");
            }
            if (!File.Exists(@"Mods\installed.txt"))
            {
                File.CreateText(@"Mods\installed.txt");
            }
            refreshModItems();
        }

        public bool isModInstalled(string name)
        {
            return File.ReadAllText(@"Mods\installed.txt").Contains(name); 
        }

        private void Inst_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedIndices.Count == 0)
            {
                MessageBox.Show(getText("MODを選択してください", "Please select MOD to operate"));
            }
            else if (listView1.SelectedIndices.Count == 1 && isModInstalled(listView1.SelectedItems[0].SubItems[1].Text))
            {
                MessageBox.Show(getText("MODはすでにインストールされているようです", "Selected MOD is already installed"));
            }
            else
            {
                List<string> installedMOD = new List<string>();
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    string modName = item.SubItems[1].Text;
                    if (!isModInstalled(modName))
                    {
                        installMod(modName, false);
                        installedMOD.Add(modName);
                    }
                }
                dialogMultiple(true,installedMOD.ToArray());
            }
        }
        private string[] getModNames(ListView.SelectedListViewItemCollection items)
        {
            List<string> list = new List<string>();
            foreach(ListViewItem item in items)
            {
                list.Add(item.SubItems[1].Text);
            }
            return list.ToArray();
        }
        private void installMod(string modName,bool dialog)
        {
            Directory.CreateDirectory("temp");
            decompressMOD(modName);
            string[] files = Directory.GetFiles("temp", "*.package", System.IO.SearchOption.AllDirectories);
            foreach (string file in files)
            {
                File.Copy(file, modDir + Path.GetFileName(file), true);
            }
            FileSystem.DeleteDirectory("temp",UIOption.OnlyErrorDialogs,RecycleOption.DeletePermanently,UICancelOption.DoNothing);
            if (dialog)
            {
                MessageBox.Show(getText("正常にインストールされました:"+modName, "The mod was installed successfully:"+modName));
            }
            File.AppendAllText(@"Mods\installed.txt", modName + ";");
        }

        private void Uninstall_Click(object sender, EventArgs e)
        {
            
            if(listView1.SelectedIndices.Count == 0)
            {
                MessageBox.Show(getText("MODを選択してください", "Please select MOD to operate"));
            }
            else if (!isModInstalled(listView1.SelectedItems[0].SubItems[1].Text))
            {
                MessageBox.Show(getText("MODはインストールされていないようです", "Selected MOD(s) is not installed"));
            }
            else
            {
                DialogResult dr = MessageBox.Show(this, getText("本当にアンインストールしますか？", "Really you want to uninstall?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No) { return; }
                List<string> uninstalledMOD = new List<string>();
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    string modName = item.SubItems[1].Text;
                    if (isModInstalled(modName))
                    {
                        uninstallMod(modName, false);
                        uninstalledMOD.Add(modName);
                    }
                }
                dialogMultiple(true, uninstalledMOD.ToArray());
            }
        }

        private void uninstallMod(string modName,bool dialog)
        {
            if (isModInstalled(modName))
            {
                string[] modFiles = getModFiles(modName);
                foreach (string modF in modFiles)
                {
                    File.Delete(modDir + modF);
                }
                string installed = File.ReadAllText(@"Mods\installed.txt");
                File.WriteAllText(@"Mods\installed.txt", installed.Replace(modName + ";", ""));
                if (dialog)
                {
                    MessageBox.Show(getText("正常にアンインストールされました:"+modName, "The mod was uninstalled successfully:"+modName));
                }
            }
        }

        private void InstAll_Click(object sender, EventArgs e)
        {
            int toInstall = 0;
            foreach (ListViewItem item in listView1.Items)
            {
                if (!isModInstalled(item.SubItems[1].Text)) { toInstall++; }
            }
            if(toInstall == 0)
            {
                MessageBox.Show(getText("インストールするMODがありません", "There aren't mods to install"));
                return;
            }
            foreach(ListViewItem item in listView1.Items)
            {
                if (!isModInstalled(item.SubItems[1].Text))
                {
                    installMod(item.SubItems[1].Text, false);
                }
            }
            MessageBox.Show(getText("正常にインストールされました", "The mod" + (toInstall > 1 ? "s were " : " was ") + "installed successfully"));
        }

        private void uninstallALL_Click(object sender, EventArgs e)
        {

            
            int toUninstall = 0;
            foreach (ListViewItem item in listView1.Items)
            {
                if (isModInstalled(item.SubItems[1].Text)) { toUninstall++; }
            }
            if (toUninstall == 0)
            {
                MessageBox.Show(getText("アンインストールするMODがありません", "There aren't mods to uninstall"));
                return;
            }
            DialogResult dr = MessageBox.Show(this, getText("本当にアンインストールしますか？", "Really you want to uninstall?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) { return; }
            foreach (ListViewItem item in listView1.Items)
            {
                if (isModInstalled(item.SubItems[1].Text))
                {
                    uninstallMod(item.SubItems[1].Text, false);
                }
            }
            MessageBox.Show(getText("正常にアンインストールされました", "The mod" + (toUninstall > 1 ? "s were " : " was ") + "uninstalled successfully"));
        }

        private void AddMod_Click(object sender, EventArgs e)
        {
            ModLoc.ShowDialog();
        }

        private void ModLoc_FileOk(object sender, CancelEventArgs e)
        {
            foreach (string modPath in ModLoc.FileNames)
            {
                string modName = Path.GetFileNameWithoutExtension(modPath);
                string ext = Path.GetExtension(modPath);
                if (File.Exists(modPath) && !File.Exists(@"Mods\" + modName + ext))
                {
                    if (Path.GetExtension(modPath) == ".package")
                    {
                        compressZIP(modPath, @"Mods\" + modName + ".zip");
                    }
                    else
                    {
                        File.Copy(modPath, @"Mods\" + modName + ext);
                    }
                }
            }
        }

        private void DeleteMod_Click(object sender, EventArgs e)
        {
            
            if(listView1.SelectedIndices.Count > 0)
            {
                DialogResult dr = MessageBox.Show(this, getText("本当に削除しますか？", "Really you want to delete?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No) { return; }
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    string modName = item.SubItems[1].Text;
                    if (isModInstalled(modName))
                    {
                        uninstallMod(modName, true);
                    }
                    deleteMod(modName);
                }
            }
            else
            {
                MessageBox.Show(getText("MODを選択してください", "Please select MOD to operate"));
            }
        }

        private void Launch_Click(object sender, EventArgs e)
        {
            Process p = Process.Start(gameExe);
            p.WaitForExit();
        }
    }
}
