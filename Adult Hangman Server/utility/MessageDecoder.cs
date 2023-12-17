using System.Text;
using System.Text.Json;
using Adult_Hangman_Libs;

namespace Adult_Hangman_Server;

public class MessageDecoder {
    public static async Task<ClientMessage> DecodeMessage(byte[] buffer, int count) {
        var receivedMessage = Encoding.UTF8.GetString(buffer, 0, count);
        var clientMessage = JsonSerializer.Deserialize<ClientMessage>(receivedMessage);
        return clientMessage;
    }
}