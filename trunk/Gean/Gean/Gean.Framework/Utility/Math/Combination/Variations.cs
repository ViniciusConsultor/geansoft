using System;

namespace Gean.MathHelper
{
    /// <summary>
    /// Generates all the variations but NOT in lexicographical order
    /// </summary>
    public class Variations : CombinatorialBase
    {

        bool _bPermutateNow = true;
        protected int[] _arrayIndecesPermutation;
        protected int[] _arrayIndecesCombination;

        public Variations(Array arrayObjects, int nKlass) :
            base(arrayObjects, nKlass)
        {
            _arrayIndecesPermutation = new int[nKlass];
            _arrayIndecesCombination = new int[nKlass];
        }

        public Variations(System.Collections.IList listObjects, int nKlass) :
            base(listObjects, nKlass)
        {
            _arrayIndecesPermutation = new int[nKlass];
            _arrayIndecesCombination = new int[nKlass];
        }

        public Variations(System.Collections.IEnumerator enumeratorObjects, int nKlass) :
            base(enumeratorObjects, nKlass)
        {
            _arrayIndecesPermutation = new int[nKlass];
            _arrayIndecesCombination = new int[nKlass];
        }

        private void FirstIndecesPermutation()
        {
            // Reinitialize the permutation index.
            for (int i = 0; i < _arrayIndecesPermutation.Length; i++)
                _arrayIndecesPermutation[i] = i;
        }

        protected override int[] FirstIndeces(bool bReturnDublicate)
        {

            // Do some extra initializing.
            FirstIndecesPermutation();

            // Reinitialize the combination index.
            for (int i = 0; i < _arrayIndecesCombination.Length; i++)
                _arrayIndecesCombination[i] = i;

            // Call the father method, because it is needed.
            return base.FirstIndeces(bReturnDublicate);
        }

        /*virtual*/
        protected override int[] NextIndeces(bool bReturnDublicate)
        {

            if (!_bInitialized)
            {
                return FirstIndeces(false);
            }


            if (_bPermutateNow)
            {
                // Generate a permutation of the current variation.
                int[] array = NextIndecesPermutation(bReturnDublicate);

                if (array.Length != 0)
                    return array;

                // If we got here we cannot generate more permutations from
                // the current combination so we advance to the next combination
                // by allowing the CODE FLOW to execute the code bellow.
                _bPermutateNow = false;
            }

            // Find the first item that has not reached its maximum value.
            int nIndex = _nKlass;
            for (int i = _arrayIndecesCombination.Length - 1; i >= 0; i--)
            {
                if (_arrayIndecesCombination[i] < _nMaxIndex - (_nKlass - 1 - i))
                {
                    nIndex = i;
                    break;
                }
            }

            // No more combinations to be generated. Every item has reached its
            // maximum value.
            if (nIndex == _nKlass)
                return new int[0];

            // Genereate the next combination in lexographical order.
            _arrayIndecesCombination[nIndex]++;
            for (int i = nIndex + 1; i < _arrayIndecesCombination.Length; i++)
            {
                _arrayIndecesCombination[i] = _arrayIndecesCombination[i - 1] + 1;
            }

            // A new cobination has been generated. The next time we must
            // permutate it.
            _bPermutateNow = true;
            FirstIndecesPermutation();

            // Absolutely needed copy.
            Array.Copy(_arrayIndecesCombination, _arrayIndeces, _nKlass);

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

        protected int[] NextIndecesPermutation(bool bReturnDublicate)
        {

            int nIndexLast = _arrayIndecesPermutation.Length - 1;

            // Find the first item so that a[i] < a[i+1];
            int nIndexI = _nKlass;
            for (int i = nIndexLast - 1; i >= 0; i--)
            {
                if (_arrayIndecesPermutation[i] < _arrayIndecesPermutation[i + 1])
                {
                    nIndexI = i;
                    break;
                }
            }

            // Find the smallest a[j] , so that j > i & a[j] > a[i]
            int nIndexJ = nIndexLast;
            for (int j = nIndexJ; j > nIndexI; j--)
            {
                if (_arrayIndecesPermutation[j] > _arrayIndecesPermutation[nIndexI])
                {
                    nIndexJ = j;
                    break;
                }
            }

            // No more permutations to be generated. 
            if (nIndexI == _nKlass)
                return new int[0];

            // Exchange the a[i] and the last item (a[n]).
            int nTmp = _arrayIndecesPermutation[nIndexI];
            _arrayIndecesPermutation[nIndexI] = _arrayIndecesPermutation[nIndexJ];
            _arrayIndecesPermutation[nIndexJ] = nTmp;

            // Reverse the items from a[i+1] till a[n];
            int i0 = nIndexI + 1;
            int j0 = nIndexLast;
            while (i0 < j0)
            {
                nTmp = _arrayIndecesPermutation[i0];
                _arrayIndecesPermutation[i0] = _arrayIndecesPermutation[j0];
                _arrayIndecesPermutation[j0] = nTmp;
                i0++;
                j0--;
            }

            for (int i1 = 0; i1 < _nKlass; i1++)
            {
                int nIndex = _arrayIndecesPermutation[i1];
                _arrayIndeces[i1] = _arrayIndecesCombination[nIndex];
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

        } // End of NextIndecesPermutation

    }
}
