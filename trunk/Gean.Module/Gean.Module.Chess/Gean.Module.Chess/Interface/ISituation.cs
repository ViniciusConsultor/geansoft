
namespace Gean.Module.Chess
{
    /// <summary>
    /// 描述一局棋的当前局面
    /// </summary>
    public interface ISituation : IFenNotation, IParse, IGenerator
    {
        /// <summary>
        /// 返回指定位置的棋格是否含有棋子
        /// </summary>
        /// <param name="dot">指定位置</param>
        bool ContainsPiece(int dot);
    }
}