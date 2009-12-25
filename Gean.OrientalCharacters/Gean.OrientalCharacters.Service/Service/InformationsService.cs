using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace Gean.EasternArt.Service
{
    public sealed class InformationsService
    {

        #region 单件实例

        /// <summary>
        /// Initializes a new instance of the <see cref="Options"/> class.
        /// </summary>
        private InformationsService()
        {
            _startupInformation = new Queue<string>();
        }

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static InformationsService Instance
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            static Singleton()
            {
                Instance = new InformationsService();
            }

            internal static readonly InformationsService Instance = null;
        }

        #endregion

        /// <summary>
        /// 获取启动信息集合
        /// </summary>
        /// <value>启动信息集合</value>
        public Queue<string> StartupInformation 
        {
            get { return _startupInformation; }
        }
        private Queue<string> _startupInformation;

        /// <summary>
        /// Adds the startup information.
        /// </summary>
        /// <param name="info">The info.</param>
        public void AddStartupInformation(string info)
        {
            lock (_startupInformation)
            {
                _startupInformation.Enqueue(info);
            }
        }

#if DEBUG
        /// <summary>
        /// [开发测试使用] 增加指定数量的信息.
        /// </summary>
        /// <param name="amount">指定的信息数量</param>
        internal void Demo_AddStartupInformation(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                //Thread.Sleep(10);
                AddStartupInformation(Guid.NewGuid().ToString());
            }
        }
#endif

    }
}
