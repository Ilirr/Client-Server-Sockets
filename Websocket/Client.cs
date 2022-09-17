using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Websocket
{
    public class Client
    {
        public void ExecuteClient()
        {
            try
            {


                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 23501);

                Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                try
                {


                    sender.Connect(localEndPoint);


                    Console.WriteLine("Socket connected to -> {0} ", sender.RemoteEndPoint.ToString());

                    while(true)
                    {
                        string msg = Console.ReadLine();
                        byte[] messageSent = Encoding.ASCII.GetBytes(msg);
                        int byteSent = sender.Send(messageSent);
                        byte[] messageReceived = new byte[1024];

                        int byteRecv = sender.Receive(messageReceived);
                        Console.WriteLine("Message from Server -> {0}", Encoding.ASCII.GetString(messageReceived, 0, byteRecv));


                        sender.Shutdown(SocketShutdown.Both);
                        sender.Close();
                    }
 
                }

                catch (ArgumentNullException ane)
                {

                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }

                catch (SocketException se)
                {

                    Console.WriteLine("SocketException : {0}", se.ToString());
                }

                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }

            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
        }
    }
}
