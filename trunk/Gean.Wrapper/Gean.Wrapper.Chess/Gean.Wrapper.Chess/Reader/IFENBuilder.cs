using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    ///   An interface that defines the minimal functionality that must
    ///   be implemented to allow the setup of a chess position. 
    ///   Normally used for a FEN or EDP parser that needs to setup
    ///   a position in multiple places such as a validation engine
    ///   and the bitmap dislay of the pieces.
    /// </summary>
    public interface IFENBuilder
    {
        /// <summary>
        /// Used to parse out a position calling the events as needed to inform the using class of what's happening.
        /// </summary>
        /// <param name="str"></param>
        void Parse(string str);
    }
}
