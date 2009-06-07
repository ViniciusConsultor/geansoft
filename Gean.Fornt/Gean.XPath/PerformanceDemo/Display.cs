namespace PerformanceDemo
{
    using System;

    public class Display
    {
        private static int m_Indent = 0;

        public static void Show(string Message)
        {
            for (int i = 0; i < (m_Indent * 4); i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine(Message);
        }

        public static void Show(int PreIndent, string Message, int PostIndent)
        {
            m_Indent += PreIndent;
            if (m_Indent < 0)
            {
                m_Indent = 0;
            }
            int num = m_Indent * 4;
            m_Indent += PostIndent;
            if (m_Indent < 0)
            {
                m_Indent = 0;
            }
            for (int i = 0; i < num; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine(Message);
        }

        public static int Indent
        {
            get
            {
                return m_Indent;
            }
            set
            {
                m_Indent = value;
            }
        }
    }
}

