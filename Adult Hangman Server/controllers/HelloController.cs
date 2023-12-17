using System.Net.WebSockets;
using Adult_Hangman_Libs;
using Adult_Hangman_Libs.Utility;
using Adult_Hangman_Server;

internal class HelloController {
    public static async void Index(WebSocket webSocket, ClientMessage message) {
        var res = new ServerMessage {
            Type = ServerMessageType.Welcome,
            Welcome = {
                Message = "Welcome to Adult Hangman!",
                ConnectedUsers = Connections.ConnectedClients.Count
            }
        };

        var resBytes = MessageEncoder.Encode(res);

        if (webSocket.State != WebSocketState.Open) return;

        await webSocket.SendAsync(new ArraySegment<byte>(resBytes, 0, resBytes.Length),
            WebSocketMessageType.Text, true, CancellationToken.None);
    }
}