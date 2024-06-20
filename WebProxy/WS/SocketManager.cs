using Fleck.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using WebProxy.Plugin;

namespace Web_Proxy.WS
{
    public class SocketManager
    {
        private static Dictionary<string, SocketChannel> connects = new Dictionary<string, SocketChannel>();
        private bool running = true;

        public SocketManager()
        {
            WebSocketChannel.WebSocket += new WebSocketChannel.WebSocketHandeler(Send);
            TestClose();
        }
        ~SocketManager()
        {
            running = false;
        }

        public static void Add(IWebSocketConnection connect)
        {
            var id = connect.ConnectionInfo.Id.ToString("N").ToUpper();
            if (!connects.ContainsKey(id))
            {
                connects.Add(id, new SocketChannel(connect));
            }            
        }

        public static void Remove(IWebSocketConnection connect)
        {
            var id = connect.ConnectionInfo.Id.ToString("N").ToUpper();
            if (!connects.ContainsKey(id))
            {
                connects.Remove(id);
            }
        }

        public static int Send(SocketMessage sm)
        {
            return Send(sm.topic, JsonConvert.SerializeObject(sm));
        }
        private static int Send(string topic, string message)
        {
            var count = 0;
            foreach (var connect in connects)
            {
                if (connect.Value.Topics.Contains(topic))
                {
                    count++;
                    connect.Value.Connect.Send(message);
                }
            }
            return count;
        }



        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="message"></param>
        public static void Receive(IWebSocketConnection connect, string message)
        {
            var id = connect.ConnectionInfo.Id.ToString("N").ToUpper();
            if (!connects.ContainsKey(id))
            {
                connects[id].LastAccessTime = DateTime.Now;              
                var sm = JsonConvert.DeserializeObject<SocketMessage>(message);
                var topic = sm.topic.ToLower();
                if (topic == SocketConfig.HeatBeat.ToLower())
                {
                    return;
                }
                else if (topic == SocketConfig.SetTopic.ToLower())
                {
                    //消息全部为话题
                    if (!connects[id].Topics.Contains(message))
                    {
                        connects[id].Topics.Add(message);
                    }
                    return;
                }                
            }
        }
               

        private void TestClose()
        {
            var seconds = SocketConfig.HeatBeatSecond;
            var thread = new Thread(() =>
            {
                while (running)
                {
                    var list = new List<string>(connects.Keys);
                    foreach(var key in list)
                    {
                        TimeSpan span = DateTime.Now - connects[key].LastAccessTime;
                        if (span.TotalSeconds > seconds)
                        {
                            connects[key].Connect.Close();
                            connects.Remove(key);
                        }
                    }
                    Thread.Sleep(seconds);
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }

    }
}
