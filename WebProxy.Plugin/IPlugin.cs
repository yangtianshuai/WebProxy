namespace WebProxy.Plugin
{
    public interface IPlugin
    {
        /// <summary>
        /// 启动插件
        /// </summary>
        /// <returns></returns>
        bool Start();
        /// <summary>
        /// 结束插件
        /// </summary>
        /// <returns></returns>
        bool End();
    }
}