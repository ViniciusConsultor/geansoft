using System;

namespace Gean.Wrapper.PlugTree
{
    public abstract class AbstractMenuCommand : AbstractCommand, IMenuCommand
    {
        bool isEnabled = true;

        public virtual bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                isEnabled = value;
            }
        }
    }
}
