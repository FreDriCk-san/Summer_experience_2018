using System;
using System.Threading;

namespace Server
{
    class ServerConsole
    {
        static ServerController server;                                                                                 // server
        static Thread listenThread;                                                                                     // listening thread

        static void Main(string[] args)
        {
            int port = 0;

            try
            {
                // enter server port
                Console.WriteLine("Enter server port: ");
                port = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }

            try
            {
                // server start
                server = new ServerController(port);
                listenThread = new Thread(new ThreadStart(server.Listen));
                listenThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                server.Disconnect(); 
            }
        }
    }
}
