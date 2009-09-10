using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Gean.Module.Chess;
using System.Drawing;
using System.Drawing.Text;

namespace Gean.Gui.ChessControl
{
    public class StepsPanel : FlowLayoutPanel
    {
        Font font = new Font("Consolas", 9);
        SizeF labelSize;


        public StepsPanel()
        {
            using (Graphics g = this.CreateGraphics())
            {
                labelSize = g.MeasureString("888. 88888", font);
            }

            this.AutoScroll = true;
        }

        public void Add(IGenerator step)
        {
            StepLabel stepLabel = new StepLabel(step);
            stepLabel.Font = font;
            stepLabel.Size = new Size((int)(labelSize.Width + 2), (int)(labelSize.Height + 5));
            stepLabel.Margin = new Padding(2, 2, 2, 2);
            stepLabel.TextAlign = ContentAlignment.MiddleLeft;
            stepLabel.Click += new EventHandler(stepLabel_Click);
            this.Controls.Add(stepLabel);
        }

        private StepLabel _selectLabel;
        void stepLabel_Click(object sender, EventArgs e)
        {
            StepLabel stepLabel = (StepLabel)sender;
            if (_selectLabel != null)
            {
                _selectLabel.BackColor = Color.Linen;
            }
            stepLabel.BackColor = Color.Yellow;
            _selectLabel = stepLabel;
        }


        class StepLabel : Label
        {
            public StepLabel(IGenerator step)
            {
                this.Tag = step;
                this.BackColor = Color.Linen;
                this.Text = step.Generator();
            }
        }
    }
}
