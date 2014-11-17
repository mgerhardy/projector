﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Projector
{
    public partial class ProjectorForm : Form
    {

        private Profil profil = new Profil("default");
        List<Button> ProfilBtn = new List<Button>();

        private bool showGroups = false;

        private int buttonStyle = 0;
        private int maxStyle = 2;

        public ProjectorForm()
        {
            InitializeComponent();

            updateProfilSelector();
        }


        private void profilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfilSelect pro = new ProfilSelect();
            if (pro.ShowDialog() == DialogResult.OK)
            {
                if (pro.selectedProfil != null) profil.changeProfil(pro.selectedProfil);
                ProfilInfo.Text = pro.selectedProfil + " [" + profil.getProperty("db_username") + '@' + profil.getProperty("db_schema") + "]";               
                updateProfilSelector();
            }
        }




        private void initApp()
        {
            XmlSetup tmpSetup = new XmlSetup();
            tmpSetup.setFileName("Projector_config.xml");
            tmpSetup.loadXml();
            string check;

            check = tmpSetup.getSetting("client_left");
            if (check != null && check != "" && int.Parse(check) > 0) this.Left = int.Parse(check);

            check = tmpSetup.getSetting("client_top");
            if (check != null && check != "" && int.Parse(check) > 0) this.Top = int.Parse(check);

            check = tmpSetup.getSetting("client_w");
            if (check != null && check != "" && int.Parse(check) > 100) this.Width = int.Parse(check);

            check = tmpSetup.getSetting("client_h");
            if (check != null && check != "" && int.Parse(check) > 100) this.Height = int.Parse(check);

            check = tmpSetup.getSetting("showgroup");
            if (check != null && check != "") this.showGroups = (int.Parse(check) == 1);

            check = tmpSetup.getSetting("showgroupbuttons");
            if (check != null && check != "") groupButtonsToolStripMenuItem.Checked = (int.Parse(check) == 1);
            mainSlitter.Panel1Collapsed = !groupButtonsToolStripMenuItem.Checked;

            check = tmpSetup.getSetting("buttonstyle");
            if (check != null && check != "") this.buttonStyle = int.Parse(check);

            groupedToolStripMenuItem.Checked = this.showGroups;

            /*
            tmpSetup.addSetting("client_left", leftPosition.ToString());
            tmpSetup.addSetting("client_top", topPosition.ToString());
            tmpSetup.addSetting("client_w", w.ToString());
            tmpSetup.addSetting("client_h", h.ToString());
             * 
             *  tmpSetup.addSetting("buttonStyle", buttonStyle.ToString());
            if (showGroups) tmpSetup.addSetting("showGroup", "1");
            else tmpSetup.addSetting("showGroup", "0");
            */
            drawGroupButtons();

        }

        private void drawGroupButtons()
        {

            flowLayoutControllPanel.Controls.Clear();

            XmlSetup pSetup = new XmlSetup();
            pSetup.setFileName("profileGroups.xml");
            pSetup.loadXml();


            Hashtable settings = pSetup.getHashMap();
            chooseGroup.Items.Clear();
            chooseGroup.Items.Add("[ALL]");

            Button tmpBtnx = new Button();
            tmpBtnx.Text = "ALL";
            tmpBtnx.Click += new System.EventHandler(btnSelectGroup);
            tmpBtnx.Width = 150;
            tmpBtnx.Height = 40;
            tmpBtnx.Tag = 0;
            
            tmpBtnx.Image = Projector.Properties.Resources.layer_group_add;
            tmpBtnx.ImageAlign = ContentAlignment.MiddleLeft;
            tmpBtnx.TextAlign = ContentAlignment.MiddleRight;
            flowLayoutControllPanel.Controls.Add(tmpBtnx);

            int select = 1;

            foreach (DictionaryEntry de in settings)
            {
                chooseGroup.Items.Add(de.Key.ToString());
                Button tmpBtn = new Button();
                tmpBtn.Text = de.Key.ToString();
                tmpBtn.Click += new System.EventHandler(btnSelectGroup);
                tmpBtn.Width = 150;
                tmpBtn.Height = 40;
                tmpBtn.Tag = select;
                tmpBtn.Image = Projector.Properties.Resources.layer_group_add;
                tmpBtn.ImageAlign = ContentAlignment.MiddleLeft;
                tmpBtn.TextAlign = ContentAlignment.MiddleRight;
                
                flowLayoutControllPanel.Controls.Add(tmpBtn);
                select++;
            }
            chooseGroup.SelectedIndex = 0;
        }

        private void btnSelectGroup(object sender, System.EventArgs e)
        {
            Button tmpBtn = (Button)sender;
            chooseGroup.SelectedIndex = (int)tmpBtn.Tag;

        }

        private void updateProfilSelector()
        {
            XmlSetup tmpSetup = new XmlSetup();
            tmpSetup.setFileName("Projector_profiles.xml");
            //tmpSetup.setFileName("Projector_global.xml");
            tmpSetup.loadXml();
            profilSelector.DropDownItems.Clear();

            /*
            Panel panel1 = new Panel();
            panel1.Location = new Point(56, 72);
            panel1.Size = new Size(264, 152);
            
            panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(panel1);
            */
           


            flowLayout.Controls.Clear();
            Hashtable assignGrp = new Hashtable();
            Hashtable currentView = new Hashtable();
            if (showGroups || chooseGroup.Text != "[ALL]")
            {

                XmlSetup pSetup = new XmlSetup();
                pSetup.setFileName("profileGroups.xml");
                pSetup.loadXml();

                Hashtable settings = pSetup.getHashMap();

                if (chooseGroup.Text != "[ALL]")
                {
                    if (pSetup.getHashMap().Contains(chooseGroup.Text))
                    {
                        string[] currGrp = pSetup.getHashMap()[chooseGroup.Text].ToString().Split('|');
                        for (int z = 0; z < currGrp.Length; z++)
                        {
                            currentView.Add(currGrp[z], currGrp[z]);
                        }
                            
                    }
                }

                if (showGroups)
                {
                    foreach (DictionaryEntry de in settings)
                    {
                        //de.Key.ToString();                
                        //de.Value.ToString();
                        //groupSelectBox.Items.Add(de.Key.ToString());

                        GroupBox grpBox = new GroupBox();
                        grpBox.Name = de.Key.ToString();
                        grpBox.Text = de.Key.ToString();
                        grpBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                        grpBox.AutoSize = true;


                        FlowLayoutPanel flowInside = new FlowLayoutPanel();
                        flowInside.Dock = DockStyle.Fill;
                        flowInside.AutoSize = true;
                        flowInside.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                        grpBox.Controls.Add(flowInside);

                        flowLayout.Controls.Add(grpBox);

                        string[] grpProfiles = de.Value.ToString().Split('|');

                        for (int z = 0; z < grpProfiles.Length; z++)
                        {
                            string prName = grpProfiles[z];
                            if (assignGrp != null && !assignGrp.Contains(prName)) assignGrp.Add(prName, flowInside);
                        }
                    }


                }


            }

            for (Int64 i = 0; i < tmpSetup.count; i++)
            {
                string keyname = "profil_" + (i + 1);
                string proName = tmpSetup.getValue(keyname);

                if (proName != null)
                {
                    profil.changeProfil(proName);
                    profilSelector.DropDownItems.Add(proName, Projector.Properties.Resources.folder_closed_16, ProfilSelectExitClick);

                    Button tmpBtn = new Button();

                    string colortext = profil.getProperty("set_bgcolor");
                    if (colortext != null && colortext.Length > 0)
                    {

                        tmpBtn.BackColor = Color.FromArgb(int.Parse(colortext));
                    }


                    tmpBtn.Text = proName;
                    tmpBtn.Click += new System.EventHandler(dynBtnClick);
                    tmpBtn.Width = 150;
                    tmpBtn.Height = 80;

                    if (buttonStyle == 1)
                    {
                        tmpBtn.Height = 25;
                        //tmpBtn.FlatStyle = FlatStyle.System;
                    }

                    if (buttonStyle == 0)
                    {
                        tmpBtn.Image = Projector.Properties.Resources.db;
                        tmpBtn.ImageAlign = ContentAlignment.TopCenter;
                        tmpBtn.TextAlign = ContentAlignment.BottomCenter;
                    }


                    if (buttonStyle == 2)
                    {
                        tmpBtn.Image = Projector.Properties.Resources.db;
                        tmpBtn.ImageAlign = ContentAlignment.TopLeft;
                        tmpBtn.TextAlign = ContentAlignment.TopRight;
                        tmpBtn.Height = 55;

                    }

                    if (chooseGroup.Text != "[ALL]" && chooseGroup.Text != "")
                    {
                        if (currentView.ContainsValue(proName)) ProfilBtn.Add(tmpBtn);
                    }
                    else
                    {
                        ProfilBtn.Add(tmpBtn);
                    }


                    if (showGroups)
                    {
                        if (assignGrp.Contains(proName))
                        {
                            FlowLayoutPanel ctrl = (FlowLayoutPanel)assignGrp[proName];
                            ctrl.Controls.Add(tmpBtn);
                        }
                    }
                    else
                    {
                        if (chooseGroup.Text != "[ALL]" && chooseGroup.Text != "")
                        {
                            if (currentView.ContainsValue(proName)) flowLayout.Controls.Add(tmpBtn);
                        }
                        else
                        {
                            flowLayout.Controls.Add(tmpBtn);
                        }
                        
                    }

                }
                else
                {
                    //missing profile in flow
                    MessageBox.Show("Missing profile " + keyname + ".", "Config error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (buttonStyle == 0)
            {
                Button syncBtn = new Button();
                syncBtn.Text = "Sync Database";
                syncBtn.Click += new System.EventHandler(syncDbBtnClick);
                syncBtn.Width = 150;
                syncBtn.Height = 60;
                syncBtn.Image = Projector.Properties.Resources.arrow_back_32;


                syncBtn.ImageAlign = ContentAlignment.TopCenter;
                syncBtn.TextAlign = ContentAlignment.BottomCenter;

                flowLayout.Controls.Add(syncBtn);
            }

            showProfilLabel.Text = profil.getName();

            /*
            PanelDrawing ptest = new PanelDrawing();
            ptest.Width = 200;
            ptest.Height = 120;
            ptest.BackColor = Color.Aquamarine;

            ptest.addValue(10);
            ptest.addValue(20);
            ptest.addValue(90);
            ptest.addValue(70);
            ptest.addValue(-30);
            ptest.addValue(10);
            ptest.addValue(15);

            flowLayout.Controls.Add(ptest);
            */
            //profilSelector.DropDownItems.Add("Test", Projector.Properties.Resources.folder_closed_16,ProfilSelectExitClick);
            //ConfigProfil testprofil = new ConfigProfil();

            //testprofil.ExportToXml(@"c:\checkcheck.xml");
        }

        private void updateButtonsState()
        {
            List<string> names = new List<string>();
            for (int i = 0; i < ProfilBtn.Count; i++)
            {
                string showAtProperty = ProfilBtn[i].Text;
                profil.changeProfil(showAtProperty);

               

                statusResult result = new statusResult();
                result = checkConnection(profil);

                names.Add(showAtProperty);

                ProfilBtn[i].Image = Projector.Properties.Resources.database_check;
                ProfilBtn[i].Update();
                if (result.status)
                {
                    ProfilBtn[i].Image = Projector.Properties.Resources.dbplus;


                    MysqlHandler TestConnect = new MysqlHandler(profil);
                    TestConnect.connect();
                    MySql.Data.MySqlClient.MySqlDataReader data = TestConnect.sql_select("SELECT version()");
                    data.Read();
                    ProfilBtn[i].Text += " " + data.GetString(0);
                    TestConnect.disConnect();

                }
                else
                {
                    ProfilBtn[i].Image = Projector.Properties.Resources.database4_2;
                    ProfilBtn[i].ForeColor = Color.Red;
                }

                ProfilBtn[i].Update();

            }
            MessageBox.Show("Close this message to return to the Original Display State. Now you can see the mysql Server Version");
            for (int i = 0; i < ProfilBtn.Count; i++)
            {
                ProfilBtn[i].Text = names[i];
            }

        }


        private void updateSelectedProfil()
        {
            showProfilLabel.Text = profil.getName();
            ProfilInfo.Text = profil.getName() + " [" + profil.getProperty("db_username") + '@' + profil.getProperty("db_schema") + "]";
            this.Text = "Profil: " + profil.getName();
            mDIToolStripMenuItem.Text = "Launch " + profil.getName() + "...";


            string showAtProperty = profil.getName();
            profil.changeProfil(showAtProperty);

            

            statusResult result = new statusResult();
            result = checkConnection(profil);


            if (result.status)
            {
                connectionState.ForeColor = Color.DarkGreen;

                MysqlHandler TestConnect = new MysqlHandler(profil);
                TestConnect.connect();
                MySql.Data.MySqlClient.MySqlDataReader data = TestConnect.sql_select("SELECT version()");
                data.Read();
                connectionState.Text = "Connection Test OK ... Version " + data.GetString(0);
                TestConnect.disConnect();

            }
            else
            {
                connectionState.ForeColor = Color.Red;
                connectionState.Text = "NO CONNECTION";
            }

        }

        private void syncDbBtnClick(object sender, System.EventArgs e)
        {
            Button sendBtn = (Button)sender;
            toolStripButton2_Click(sender, e);
        }

        private void dynBtnClick(object sender, System.EventArgs e)
        {
            Button sendBtn = (Button)sender;

            string showAtProperty = sendBtn.Text;

            if (showAtProperty != null)
            {
                profil.changeProfil(showAtProperty);

              

                statusResult result = new statusResult();
                result = checkConnection(profil);

                if (result.status)
                {

                    profil.changeProfil(showAtProperty);
                    updateSelectedProfil();
                    ProfilInfo.Text = showAtProperty + " [" + profil.getProperty("db_username") + '@' + profil.getProperty("db_schema") + "]";
                    mDIToolStripMenuItem_Click(sender, e);
                }
                else
                {
                    MessageBox.Show(this, result.message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }


        private void ProfilSelectExitClick(object who, EventArgs e)
        {
            System.Windows.Forms.ToolStripDropDownItem check = (System.Windows.Forms.ToolStripDropDownItem)who;
            string showAtProperty = check.Text;

            if (showAtProperty != null)
            {
                profil.changeProfil(showAtProperty);
                updateSelectedProfil();

                //mDIToolStripMenuItem_Click(who, e);
            }

        }

        private void setupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setupForm sForm = new setupForm();

            sForm.Text = profil.getName();

            sForm.host.Text = profil.getProperty("db_host");
            sForm.username.Text = profil.getProperty("db_username");
            sForm.password.Text = profil.getProperty("db_password");
            sForm.schema.Text = profil.getProperty("db_schema");
            sForm.diffviewsetup.Text = profil.getProperty("diff_command");
            sForm.diffviewparam.Text = profil.getProperty("diff_param");
            sForm.constrain_setup.Checked = (profil.getProperty("foreign_key_check") == "1");

            string colortext = profil.getProperty("set_bgcolor");
            if (colortext != null && colortext.Length > 0)
            {
                sForm.colorText.Text = colortext;
                sForm.colorText.BackColor = Color.FromArgb(int.Parse(colortext));
            }

            if (sForm.ShowDialog() == DialogResult.OK)
            {
                profil.setProperty("db_host", sForm.host.Text);
                profil.setProperty("db_username", sForm.username.Text);
                profil.setProperty("db_password", sForm.password.Text);
                profil.setProperty("db_schema", sForm.schema.Text);
                profil.setProperty("set_bgcolor", sForm.colorText.Text);
                profil.setProperty("diff_command", sForm.diffviewsetup.Text);
                profil.setProperty("diff_param", sForm.diffviewparam.Text);
                if (sForm.constrain_setup.Checked)
                    profil.setProperty("foreign_key_check", "1");
                else
                    profil.setProperty("foreign_key_check", "0");
                profil.saveSetup();
                updateProfilSelector();
            }
        }


        private statusResult checkConnection(Profil testPro)
        {
            statusResult result = new statusResult();
            MysqlHandler TestConnect = new MysqlHandler(testPro);

            try
            {
                TestConnect.connect();
            }
            catch (Exception)
            {

                //throw;
            }

            if (TestConnect.lastSqlErrorMessage.Length > 0)
            {
                result.message = TestConnect.lastSqlErrorMessage;
                result.status = false;
                result.StatusKey = 0;
                return result;
            }
            else
            {
                TestConnect.disConnect();
                result.message = Projector.Properties.Resources.MysqlConnectionTestSucces; ;
                result.status = true;
                result.StatusKey = 1;
                return result;


            }

        }


        private void databaseWatchToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void startWatcher_Click(object sender, EventArgs e)
        {
            databaseWatchToolStripMenuItem_Click(sender, e);
        }

        private void sensorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MysqlSensor sensor = new MysqlSensor();
            if (profil.getProperty("set_bgcolor") != null && profil.getProperty("set_bgcolor").Length > 2)
            {
                sensor.BackColor = Color.FromArgb(int.Parse(profil.getProperty("set_bgcolor")));
            }
            //sensor.sensorProfil = profil;
            sensor.sensorProfil.getValuesFromProfil(profil);
            sensor.getStartupData();
            sensor.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            sensorToolStripMenuItem_Click(sender, e);

        }

        private void querysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            queryBrowser qb = new queryBrowser();
            if (profil.getProperty("set_bgcolor") != null && profil.getProperty("set_bgcolor").Length > 2)
            {
                qb.BackColor = Color.FromArgb(int.Parse(profil.getProperty("set_bgcolor")));
            }
            //watcher.profil = profil;
            //qb.sensorProfil = profil;
            qb.sensorProfil.getValuesFromProfil(profil);
            qb.listTables();
            qb.Show();
        }

        private void mDIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (profil.getProperty("db_username") != null)
            {
                MdiForm mdif = new MdiForm();
                //mdif.currentProfil = profil;
                if (mdif.currentProfil == null) mdif.currentProfil = new Profil("midi");
                mdif.currentProfil.getValuesFromProfil(profil);
                mdif.Text = " [" + profil.getProperty("db_username") + '@' + profil.getProperty("db_host") + "/" + profil.getProperty("db_schema") + "]";
                mdif.Show();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            copyDb cform = new copyDb();
            XmlSetup tmpSetup = new XmlSetup();
            tmpSetup.setFileName("Projector_profiles.xml");
            tmpSetup.loadXml();
            profilSelector.DropDownItems.Clear();

            cform.sourceSelect.Items.Clear();
            cform.targetSelect.Items.Clear();

            for (Int64 i = 0; i < tmpSetup.count; i++)
            {
                string keyname = "profil_" + (i + 1);
                string proName = tmpSetup.getValue(keyname);

                if (proName != null)
                {
                    // profilSelector.DropDownItems.Add(proName, Projector.Properties.Resources.folder_closed_16, ProfilSelectExitClick);

                    cform.sourceSelect.Items.Add(proName);
                    cform.targetSelect.Items.Add(proName);

                }
            }



            cform.Show();
        }

        private void showProfilLabel_Click(object sender, EventArgs e)
        {
            updateProfilSelector();
        }

        private void exportCurrentProfilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (exportProfileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                profil.exportXml(exportProfileDlg.FileName);
            }
        }

        private void ProjectorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            int leftPosition = this.Left;
            int topPosition = this.Top;
            int w = this.Width;
            int h = this.Height;

            XmlSetup tmpSetup = new XmlSetup();
            tmpSetup.setFileName("Projector_config.xml");

            tmpSetup.addSetting("client_left", leftPosition.ToString());
            tmpSetup.addSetting("client_top", topPosition.ToString());
            tmpSetup.addSetting("client_w", w.ToString());
            tmpSetup.addSetting("client_h", h.ToString());
            tmpSetup.addSetting("buttonstyle", buttonStyle.ToString());
            if (showGroups) tmpSetup.addSetting("showgroup", "1");
            else tmpSetup.addSetting("showgroup", "0");

            if (groupButtonsToolStripMenuItem.Checked) tmpSetup.addSetting("showgroupbuttons", "1");
            else tmpSetup.addSetting("showgroupbuttons", "0");

            tmpSetup.saveXml();
        }

        private void ProjectorForm_Shown(object sender, EventArgs e)
        {
            initApp();
            updateProfilSelector();
        }

        private void groupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfilGroup p1 = new ProfilGroup();

            XmlSetup tmpSetup = new XmlSetup();
            tmpSetup.setFileName("Projector_profiles.xml");

            tmpSetup.loadXml();

            for (Int64 i = 0; i < tmpSetup.count; i++)
            {
                string keyname = "profil_" + (i + 1);
                string proName = tmpSetup.getValue(keyname);

                if (proName != null)
                {


                    ListViewItem addItem = new ListViewItem();
                    addItem.Text = proName;
                    p1.GroupedDatabases.Items.Add(addItem);
                }
            }
            XmlSetup pSetup = new XmlSetup();
            pSetup.setFileName("profileGroups.xml");
            pSetup.loadXml();

            // 


            if (p1.ShowDialog() == System.Windows.Forms.DialogResult.OK && p1.groupName.Text.Length > 1)
            {


                p1.groupName.Text = p1.groupName.Text.Replace(" ", "_");
                p1.groupName.Text = p1.groupName.Text.Replace("-", "_");
                p1.groupName.Text = p1.groupName.Text.Replace("/", "_");
                p1.groupName.Text = p1.groupName.Text.Replace(@"\", "_");
                p1.groupName.Text = p1.groupName.Text.Replace(":", "_");

                string profilNames = "";
                string split = "";
                for (int b = 0; b < p1.GroupedDatabases.Items.Count; b++)
                {



                    if (p1.GroupedDatabases.Items[b].Checked)
                    {

                        profilNames += split + p1.GroupedDatabases.Items[b].Text;
                        split = "|";
                    }

                }

                pSetup.addSetting(p1.groupName.Text, profilNames);

                pSetup.saveXml();
                drawGroupButtons();
                updateProfilSelector();

            }

        }

        private void groupQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupQuery gq = new GroupQuery();

            gq.Show();

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            toolStripButton3.Enabled = false;
            updateButtonsState();
            toolStripButton3.Enabled = true;
        }

        private void backupProfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveProject.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ConfigProfil exporter = new ConfigProfil();
                exporter.ExportToXml(saveProject.FileName);

            }
        }

        private void loadProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openProjectDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ConfigProfil exporter = new ConfigProfil();
                exporter.importFromXml(openProjectDlg.FileName);
                initApp();
                updateProfilSelector();
            }
        }

        private void importPartsOfProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openProjectDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ConfigProfil exporter = new ConfigProfil();
                List<string> profilListing = exporter.getImportableProfiles(openProjectDlg.FileName);

                if (profilListing != null && profilListing.Count > 0)
                {
                    ImportSettings setImport = new ImportSettings();

                    for (int i = 0; i < profilListing.Count; i++)
                    {
                        setImport.ImportListView.Items.Add(profilListing[i]);
                    }

                    if (setImport.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {

                    }

                }

            }
        }

        private void groupedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showGroups = groupedToolStripMenuItem.Checked;
            updateProfilSelector();
        }

        private void switchButtonModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonStyle++;
            if (buttonStyle > maxStyle) buttonStyle = 0;
            updateProfilSelector();
        }

        private void chooseGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateProfilSelector();
        }

        private void flowLayout_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupButtonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainSlitter.Panel1Collapsed = !groupButtonsToolStripMenuItem.Checked;
        }

        private void startToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            Boolean profilesvalid = (profil.getProperty("db_username") != null && profil.getProperty("db_username") != "");
            databaseWatchToolStripMenuItem.Enabled = profilesvalid;
            mDIToolStripMenuItem.Enabled = profilesvalid;
            groupQueryToolStripMenuItem.Enabled = profilesvalid;
            setupToolStripMenuItem.Enabled = profilesvalid;
            exportCurrentProfilToolStripMenuItem.Enabled = profilesvalid;
            groupButtonsToolStripMenuItem.Enabled = profilesvalid;
        }




    }
}
