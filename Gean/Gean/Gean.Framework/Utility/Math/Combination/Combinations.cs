using System;

namespace Gean.MathHelper
{
	/// <summary>
	/// Generates cobination without dublications in lexicographical order
	/// </summary>
	public class Combinations : CombinatorialBase
	{
		public Combinations(Array arrayObjects, int nKlass) : 
			base(arrayObjects, nKlass)
		{
		}

		public Combinations(System.Collections.IList listObjects, int nKlass) : 
			base(listObjects, nKlass) 
		{
		}

		public Combinations(System.Collections.IEnumerator enumeratorObjects, int nKlass) : 
			base(enumeratorObjects, nKlass) 
		{
		}

		/*virtual*/
        protected override int[] NextIndeces(bool bReturnDublicate)
        {

			if (!_bInitialized) {
				return FirstIndeces(false);
			}

			// Find the first item that has not reached its maximum value.
			int nIndex = _nKlass;
			for (int i = _arrayIndeces.Length - 1; i >= 0; i--) {
				if (_arrayIndeces[i] < _nMaxIndex - (_nKlass - 1 - i)) {
					nIndex = i;
					break;
				}
			}

			// No more combinations to be generated. Every item has reached its
			// maximum value.
			if (nIndex == _nKlass) 
				return new int[0];

			// Genereate the next combination in lexographical order.
			_arrayIndeces[nIndex]++;
			for (int i = nIndex + 1; i < _arrayIndeces.Length; i++) {
				_arrayIndeces[i] = _arrayIndeces[i-1] + 1;
			}

			if (bReturnDublicate) {
				// Copy not to allow modification of _arrayIndeces.
				Array.Copy(_arrayIndeces, _arrayIndecesDummy, _nKlass);
				return _arrayIndecesDummy;
			} else {
				return _arrayIndeces;
			}
		} // End of NextIndeces

	}
}
