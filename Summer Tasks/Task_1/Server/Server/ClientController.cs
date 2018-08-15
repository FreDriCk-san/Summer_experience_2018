using System;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class ClientController
    {
        protected internal string Id { get; private set; }                                                              // personal ID (GUID)
        protected internal NetworkStream Stream { get; private set; }                                                   // main thread for network connection
        TcpClient client;                                                                                               // client connections
        ServerController server;                                                                                        // server controller


        // Constructor
        public ClientController(TcpClient tcpClient, ServerController serverController)
        {
            Id = Guid.NewGuid().ToString();
            client = tcpClient;
            server = serverController;
            serverController.AddConnection(this);
        }


        // User processing
        public void Process()
        {
            try
            {
                Stream = client.GetStream();
                // receiving user's Id
                string message = GetMessage();
                message = Id + " joined the chat.";

                // send message to server about our presence
                server.BroadcastMessage("\n//------------NEW USER ONLINE--------------//\n", this.Id);
                server.BroadcastMessage(message, this.Id);
                server.BroadcastMessage("\n//-------------------------------//\n", this.Id);

                // show message on server console
                Console.WriteLine("\n//------------NEW USER ONLINE--------------//");
                Console.WriteLine(message);
                Console.WriteLine("//-------------------------------//\n");

                // receiving messages from users
                while (true)
                {
                    try
                    {
                        message = GetMessage();

                        if (message.Length == 0)
                        {
                            // if '/disconnect' was typed (disconnect current user from chat)
                            Leave(this.Id);
                            break;
                        }

                        // show message on console
                        message = String.Format("{0}: {1} (ONLINE: {2} users)", Id,
                            message, server.OnlineClients().ToString());
                        Console.WriteLine(message);
                        server.BroadcastMessage(message, this.Id);

                    }
                    catch
                    {
                        Leave(this.Id);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                // in case of user's disconnection remove all resources
                server.RemoveConnection(this.Id);
                Close();
            }
        }


        // Reading incoming message and converts it to string
        private string GetMessage()
        {
            byte[] data = new byte[64];                                                                                 // buffer for incoming data
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                // encode incoming stream of bytes into the String, and add it to StringBuilder
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (Stream.DataAvailable);

            return builder.ToString();
        }


        // User has left the chat
        private void Leave(string id)
        {
            string message = String.Format("{0}: left the chat", id);
            Console.WriteLine(message);
            server.BroadcastMessage(message, id);
        }


        // Closing all the connections
        protected internal void Close()
        {
            if (Stream != null)
                // stream tripping
                Stream.Close();

            if (client != null)
                // client tripping
                client.Close();
        }
    }
}
