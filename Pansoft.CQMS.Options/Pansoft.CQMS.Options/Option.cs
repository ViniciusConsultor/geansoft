using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Pansoft.CQMS.Options
{

    public sealed class Option<T> where T : IOptionSerializable
    {
        #region constructor

        private Option()
        {
        }

        #endregion

        #region property AND field for property

        public T OptionEntity
        {
            get { return (T)_optionEntity; }
        }
        private T _optionEntity;

        #endregion

        #region public static methods

        public static Option<T> Builder(Type type)
        {
            Console.WriteLine(type);
            return null;
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        #endregion

        #region fields

        #endregion
    }
}
