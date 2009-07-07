using System;
using System.Collections.Generic;
using System.Text;

namespace Gean
{
    [Serializable]
    public class IntString
    {
        public IntString(ulong i)
        {
            this.Value = i;
            this.Digit = IntString.GetDigit(i);
        }
        public IntString()
        {
            this.Value = 0;
            this.Digit = 3;
        }

        public ulong Value { get; protected set; }
        public uint Digit { get; set; }

        public virtual void Next()
        {
            this.Value++;
        }

        public virtual void Back()
        {
            this.Value--;
        }

        public static IntString operator +(IntString a, IntString b)
        {
            IntString c = new IntString();
            c.Value = a.Value + b.Value;
            return c;
        }

        public static IntString operator -(IntString a, IntString b)
        {
            IntString c = new IntString();
            c.Value = a.Value - b.Value;
            return c;
        }

        public static bool operator ==(IntString a, IntString b)
        {
            return a.Value.Equals(b.Value);
        }

        public static bool operator !=(IntString a, IntString b)
        {
            return !a.Value.Equals(b.Value);
        }

        public override bool Equals(object obj)
        {
            IntString ins = (IntString)obj;
            return ins.Value.Equals(this.Value);
        }

        public override int GetHashCode()
        {
            return unchecked(7 * Value.GetHashCode());
        }

        public override string ToString()
        {
            return this.ToString(this.Digit);
        }
        public string ToString(uint digit)
        {
            uint n = IntString.GetDigit(this.Value);
            if (n > digit)
            {
                this.Digit = n;
            }
            else
            {
                this.Digit = digit;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.Digit - n; i++)
            {
                sb.Append('0');
            }
            sb.Append(this.Value);
            return sb.ToString();
        }

        public static uint GetDigit(ulong value)
        {
            uint numlen = 0;
            long flag = 0;
            long x;
            do
            {
                if (value == 0) //判断是否为0
                {
                    numlen++;
                    break;
                }
                x = (long)value / (long)Math.Pow(10, numlen);//例子123/10=12,12%10=2这样就可以把某位数取出
                flag = x % 10;
                if (flag == 0 && x < 10)
                {
                    flag = -1;
                }
                else
                {
                    numlen++;
                }
            }
            while (flag != -1);
            return numlen;
        }

    }
}
