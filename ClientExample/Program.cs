using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //This is possible because Client is IDisposable
            using (Client client = new Client("localhost", 9999))
            {

                //Take input from console and send
                //If input is Exit - stop it
                while (true)
                {
                    Console.WriteLine("Skriv din forespørgsel til serveren. Skriv exit for at stoppe programmet.");
                    string request = Console.ReadLine();
                    if (request.ToLower() == "exit")
                        break;
                    client.SendLine(request);

                    if (client.ReceiveLine(out string response))
                    {
                        Console.WriteLine("Svar fra server: " + response);
                    }
                }
            }

        }
    }
}
