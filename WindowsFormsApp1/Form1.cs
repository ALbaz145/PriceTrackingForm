﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            FormSearch form = new FormSearch();
            form.ShowDialog();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormAgregar form = new FormAgregar();
            form.ShowDialog();
        }
    }
}
