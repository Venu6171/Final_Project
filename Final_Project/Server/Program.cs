using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public class SynchronousSocketListener
    {
        public static List<TcpClient> clients;
        public static Dictionary<string, string> clientsInfo = new Dictionary<string, string>();
        public static void Main(String[] args)
        {
            //StartListening();

            //StartListening();

            TcpListener listener = new TcpListener(IPAddress.Any, 8000);
            clients = new List<TcpClient>();
            listener.Start();
         
            while (true) // Add your exit flag here
            {
                TcpClient client;
                client = listener.AcceptTcpClient();
                clients.Add(client);
                Console.WriteLine("Connected.");
                ThreadPool.QueueUserWorkItem(ThreadProc, client);
            }

        }

        private static void ThreadProc(object obj)
        {
            TcpClient client = obj as TcpClient;

            string data = null;
            byte[] bytes = new byte[1024];
            data = null;
            NetworkStream stream = client.GetStream();

            int counter = 0;
            int i;
            try
            {


                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);

                    clientsInfo.Add(data, client.Client.RemoteEndPoint.ToString());

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    int ClientID = clients.Count;
                    byte[] intBytes = BitConverter.GetBytes(ClientID);
                    //Array.Reverse(intBytes);
                    //byte[] result = intBytes;

                    //byte[] ClientID = System.Text.Encoding.ASCII.GetBytes();
                    if (counter != 0)
                    {
                        stream.Write(msg, 0, msg.Length);
                    }
                    Console.WriteLine("Sent: {0}", data);

                    counter++;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
