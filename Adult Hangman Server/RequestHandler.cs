using System.Net;
using System.Net.WebSockets;
using System.Text.Json;
using Adult_Hangman_Server;

internal class RequestHandler {
    public static async Task HandleWebSocketConnectionAsync(HttpListenerContext context) {
        try {
            // Get the context/websocker
            WebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
            using var webSocket = webSocketContext.WebSocket;

            // Generate a unique ID for this client
            var clientId = Guid.NewGuid();

            // Add the client to the connectedClients dictionary
            Connections.AddPlayer(clientId, webSocket);

            // Create some space for messages
            var buffer = new byte[1024];

            // Keep open till disconnect
            try {
                while (webSocket.State == WebSocketState.Open) {
                    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    switch (result.MessageType) {
                        case WebSocketMessageType.Text:
                            try {
                                var message = await MessageDecoder.DecodeMessage(buffer, result.Count);
                                Router.RouteRequest(webSocket, message);
                            } catch (JsonException e) {
                                Console.WriteLine($"Invalid JSON format: {e.Message}");
                            }

                            break;
                        case WebSocketMessageType.Close:
                            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty,
                                CancellationToken.None);
                            break;
                    }
                }
            } catch (Exception e) {
                Console.WriteLine(e);
                Connections.RemovePlayer(clientId);
            }
        } catch (Exception e) {
            // Log errors
            Console.WriteLine("Player disconnected");
        }
    }
}