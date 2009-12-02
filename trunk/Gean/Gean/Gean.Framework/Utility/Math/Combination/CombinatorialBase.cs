using System;
using System.Collections;


namespace Gean.MathHelper
{
    /// <summary>
    /// 
    /// </summary>
    abstract public class CombinatorialBase : IEnumerator
    {
        // This is a local copy of the items that need to be supplied 
        // in various combinatorial forms.
        // !! NOTE !! : This must be a shallow copy of the original list of items.
        //				or it must be a deep one ???
        protected Array _arrayObj;

        protected int _nKlass;
        protected int _nMaxIndex;

        // Because we use _arrayIndeces internally to store the current combination
        // we need the DUMMY array to be returned to the user so that he cannot modify
        // the internal state of the COMBINATION which will be possible if 
        // we return direcly _arrayIndeces. This is one of the weaknesses of the 
        // C# & CLI -> There is no const-nes over arrays in the sence of C++, too bad :-(
        protected int[] _arrayIndeces;
        protected int[] _arrayIndecesDummy;

        /// <summary>
        /// Hold the items that build up the current combination.
        /// </summary>
        protected Array _arrayCurrentObj;

        protected bool _bInitialized = false;

        /// <summary>
        /// This is the GENERIC Type constructor.
        /// </summary>
        /// <param name="arrayObjects"></param>
        /// <param name="nKlass"></param>
        protected CombinatorialBase(Array arrayObjects, int nKlass)
        {
            // Check the validity of the arguments
            DoArgumentCheck(arrayObjects.Length, nKlass);

            _nKlass = nKlass;

            // Always takes the ZERO (FIRST) dimension of the array. There is no
            // problem in manipulation multidimensional arrays.
            _nMaxIndex = arrayObjects.GetLength(0) - 1;

            _arrayIndeces = new int[_nKlass];
            _arrayIndecesDummy = new int[_nKlass];

            _arrayCurrentObj = new Object[_nKlass];
            //			_arrayCurrentCombination = Array.CreateInstance( 
            //				arrayObjects.GetValue(0).GetType(), _nKlass);

            // Make a shallow copy of the source array.
            //			_arrayObj = Array.CreateInstance( arrayObjects.GetValue(0).GetType(), arrayObjects.Length);
            _arrayObj = Array.CreateInstance(Type.GetType("System.Object"), arrayObjects.Length);
            Array.Copy(arrayObjects, _arrayObj, arrayObjects.Length);
        }

        /// <summary>
        /// Handles the lists of elements.
        /// </summary>
        /// <param name="listObjects"></param>
        /// <param name="nKlass"></param>
        protected CombinatorialBase(IList listObjects, int nKlass)
        {

            // Check the validity of the arguments
            DoArgumentCheck(listObjects.Count, nKlass);

            _nKlass = nKlass;

            // Always takes the ZERO (FIRST) dimension of the array. There is no
            // problem in manipulation multidimensional arrays.
            _nMaxIndex = listObjects.Count - 1;

            _arrayIndeces = new int[_nKlass];
            _arrayIndecesDummy = new int[_nKlass];

            _arrayCurrentObj = new Object[_nKlass];

            // Make a shallow copy of the source array.
            //			_arrayObj = Array.CreateInstance(listObjects[0].GetType(), listObjects.Count);
            _arrayObj = Array.CreateInstance(Type.GetType("System.Object"), listObjects.Count);
            listObjects.CopyTo(_arrayObj, 0);
        }

