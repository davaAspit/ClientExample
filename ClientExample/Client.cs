using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientExample
{
    public class Client : IDisposable
    {
        /// <summary>
        /// Used to store the server connection
        /// </summary>
        public TcpClient ServerConnection { get; private set; }

        public string Hostname { get; }
        public int Port { get; }


        /// <summary>
        /// Initializes the server connection with the the hostname and port
        /// </summary>
        /// <param name="hostname">Hostname of the server. Use localhost if internal server</param>
        /// <param name="port">The port of the server</param>
        public Client(string hostname, int port)
        {
            Hostname = hostname;
            Port = port;
        }

        /// <summary>
        /// Used to send a request to the server and receive the response.
        /// </summary>
        /// <param name="request">The request to the server</param>
        /// <returns>The response from the server (if everything went good). Otherwise an error message</returns>
        public string SendAndReceive(string request)
        {
            string response = "";
            try
            {
                using(ServerConnection = new TcpClient(Hostname, Port))
                using (NetworkStream ns = ServerConnection.GetStream())
                using (StreamWriter writer = new StreamWriter(ns))
                using (StreamReader reader = new StreamReader(ns))
                {
                    writer.AutoFlush = true;

                    writer.WriteLine(request);

                    response = reader.ReadLine();
                }
            }
            catch (IOException ex)
            {
                response = $"Fejl: {ex.Message}";
            }
            return response;
        }

        public void Dispose()
        {
            ServerConnection.Dispose();
        }
    }
}
