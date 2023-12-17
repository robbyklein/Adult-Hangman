using System.Net.WebSockets;
using Adult_Hangman_Libs;

namespace Adult_Hangman_Server;

public class Router {
    public static void RouteRequest(WebSocket webSocket, ClientMessage message) {
        switch (message.Type) {
            case ClientMessageType.Hello:
                HelloController.Index(webSocket, message);
                break;
        }
    }
}