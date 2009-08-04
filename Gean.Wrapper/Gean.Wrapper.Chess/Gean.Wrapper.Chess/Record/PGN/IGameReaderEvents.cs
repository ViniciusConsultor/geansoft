using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// Defines all of the events that a game parser may call to
    /// listeners so that they may build a chess game.
    /// </summary>
    public interface IGameReaderEvents
    {
        /// <summary>
        /// Called when a new game in the parsed data file has been located.
        /// </summary>
        /// <param name="iParser"></param>
        void NewGame(IGameReader iParser);
        /// <summary>
        /// Called when you are no longer parsing the header tags and are
        /// beginning to enter the parsing of the moves.
        /// </summary>
        /// <param name="iParser"></param>
        void ExitHeader(IGameReader iParser);
        /// <summary>
        /// Called when the parser is entering a variation.
        /// </summary>
        /// <param name="iParser"></param>
        void EnterVariation(IGameReader iParser);
        /// <summary>
        /// Calle when the parser is leaving a variation.
        /// </summary>
        /// <param name="iParser"></param>
        void ExitVariation(IGameReader iParser);
        /// <summary>
        /// Called when the parser is completely finished parsing a file.
        /// </summary>
        /// <param name="iParser"></param>
        void Starting(IGameReader iParser);
        /// <summary>
        /// Called when the parser is completely finished parsing a file.
        /// </summary>
        /// <param name="iParser"></param>
        void Finished(IGameReader iParser);
        /// <summary>
        /// Called when the parser has determined game header information
        /// such as Player names, ELO, ECO, Dates, and others.
        /// See the PGN standards documentation.
        /// </summary>
        /// <param name="iParser"></param>
        void TagParsed(IGameReader iParser);
        /// <summary>
        /// Called when the parser has determined a NAG comments based on PGN
        /// standards.
        /// </summary>
        /// <param name="iParser"></param>
        void NagParsed(IGameReader iParser);
        /// <summary>
        /// Called when the parser has determined a move is present.
        /// </summary>
        /// <param name="iParser"></param>
        void MoveParsed(IGameReader iParser);
        /// <summary>
        /// Called when the parser has determined a comment is present.
        /// </summary>
        /// <param name="iParser"></param>
        void CommentParsed(IGameReader iParser);
        /// <summary>
        /// Used to signal an end of game marker has been found ie 1/2-1/2, 1-0, 0-1
        /// </summary>
        /// <param name="iParser"></param>
        void EndMarker(IGameReader iParser);
    }
}
