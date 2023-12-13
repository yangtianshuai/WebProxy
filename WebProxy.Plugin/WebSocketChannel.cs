using System.Collections.Generic;
using System.Threading;

namespace WebProxy.Plugin
{
    public class WebSocketChannel
    {
        public delegate int WebSocketHandeler(SocketMessage sm);
        public static WebSocketHandeler WebSocket;
        private static bool running = true;
        private static object lockObj = new object();
        public static List<SocketMessage> queues = new List<SocketMessage>();

        public WebSocketChannel()
        {
            SendTimer();
        }
        ~WebSocketChannel()
        {
            running = false;
        }       

        public static void Send(string topic, string message)
        {     
            Send(new SocketMessage
            {
                topic = topic,
                message = message
            });
        }
        public static void Send(SocketMessage sm)
        {
            lock (lockObj)
            {
                queues.Add(sm);
            }            
        }

        private static void SendTimer()
        {
            var seconds = 200;
            var thread = new Thread(() =>
            {
                while (running)
                {
                    if(queues.Count > 0)
                    {
                        var sm = queues[0];
                        WebSocket?.Invoke(sm);
                        lock (lockObj)
                        {
                            queues.RemoveAt(0);
                        }
                    }                    
                    Thread.Sleep(seconds);
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }
    }

    public class SocketMessage
    {
        public string id { get; set; }
        public string topic { get; set; }
        public string message { get; set; }
    }
}
