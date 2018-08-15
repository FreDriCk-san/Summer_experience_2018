using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class ServerController
    {
        static TcpListener tcpListener;                                                                                 // server for listening
        List<ClientController> clients = new List<ClientController>();                                                  // all the connections
        private readonly int port;

        
        //Constructor
        public ServerController(int port)
        {
            this.port = port;
        }


        // Add connection to our List
        protected internal void AddConnection(ClientController clientObject)
        {
            try
            {
                clients.Add(clientObject);
            }
            catch (ArgumentOutOfRangeException rangeExp)
            {
                Console.WriteLine(rangeExp.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // See all online users
        protected internal int OnlineClients()
        {
            return clients.Count;
        }


        // Remove connection
        protected internal void RemoveConnection(string id)
        {
            // receive by Id closed connection
            ClientController client = clients.FirstOrDefault(c => c.Id == id);
            // remove it from connection List
            if (client != null)
                clients.Remove(client);
        }


        // Listen for incoming connections
        protected internal void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, port);
                // start listening
                tcpListener.Start();
                Console.WriteLine("The server was started. Please, wait for connections...");

                while (true)
                {
                    // set limit of connected users (current: 10)
                    if (clients.Count < 10)
                    {
                        TcpClient tcpClient = tcpListener.AcceptTcpClient();

                        ClientController clientController = new ClientController(tcpClient, this);
                        Thread clientThread = new Thread(new ThreadStart(clientController.Process));
                        // start thread for interaction with user
                        clientThread.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }


        // Broadcast messages to connected users
        protected internal void BroadcastMessage(string message, string id)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            for (int i = 0; i < clients.Count; i++)
            {
                // if user Id is not the same as sender's Id
                if (clients[i].Id != id)
                {
                    // data transfering
                    clients[i].Stream.Write(data, 0, data.Length);
                }
            }
        }


        // Disconnection of all users
        protected internal void Disconnect()
        {
            // server stoppage
            tcpListener.Stop();

            for (int i = 0; i < clients.Count; i++)
            {
                // disconnect all users
                clients[i].Close();
            }

            Environment.Exit(0);
        }
    }
}
