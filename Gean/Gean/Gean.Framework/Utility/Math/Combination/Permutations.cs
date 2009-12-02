using System;
using System.Collections;

namespace Gean.MathHelper
{
    /// <summary>
    /// Generates all the permutations in lexicographical order
    /// </summary>
    public class Permutations : CombinatorialBase
    {
        public Permutations(Array arrayObjects)
            : base(arrayObjects, arrayObjects.Length)
        {
        }

        public Permutations(IList listObjects)
            : base(listObjects, listObjects.Count)
        {
        }

        public Permutations(IEnumerator enumeratorObjects)
            : base(enumeratorObjects, 1)
        {

            // In permutations the class is the same as the number of elements 
            // in the object collection.
            _nKlass = _nMaxIndex + 1;

            // We need to reinitialize these objects because in the constuctor call
            // of the base class we pass "1" as "nKlass" argument which is not proper
            // in most cases.
            _arrayIndeces = new int[_nKlass];
            _arrayIndecesDummy = new int[_nKlass];
            _arrayCurrentObj = new Object[_nKlass];
        }

        /*virtual*/
        override protected int[] NextIndeces(bool bReturnDublicate)
        {

            if (!_bInitialized)
            {
                return FirstIndeces(false);
            }

            int nIndexLast = _arrayIndeces.Length - 1;

            // Find the first item so that a[i] < a[i+1];
            int nIndexI = _nKlass;
            for (int i = nIndexLast - 1; i >= 0; i--)
            {
                if (_arrayIndeces[i] < _arrayIndeces[i + 1])
                {
                    nIndexI = i;
                    break;
                }
            }

            // Find the smallest a[j] , so that j > i & a[j] > a[i]
            int nIndexJ = nIndexLast;
            for (int j = nIndexJ; j > nIndexI; j--)
            {
                if (_arrayIndeces[j] > _arrayIndeces[nIndexI])
                {
                    nIndexJ = j;
                    break;
                }
            }

            // No more permutations to be generated. 
            if (nIndexI == _nKlass)
                return new int[0];

            // Exchange the a[i] and the last item (a[n]).
            int nTmp = _arrayIndeces[nIndexI];
            _arrayIndeces[nIndexI] = _arrayIndeces[nIndexJ];
            _arrayIndeces[nIndexJ] = nTmp;

            // Reverse the items from a[i+1] till a[n];
            int i0 = nIndexI + 1;
            int j0 = nIndexLast;
            while (i0 < j0)
            {
                nTmp = _arrayIndeces[i0];
                _arrayIndeces[i0] = _arrayIndeces[j0];
                _arrayIndeces[j0] = nTmp;
                i0++;
                j0--;
            }

            // The RETURN section.
            if (bReturnDublicate)
            {
                // Copy not to allow modification of _arrayIndeces.
                Array.Copy(_arrayIndeces, _arrayIndecesDummy, _nKlass);
                return _arrayIndecesDummy;
            }
            else
            {
                return _arrayIndeces;
            }

        } // End of NextIndeces

    }
}
