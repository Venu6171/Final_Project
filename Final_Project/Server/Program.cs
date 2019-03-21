using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace Server
{
    public class SynchronousSocketListener
    {

        public static string data_client_A = null;
        public static string data_client_B = null;

        public static void StartListening()
        {
            byte[] bytes = new Byte[1024];
            byte[] bytes_2 = new Byte[1024];
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            IPEndPoint localEndPoint_Client_A = new IPEndPoint(ipAddress, 10000);

            IPEndPoint localEndPoint_Client_B = new IPEndPoint(ipAddress, 10001);


            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            Socket listener2 = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);


            try
            {
                listener.Bind(localEndPoint_Client_A);
                listener.Listen(10);
                listener2.Bind(localEndPoint_Client_B);
                listener2.Listen(10);


                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");



                    Socket handler_Client_A = listener.Accept();
                    Socket handler_Client_B = listener2.Accept();
                    data_client_A = null;
                    data_client_B = null;
                    Console.WriteLine("Both Clients Connected");


                    int bytesRec = handler_Client_A.Receive(bytes);
                    data_client_A += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    int bytesRec_2 = handler_Client_B.Receive(bytes_2);
                    data_client_B += Encoding.ASCII.GetString(bytes_2, 0, bytesRec_2);



                    Console.WriteLine("Text received: {0}", data_client_A);
                    Console.WriteLine("Text received: {0}", data_client_B);

                    byte[] msg = Encoding.ASCII.GetBytes(data_client_A);
                    handler_Client_B.Send(msg);

                    byte[] msg_2 = Encoding.ASCII.GetBytes(data_client_B);
                    handler_Client_A.Send(msg_2);




                    handler_Client_A.Shutdown(SocketShutdown.Both);
                    handler_Client_A.Close();
                    handler_Client_B.Shutdown(SocketShutdown.Both);
                    handler_Client_B.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


        }

        public static int Main(String[] args)
        {
            StartListening();


            return 0;
        }
    }
}
