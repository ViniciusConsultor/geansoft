using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace Gean.OrientalCharacters.Service.Service
{
    public class LogService
    {
        private static Logger GetLogger(Type type)
        {
            return LogManager.GetLogger(type.FullName);
        }
        //void a()
        //{
        //    Logger lg;
        //    lg.Trace();
        //    lg.Debug();
        //    lg.Info();
        //    lg.Warn(); 
        //    lg.Error();
        //    lg.Fatal();
        //}
        //public static void Trace()
        //{
        //    GetLogger().Trace();
        //}
    }
}
