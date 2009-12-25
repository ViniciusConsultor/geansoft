using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Gean.EasternArt.Service
{
    /// <summary>
    /// Maintains references to the core service implementations.
    /// </summary>
    public static class ServiceManager
    {
        /// <summary>
        /// 获取ServiceManager初始化状态
        /// </summary>
        /// <value>完成<c>true</c>; otherwise, <c>false</c>.</value>
        public static bool InitializeState
        {
            get { return _initializeState; }
        }
        private static bool _initializeState = false;
        /// <summary>
        /// 初始化所有的服务
        /// </summary>
        public static void InitializeService()
        {
            _informationsService.Demo_AddStartupInformation(35);

            //初始化完成，置完成状态为真
            _initializeState = true;
        }

        static InformationsService _informationsService = InformationsService.Instance;
        public static InformationsService InformationsService
        {
            get { return _informationsService; }
            //set
            //{
            //    if (value == null)
            //        throw new ArgumentNullException();
            //    _informationsService = value;
            //}
        }

    }
}
