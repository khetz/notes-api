using System.Net.Http.Json;

namespace Application.Services;

public class EmbeddingService
{
    private readonly HttpClient _httpClient;

    public EmbeddingService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Voyage");
    }

    public async Task<float[]> GetEmbeddingsAsync(string text)
    {
        var request = new
        {
            model = "voyage-3",
            input = new[] { text }
        };

        var response = await _httpClient.PostAsJsonAsync("https://api.voyageai.com/v1/embeddings", request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<VoyageResponse>();
        return result!.Data[0].Embedding;
    }

    // Convert float[] to bytes for SQL Server storage
    public static byte[] ToBytes(float[] embedding)
    {
        var bytes = new byte[embedding.Length * 4];
        Buffer.BlockCopy(embedding, 0, bytes, 0, bytes.Length);
        return bytes;
    }

    // Convert bytes back to float[]
    public static float[] FromBytes(byte[] bytes)
    {
        var floats = new float[bytes.Length / 4];
        Buffer.BlockCopy(bytes, 0, floats, 0, bytes.Length);
        return floats;
    }

    // Cosine similarity between two vectors
    public static float CosineSimilarity(float[] a, float[] b)
    {
        var dot = a.Zip(b, (x, y) => x * y).Sum();
        var magA = MathF.Sqrt(a.Sum(x => x * x));
        var magB = MathF.Sqrt(b.Sum(x => x * x));
        return dot / (magA * magB);
    }

    record VoyageResponse(List<VoyageEmbedding> Data);
    record VoyageEmbedding(float[] Embedding);
}
