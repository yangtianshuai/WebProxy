using Fleck;
using Proxy.Common;

namespace Web_Proxy.WS
{
    public class WebSocket
    {
        private static WebSocket webSocket;
        private WebSocketServer _server;
        private WebSocket()
        {

        }

        public static WebSocket Current
        {
            get
            {
                if (webSocket == null)
                {
                    webSocket = new WebSocket();
                }
                return webSocket;
            }
        }

        public void Start(int? port = null)
        {
            if (port != null)
            {
                SocketConfig.Port = port.Value;
            }

            while (SystemHelper.PortInUse(SocketConfig.Port))
            {
                SocketConfig.Port++;
            }

            if (_server == null)
            {
                _server = new WebSocketServer("ws://localhost:" + SocketConfig.Port);
            }
            _server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    SocketManager.Add(socket);
                };
                socket.OnClose = () =>
                {
                    SocketManager.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    SocketManager.Receive(socket, message);
                };
            });
        }
    }
}
