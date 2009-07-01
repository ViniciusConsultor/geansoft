using Gean.Pdf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
namespace Gean.FrameworkUnitTesting
{


    /// <summary>
    ///这是 PdfDocumentTest 的测试类，旨在
    ///包含所有 PdfDocumentTest 单元测试
    ///</summary>
    [TestClass()]
    public class PdfDocumentTest
    {

        #region
        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试属性
        // 
        //编写测试时，还可使用以下属性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion
        #endregion

        /// <summary>
        ///PdfDocument 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void PdfDocumentAllTest()
        {
            //Create a Catalog Dictionary
            CatalogDictionary catalogDict = new CatalogDictionary();

            //Create a Page Tree Dictionary
            PageTreeDictionary pageTreeDict = new PageTreeDictionary();

            //Create a Font Dictionary
            FontDictionary timesRoman = new FontDictionary();
            FontDictionary timesItalic = new FontDictionary();

            //Create the info Dictionary
            InfoDictionary infoDict = new InfoDictionary();

            //Create the font called Times Roman
            timesRoman.CreateFontDict("T1", "Times-Roman");

            //Create the font called Times Italic
            timesItalic.CreateFontDict("T2", "Times-Italic");

            //Set the info Dictionary. 
            infoDict.SetInfo("title", "author", "company");

            //Create a PdfDocument
            PdfDocument pdf = new PdfDocument();

            //Open a file specifying the file name as the output pdffile 
            FileStream file = new FileStream(@"e:\My Desktop\text.pdf", FileMode.Create);

            int size = 0;

            file.Write(pdf.GetHeader("1.5", out size), 0, size);
            //file.Close();
            //Now we finished doing the first step

            //Create a Page Dictionary , this represents a visible page
            PageDictionary page = new PageDictionary();
            ContentDictionary content = new ContentDictionary();

            //The page size object will hold all the page size information

            PdfPageSize pSize = new PdfPageSize(612, 792);
            pSize.SetMargins(10, 10, 10, 10);
            page.CreatePage(pageTreeDict.objectNum, pSize);
            pageTreeDict.AddPage(page.objectNum);
            page.AddResource(timesRoman, content.objectNum);

            //Create a Text And Table Object that present the elements in the page

            PdfLayout textAndtable = new PdfLayout(pSize);

            //Add text to the page
            string str = "Create a Text And Table Object that present the elements in the page";
            textAndtable.AddText(100, 150, str, 15, "Arial", TextAlignByTable.CenterAlign);

            //Create the array for alignment value.

            //This is specified for text in each column 

            //of the table, here we have two columns

            TextAlignByTable[] align = new TextAlignByTable[2];
            align[0] = TextAlignByTable.LeftAlign;
            align[1] = TextAlignByTable.LeftAlign;

            //Specify the color for the cell and the line

            PdfColor cellColor = new PdfColor(100, 100, 100);
            PdfColor lineColor = new PdfColor(98, 200, 200);

            //Fill in the parameters for the table

            PdfTableParams table = new PdfTableParams(2, 200, 200);
            table.Y = 700;
            table.X = 100;
            table.RowHeight = 20;

            //Set the parameters of this table

            textAndtable.SetParams(table, cellColor, TextAlignByTable.CenterAlign, 3);
            textAndtable.AddRow(false, 10, "T1", align, "First Column", "Second Column");
            textAndtable.AddRow(false, 10, "T1", align, "Second Row", "Second Row");

            //Repeat till we require the number of rows.

            //After drawing table and text add them to the page 

            content.SetStream(textAndtable.EndTable(lineColor));
            content.SetStream(textAndtable.EndText());

            size = 0;
            //file = new FileStream(@"e:\My Desktop\text.pdf", FileMode.Append);
            file.Write(page.GetPageDict(file.Length, out size), 0, size);
            file.Write(content.GetContentBytes(file.Length, out size), 0, size);
            //file.Close();

            ////Write everything file size=0;

            //file = new FileStream(@"e:\My Desktop\text.pdf", FileMode.Append);
            file.Write(catalogDict.GetCatalogDict(pageTreeDict.objectNum, file.Length, out size), 0, size);
            file.Write(pageTreeDict.GetPageTree(file.Length, out size), 0, size);
            file.Write(timesRoman.GetFontDict(file.Length, out size), 0, size);
            file.Write(timesItalic.GetFontDict(file.Length, out size), 0, size);
            file.Write(infoDict.GetInfoDict(file.Length, out size), 0, size);
            file.Write(pdf.CreateXrefTable(file.Length, out size), 0, size);
            file.Write(pdf.GetTrailer(catalogDict.objectNum, infoDict.objectNum, out size), 0, size);
            file.Close();
        }
    }
}
