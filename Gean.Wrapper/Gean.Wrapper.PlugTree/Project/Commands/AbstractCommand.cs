using System;


namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// Abstract implementation of the <see cref="ICommand"/> interface.
    /// </summary>
    public abstract class AbstractCommand : ICommand
    {

        /// <summary>
        /// Returns the owner of the command.
        /// </summary>
        public virtual object Owner
        {
            get
            {
                return _Owner;
            }
            set
            {
                _Owner = value;
                OnOwnerChanged(EventArgs.Empty);
            }
        }
        private object _Owner = null;

        /// <summary>
        /// Invokes the command.
        /// </summary>
        public abstract void Run();


        protected virtual void OnOwnerChanged(EventArgs e)
        {
            if (OwnerChanged != null)
            {
                OwnerChanged(this, e);
            }
        }

        public event EventHandler OwnerChanged;
    }
}
