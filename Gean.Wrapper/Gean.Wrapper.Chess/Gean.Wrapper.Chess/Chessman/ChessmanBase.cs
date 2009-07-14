using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 棋子抽象类(王,后,车,象,马,兵)
    /// </summary>
    public abstract class ChessmanBase
    {
        public ChessmanBase(ChessboardGrid grid, Enums.ChessmanSide side, Enums.ChessmanType type)
        {
            //设置该棋子的名字的前缀是实例该棋子时，该棋子的坐标，后缀是该棋子
            this._name = grid.ToString() + " - " + this.ChessmanType.ToString();
            this.ChessmanType = type;
            this.ChessmanSide = side;
            this.InitializeComponent();
        }
        public override string ToString()
        {
            return "Chessman: " + this._name;
        }

        /// <summary>
        /// 获取或设置该棋子的名字。
        /// 该棋子的名字组成部份：前缀是实例该棋子时，该棋子的坐标，后缀是该棋子。
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        private string _name;

        public ChessboardGrid GridOwner { get; private set; }

        /// <summary>
        /// 棋子的战方
        /// </summary>
        public Enums.ChessmanSide ChessmanSide { get; private set; }
        /// <summary>
        /// 棋子的类型
        /// </summary>
        public Enums.ChessmanType ChessmanType { get; private set; }

        /// <summary>
        /// 将该棋子移入指定的棋格中
        /// </summary>
        /// <param name="grid">指定的棋格</param>
        /// <returns></returns>
        public virtual bool MoveToGrid(ChessboardGrid grid)
        {
            if (grid.IsUsable(this))
            {
                this.GridOwner = grid;
                this.GridOwner.ChessmanOwner = this;
                return true;
            }
            return false;
        }
        public virtual bool IsKilled()
        {
            return true;
        }

        public abstract void InitializeComponent();
        public abstract string ToSimpleString();

    }

}
