using Fleck.Interfaces;
using System;
using System.Collections.Generic;

namespace Web_Proxy.WS
{
    public class SocketChannel
    {
        public SocketChannel()
        {
            CreateTime = DateTime.Now;
        }
        public SocketChannel(IWebSocketConnection connect) : this()
        {
            Connect = connect;
        }

        public DateTime CreateTime { get; set; }
        public IWebSocketConnection Connect { get; set; }

        public List<string> Topics { get; set; }

        public DateTime LastAccessTime { get; set; }
    }
}
