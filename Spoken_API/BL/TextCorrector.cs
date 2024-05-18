using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class TextCorrector
{
    private readonly string apiUrl = "https://api-inference.huggingface.co/models/vennify/t5-base-grammar-correction";
    private readonly string apiToken = "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";
    private readonly int maxRetries = 5;
    private readonly int delayMilliseconds = 2000;

    public async Task<string> CorrectGrammarAsync(string text)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

            var jsonContent = new StringContent("{\"inputs\":\"" + text + "\"}", Encoding.UTF8, "application/json");

            int retryCount = 0;
            while (true)
            {
                try
                {
                    HttpResponseMessage response = await client.PostAsync(apiUrl, jsonContent);
                    response.EnsureSuccessStatusCode();

                    string result = await response.Content.ReadAsStringAsync();

                    JArray jsonResult = JArray.Parse(result);
                    string correctedText = jsonResult[0]["generated_text"].ToString();

                    return correctedText;
                }
                catch (HttpRequestException ex) when (retryCount < maxRetries)
                {
                    retryCount++;
                    Console.WriteLine($"Request failed with {ex.Message}. Retrying ({retryCount}/{maxRetries})...");
                    await Task.Delay(delayMilliseconds);
                }
            }
        }
    }
}



