using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.Communications
{
    public class GameClient
    {
        public TcpClient TCPClient { get; private set; }
        
        public string Name { get; set; }

        public bool IsConnected
        {
            get
            {
                try
                {
                    return !(TCPClient.Client.Poll(1, SelectMode.SelectRead) && TCPClient.Client.Available == 0);
                }
                catch
                {
                    return false;
                }
            }
        }

        public GameClient(TcpClient tcpclient)
        {
            TCPClient = tcpclient;
            Name = "Guest";
        }

        public void SendMessage(string message)
        {
            NetworkStream stream = TCPClient.GetStream();
            byte[] data = Encoding.ASCII.GetBytes(message);
            
            stream.Write(data, 0, data.Length);
        }

        public string? ReceiveData()
        {
            NetworkStream stream = TCPClient.GetStream();

            if (stream.DataAvailable)
            {
                byte[] buffer = new byte[1024];
                
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                
                string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                return receivedData;
            }

            return null;
        }

        public void CloseConnection()
        {
            TCPClient.Close();

        }

        public void Pulse()
        {

        }
    }


}
