using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Client
{
    class ClientConsole
    {                                                      
        static TcpClient client;                                                                                        // client connections
        static NetworkStream stream;                                                                                    // main thread for network connection


        static void Main(string[] args)
        {
            string host = null;                                                                                         // server IPV4 address
            int port = 0;                                                                                               // server port

            try
            {
                Console.WriteLine("Enter server IPV4 address: ");
                host = Console.ReadLine();

                Console.WriteLine("Enter server port: ");
                port = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }

            Console.WriteLine("\nWelcome!");
            client = new TcpClient();
            try
            {
                // user connection
                client.Connect(host, port);
                stream = client.GetStream();

                Console.WriteLine("HINT:  Type '/disconnect' if you want to disconnect from chat.");
                byte[] data = Encoding.Unicode.GetBytes("connect");
                stream.Write(data, 0, data.Length);

                // strat new thread for receiving data
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start();
                SendMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            finally
            {
                Disconnect();
            }
        }


        // Send message
        static void SendMessage()
        {
            Console.WriteLine("Enter your message: ");

            while (true)
            {
                // read and transform message into array of bytes
                string message = Console.ReadLine();
                if (message.Length == 0 || null == message)
                {
                    // there is no reason to send empty message
                    continue;
                }
                else if (message == "/disconnect" && message.Length <= 11)
                {
                    Disconnect();
                }
                else
                {
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                }
            }
        }


        // Receive message
        static void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64];                                                                         // buffer for incoming data
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        // encode incoming stream of bytes into the String, and add it to StringBuilder
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    // show message on console
                    Console.WriteLine(message);
                }
                // if connection was interrupted
                catch
                {
                    Console.WriteLine("Connection was interrupted!");
                    Console.ReadLine();
                    Disconnect();
                }
            }
        }


        // Disconnect from server
        static void Disconnect()
        {
            if (stream != null)
                // stream tripping
                stream.Close();

            if (client != null)
                // client tripping
                client.Close();

            Environment.Exit(0);
        }
    }
}
