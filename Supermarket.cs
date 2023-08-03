using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OpenAiTest
{
	public class Supermarket
	{
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> SendMessageToGpt3(string message, double temperature, string systemPrompt)
        {
            string apiKey = "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";

            // Setting up the OpenAI API url and authorization
            var url = "https://api.openai.com/v1/chat/completions";
            
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);

            // Setting up the message body for chat-based model
            var data = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = message }
            },
                temperature,
            };

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            try
            {
                
                Console.WriteLine("Processing ....");
                Console.WriteLine();

                // Sending the POST request to the API
                var response = await client.PostAsync(url, content);

                Console.WriteLine("Done.");
                Console.WriteLine();

                // Getting the response
                var responseString = await response.Content.ReadAsStringAsync();

                return ParseGpt3Response(responseString);
            }
            catch
            {
                throw;
            }
        }

        private string? ParseGpt3Response(string json)
        {
            var jsonObject = JObject.Parse(json);
            if (jsonObject == null)
                return null;

            var choices = jsonObject["choices"];
            if (choices == null)
                return null;

            var firstChoice = choices[0];
            if (firstChoice == null)
                return null;

            var message = firstChoice["message"];
            if (message == null)
                return null;

            var content = message["content"];
            if (content == null)
                return null;

            return content.ToString();
        }

    }
}

