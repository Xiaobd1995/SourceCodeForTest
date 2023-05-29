using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ZmqServerCSharp
{
    class ZmqMng
    {
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct zmq_msg_t
        {
            public fixed ushort _[32];
        }

        public enum Socket_Types
        {
            ZMQ_PAIR = 0,
            ZMQ_PUB = 1,
            ZMQ_SUB = 2,
            ZMQ_REQ = 3,
            ZMQ_REP = 4,
            ZMQ_DEALER = 5,
            ZMQ_ROUTER = 6,
            ZMQ_PULL = 7,
            ZMQ_PUSH = 8,
            ZMQ_XPUB = 9,
            ZMQ_XSUB = 10,
        }

        public enum Deprecated_Aliases
        {
            ZMQ_XREQ = Socket_Types.ZMQ_DEALER,
            ZMQ_XREP = Socket_Types.ZMQ_ROUTER
        }

        public enum Socket_Options
        {
            ZMQ_AFFINITY = 4,
            ZMQ_IDENTITY = 5,
            ZMQ_SUBSCRIBE = 6,
            ZMQ_UNSUBSCRIBE = 7,
            ZMQ_RATE = 8,
            ZMQ_RECOVERY_IVL = 9,
            ZMQ_SNDBUF = 11,
            ZMQ_RCVBUF = 12,
            ZMQ_RCVMORE = 13,
            ZMQ_FD = 14,
            ZMQ_EVENTS = 15,
            ZMQ_TYPE = 16,
            ZMQ_LINGER = 17,
            ZMQ_RECONNECT_IVL = 18,
            ZMQ_BACKLOG = 19,
            ZMQ_RECONNECT_IVL_MAX = 21,
            ZMQ_MAXMSGSIZE = 22,
            ZMQ_SNDHWM = 23,
            ZMQ_RCVHWM = 24,
            ZMQ_MULTICAST_HOPS = 25,
            ZMQ_RCVTIMEO = 27,
            ZMQ_SNDTIMEO = 28,
            ZMQ_IPV4ONLY = 31,
            ZMQ_LAST_ENDPOINT = 32,
            ZMQ_ROUTER_MANDATORY = 33,
            ZMQ_TCP_KEEPALIVE = 34,
            ZMQ_TCP_KEEPALIVE_CNT = 35,
            ZMQ_TCP_KEEPALIVE_IDLE = 36,
            ZMQ_TCP_KEEPALIVE_INTVL = 37,
            ZMQ_TCP_ACCEPT_FILTER = 38,
            ZMQ_DELAY_ATTACH_ON_CONNECT = 39,
            ZMQ_XPUB_VERBOSE = 40,
        }

        public enum Message_Options
        {
            ZMQ_MORE = 1,
        }

        public enum Send_Recv_Options
        {
            ZMQ_DONTWAIT = 1,
            ZMQ_SNDMORE = 2,
        }

        public unsafe class libzmq
        {
            [DllImport("libzmq-v140-mt-gd-4_3_5.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr zmq_ctx_new();

            [DllImport("libzmq-v140-mt-gd-4_3_5.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr zmq_socket(IntPtr context, int type);

            [DllImport("libzmq-v140-mt-gd-4_3_5.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int zmq_send(IntPtr socket, char[] str, int length);

            [DllImport("libzmq-v140-mt-gd-4_3_5.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int zmq_recv(IntPtr socket, char[] str, int length);

            [DllImport("libzmq-v140-mt-gd-4_3_5.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int zmq_close(IntPtr socket);

            [DllImport("libzmq-v140-mt-gd-4_3_5.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int zmq_bind(IntPtr socket, string addr);

            [DllImport("libzmq-v140-mt-gd-4_3_5.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int zmq_connect(IntPtr socket, string addr);

            [DllImport("libzmq-v140-mt-gd-4_3_5.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int zmq_msg_send(zmq_msg_t* msg, IntPtr socket, int flag);

            [DllImport("libzmq-v140-mt-gd-4_3_5.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int zmq_msg_recv(zmq_msg_t* msg, IntPtr socket, int flag);

            [DllImport("libzmq-v140-mt-gd-4_3_5.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int zmq_msg_init(zmq_msg_t* msg);

            [DllImport("libzmq-v140-mt-gd-4_3_5.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int zmq_msg_init_size(zmq_msg_t* msg, int size);

            [DllImport("libzmq-v140-mt-gd-4_3_5.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int zmq_setsockopt(void* socket, int option, void* optval, int optvallen);

            [DllImport("libzmq-v140-mt-gd-4_3_5.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void* zmq_msg_data(zmq_msg_t* msg);

            [DllImport("libzmq-v140-mt-gd-4_3_5.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int zmq_msg_close(zmq_msg_t* msg);
        }
    }
}
