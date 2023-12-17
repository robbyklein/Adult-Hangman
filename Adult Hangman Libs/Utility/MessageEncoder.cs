using System.Text;
using System.Text.Json;

namespace Adult_Hangman_Libs.Utility;

public class MessageEncoder {
    public static byte[] Encode<t>(t msg) {
        var json = JsonSerializer.Serialize(msg);
        return Encoding.UTF8.GetBytes(json);
    }
}