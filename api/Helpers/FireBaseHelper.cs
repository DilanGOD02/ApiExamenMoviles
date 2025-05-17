using Google.Apis.Auth.OAuth2;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

public static class FirebaseHelper
{
  private static readonly HttpClient client = new HttpClient();

  public static async Task SendPushNotificationToTopicAsync(string topic, string title, string body)
  {
    var accessToken = await GetAccessTokenAsync();

    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

    var message = new
    {
      message = new
      {
        topic = topic, // Sending to the topic
        notification = new
        {
          title = title,
          body = body
        }
      }
    };

    var json = JsonSerializer.Serialize(message);

    var response = await client.PostAsync(
        "https://fcm.googleapis.com/v1/projects/examen-moviles-42eb9/messages:send",
        new StringContent(json, Encoding.UTF8, "application/json")
    );

    if (!response.IsSuccessStatusCode)
    {
      var error = await response.Content.ReadAsStringAsync();
      Console.WriteLine($"Error sending FCM: {error}");
    }
  }

  private static async Task<string> GetAccessTokenAsync()
  {
    // Synchronously load the credentials
    GoogleCredential credential = GoogleCredential
        .FromFile("firebase-adminsdk.json")
        .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");

    // Now you can await on the async method to get the access token
    var accessToken = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();

    return accessToken;
  }
}