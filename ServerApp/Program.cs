using Server.ItSelf;
using  System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServerHost server = new(new StaticFileHandler(Path.Combine(Environment.CurrentDirectory, "www")));
            server.Start();
        }
    }
}