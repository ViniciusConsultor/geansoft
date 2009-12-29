using Gean.Logging;
using Gean.OrientalCharacters.Service;
using NLog;

namespace Gean.EasternArt.Service
{
    /// <summary>
    /// Maintains references to the core service implementations.
    /// </summary>
    public static class ServiceManager
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

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
            _informationsService.AddStartupInformation(StringService.ServiceManagerGettingStarted);
#if DEBUG
            _informationsService.Demo_AddStartupInformation(8);
#endif
            _logger.Info(LogString.Normal("ServiceManager"));
            //��ʼ����ɣ������״̬Ϊ��
            _initializeState = true;
            _informationsService.AddStartupInformation(StringService.ServiceManagerActivated);
        }

        static InformationsService _informationsService = InformationsService.Instance;
        public static InformationsService InformationsService
        {
            get { return _informationsService; }
        }
        
    }
}
