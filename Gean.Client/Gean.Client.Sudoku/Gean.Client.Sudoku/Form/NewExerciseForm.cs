using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Gean.Wrapper.Sudoku;

namespace Gean.Client.Sudoku
{
    internal partial class NewExerciseForm : Form
    {
        public NewExerciseForm()
        {
            InitializeComponent();
        }

        internal Exercise Exercise { get; private set; }
        internal string DoString
        {
            get { return this._ExerciseTextBox.Text; }
        }

        private void _AcceptButton_Click(object sender, EventArgs e)
        {
            if (this.DoString.Length != 81 && this.DoString.Length != 161)
            {
                MessageBox.Show("Text cannot DoData!\r\nPlease reinput!");
                this._ExerciseTextBox.Focus();
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Exercise = new Exercise("", "", this._ExerciseTextBox.Text);
                this.Close();
            }
        }

        private void _CancleButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void _ClearButton_Click(object sender, EventArgs e)
        {
            this._ExerciseTextBox.Text = string.Empty;
            this._ExerciseTextBox.Focus();
        }

    }
}