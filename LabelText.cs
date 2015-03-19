﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Projector
{
    public partial class LabelText : UserControl
    {

        public Boolean doNotSend = false;

        public LabelText()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.Text = TextBox.Text;
        }

        public void setLabel(string name)
        {
            this.TextLabel.Text = name;
        }

        public void setText(string value)
        {
            this.TextBox.Text = value;
        }

        public void setTop(int top)
        {
            this.Top = top;
        }
        public String getText()
        {
            return this.Text;
        }
        public void setLeft(int left)
        {
            this.Left = left;
        }
        public void setEnabled(Boolean onOff)
        {
            this.Enabled = onOff;
        }

        public void setVisibility(Boolean onOff)
        {
            this.Visible = onOff;
        }
        public void setInputWidth(int width)
        {
            this.TextBox.Width = width;
        }

        public void setLabelWidth(int width)
        {
            this.TextLabel.AutoSize = false;
            this.TextLabel.Width = width;
        }

        public void setHeight(int height)
        {
            if (height > 20)
            {
                this.TextBox.Multiline = true;
                
                this.TextBox.Height = height;
                
            }
        }

        public void setpasswordChars()
        {
            this.TextBox.UseSystemPasswordChar = true;
            this.doNotSend = true;
        }

    }
}
