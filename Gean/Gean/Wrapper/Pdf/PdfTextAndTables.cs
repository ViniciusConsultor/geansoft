using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Pdf
{
    /// <summary>
    /// Draw a table in the pdf file
    /// </summary>
    public class PdfTextAndTables
    {
        private uint fixedTop, lastY;
        private uint tableX;
        private PdfPageSize pSize;
        private ArrayList rowY;
        private uint cPadding;
        private string errMsg;
        private uint numColumn, rowHeight, numRow;
        private uint[] colWidth;
        private uint textX, textY;
        private string tableStream;
        private uint tableWidth;
        private PdfColor cColor;
        private string textStream;

        private uint[] TimesRomanWidth ={0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,250,333,408,500,
		500,833,778,333,333,333,500,564,250,333,250,278,500,500,500,500,500,500,500,500,500,500,278,278,564,564,564,444,
		921,722,667,667,722,611,556,722,722,333,389,722,611,889,722,722,556,722,667,556,611,722,722,944,722,722,611,333,
		278,333,469,500,333,444,500,444,500,444,333,500,500,278,278,500,278,778,500,500,500,500,333,389,278,500,500,722,
		500,500,444,480,200,480,541,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,333,500,500,167,500,500,
		500,500,180,444,500,333,333,556,556,500,500,500,250,453,350,333,444,444,500,100,100,444,333,333,333,333,333,333,
		333,333,333,333,333,333,333,100,889,276,611,722,889,310,667,278,278,500,722,500};

        public PdfTextAndTables(PdfPageSize pageSize)
        {
            fixedTop = lastY = 0;
            pSize = pageSize;
            tableX = 0;
            textStream = tableStream = errMsg = null;
            rowHeight = 0; numRow = 0;
            textX = 0; textY = 0;
        }
        /// <summary>
        /// Set the parameters of the table
        /// </summary>
        public bool SetParams(PdfTableParams table, PdfColor cellColor, TextAlignByTable alignment, uint cellPadding)
        {
            if ((table.yPos > (pSize.yHeight - pSize.topMargin)) ||
                (tableWidth > (pSize.xWidth - (pSize.leftMargin + pSize.rightMargin))))
                return false;
            tableWidth = table.tableWidth;
            switch (alignment)
            {
                case (TextAlignByTable.LeftAlign):
                    tableX = pSize.leftMargin;
                    break;
                case (TextAlignByTable.CenterAlign):
                    tableX = (pSize.xWidth - (pSize.leftMargin + pSize.rightMargin)
                        - tableWidth) / 2;
                    break;
                case (TextAlignByTable.RightAlign):
                    tableX = pSize.xWidth - (pSize.rightMargin + tableWidth);
                    break;
            };
            textX = tableX;
            textY = table.yPos;
            fixedTop = table.yPos;
            rowHeight = table.rowHeight;
            numColumn = table.numColumn;
            cColor = cellColor;
            cPadding = cellPadding;
            colWidth = new uint[numColumn];
            colWidth = table.columnWidths;
            rowY = new ArrayList();
            return true;

        }
        /// <summary>
        /// Get the lines of text in the cells, when text wrap is true
        /// </summary>
        /// <param name="rowText"></param>
        /// <param name="fontSize"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private string[][] GetLinesInCell(string[] rowText, uint fontSize)
        {
            string[][] text = new string[numColumn][];
            char[] s = { '\x0020' };
            for (int i = 0; i < rowText.Length; i++)
            {
                uint width = (colWidth[i] - 2 * cPadding) * 1000 / fontSize;
                string cellText = rowText[i];//.TrimStart(s);
                //No entries are mandatory. So just break in case if any entry is not 
                //available
                if (cellText == null)
                {
                    break;
                }
                ArrayList lineText = new ArrayList();
                int index = 0;
                uint cWidth = 0;
                int words = 0;
                for (int chars = 0; chars <= cellText.Length; chars++)
                {
                    char[] cArray = cellText.ToCharArray();
                    do
                    {
                        cWidth += TimesRomanWidth[cArray[words]];
                        words++;
                    } while (cWidth < width && words < cArray.Length);

                    if (words == cArray.Length)
                    {
                        string line = cellText.Substring(0, words);
                        line = line.TrimEnd(s);
                        lineText.Add(line);
                        break;
                    }
                    else
                    {
                        words--;
                        int space = cellText.LastIndexOf('\x0020', words, words + 1);
                        if (space > 0)
                        {
                            string line = cellText.Substring(0, space + 1);
                            //To remove the trailing space from the word
                            line = line.TrimEnd(s);
                            lineText.Add(line);
                            index = space + 1;
                            words = 0;
                        }
                        else
                        {
                            string line = cellText.Substring(0, words);
                            lineText.Add(line);
                            index = words;
                            words = 0;
                        }
                    }
                    cWidth = 0;
                    chars = 0;
                    cellText = cellText.Substring(index);
                }
                text[i] = new string[lineText.Count];
                //Copy the lines into the array to be returned
                for (int j = 0; j < lineText.Count; j++)
                    text[i][j] = (string)lineText[j];

            }
            return text;
        }
        /// <summary>
        /// Get the length of the string in points
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        private uint StrLen(string text, uint fontSize)
        {
            char[] cArray = text.ToCharArray();
            uint cWidth = 0;
            foreach (char c in cArray)
            {
                cWidth += TimesRomanWidth[c];
            }
            uint k = (cWidth * fontSize / 1000);
            return (cWidth * fontSize / 1000);
        }
        /// <summary>
        /// Add One row to the table
        /// </summary>
        /// <param name="textWrap"></param>
        /// <param name="fontSize"></param>
        /// <param name="fontName"></param>
        /// <param name="alignment"></param>
        /// <param name="rowText"></param>
        /// <returns></returns>
        public bool AddRow(bool textWrap, uint fontSize, string fontName, TextAlignByTable[] alignment, params string[] rowText)
        {
            if (rowText.Length > numColumn)
                return false;
            uint maxLines = 1;
            //The x cordinate of the aligned table
            uint startX = tableX;
            uint y = textY;
            uint x = 0;
            const uint yCellMargin = 4;
            //if(rowText.Length<numColumn || alignment.Length<numColumn)
            //	return false;
            //increment the number of rows as it is added
            numRow++;
            //Wrap the text if textWrap is true
            if (textWrap)
            {
                string[][] text = GetLinesInCell(rowText, fontSize);
                //Loop for the required number of columns
                for (int column = 0; column < rowText.Length; column++)
                {
                    startX += colWidth[column];
                    uint lines;
                    //No entries are mandatory. So just break in case if any entry is not 
                    //available
                    if (text[column] == null)
                        break;
                    //Loop for the number of lines in a cell
                    for (lines = 0; lines < (uint)text[column].Length; lines++)
                    {
                        y = (uint)(textY - ((lines + 1) * rowHeight)) + yCellMargin;
                        try
                        {
                            switch (alignment[column])
                            {
                                case (TextAlignByTable.LeftAlign):
                                    x = startX - colWidth[column] + cPadding;
                                    break;
                                case (TextAlignByTable.CenterAlign):
                                    x = startX - (colWidth[column] + StrLen(text[column][lines], fontSize)) / 2;
                                    break;
                                case (TextAlignByTable.RightAlign):
                                    x = startX - StrLen(text[column][lines], fontSize) - cPadding;
                                    break;
                            };
                        }
                        catch (Exception E)
                        {
                            errMsg = "String too long to fit in the Column" + E.Message;
                            Exception e = new Exception(errMsg);
                            throw e;
                        }
                        //( this is a escape character in adobe so remove it.
                        text[column][lines] = text[column][lines].Replace("(", "\\(");
                        text[column][lines] = text[column][lines].Replace(")", "\\)");
                        tableStream += string.Format("\rBT/{0} {1} Tf \r{2} {3} Td \r({4}) Tj\rET",
                            fontName, fontSize, x, y, text[column][lines]);
                    }
                    //Calculate the maximum number of lines in this row
                    if (lines > maxLines)
                        maxLines = lines;
                }
                //Change the Y cordinate to skip to next page
                if (textY < pSize.bottomMargin)
                {
                    textY = 0;
                    return false;
                }
                //Change Y cordinate to skip to next row
                else
                {
                    textY = textY - rowHeight * maxLines;
                    rowY.Add(textY);
                    rowY.Add(rowHeight * maxLines);
                }
            }
            //If no text wrap is required
            else
            {
                for (int column = 0; column < rowText.Length; column++)
                {
                    startX += colWidth[column];
                    y = (uint)(textY - rowHeight) + yCellMargin;
                    try
                    {
                        switch (alignment[column])
                        {
                            case (TextAlignByTable.LeftAlign):
                                x = startX - colWidth[column] + cPadding;
                                break;
                            case (TextAlignByTable.CenterAlign):
                                x = startX - (colWidth[column] + StrLen(rowText[column], fontSize)) / 2;
                                break;
                            case (TextAlignByTable.RightAlign):
                                x = startX - StrLen(rowText[column], fontSize) - cPadding;
                                break;
                        };
                    }
                    catch (Exception E)
                    {
                        errMsg = "String too long to fit in the Column" + E.Message;
                        Exception error = new Exception(errMsg);
                        throw error;
                    }
                    tableStream += string.Format("\rBT/{0} {1} Tf \r{2} {3} Td \r({4}) Tj\rET",
                        fontName, fontSize, x, y, rowText[column]);
                }
                if (textY < pSize.bottomMargin)
                {
                    textY = 0;
                    return false;
                }
                //Change Y cordinate to skip to next row
                else
                {
                    textY = textY - rowHeight;
                    rowY.Add(textY);
                    rowY.Add(rowHeight);
                }
            }
            return true;
        }
        /// <summary>
        /// start the Page Text element at the X Y position
        /// </summary>
        /// <returns></returns>
        public void AddText(uint X, uint Y, string text, uint fontSize, string fontName, TextAlignByTable alignment)
        {
            Exception invalidPoints = new Exception("The X Y cordinate outof Page Boundary");
            if (X > pSize.xWidth || Y > pSize.yHeight)
                throw invalidPoints;
            uint startX = 0;
            switch (alignment)
            {
                case (TextAlignByTable.LeftAlign):
                    startX = X;
                    break;
                case (TextAlignByTable.CenterAlign):
                    startX = X - (StrLen(text, fontSize)) / 2;
                    break;
                case (TextAlignByTable.RightAlign):
                    startX = X - StrLen(text, fontSize);
                    break;
            };
            textStream += string.Format("\rBT/{0} {1} Tf\r{2} {3} Td \r({4}) Tj\rET\r",
                fontName, fontSize, startX, (pSize.yHeight - Y), text);
        }
        /// <summary>
        /// End the Text Element on a page
        /// </summary>
        /// <returns></returns>
        public string EndText()
        {
            Exception noTextStream = new Exception("No Text Element Created");
            if (textStream == null)
                throw noTextStream;
            string stream = textStream;
            textStream = null;
            return stream;
        }
        /// <summary>
        /// Call to end the Table Creation and Get the Stream Data
        /// </summary>
        /// <returns></returns>
        public string EndTable(PdfColor lineColor)
        {
            string tableCode;
            string rect = null;
            uint x = tableX;
            uint yBottom = 0;
            //if required number of rows are added
            if (rowY.Count < numRow * 2)
                return null;
            //Draw the number of rows.
            //int rowHeight=Get
            for (int row = 0, yCor = 0; row < numRow; row++, yCor += 2)
            {
                rect += string.Format("{0} {1} {2} {3} re\r",
                    x, rowY[yCor], tableWidth, rowY[yCor + 1]);
            }
            //To get the ycordinate of the last row in the table
            if (rowY.Count < 1)
                return null;
            yBottom = (uint)rowY[rowY.Count - 2];
            string line = null;
            //Draw lines to form the columns
            for (uint column = 0; column < numColumn; column++)
            {
                x += colWidth[column];
                line += string.Format("{0} {1} m\r{0} {2} l\r",
                    x, fixedTop, yBottom);
            }
            //Create the code for the Table
            tableCode = string.Format("\rq\r{5} {6} {7} RG {2} {3} {4} rg\r{0}\r{1}B\rQ\r",
                line, rect, cColor.red, cColor.green, cColor.blue, lineColor.red, lineColor.green, lineColor.blue);

            lastY = yBottom;
            tableCode += tableStream;
            //Initailize the variables so that they can be used again
            tableStream = null;
            numRow = 0;
            rowY = null;
            return tableCode;
        }

        /// <summary>
        /// Get the Y cordinate for next Table to be appended
        /// </summary>
        /// <returns></returns>
        public uint GetY()
        {
            return lastY;
        }
    }

    /// <summary>
    /// Used with the Text inside a Table
    /// </summary>
    public enum TextAlignByTable
    {
        LeftAlign, CenterAlign, RightAlign
    }
}
