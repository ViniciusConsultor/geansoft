using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gean.Math;

namespace Gean.Framework.Demo
{
    partial class Program
    {
        private static void BigIntegerDemo()
        {
            // Known problem -> these two pseudoprimes passes my implementation of
            // primality test but failed in JDK's isProbablePrime test.

            byte[] pseudoPrime1 = { (byte)0x00,
                        (byte)0x85, (byte)0x84, (byte)0x64, (byte)0xFD, (byte)0x70, (byte)0x6A,
                        (byte)0x9F, (byte)0xF0, (byte)0x94, (byte)0x0C, (byte)0x3E, (byte)0x2C,
                        (byte)0x74, (byte)0x34, (byte)0x05, (byte)0xC9, (byte)0x55, (byte)0xB3,
                        (byte)0x85, (byte)0x32, (byte)0x98, (byte)0x71, (byte)0xF9, (byte)0x41,
                        (byte)0x21, (byte)0x5F, (byte)0x02, (byte)0x9E, (byte)0xEA, (byte)0x56,
                        (byte)0x8D, (byte)0x8C, (byte)0x44, (byte)0xCC, (byte)0xEE, (byte)0xEE,
                        (byte)0x3D, (byte)0x2C, (byte)0x9D, (byte)0x2C, (byte)0x12, (byte)0x41,
                        (byte)0x1E, (byte)0xF1, (byte)0xC5, (byte)0x32, (byte)0xC3, (byte)0xAA,
                        (byte)0x31, (byte)0x4A, (byte)0x52, (byte)0xD8, (byte)0xE8, (byte)0xAF,
                        (byte)0x42, (byte)0xF4, (byte)0x72, (byte)0xA1, (byte)0x2A, (byte)0x0D,
                        (byte)0x97, (byte)0xB1, (byte)0x31, (byte)0xB3,
                };

            byte[] pseudoPrime2 = { (byte)0x00,
                        (byte)0x99, (byte)0x98, (byte)0xCA, (byte)0xB8, (byte)0x5E, (byte)0xD7,
                        (byte)0xE5, (byte)0xDC, (byte)0x28, (byte)0x5C, (byte)0x6F, (byte)0x0E,
                        (byte)0x15, (byte)0x09, (byte)0x59, (byte)0x6E, (byte)0x84, (byte)0xF3,
                        (byte)0x81, (byte)0xCD, (byte)0xDE, (byte)0x42, (byte)0xDC, (byte)0x93,
                        (byte)0xC2, (byte)0x7A, (byte)0x62, (byte)0xAC, (byte)0x6C, (byte)0xAF,
                        (byte)0xDE, (byte)0x74, (byte)0xE3, (byte)0xCB, (byte)0x60, (byte)0x20,
                        (byte)0x38, (byte)0x9C, (byte)0x21, (byte)0xC3, (byte)0xDC, (byte)0xC8,
                        (byte)0xA2, (byte)0x4D, (byte)0xC6, (byte)0x2A, (byte)0x35, (byte)0x7F,
                        (byte)0xF3, (byte)0xA9, (byte)0xE8, (byte)0x1D, (byte)0x7B, (byte)0x2C,
                        (byte)0x78, (byte)0xFA, (byte)0xB8, (byte)0x02, (byte)0x55, (byte)0x80,
                        (byte)0x9B, (byte)0xC2, (byte)0xA5, (byte)0xCB,
                };

            Console.WriteLine("List of primes < 2000\n---------------------");
            int limit = 100, count = 0;
            for (int i = 0; i < 2000; i++)
            {
                if (i >= limit)
                {
                    Console.WriteLine();
                    limit += 100;
                }

                BigInteger p = new BigInteger(-i);
                if (p.isProbablePrime())
                {
                    Console.Write(i + ", ");
                    count++;
                }
            }
            Console.WriteLine("\nCount = " + count);

            BigInteger bi1 = new BigInteger(pseudoPrime1);
            Console.WriteLine("\n\nPrimality testing for\n" + bi1.ToString() + "\n");
            Console.WriteLine("SolovayStrassenTest(5) = " + bi1.SolovayStrassenTest(5));
            Console.WriteLine("RabinMillerTest(5) = " + bi1.RabinMillerTest(5));
            Console.WriteLine("FermatLittleTest(5) = " + bi1.FermatLittleTest(5));
            Console.WriteLine("isProbablePrime() = " + bi1.isProbablePrime());

            Console.Write("\nGenerating 512-bits random pseudoprime. . .");
            Random rand = new Random();
            BigInteger prime = BigInteger.genPseudoPrime(512, 5, rand);
            Console.WriteLine("\n" + prime + "\n");

            //int dwStart = System.Environment.TickCount;
            //BigInteger.MulDivTest(100000);
            //BigInteger.RSATest(10);
            //BigInteger.RSATest2(10);
            //Console.WriteLine(System.Environment.TickCount - dwStart);

            Console.WriteLine("Complated...........\n\n");
            Console.ReadKey();
        }

    }
}
