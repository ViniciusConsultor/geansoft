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
        /// ��ȡServiceManager��ʼ��״̬
        /// </summary>
        /// <value>���<c>true</c>; otherwise, <c>false</c>.</value>
        public static bool InitializeState
        {
            get { return _initializeState; }
        }
        private static bool _initializeState = false;
        /// <summary>
        /// ��ʼ�����еķ���
        /// </summary>
        public static void InitializeService()
        {
            _informationsService.Demo_AddStartupInformation(35);

            //��ʼ����ɣ������״̬Ϊ��
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
