using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Adult_Hangman_Libs;

namespace Adult_Hangman_Client;

public class RequestHandler {
    public static async Task ReceiveMessages(ClientWebSocket webSocket) {
        var buffer = new byte[1024];

        try {
            while (webSocket.State == WebSocketState.Open) {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close) {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty,
                        CancellationToken.None);
                    Console.WriteLine("Disconnected from server");
                } else if (result.MessageType == WebSocketMessageType.Text) {
                    var resString = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    var res = JsonSerializer.Deserialize<ServerMessage>(resString);

                    Console.WriteLine("-------------------------------");
                    Console.WriteLine("Welcome to Adult Hangman!");
                    Console.WriteLine($"Connected players: {res.Welcome.ConnectedUsers}");
                    Console.WriteLine("-------------------------------");
                }
            }
        } catch (WebSocketException e) {
            Console.WriteLine(e);
        }
    }
}