using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Net.KeepSocket
{
    public class SplitBytes
    {
        private byte[] receiveAllByte;

        public SplitBytes()
        {
            receiveAllByte = null;
        }

        public byte[] ReceiveAllByte
        {
            get
            {
                return receiveAllByte;
            }
        }

        public void Dispose()
        {
            receiveAllByte = null;
        }

        public void AddBytes(byte[] recByte, int count)
        {
            byte[] f;

            if (receiveAllByte != null)
            {
                f = new byte[receiveAllByte.Length + count];

                for (int i = 0; i < receiveAllByte.Length; i++)
                {
                    f[i] = receiveAllByte[i];
                }
                for (int i = 0; i < count; i++)
                {
                    f[i + receiveAllByte.Length] = recByte[i];
                }
            }
            else
            {
                f = new byte[count];
                for (int i = 0; i < count; i++)
                {
                    f[i] = recByte[i];
                }
            }

            receiveAllByte = f;
        }
    }
}
