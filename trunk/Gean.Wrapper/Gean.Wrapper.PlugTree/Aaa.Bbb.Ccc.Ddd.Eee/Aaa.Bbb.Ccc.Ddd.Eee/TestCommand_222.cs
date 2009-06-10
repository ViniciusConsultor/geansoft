using System;
using System.Collections.Generic;
using System.Text;
using Gean.Wrapper.PlugTree;
using System.Windows.Forms;
using System.Drawing;

namespace Aaa.Bbb.Ccc.Ddd.Eee
{
    public class TestCommand_222 : ICommand
    {
        #region ICommand 成员

        public object Owner
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Run()
        {
            Form form = new Form();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Size = new Size(650, 300);
            form.Text = this.GetType().FullName;
            form.Show();
        }

        public event EventHandler OwnerChanged;

        #endregion
    }
}
