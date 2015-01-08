﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace Projector
{
    public partial class ReflectList : UserControl
    {


        private ReflectionScript onSelectScript;

        public ReflectList()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            exportCsv();
        }

        public void setListView(ListView list)
        {
            if (list != null)
            {
                ListViewWorker Worker = new ListViewWorker();
                Worker.copyListView(list, this.listView);
            }
        }

        public ListView getListView()
        {
            ListView list = new ListView();
            ListViewWorker Worker = new ListViewWorker();
            Worker.copyListView(this.listView, list);
            return list;
        }

        public void joinFields(string left, string right, string newName, string between)
        {
            Stopwatch watch = new Stopwatch();
             
            this.listView.Columns.Add(newName);
            int newCnt = listView.Columns.Count - 2;
            int rPos = -1;
            int lPos = -1;
            for (int a = 0; a < listView.Columns.Count; a++)
            {
                if (listView.Columns[a].Text == right)
                {
                    rPos = a;
                }
                if (listView.Columns[a].Text == left)
                {
                    lPos = a;
                }
            }

            if (lPos > -1 && rPos > -1)
            {
                watch.Start();
                for (int i = 0; i < this.listView.Items.Count; i++)
                {
                    string rText = this.listView.Items[i].SubItems[rPos].Text;
                    string lText = this.listView.Items[i].SubItems[lPos].Text;
                    string newText = lText + between + rText;
                    this.listView.Items[i].SubItems.Add(newText);
                    if (watch.ElapsedMilliseconds > 1000)
                    {
                        this.listView.TopItem = this.listView.Items[i];
                        this.listView.Update();
                        Application.DoEvents();
                        watch.Restart();
                    }
                }
            }

        }

        public void mark(string fieldname, string value)
        {
            Stopwatch watch = new Stopwatch();

            int lPos = -1;
            for (int a = 0; a < listView.Columns.Count; a++)
            {

                if (listView.Columns[a].Text == fieldname)
                {
                    lPos = a;
                }
            }

            if (lPos > -1)
            {
                watch.Start();
                for (int i = 0; i < this.listView.Items.Count; i++)
                {

                    string lText = this.listView.Items[i].SubItems[lPos].Text;
                    if (lText == value)
                    {
                        this.listView.TopItem = this.listView.Items[i];
                        this.listView.Items[i].Selected = true;
                        this.listView.Update();
                        Application.DoEvents();
                        return;
                    }
                    if (watch.ElapsedMilliseconds > 1000)
                    {
                        this.listView.TopItem = this.listView.Items[i];
                        this.listView.Update();
                        Application.DoEvents();
                        watch.Restart();
                    }
                }
            }

        }


        public void split(string fieldname, string splitChars)
        {
            Stopwatch watch = new Stopwatch();

            int lPos = -1;
            for (int a = 0; a < listView.Columns.Count; a++)
            {

                if (listView.Columns[a].Text == fieldname)
                {
                    lPos = a;
                }
            }

            if (lPos > -1)
            {
                watch.Start();
                for (int i = 0; i < this.listView.Items.Count; i++)
                {

                    string lText = this.listView.Items[i].SubItems[lPos].Text;
                    string[] newTexts = lText.Split(splitChars.ToCharArray());
                    this.listView.Items[i].SubItems[lPos].Text = newTexts[0];
                    if (watch.ElapsedMilliseconds > 1000)
                    {
                        this.listView.TopItem = this.listView.Items[i];
                        this.listView.Update();
                        Application.DoEvents();
                        watch.Restart();
                    }
                }
            }

        }



        public void firstWord(string fieldname)
        {
            split(fieldname, " ");

        }



        public void joinListView(string onJoin,ListView list)
        {
            if (list != null)
            {
                ListViewWorker Worker = new ListViewWorker();
                Worker.mergeListView(onJoin, list, this.listView);
            }
        }


        public void setTop(int val){
            this.Top = val;
        }

        public void setLeft(int val)
        {
            this.Left = val;
        }

        public void setWidth(int val)
        {
            this.Width = val;
        }

        public void setHeight(int val)
        {
            this.Height = val;
        }

        public string getEntry(string entry, int rownumber)
        {
            ListViewWorker Worker = new ListViewWorker();
            return Worker.getEntry(listView, entry, rownumber);
        }

        public string getSelectedRowEntry(string entry)
        {
            if (listView.SelectedItems.Count == 1)
            {
                ListViewWorker Worker = new ListViewWorker();
                return Worker.getEntry(listView, entry, listView.SelectedIndices[0]);
            }
            return "";
        }

        public void Iterate()
        {
            foreach (ListViewItem lItem in this.listView.Items){
                intIterate(lItem);
            }
        }

        private void intIterate(ListViewItem lItem)
        {
            if (this.onSelectScript != null)
            {
               
                int number = 0;
                foreach (ListViewItem.ListViewSubItem lwItem in lItem.SubItems)
                {
                    string headName = this.listView.Columns[number].Text;
                    string[] solits = headName.Split(' ');
                    string varName = solits[0];
                    this.onSelectScript.createOrUpdateStringVar("&" + varName, lwItem.Text);
                    number++;
                    this.onSelectScript.createOrUpdateStringVar("&ROW." + number, lwItem.Text);
                }
                RefScriptExecute executer = new RefScriptExecute(this.onSelectScript, this);
                executer.run();
            }
            
        }


        public void OnSelectItem(ReflectionScript refScript)
        {
            this.onSelectScript = refScript;
        }

        public void saveAsCsv(string filename)
        {
            this.exportCsvToFile(filename);
        }

        private void exportCsv()
        {            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.exportCsvToFile(saveFileDialog.FileName);
            }
        }

        public void getListFromDatabase(ReflectionDatabase db)
        {
            List<Hashtable> dbRes = db.lastResult;
            if (dbRes != null)
            {
                Boolean first = true;
                this.listView.Items.Clear();
                foreach (Hashtable row in dbRes)
                {
                    int rowNr = 0;
                    ListViewItem adLwItm = new ListViewItem();
                    foreach (DictionaryEntry line in row){

                        if (first){
                            this.listView.Columns.Add(line.Key.ToString());
                        }
                        string val = "";
                        if (line.Value != null){
                            val = line.Value.ToString();
                        }
                        if (rowNr == 0)
                        {
                            adLwItm.Text = val;
                        }
                        else
                        {
                            adLwItm.SubItems.Add(val);
                        }
                        rowNr++;
                        
                    }
                    this.listView.Items.Add(adLwItm);
                    first = false;
                }
            }
        }

        private void exportCsvToFile(string filename)
        {
            ListViewWorker Worker = new ListViewWorker();
            string csvContent = Worker.exportCsv(this.listView);

            System.IO.File.WriteAllText(filename, csvContent);
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.onSelectScript != null)
            {
                if (this.listView.SelectedItems.Count > 0)
                {
                    int number = 0;
                    foreach (ListViewItem.ListViewSubItem lwItem in this.listView.SelectedItems[0].SubItems)
                    {
                        string headName = this.listView.Columns[number].Text;
                        string[] solits = headName.Split(' ');
                        string varName = solits[0];
                        this.onSelectScript.createOrUpdateStringVar("&"+varName, lwItem.Text);
                        number++;
                        this.onSelectScript.createOrUpdateStringVar("&ROW." + number, lwItem.Text);
                    }
                    RefScriptExecute executer = new RefScriptExecute(this.onSelectScript, this);
                    executer.run();
                }
            }
        }
    }
}
