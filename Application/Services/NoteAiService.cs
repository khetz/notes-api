using Anthropic.SDK;
using Anthropic.SDK.Messaging;
using Domain.Entities;
using System.Text.Json;

namespace Application.Services;

public class NoteAiService
{
    private readonly AnthropicClient _client;

    public NoteAiService(AnthropicClient client)
    {
        _client = client;
    }

    public async Task<NoteAiResponse> AnalyseNoteAsync(string title, string content, string category)
    {
        var response = await _client.Messages.GetClaudeMessageAsync(new MessageParameters
        {
            Model = "claude-sonnet-4-5",
            MaxTokens = 512,
            System = [
                new SystemMessage(
                    "You are a note analysis assistant. " +
                    "Always response with valid JSON only - no preamble, no markdown, no explanation. " +
                    "Return exactly this structure: { \"summary\": string, \"tags\": string[] }"
                    )
            ],
            Messages = [
                new Message(RoleType.User,
                $"Analyse this note and return a summary and 3-5 tags. \n\n" +
                $"Category: {category}\n" +
                $"Title: {title}\n" +
                $"Content: {content}")
            ]
        });

        var json = response.Content[0].ToString()!
            .Replace("```json", "")
            .Replace("```", "")
            .Trim();

        try
        {
            var result = JsonSerializer.Deserialize<NoteAiResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result ?? new NoteAiResponse("Could not summarise note.", new List<string>());
        }
        catch
        {
            return new NoteAiResponse("Could not summarise note.", new List<string>());
        }
    }
}
