using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessRecordFlag : IEnumerable<string>
    {
        private Dictionary<string, string> _flags = new Dictionary<string, string>();

        public ChessRecordFlag()
        {
            _flags.Add("Move", this.Move);
            //_flags.to
        }

/* 简易评论标记符
  在目标格坐标前注明：[-] 走子； [:] or [x] 吃子
  在目标格坐标后注明：[+] 将军； [++] 双将；[x]or[#] 将死；[e.p.] 吃过路兵(en passant)；[棋子名] 升变
*/

        /// <summary>
        /// 目标格坐标前：走子
        /// </summary>
        private string Move = "-";
        /// <summary>
        /// 目标格坐标前：吃子
        /// </summary>
        private string Capture = "x";
        /// <summary>
        /// 目标格坐标后：将军
        /// </summary>
        private string Check = "+";
        /// <summary>
        /// 目标格坐标后：双将
        /// </summary>
        private string DoubleCheck = "++";
        /// <summary>
        /// 目标格坐标后：将死
        /// </summary>
        private string Checkmate = "x";
        /// <summary>
        /// 目标格坐标后：吃过路兵
        /// </summary>
        private string EnPassant = "e.p.";

        /// <summary>
        /// 好棋。有利的棋招
        /// </summary>
        private string Favorable = "!";
        /// <summary>
        /// 好棋。非常有利的棋招
        /// </summary>
        private string FavorablePro = "!!";
        /// <summary>
        /// 昏招
        /// </summary>
        private string Misestimate = "?";
        /// <summary>
        /// 严重的昏招
        /// </summary>
        private string MisestimatePro = "??";
        /// <summary>
        /// 后果不明的棋招
        /// </summary>
        private string UnknownConsequences = "!?";


        #region IEnumerable<string> 成员

        public IEnumerator<string> GetEnumerator()
        {
            return _flags.Values.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _flags.Values.GetEnumerator();
        }

        #endregion
    }
}
