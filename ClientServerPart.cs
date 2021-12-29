using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace MessangerProject
{
    public class MyMessage
    {
        public string UserName { get; set; }

        public DateTime Time { get; set; }

        public string MessageText { get; set; }
    }
        
    public class ClientServerPart
    {
        private TcpClient _user;
        private NetworkStream _stream;
        private StreamWriter _sw;
        private StreamReader _sr;


        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }


        public ClientServerPart(string UserName)
        {
            _UserName = UserName;
            _user = new TcpClient();
            _user.Connect("localhost", 8000);

            _stream = _user.GetStream();
            _sw = new StreamWriter(_stream);
            _sr = new StreamReader(_stream);
        }


        public void SendMessage(string Text)
        {
            _sw.WriteLine(MadeMessage(Text));
            _sw.Flush();
        }

        public MyMessage RecieveMessage()
        {
            return JsonConvert.DeserializeObject<MyMessage>(_sr.ReadLine());
        }

        private string MadeMessage(string Text)
        {
            MyMessage message = new MyMessage()
            {
                Time = DateTime.Now,
                UserName = _UserName,
                MessageText = Text,
            };

            return JsonConvert.SerializeObject(message);
        }
    }
}
