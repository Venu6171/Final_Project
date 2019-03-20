using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SynchronousSocketListener
{

    // Incoming data from the client.  
    public static string data_client_A = null;
    public static string data_client_B = null;

    public static void StartListening()
    {
        // Data buffer for incoming data.  
        byte[] bytes = new Byte[1024];
        byte[] bytes_2 = new Byte[1024];

        // Establish the local endpoint for the socket.  
        // Dns.GetHostName returns the name of the   
        // host running the application.  
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint localEndPoint_Client_A = new IPEndPoint(ipAddress, 10000);

        IPEndPoint localEndPoint_Client_B = new IPEndPoint(ipAddress, 10001);

        // Create a TCP/IP socket.  
        Socket listener = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);
        Socket listener2 = new Socket(ipAddress.AddressFamily,
             SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and   
        // listen for incoming connections.  
        try
        {
            listener.Bind(localEndPoint_Client_A);
            listener.Listen(10);

            listener2.Bind(localEndPoint_Client_B);
            listener2.Listen(10);

            // Start listening for connections.  
            while (true)
            {
                Console.WriteLine("Waiting for a connection...");

                // Program is suspended while waiting for an incoming connection.  

                Socket handler_Client_A = listener.Accept();
                Socket handler_Client_B = listener2.Accept();
                data_client_A = null;
                data_client_B = null;
                Console.WriteLine("Both Clients Connected");

                // An incoming connection needs to be processed.  
                
                int bytesRec = handler_Client_A.Receive(bytes);
                data_client_A += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                int bytesRec_2 = handler_Client_B.Receive(bytes_2);
                data_client_B += Encoding.ASCII.GetString(bytes_2, 0, bytesRec_2);


                // Show the data on the console.  
                Console.WriteLine("Text received: {0}", data_client_A);
                Console.WriteLine("Text received: {0}", data_client_B);


                // Echo the data back to the Client.

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

        Console.WriteLine("\nPress ENTER to continue...");
        Console.Read();

    }

    public static int Main(String[] args)
    {
        StartListening();
        return 0;
    }
}