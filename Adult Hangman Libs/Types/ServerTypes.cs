namespace Adult_Hangman_Libs;

public enum ServerMessageType {
    Welcome
}

public class ServerMessage {
    public ServerMessageType Type { get; set; } = ServerMessageType.Welcome;
    public ServerWelcomeMessage Welcome { get; set; } = new();
}

public class ServerWelcomeMessage {
    public string Message { get; set; } = string.Empty;
    public int ConnectedUsers { get; set; } = 0;
}