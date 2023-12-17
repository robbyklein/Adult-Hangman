using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace Adult_Hangman_Server;

public class Connections {
    public static ConcurrentDictionary<Guid, PlayerData> ConnectedClients = new();

    public static void AddPlayer(Guid clientId, WebSocket webSocket) {
        if (ConnectedClients.Count > 0) throw new Exception("Server at capacity");
        ConnectedClients.TryAdd(clientId, new PlayerData(webSocket));
    }

    public static void RemovePlayer(Guid clientId) {
        ConnectedClients.TryRemove(clientId, out _);
    }

    public struct PlayerData {
        public WebSocket WebSocket;
        public int CurrentRoom = 0;

        public PlayerData(WebSocket webSocket) {
            WebSocket = webSocket;
        }
    }
}