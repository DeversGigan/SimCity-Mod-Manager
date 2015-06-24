namespace SimCityMODManager
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.SimCityLocation = new System.Windows.Forms.OpenFileDialog();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Inst = new System.Windows.Forms.Button();
            this.Uninstall = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.InstALL = new System.Windows.Forms.Button();
            this.uninstallALL = new System.Windows.Forms.Button();
            this.AddMod = new System.Windows.Forms.Button();
            this.ModLoc = new System.Windows.Forms.OpenFileDialog();
            this.DeleteMod = new System.Windows.Forms.Button();
            this.Launch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SimCityLocation
            // 
            this.SimCityLocation.Filter = "SimCity.exe|Simcity.exe";
            this.SimCityLocation.FileOk += new System.ComponentModel.CancelEventHandler(this.SimCityLocation_FileOk);
            // 
            // listView1
            // 
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.LabelWrap = false;
            this.listView1.Location = new System.Drawing.Point(12, 4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(265, 230);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Inst
            // 
            this.Inst.Location = new System.Drawing.Point(296, 70);
            this.Inst.Name = "Inst";
            this.Inst.Size = new System.Drawing.Size(155, 23);
            this.Inst.TabIndex = 1;
            this.Inst.Text = "Install";
            this.Inst.UseVisualStyleBackColor = true;
            this.Inst.Click += new System.EventHandler(this.Inst_Click);
            // 
            // Uninstall
            // 
            this.Uninstall.Location = new System.Drawing.Point(296, 99);
            this.Uninstall.Name = "Uninstall";
            this.Uninstall.Size = new System.Drawing.Size(155, 23);
            this.Uninstall.TabIndex = 2;
            this.Uninstall.Text = "Uninstall";
            this.Uninstall.UseVisualStyleBackColor = true;
            this.Uninstall.Click += new System.EventHandler(this.Uninstall_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // InstALL
            // 
            this.InstALL.Location = new System.Drawing.Point(296, 128);
            this.InstALL.Name = "InstALL";
            this.InstALL.Size = new System.Drawing.Size(155, 23);
            this.InstALL.TabIndex = 3;
            this.InstALL.Text = "Install ALL";
            this.InstALL.UseVisualStyleBackColor = true;
            this.InstALL.Click += new System.EventHandler(this.InstAll_Click);
            // 
            // uninstallALL
            // 
            this.uninstallALL.Location = new System.Drawing.Point(296, 157);
            this.uninstallALL.Name = "uninstallALL";
            this.uninstallALL.Size = new System.Drawing.Size(155, 23);
            this.uninstallALL.TabIndex = 4;
            this.uninstallALL.Text = "Uninstall All";
            this.uninstallALL.UseVisualStyleBackColor = true;
            this.uninstallALL.Click += new System.EventHandler(this.uninstallALL_Click);
            // 
            // AddMod
            // 
            this.AddMod.Location = new System.Drawing.Point(296, 12);
            this.AddMod.Name = "AddMod";
            this.AddMod.Size = new System.Drawing.Size(155, 23);
            this.AddMod.TabIndex = 5;
            this.AddMod.Text = "Add mod to Mods...";
            this.AddMod.UseVisualStyleBackColor = true;
            this.AddMod.Click += new System.EventHandler(this.AddMod_Click);
            // 
            // ModLoc
            // 
            this.ModLoc.Filter = "Simcity Mod Archive(*.zip *.rar)|*.rar;*.zip|Simcity Mod Package(*.package)|*.pac" +
    "kage";
            this.ModLoc.Multiselect = true;
            this.ModLoc.FileOk += new System.ComponentModel.CancelEventHandler(this.ModLoc_FileOk);
            // 
            // DeleteMod
            // 
            this.DeleteMod.Location = new System.Drawing.Point(296, 41);
            this.DeleteMod.Name = "DeleteMod";
            this.DeleteMod.Size = new System.Drawing.Size(155, 23);
            this.DeleteMod.TabIndex = 6;
            this.DeleteMod.Text = "Delete mod from Mods...";
            this.DeleteMod.UseVisualStyleBackColor = true;
            this.DeleteMod.Click += new System.EventHandler(this.DeleteMod_Click);
            // 
            // Launch
            // 
            this.Launch.Location = new System.Drawing.Point(296, 203);
            this.Launch.Name = "Launch";
            this.Launch.Size = new System.Drawing.Size(155, 23);
            this.Launch.TabIndex = 7;
            this.Launch.Text = "Launch Game";
            this.Launch.UseVisualStyleBackColor = true;
            this.Launch.Click += new System.EventHandler(this.Launch_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 242);
            this.Controls.Add(this.Launch);
            this.Controls.Add(this.DeleteMod);
            this.Controls.Add(this.AddMod);
            this.Controls.Add(this.uninstallALL);
            this.Controls.Add(this.InstALL);
            this.Controls.Add(this.Uninstall);
            this.Controls.Add(this.Inst);
            this.Controls.Add(this.listView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "SimCity MOD Manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog SimCityLocation;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button Inst;
        private System.Windows.Forms.Button Uninstall;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button InstALL;
        private System.Windows.Forms.Button uninstallALL;
        private System.Windows.Forms.Button AddMod;
        private System.Windows.Forms.OpenFileDialog ModLoc;
        private System.Windows.Forms.Button DeleteMod;
        private System.Windows.Forms.Button Launch;
    }
}

