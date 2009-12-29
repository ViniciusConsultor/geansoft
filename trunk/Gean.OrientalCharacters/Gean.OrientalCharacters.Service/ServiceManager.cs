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
            _informationsService.AddStartupInformation(StringService.ServiceManagerGettingStarted);
#if DEBUG
            _informationsService.Demo_AddStartupInformation(8);
#endif
            _logger.Info(LogString.Normal("ServiceManager"));
            //初始化完成，置完成状态为真
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
