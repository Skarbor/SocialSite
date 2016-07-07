using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Communicator
{
    public class CommunicatorWebSocketsManager
    {
        static CommunicatorRepository communicatorRepository = new CommunicatorRepository();

        static Dictionary<string, WebSocket> WebSockets;
        static Dictionary<string, Thread> userThreads;

        static CommunicatorWebSocketsManager()
        {
            WebSockets = new Dictionary<string, WebSocket>();
            userThreads = new Dictionary<string, Thread>();
        }

        static public void AddWebSocket(string userId, WebSocket socket)
        {
            if (WebSockets.ContainsKey(userId))
            {
                WebSockets[userId] = socket;
            }
            else WebSockets.Add(userId, socket);

            Listening(userId, socket);
        }

        static public async void SendMessageToUser(string sendingUserId, string receivingUserId, string message, int messageId)
        {
            var token = CancellationToken.None;
            var type = WebSocketMessageType.Text;
            var data = Encoding.UTF8.GetBytes(sendingUserId+":"+ receivingUserId+":" + messageId + ":" +message);
            var buffer = new ArraySegment<Byte>(data);

            //prześlij wiadomość bezpośrednio jeśli user jest zalogowany
            if (WebSockets.ContainsKey(receivingUserId))
            {
                await WebSockets[receivingUserId].SendAsync(buffer, type, true, token);
            }  
        }

        static public async Task Listening(string userId, WebSocket socket)
        {
            System.Diagnostics.Debug.WriteLine("Listening: start...");
            var token = CancellationToken.None;

            var buffer = new ArraySegment<Byte>(new Byte[4096]);

            while (true)
            {
                if (socket.State != WebSocketState.Open) break;

                WebSocketReceiveResult received = await socket.ReceiveAsync(buffer, token);
                if (received.CloseStatus == WebSocketCloseStatus.EndpointUnavailable) return;


                System.Diagnostics.Debug.WriteLine("Listening: received message");
                if (received.MessageType == WebSocketMessageType.Text)
                {
                    string request = Encoding.UTF8.GetString(buffer.Array,
                                                      buffer.Offset,
                                                      buffer.Count);
                    buffer = new ArraySegment<Byte>(new Byte[4096]);

                    request = request.Substring(0, request.IndexOf('\0'));

                    string receivingUserId = request.Substring(0, request.IndexOf(':'));
                    string messageText = request.Substring(request.IndexOf(':') + 1);

                    System.Diagnostics.Debug.WriteLine("Listening: message text: " + messageText);

                    int messageId = communicatorRepository.SendMessage(userId, receivingUserId, messageText);

                    SendMessageToUser(userId, receivingUserId, messageText, messageId);
                }
            }
        }
    }
}
