namespace Adult_Hangman_Libs;

public enum ClientMessageType {
    Hello
}

public class ClientMessage {
    public ClientMessageType Type { get; set; } = ClientMessageType.Hello;
    public ClientAnswerMessage ClientAnswer { get; set; } = new();
}

public class ClientAnswerMessage {
    public string Text { get; set; } = string.Empty;
}