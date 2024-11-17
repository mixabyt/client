using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Text.Json;


namespace WinFormsApp2
{
    public class WebSocketClient
    {
        private ClientWebSocket _webSocket;
        private Uri _serverUri;
        private CancellationToken _token;
        public event Action<string, JsonElement> OnMessageReceived;

        public WebSocketClient(string url)
        {
            _serverUri = new Uri(url);
            _token = CancellationToken.None;
            _webSocket = new ClientWebSocket();
        }

        public async Task Connect()
        {
            try
            {
                await _webSocket.ConnectAsync(_serverUri, _token);
                await ReceiveMessages();
            }
            catch (Exception ex)
            {
                Debug.Print($"помилка при Сonnect: {ex}");
            }
        }

        public async Task SendMessage(object data)
        {
            if (_webSocket.State == WebSocketState.Open)
            {

                string json = JsonConvert.SerializeObject(data);
                byte[] buffer = Encoding.UTF8.GetBytes(json);
                await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, System.Threading.CancellationToken.None);
            }
            else
            {
                Debug.Print("WebSocket connection was lost");
            }

        }

        private async Task ReceiveMessages()
        {
            var buffer = new byte[1024 * 1000000];
            while (_webSocket.State == WebSocketState.Open)
            {
                // отримай повідомлення в байтах і конвертуй в стрінг 
                var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), _token);
            Debug.Print("ОТРИМАЛИ ПОВІДОМЛЕННЯ");
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                try
                {
                    // Парсимо JSON 
                    var jsonDoc = JsonDocument.Parse(message);
                    var root = jsonDoc.RootElement;

                    if (root.TryGetProperty("type", out JsonElement typeElement))
                    {
                        string messageType = typeElement.GetString();
                        // Викликаємо подію з типом повідомлення і самим повідомленням
                        OnMessageReceived?.Invoke(messageType, root);
                    }
                    else
                    {
                        Debug.Print("Json некоректний, немає поля type");
                    }
                }
                catch (System.Text.Json.JsonException jsonEx)
                {
                    Debug.WriteLine($"Invalid JSON: {jsonEx.Message}");
                }

            }
        }
        public async Task Disconnect()
        {
            if (_webSocket.State == WebSocketState.Open)
            {
                try
                {
                   
                    await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", _token);
                    Debug.Print("WebSocket connection closed.");
                }
                catch (Exception ex)
                {
                    Debug.Print($"Error while closing WebSocket: {ex}");
                }
            }
            else
            {
                Debug.Print("WebSocket connection is already closed.");
            }
        }
    }


}