        /// <summary>
        /// Handles the lists of elements.
        /// </summary>
        /// <param name="enumeratorObjects"></param>
        /// <param name="nKlass"></param>
        protected CombinatorialBase(IEnumerator enumeratorObjects, int nKlass)
        {

            _nKlass = nKlass;

            // Because when an enumerator is used we don't know the exact number of items,
            // in order to create an array we must first go through all the objects once.
            int nEnumCount = 0;
            enumeratorObjects.Reset();
            while (enumeratorObjects.MoveNext())
            {
                nEnumCount++;
            }

            // Check the validity of the arguments
            DoArgumentCheck(nEnumCount, nKlass);

            _nMaxIndex = nEnumCount - 1;

            _arrayIndeces = new int[_nKlass];
            _arrayIndecesDummy = new int[_nKlass];

            _arrayCurrentObj = new Object[_nKlass];

            // Make a shallow copy of the source enumerator.
            _arrayObj = Array.CreateInstance(Type.GetType("System.Object"), nEnumCount);
            int i = 0;
            enumeratorObjects.Reset();
            while (enumeratorObjects.MoveNext())
            {
                Object obj = enumeratorObjects.Current;
                _arrayObj.SetValue(obj, i);
                i++;
            }
        }

        virtual protected void DoArgumentCheck(int items, int klass)
        {
            if (klass <= 0)
                throw new ArgumentOutOfRangeException("nKlass", klass,
                "Second parameter (nKlass) to CombinatorialBase constructor must be > 0");

            if (items < klass)
                throw new ArgumentOutOfRangeException("nKlass", klass,
                    "Less than needed objects supplied. Second " +
                    "parameter of CombinatorialBase cannot be greater that the number " +
                    "of objects");
        }

        public Object Current
        {
            get { return CurrentItems(); }
        }

        public bool MoveNext()
        {

            int[] indeces = NextIndeces(false);

            if (indeces.Length > 0)
                return true;
            else
                return false;
        }

        public void Reset()
        {
            _bInitialized = false;
        }

        protected int[] FirstIndeces()
        {
            return FirstIndeces(true);
        }

        /// <summary>
        /// This one is made virtual. If you need a different initializing
        /// sequence than the default {0, 1, 2, ..., N} then override it.
        /// </summary>
        /// <param name="bReturnDublicate"></param>
        /// <returns></returns>
        virtual protected int[] FirstIndeces(bool returnDublicate)
        {
            for (int i = 0; i < _arrayIndeces.Length; i++)
                _arrayIndeces[i] = i;

            _bInitialized = true;

            if (returnDublicate)
            {
                // Copy not to allow modification of _arrayIndeces.
                Array.Copy(_arrayIndeces, _arrayIndecesDummy, _nKlass);
                return _arrayIndecesDummy;
            }
            else
            {
                return _arrayIndeces;
            }
        } // End of FirstIndeces

        protected int[] NextIndeces()
        {
            return NextIndeces(true);
        }

        abstract protected int[] NextIndeces(bool returnDublicate);

        protected Array NextItems()
        {

            // Generate the indeces of the elements that are going to
            // take part in the next combination.
            int[] res = NextIndeces(false);

            if (res == null) return null;

            for (int j = 0; j < _arrayIndeces.Length; j++)
            {
                int nIndex = _arrayIndeces[j];
                _arrayCurrentObj.SetValue(_arrayObj.GetValue(nIndex), j);
            }

            return _arrayCurrentObj;
        }

        public int[] CurrentIndeces
        {

            get
            {
                if (!_bInitialized)
                    throw new InvalidOperationException("CombinatorialBase collection must be Reset() before usage");

                // Copy not to allow modification of _arrayIndeces.
                Array.Copy(_arrayIndeces, _arrayIndecesDummy, _nKlass);
                return _arrayIndecesDummy;
            }
        }

        protected Array CurrentItems()
        {

            if (!_bInitialized)
                throw new InvalidOperationException("CombinatorialBase collection must be Reset() before usage");

            // Fill the return array properly.
            for (int j = 0; j < _arrayIndeces.Length; j++)
            {
                int nIndex = _arrayIndeces[j];
                _arrayCurrentObj.SetValue(_arrayObj.GetValue(nIndex), j);
            }

            // And return it.
            return _arrayCurrentObj;
        }

    }
}
