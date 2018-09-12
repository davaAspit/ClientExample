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
        /// Used to send a request to the server.
        /// </summary>
        /// <param name="request">The request to the server</param>
        /// <returns>A boolean defining whether it went good or bad</returns>
        public bool SendLine(string request)
        {
            try
            {
                using(ServerConnection = new TcpClient(Hostname, Port))
                using (NetworkStream ns = ServerConnection.GetStream())
                using (StreamWriter writer = new StreamWriter(ns))
                {
                    writer.AutoFlush = true;

                    writer.WriteLine(request);
                }
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Reads a line from the server.
        /// </summary>
        /// <param name="response">The response from the server</param>
        /// <returns>A boolean defining whether it went good or bad</returns>
        public bool ReceiveLine(out string response)
        {
            response = "";
            try
            {
                using (ServerConnection = new TcpClient(Hostname, Port))
                using (NetworkStream ns = ServerConnection.GetStream())
                using (StreamReader reader = new StreamReader(ns))
                {
                    response = reader.ReadLine();
                }
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            ServerConnection.Dispose();
        }
    }
}
