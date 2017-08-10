namespace SocketAsyncEventArgsServer
{
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;

    internal class SocketAsyncEventArgsPool
    {
        Stack<SocketAsyncEventArgs> m_pool;

        // Initializes the object pool to the specified size
        // The "capacity" parameter is the maximum number of 
        // SocketAsyncEventArgs objects the pool can hold
        public SocketAsyncEventArgsPool(int capacity)
        {
            this.m_pool = new Stack<SocketAsyncEventArgs>(capacity);
        }

        // The number of SocketAsyncEventArgs instances in the pool
        public int Count
        {
            get
            {
                return this.m_pool.Count;
            }
        }

        // Removes a SocketAsyncEventArgs instance from the pool
        // and returns the object removed from the pool
        public SocketAsyncEventArgs Pop()
        {
            lock (this.m_pool)
            {
                return this.m_pool.Pop();
            }
        }

        // Add a SocketAsyncEventArg instance to the pool
        // The "item" parameter is the SocketAsyncEventArgs instance 
        // to add to the pool
        public void Push(SocketAsyncEventArgs item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Items added to a SocketAsyncEventArgsPool cannot be null");
            }

            lock (this.m_pool)
            {
                this.m_pool.Push(item);
            }
        }
    }
}