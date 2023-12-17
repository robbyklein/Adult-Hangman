using System.Net.WebSockets;
using Adult_Hangman_Libs;
using Adult_Hangman_Libs.Utility;

namespace Adult_Hangman_Client;

public class Client {
    public static int Port = 3000;

    public static async Task Start() {
        // Create uri/websocket
        var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";
        var uri = new Uri($"ws://localhost:{Port}/");
        using var webSocket = new ClientWebSocket();

        try {
            // Open connection
            await webSocket.ConnectAsync(uri, CancellationToken.None);

            // Send initial message
            var msg = new ClientMessage { Type = ClientMessageType.Hello };
            var msgBytes = MessageEncoder.Encode(msg);
            await SendMessage(webSocket, msgBytes);

            // Keep open / receive messages
            await RequestHandler.ReceiveMessages(webSocket);
        } catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static async Task SendMessage(ClientWebSocket webSocket, byte[] message) {
        await webSocket.SendAsync(new ArraySegment<byte>(message, 0, message.Length),
            WebSocketMessageType.Text, true, CancellationToken.None);
    }
}