using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace Gean.Hardware.SerialCommunication
{
    /// <summary>
    /// The Win32 DCB structure is implemented below in a C# class.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class DCB
    {

        private UInt32 DCBlength;
        public UInt32 BaudRate;
        BitVector32 Control;
        internal UInt16 wReserved;
        public UInt16 XonLim;
        public UInt16 XoffLim;
        public byte ByteSize;
        public byte Parity;
        public byte StopBits;
        public sbyte XonChar;
        public sbyte XoffChar;
        public sbyte ErrorChar;
        public sbyte EofChar;
        public sbyte EvtChar;
        internal UInt16 wReserved1;
        private readonly BitVector32.Section sect1;
        private readonly BitVector32.Section DTRsect;
        private readonly BitVector32.Section sect2;
        private readonly BitVector32.Section RTSsect;

        public DCB()
        {
            //
            // Initialize the length of the structure. Marshal.SizeOf returns
            // the size of the unmanaged object (basically the object that
            // gets marshalled).
            //
            this.DCBlength = (uint)Marshal.SizeOf(this);
            // initialize BitVector32
            Control = new BitVector32(0);
            // of the following 4 sections only 2 are needed
            sect1 = BitVector32.CreateSection(0x0f);
            // this is where the DTR setting is stored
            DTRsect = BitVector32.CreateSection(3, sect1);
            sect2 = BitVector32.CreateSection(0x3f, DTRsect);
            // this is where the RTS setting is stored
            RTSsect = BitVector32.CreateSection(3, sect2);
        }

        /// <summary>
        /// We need to have to define reserved fields in the DCB class definition
        /// to preserve the size of the 
        /// underlying structure to match the Win32 structure when it is 
        /// marshaled. Use these fields to suppress compiler warnings.
        /// </summary>
        internal void _SuppressCompilerWarnings()
        {
            wReserved += 0;
            wReserved1 += 0;
        }

        /// <summary>
        /// Enumeration for fDtrControl bit field. 
        /// </summary>
        public enum DtrControlFlags
        {
            /// <summary>
            /// 
            /// </summary>
            Disable = 0,
            /// <summary>
            /// 
            /// </summary>
            Enable = 1,
            /// <summary>
            /// 
            /// </summary>
            Handshake = 2
        }

        /// <summary>
        /// Enumeration for fRtsControl bit field. 
        /// </summary>
        public enum RtsControlFlags
        {
            /// <summary>
            /// 
            /// </summary>
            Disable = 0,
            /// <summary>
            /// 
            /// </summary>
            Enable = 1,
            /// <summary>
            /// 
            /// </summary>
            Handshake = 2,
            /// <summary>
            /// 
            /// </summary>
            Toggle = 3
        }

        /// <summary>
        /// Helper constants for manipulating the bit fields.
        /// these are defined as an enum in order to preserve memory
        /// </summary>
        [Flags]
        enum ctrlBit
        {
            /// <summary>
            /// 
            /// </summary>
            fBinaryMask = 0x001,
            /// <summary>
            /// 
            /// </summary>
            fParityMask = 0x0002,
            /// <summary>
            /// 
            /// </summary>
            fOutxCtsFlowMask = 0x0004,
            /// <summary>
            /// 
            /// </summary>
            fOutxDsrFlowMask = 0x0008,
            /// <summary>
            /// 
            /// </summary>
            fDtrControlMask = 0x0030,
            /// <summary>
            /// 
            /// </summary>
            fDsrSensitivityMask = 0x0040,
            /// <summary>
            /// 
            /// </summary>
            fTXContinueOnXoffMask = 0x0080,
            /// <summary>
            /// 
            /// </summary>
            fOutXMask = 0x0100,
            /// <summary>
            /// 
            /// </summary>
            fInXMask = 0x0200,
            /// <summary>
            /// 
            /// </summary>
            fErrorCharMask = 0x0400,
            /// <summary>
            /// 
            /// </summary>
            fNullMask = 0x0800,
            /// <summary>
            /// 
            /// </summary>
            fRtsControlMask = 0x3000,
            /// <summary>
            /// 
            /// </summary>
            fAbortOnErrorMask = 0x4000
        }

        // get and set of bool works with the underlying BitVector32 by using a mask for each bit field we can let the compiler and JIT do the work

        public bool fBinary
        {
            get { return (Control[(int)ctrlBit.fBinaryMask]); }
            set { Control[(int)ctrlBit.fBinaryMask] = value; }
        }
        public bool fParity
        {
            get { return (Control[(int)ctrlBit.fParityMask]); }
            set { Control[(int)ctrlBit.fParityMask] = value; }
        }
        public bool fOutxCtsFlow
        {
            get { return (Control[(int)ctrlBit.fOutxCtsFlowMask]); }
            set { Control[(int)ctrlBit.fOutxCtsFlowMask] = value; }
        }
        public bool fOutxDsrFlow
        {
            get { return (Control[(int)ctrlBit.fOutxDsrFlowMask]); }
            set { Control[(int)ctrlBit.fOutxDsrFlowMask] = value; }
        }

        // we have to use a segment because the width of the underlying information is wider than just one bit

        public byte fDtrControl
        {
            get { return (byte)Control[DTRsect]; }
            set { Control[DTRsect] = (int)value; }
        }
        public bool fDsrSensitivity
        {
            get { return Control[(int)ctrlBit.fDsrSensitivityMask]; }
            set { Control[(int)ctrlBit.fDsrSensitivityMask] = value; }
        }
        public bool fTXContinueOnXoff
        {
            get { return Control[(int)ctrlBit.fTXContinueOnXoffMask]; }
            set { Control[(int)ctrlBit.fTXContinueOnXoffMask] = value; }
        }
        public bool fOutX
        {
            get { return Control[(int)ctrlBit.fOutXMask]; }
            set { Control[(int)ctrlBit.fOutXMask] = value; }
        }
        public bool fInX
        {
            get { return Control[(int)ctrlBit.fInXMask]; }
            set { Control[(int)ctrlBit.fInXMask] = value; }
        }
        public bool fErrorChar
        {
            get { return Control[(int)ctrlBit.fErrorCharMask]; }
            set { Control[(int)ctrlBit.fErrorCharMask] = value; }
        }
        public bool fNull
        {
            get { return Control[(int)ctrlBit.fNullMask]; }
            set { Control[(int)ctrlBit.fNullMask] = value; }
        }

        /// <summary>
        /// we have to use a segment because the width of the underlying information is wider than just one bit
        /// </summary>
        /// <value>The f RTS control.</value>
        public byte fRtsControl
        {
            get { return (byte)Control[RTSsect]; }
            set { Control[RTSsect] = (int)value; }
        }

        public bool fAbortOnError
        {
            get { return Control[(int)ctrlBit.fAbortOnErrorMask]; }
            set { Control[(int)ctrlBit.fAbortOnErrorMask] = value; }
        }

        /// <summary>
        /// Method to dump the DCB to take a look and help debug issues.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("DCB:\r\n");
            sb.AppendFormat(null, "  BaudRate:     {0}\r\n", BaudRate);
            sb.AppendFormat(null, "  Control:      0x{0:x}\r\n", Control.Data);
            sb.AppendFormat(null, "    fBinary:           {0}\r\n", fBinary);
            sb.AppendFormat(null, "    fParity:           {0}\r\n", fParity);
            sb.AppendFormat(null, "    fOutxCtsFlow:      {0}\r\n", fOutxCtsFlow);
            sb.AppendFormat(null, "    fOutxDsrFlow:      {0}\r\n", fOutxDsrFlow);
            sb.AppendFormat(null, "    fDtrControl:       {0}\r\n", fDtrControl);
            sb.AppendFormat(null, "    fDsrSensitivity:   {0}\r\n", fDsrSensitivity);
            sb.AppendFormat(null, "    fTXContinueOnXoff: {0}\r\n", fTXContinueOnXoff);
            sb.AppendFormat(null, "    fOutX:             {0}\r\n", fOutX);
            sb.AppendFormat(null, "    fInX:              {0}\r\n", fInX);
            sb.AppendFormat(null, "    fNull:             {0}\r\n", fNull);
            sb.AppendFormat(null, "    fRtsControl:       {0}\r\n", fRtsControl);
            sb.AppendFormat(null, "    fAbortOnError:     {0}\r\n", fAbortOnError);
            sb.AppendFormat(null, "  XonLim:       {0}\r\n", XonLim);
            sb.AppendFormat(null, "  XoffLim:      {0}\r\n", XoffLim);
            sb.AppendFormat(null, "  ByteSize:     {0}\r\n", ByteSize);
            sb.AppendFormat(null, "  Parity:       {0}\r\n", Parity);
            sb.AppendFormat(null, "  StopBits:     {0}\r\n", StopBits);
            sb.AppendFormat(null, "  XonChar:      {0}\r\n", XonChar);
            sb.AppendFormat(null, "  XoffChar:     {0}\r\n", XoffChar);
            sb.AppendFormat(null, "  ErrorChar:    {0}\r\n", ErrorChar);
            sb.AppendFormat(null, "  EofChar:      {0}\r\n", EofChar);
            sb.AppendFormat(null, "  EvtChar:      {0}\r\n", EvtChar);

            return sb.ToString();
        }
    }
}
