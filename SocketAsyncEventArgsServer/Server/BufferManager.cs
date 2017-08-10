namespace SocketAsyncEventArgsServer
{
    using System.Collections.Generic;
    using System.Net.Sockets;

    internal class BufferManager
    {
        byte[] m_buffer; // the underlying byte array maintained by the Buffer Manager

        int m_bufferSize;

        int m_currentIndex;
        private Stack<int> m_freeIndexPool;
        int m_numBytes; // the total number of bytes controlled by the buffer pool

        public BufferManager(int totalBytes, int bufferSize)
        {
            this.m_numBytes = totalBytes;
            this.m_currentIndex = 0;
            this.m_bufferSize = bufferSize;
            this.m_freeIndexPool = new Stack<int>();
        }

        // Removes the buffer from a SocketAsyncEventArg object.  
        // This frees the buffer back to the buffer pool
        public void FreeBuffer(SocketAsyncEventArgs args)
        {
            this.m_freeIndexPool.Push(args.Offset);
            args.SetBuffer(null, 0, 0);
        }

        // Allocates buffer space used by the buffer pool
        public void InitBuffer()
        {
            // create one big large buffer and divide that 
            // out to each SocketAsyncEventArg object
            this.m_buffer = new byte[this.m_numBytes];
        }

        // Assigns a buffer from the buffer pool to the 
        // specified SocketAsyncEventArgs object
        // <returns>true if the buffer was successfully set, else false</returns>
        public bool SetBuffer(SocketAsyncEventArgs args)
        {
            if (this.m_freeIndexPool.Count > 0)
            {
                args.SetBuffer(this.m_buffer, this.m_freeIndexPool.Pop(), this.m_bufferSize);
            }
            else
            {
                if ((this.m_numBytes - this.m_bufferSize) < this.m_currentIndex)
                {
                    return false;
                }

                args.SetBuffer(this.m_buffer, this.m_currentIndex, this.m_bufferSize);
                this.m_currentIndex += this.m_bufferSize;
            }

            return true;
        }
    }
}