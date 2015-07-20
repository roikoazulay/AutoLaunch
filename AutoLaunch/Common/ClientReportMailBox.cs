using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AutomationCommon
{
    public class ClientReportMailBox
    {
        private int _msgIndex = 1;
        private int MAX_QUEUE_SIZE = 20000;//overflow protection
        public ConcurrentQueue<ClientMessage> _mailBox;

        private ClientReportMailBox()
        {
            _mailBox = new ConcurrentQueue<ClientMessage>();
        }

        public void AddMsgToMailBox(ClientMessage msg)
        {
            ClientMessage data;
            msg.Index = _msgIndex++;//adding index
            _mailBox.Enqueue(msg);
            // Overflow protection (when client is not connected)
            while (_mailBox.Count > MAX_QUEUE_SIZE)
                _mailBox.TryDequeue(out data);
        }

        public List<ClientMessage> GetMailBox()
        {
            var msgs = new List<ClientMessage>();
            var data = new ClientMessage();
            int bufferSize = _mailBox.Count;
            bufferSize = bufferSize > 100 ? 100 : bufferSize;//limit the sent data up to 100 messages
            for (int i = 0; i < bufferSize; i++)
            {
                _mailBox.TryDequeue(out data);
                msgs.Add(data);
            }

            return msgs;
        }
    }

    public class ClientMessage
    {
        public DateTime Time { get; set; }

        public string Info { get; set; }

        public Enums.Status Status { get; set; }

        public int Index { get; set; }
    }
}