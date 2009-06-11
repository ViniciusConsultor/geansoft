using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Library.UI.Controls
{
    /// <summary>
    /// 拖拽结果
    /// </summary>
    public class DragDropResult
    {
        TreeNodePro _dropPutNode;
        TreeNodePro[] _dragdropNodes;
        DragDropResultType _dropResultType;

        /// <summary>
        /// 拖拽的放开时鼠标处的节点
        /// </summary>
        public TreeNodePro DropPutNode
        {
            get { return _dropPutNode; }
        }
        /// <summary>
        /// 被拖拽的节点
        /// </summary>
        public TreeNodePro[] DragDropNodes
        {
            get { return _dragdropNodes; }
        }
        /// <summary>
        /// 拖拽放开时处于鼠标处节点(DropPutNode)的前面还是后面
        /// </summary>
        public DragDropResultType DropResultType
        {
            get { return _dropResultType; }
        }
        public DragDropResult(TreeNodePro[] dragdropNodes, TreeNodePro dropNode, DragDropResultType dropResultType)
        {
            _dragdropNodes = dragdropNodes;
            _dropPutNode = dropNode;
            _dropResultType = dropResultType;
        }
    }

}
