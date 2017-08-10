namespace SocketAsyncEventArgsServer
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class SocketHelper
    {
        private static bool _isBigEndian;

        private static bool _isEndianChecked = false;

        public static object GetValueByType(DataItem item)
        {
            object result = null;
            switch (item.Type & 0x0F)
            {
                case 0:
                    if (item.Id == 2)
                    {
                        // 报警内容中文字段用GB2312编码
                        result = Encoding.GetEncoding("GB2312").GetString(item.Value).TrimEnd();
                    }
                    else
                    {
                        result = Encoding.ASCII.GetString(item.Value).TrimEnd();
                    }

                    break;
                case 1:
                    if (item.Value.Length < 8)
                    {
                        throw new Exception("尝试转换为double数据失败，长的为" + item.Value.Length);
                    }

                    result = BitConverter.ToDouble(item.Value, 0);
                    break;
                case 2:
                    if (item.Value.Length < 4)
                    {
                        throw new Exception("尝试转换为int数据失败，长的为" + item.Value.Length);
                    }

                    result = NetworkToHost(BitConverter.ToInt32(item.Value, 0));
                    break;
                case 3:
                    result = item.Value[0];
                    break;
                case 4:
                    result = item.Value[0];
                    break;
                case 5:
                    result = item.Value;
                    break;
                default:
                    result = BitConverter.ToString(item.Value);
                    break;
            }
            return result;
        }

        internal static string BinToHexString(byte[] buffer)
        {
            const string M = "0123456789ABCDEF";
            var sb = new StringBuilder(64);
            foreach (var c in buffer)
            {
                sb.Append(M[(c & 0xF0) >> 4]);
                sb.Append(M[c & 0x0F]);
            }

            return sb.ToString();
        }

        internal static T BytesToStruct<T>(byte[] bytes)
            where T : struct
        {
            T str = new T();

            int size = Marshal.SizeOf(str);
            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.Copy(bytes, 0, ptr, size);

            str = (T)Marshal.PtrToStructure(ptr, str.GetType());
            Marshal.FreeHGlobal(ptr);

            return str;
        }

        internal static uint HostToNetwork(uint i)
        {
            if (IsBigEndian()) return i;
            return (i & 0xff) << 24 | (i & 0xff00) << 8 | (i >> 8) & 0xff00 | (i >> 24) & 0xff;
        }

        internal static ushort HostToNetwork(ushort i)
        {
            if (IsBigEndian()) return i;
            return (ushort)((((i & 0xff) << 8) | ((i & 0xff00) >> 8)) & 0x0000FFFF);
        }

        internal static int HostToNetwork(int i)
        {
            return (int)NetworkToHost((uint)i);
        }

        internal static short HostToNetwork(short i)
        {
            return (short)NetworkToHost((ushort)i);
        }

        internal static uint NetworkToHost(uint i)
        {
            return HostToNetwork(i);
        }

        internal static ushort NetworkToHost(ushort i)
        {
            return HostToNetwork(i);
        }

        internal static int NetworkToHost(int i)
        {
            return HostToNetwork(i);
        }

        internal static short NetworkToHost(short i)
        {
            return HostToNetwork(i);
        }

        internal static byte[] StringToByteArray(string str, int length)
        {
            return Encoding.ASCII.GetBytes(str.PadRight(length, ' '));
        }

        internal static byte[] StructToBytes(object structObj)
        {
            int size = Marshal.SizeOf(structObj);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(structObj, buffer, false);
                byte[] bytes = new byte[size];
                Marshal.Copy(buffer, bytes, 0, size);
                return bytes;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        private static bool IsBigEndian()
        {
            if (!_isEndianChecked)
            {
                _isEndianChecked = true;
                int nCheck = 0x01aa;
                _isBigEndian = (nCheck & 0xff) == 0x01;
            }

            return _isBigEndian;
        }
    }
}