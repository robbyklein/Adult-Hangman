using Adult_Hangman_Client;

internal class Program {
    private static async Task Main(string[] args) {
        await Client.Start();
    }
}