using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace OspreyLauncher
{
    public class UDPTrigger
    {
        public event LaunchHandler Launch;
        public delegate void LaunchHandler();

        public UDPTrigger()
        {
            Thread listnerThread = new Thread(new ThreadStart(listenUDP));
            listnerThread.Start(); // Start the thread
        }

        private void listenUDP()
        {
            //Creates a UdpClient for reading incoming data.
            UdpClient receivingUdpClient = new UdpClient(4141);
            while (true)
            {
                //Creates an IPEndPoint to record the IP Address and port number of the sender.  
                // The IPEndPoint will allow you to read datagrams sent from any source.
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                try
                {
                    // Blocks until a message returns on this socket from a remote host.
                    Byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);

                    if (Launch != null)
                        Launch();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

    }
}
