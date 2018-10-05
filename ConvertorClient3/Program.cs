using System;
using System.IO;
using System.Net.Sockets;

namespace ConvertorClient3
{
    class Program
    {
        private static TcpClient _clientSocket = null;
        private static Stream _nstream = null;
        private static StreamWriter _sWriter = null;
        private static StreamReader _sReader = null;

        static void Main(string[] args)
        {
            try
            {
                using (_clientSocket = new TcpClient("127.0.0.1", 10))
                {
                    using (_nstream = _clientSocket.GetStream())
                    {
                        {
                            _sWriter = new StreamWriter(_nstream) { AutoFlush = true };
                            Console.WriteLine("The client is ready!");
                            for (int i = 2; i > 1; i++)
                            {
                                Console.WriteLine("Type ToGrams or ToOunces, space and the amount to be converted and press Enter ");
                                string clientMsg = Console.ReadLine();
                                _sWriter.WriteLine(clientMsg);
                                _sReader = new StreamReader(_nstream);
                                string rdMsgFromServer = _sReader.ReadLine();
                                if (rdMsgFromServer != null)
                                {
                                    Console.WriteLine("------------------------------------------------------------------------------------");
                                    Console.WriteLine("Client recieved the result from Server:" + rdMsgFromServer);
                                    Console.WriteLine("------------------------------------------------------------------------------------");
                                }
                                else
                                {
                                    Console.WriteLine("Client recieved null message from Server ");
                                }
                            }

                        }
                    }
                    Console.WriteLine("Press enter to stop the client!");
                }

                Console.ReadKey();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
        }
    }
}
