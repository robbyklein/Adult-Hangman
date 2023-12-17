using System.Net;

internal class Server {
    public static async Task Start() {
        // Create an http listener
        var listener = new HttpListener();
        var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";
        listener.Prefixes.Add($"http://+:{port}/");

        // Start it
        listener.Start();
        Console.WriteLine($"Listening on port {port}");

        // Keep it running
        while (true) {
            // Check for requests
            var context = await listener.GetContextAsync();

            // Handle them if websocket
            if (context.Request.IsWebSocketRequest) {
                // Send it to handler
                _ = RequestHandler.HandleWebSocketConnectionAsync(context);
            }
            // Otherwise send 400 status
            else {
                context.Response.StatusCode = 400;
                context.Response.Close();
            }
        }
    }
}