using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.LogService
{
    public class LogService : IService<string>
    {
        public bool Initializes(params string[] args)
        {
            return true;
        }

        public bool ReStart(params string[] args)
        {
            return true;
        }

        public bool Start()
        {
            return true;
        }

        public bool Stop()
        {
            return true;
        }
    }
}
