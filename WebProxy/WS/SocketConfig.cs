namespace Web_Proxy.WS
{
    public class SocketConfig
    {
        public readonly static string HeatBeat = "HeartBeat";
        public readonly static string SetTopic = "SetTopic";

        public static int Port { get; set; } = 8655;

        public static int HeatBeatSecond = 600000;
    }
}
