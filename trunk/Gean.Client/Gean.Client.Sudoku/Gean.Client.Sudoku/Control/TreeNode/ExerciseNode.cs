using System;
using System.Collections.Generic;
using System.Text;
using Gean.Library.UI.Controls;
using Gean.Wrapper.Sudoku;

namespace Gean.Client.Sudoku
{
    public class ExerciseNode : TreeNodePro
    {
        public Exercise Exercise { get; set; }

        public override TreeNodeType NodeType
        {
            get { throw new NotImplementedException(); }
        }

        public override void LoadData()
        {
            throw new NotImplementedException();
        }

        protected override void LoadChildNodes()
        {
            throw new NotImplementedException();
        }

        public override string CollapseImageKey
        {
            get { throw new NotImplementedException(); }
        }
    }
}
