using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Gean.Gui.WinForm;

namespace Gean.UI.WinForms.Demo
{
    public partial class CloudsPanelForm : Form
    {
        private CloudsPanel _panel = new CloudsPanel();
        public CloudsPanelForm()
        {
            InitializeComponent();

            _panel.Dock = DockStyle.Fill;

            _splitContainer.Panel1.Controls.Add(_panel);
            _propertyGrid.SelectedObject = _panel;
        }

        private void _canelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _okButton_Click(object sender, EventArgs e)
        {
            List<IGenerator> gens = new List<IGenerator>();
            for (int i = 0; i < 255; i++)
            {
                gens.Add(new Geee(i + 1));
            }
            _panel.Clear();
            _panel.AddRange(gens.ToArray());
            _panel.Refresh();
            this.Text = DateTime.Now.ToLongTimeString();
        }
    }

    #region IGenerator - Geee
    class Geee : IGenerator
    {
        private int _number;
        public Geee(int i)
        {
            _number = i;
        }

        #region IGenerator 成员

        UtilityRandom rand = new UtilityRandom();

        private string _value = "";
        public string Generator()
        {
            if (string.IsNullOrEmpty(_value))
            {
                _value = ((new StringBuilder()).Append(_number.ToString()).Append(". ").Append(rand.GetString(rand.GetInt(2, 12), UtilityRandom.RandomCharType.All))).ToString();
            }
            return _value;
        }

        #endregion
    }
    #endregion
}
