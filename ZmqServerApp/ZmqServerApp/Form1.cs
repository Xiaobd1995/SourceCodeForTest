using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using ZmqServerCSharp;

namespace ZmqServerApp
{
    public partial class Form1 : Form
    {
        /* User define macro */
        int RLT_OK = 0;
        int RLT_ERROR = 0;
        int RLT_FAILED = 0;
        int RLT_TIMEROUT = 0;
        /* User define macro */

        /* global variable*/

        Thread myThread;
        IntPtr socket_req;
        int timer_out = 100; //ms
        bool send_flag = false;
        string default_port_str = "40001";
        /* global variable*/
        public Form1()
        {
            InitializeComponent();
            StartZmqServer();
        }

        public int StartZmqServer()
        {
            int rlt_code = RLT_OK;
            rlt_code = ZmqServerInit();
            if (rlt_code == RLT_OK)
            {
                ZmqServerConnect();
            }
            return rlt_code;
        }

        int ZmqServerInit()
        {
            int rlt_code = RLT_OK; 
            myThread = new Thread(new ThreadStart(threadOut));
            return rlt_code;
        } 

        int ZmqServerConnect()
        {
            int rlt_code = RLT_OK; 
            IntPtr context_req = ZmqMng.libzmq.zmq_ctx_new();
            socket_req = ZmqMng.libzmq.zmq_socket(context_req, (int)ZmqMng.Socket_Types.ZMQ_PUB);    //发布订阅模式
            //socket_req = ZmqMng.libzmq.zmq_socket(context_req, (int)ZmqMng.Socket_Types.ZMQ_REP);  //请求-应答模式

            int result = ZmqMng.libzmq.zmq_bind(socket_req, "tcp://*:" + default_port_str);
            if (result == 0)
            {
                Console.WriteLine("Zmq server connect success!!");
            }
            send_flag = true;
            myThread.Start();
            return rlt_code;
        }

        public void threadOut()
        {
            while (send_flag)
            {
                string send_mess = send_info.Text;
                if (socket_req != null)
                { 
                    send_cmd(send_mess, socket_req);
                }
                Thread.Sleep(timer_out);
            }
        }
         
        public unsafe static void send_cmd(string str, IntPtr sockePtr)
        {
            ZmqMng.zmq_msg_t* msg1 = stackalloc ZmqMng.zmq_msg_t[1];
            char[] ptr = str.ToCharArray();
            int result = ZmqMng.libzmq.zmq_send(sockePtr, ptr, ptr.Length);
        }
         
        public unsafe static void recv_cmd(IntPtr sockePtr)
        {
            char[] ptr = new char[100];
            int i = ZmqMng.libzmq.zmq_recv(sockePtr, ptr, 100);
            Console.WriteLine("qwe");
        }

        private void close_Click(object sender, EventArgs e)
        {
            if (socket_req != null)
            {
                ZmqMng.libzmq.zmq_close(socket_req); 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ZmqServerConnect();
        }
    }
}
