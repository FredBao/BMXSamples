using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace FanucToolManageDll
{
    using System.Diagnostics;
    using System.IO;

    public class FanucToolManageDll
    {
        [DllImport("FanucToolMonitorCAPI.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr DoConnect(string ipaddress, int port, int timeout);

        [DllImport("FanucToolMonitorCAPI.dll", CharSet = CharSet.Unicode)]
        private static extern void Disconnect(IntPtr handle);

        [DllImport("FanucToolMonitorCAPI.dll", CharSet = CharSet.Unicode)]
        private static extern bool GetCustomMacro(IntPtr handle, int MacroNo, out double dMacroValue);

        [DllImport("FanucToolMonitorCAPI.dll", CharSet = CharSet.Unicode)]
        private static extern bool GetToolOffsetInfo(IntPtr handle, int iTNo, out int GeometryL, out int GeometryR, out int WearL, out int WearR);

        [DllImport("FanucToolMonitorCAPI.dll", CharSet = CharSet.Unicode)]
        private static extern bool WriteToolOffsetInfo(IntPtr hConnection, int ToolNo, int iType, int lOfsVal);

        [DllImport("FanucToolMonitorCAPI.dll", CharSet = CharSet.Unicode)]
        private static extern bool GetAllToolOffsetInfo(IntPtr hConnection, int type, int[] pToolOffset, int iArylen);

        [DllImport("FanucToolMonitorCAPI.dll", CharSet = CharSet.Unicode)]
        private static extern bool WriteAllToolOffsetInfo(IntPtr hConnection, int type, int[] pToolOffset, int iArylen);

        [DllImport("FanucToolMonitorCAPI.dll", CharSet = CharSet.Unicode)]
        private static extern bool SetPath(IntPtr hConnection, int iPath);

        [DllImport("FanucToolMonitorCAPI.dll", CharSet = CharSet.Unicode)]
        private static extern bool GetToolOffsetInfo_T(IntPtr hConnection, int ToolNo, int iType, out int lOfsVal);

        [DllImport("FanucToolMonitorCAPI.dll", CharSet = CharSet.Unicode)]
        private static extern bool WriteToolOffsetInfo_T(IntPtr hConnection, int ToolNo, int iType, int lOfsVal);

        [DllImport("FanucToolMonitorCAPI.dll", CharSet = CharSet.Unicode)]
        private static extern bool GetAllToolOffsetInfo_T(IntPtr hConnection, int type, int[] pToolOffset, int iArylen);

        [DllImport("FanucToolMonitorCAPI.dll", CharSet = CharSet.Unicode)]
        private static extern bool WriteAllToolOffsetInfo_T(IntPtr hConnection, int type, int[] pToolOffset, int iArylen);
        //
        [DllImport("FanucToolMonitorCAPI.dll", CharSet = CharSet.Unicode)]
        private static extern bool GetDeviceParameter(IntPtr hConnection, int nParameter, out uint pBuffer);

        [DllImport("FanucToolMonitorCAPI.dll", CharSet = CharSet.Unicode)]
        private static extern bool SetDeviceParameter(IntPtr hConnection, int nParameter, ref uint pBuffer);

        [DllImport("FanucToolMonitorCAPI.dll", CharSet = CharSet.Unicode)]
        private static extern bool GetToolLife(IntPtr hConnection, int ToolNo, out int pToolLife);

        [DllImport("FanucToolMonitorCAPI.dll", CharSet = CharSet.Unicode)]
        private static extern bool SetToolLifeValue(IntPtr hConnection, int TGrpNo, int Tlife, int TCnt, int type);

        [DllImport("FanucToolMonitorCAPI.dll", CharSet = CharSet.Unicode)]
        private static extern bool GetToolCnt(IntPtr hConnection, int ToolNo, out int pToolCnt);

        private IntPtr m_connection;

        private int m_ToolCount;

        public FanucToolManageDll(int iToolCount)
        {
            m_connection = IntPtr.Add(IntPtr.Zero, 100);
            m_ToolCount = iToolCount;
        }

        public bool Connect(string ipaddress, int port, int timeout)
        {
            var filePath = "D:\\FanucToolManageDll.txt";

            var connected = false;
            try
            {
                File.AppendAllLines(filePath, new[] { $"Execute Connect {m_connection}" });
                m_connection = DoConnect(ipaddress, port, timeout);
                connected = m_connection != IntPtr.Zero;
                File.AppendAllLines(filePath, new[] { $"Execute Connect Result: {ipaddress}:{port}  m_connection {m_connection} != IntPtr.Zero = {connected}" });
            }
            catch (Exception e)
            {
                File.AppendAllText(filePath, $"{e.Message.ToString()}");
            }

            return connected;
        }

        public void Disconnect()
        {
            if (m_connection != IntPtr.Zero)
                Disconnect(m_connection);
            m_connection = IntPtr.Zero;
        }

        public bool CheckConnectionState()
        {
            if (m_connection != IntPtr.Zero)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GetAllToolOffsetInfo(int[] pToolOffset, int iArylen)
        {

            if (CheckConnectionState())
            {
                return GetAllToolOffsetInfo(m_connection, -3, pToolOffset, iArylen);
            }
            else
                return false;

        }

        public bool WriteToolOffsetInfo(int ToolNo, int iType, int lOfsVal)
        {
            if (CheckConnectionState())
            {
                return WriteToolOffsetInfo(m_connection, ToolNo, iType, lOfsVal);
            }
            else
                return false;

        }

        public bool WriteAllToolOffsetInfo(int[] pToolOffset, int iArylen)
        {
            if (CheckConnectionState())
            {
                return WriteAllToolOffsetInfo(m_connection, -3, pToolOffset, iArylen);
            }
            else
                return false;

        }

        public bool GetAllToolOffsetInfo_T(int[] pToolOffset, int iArylen)
        {

            if (CheckConnectionState())
            {
                return GetAllToolOffsetInfo_T(m_connection, -2, pToolOffset, iArylen);
            }
            else
                return false;

        }

        public bool GetToolOffsetInfo_T(int ToolNo, int iType, out int lOfsVal)
        {
            lOfsVal = 0;
            if (CheckConnectionState())
            {
                return GetToolOffsetInfo_T(m_connection, ToolNo, iType, out lOfsVal);
            }
            else
                return false;
        }

        public bool WriteToolOffsetInfo_T(int ToolNo, int iType, int lOfsVal)
        {
            if (CheckConnectionState())
            {
                return WriteToolOffsetInfo_T(m_connection, ToolNo, iType, lOfsVal);
            }
            else
                return false;

        }

        public bool WriteAllToolOffsetInfo_T(int[] pToolOffset, int iArylen)
        {
            if (CheckConnectionState())
            {
                return WriteAllToolOffsetInfo_T(m_connection, -2, pToolOffset, iArylen);
            }
            else
                return false;

        }

        public bool SetPath(int iPath)
        {
            if (CheckConnectionState())
            {
                return SetPath(m_connection, iPath);
            }
            else
                return false;
        }

        public struct TOOL_OFFSET
        {
            public int nToolNum;
            public int nX;
            public int nY;
            public int nR;
            public int nT;
        }

        public struct TOOL_LIFE
        {
            public int nToolNum;
            public int nLife;
            public int nUsed;
        }

        public bool GetAllToolOffsetInfo_T(TOOL_OFFSET[] pToolOffset)
        {
            int[] pToolOffsetA = new int[4 * m_ToolCount];
            bool bRet = false;
            bRet = GetAllToolOffsetInfo_T(pToolOffsetA, 400);

            if (!bRet)
                return false;

            for (int i = 0; i < (m_ToolCount > pToolOffset.Length ? pToolOffset.Length : m_ToolCount); i++)
            {
                pToolOffset[i].nToolNum = i + 1;
                pToolOffset[i].nX = pToolOffsetA[i * 4 + 0];
                pToolOffset[i].nY = pToolOffsetA[i * 4 + 1];
                pToolOffset[i].nR = pToolOffsetA[i * 4 + 2];
                pToolOffset[i].nT = pToolOffsetA[i * 4 + 3];
            }

            return true;
        }

        public bool GetAllToolLifeInfo_T(TOOL_LIFE[] pToolLife, int iToolNoStart, int iToolNoEnd)
        {
            if (iToolNoStart > iToolNoEnd)
                return false;
            if (iToolNoEnd - iToolNoStart + 1 > pToolLife.Length)
                return false;
            for (int i = iToolNoStart, j = 0; i <= (iToolNoEnd > m_ToolCount ? m_ToolCount : iToolNoEnd); i++, j++)
            {
                pToolLife[j].nToolNum = i;
                GetToolLife(m_connection, i, out pToolLife[j].nLife);
                GetToolCnt(m_connection, i, out pToolLife[j].nUsed);
            }

            return true;
        }

        public bool GetThresholdValue(int nParameter, out uint nVal)
        {
            return GetDeviceParameter(m_connection, nParameter, out nVal);
        }

    }
}
