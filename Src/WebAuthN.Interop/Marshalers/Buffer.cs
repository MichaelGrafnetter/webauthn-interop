using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Buffer
    {
        public int Length;
        public byte[] Data;

        public Buffer(byte[] data)
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
