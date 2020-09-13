using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Buffer
    {
        public int Length;
        public byte[] Data;

        internal Buffer(byte[] data)
        {
            if(data != null)
            {
                this.Length = data.Length;
                this.Data = data;
            }
            else
            {
                this.Length = 0;
                this.Data = null;
            }
        }
    }
}
