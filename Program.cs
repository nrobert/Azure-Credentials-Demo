﻿using System.Diagnostics;
using Azure.AI.OpenAI;
using Azure.Identity;
using OpenAI.Chat;

Console.WriteLine("Hello, DevDayBE!");

// Call Azure OpenAI using Credentials: will use Managed Identity or Azure CLI
var tokenCredential = new ChainedTokenCredential(new ManagedIdentityCredential(), new AzureCliCredential());
var client = new AzureOpenAIClient(new Uri("https://oai-demo-identity-swc.openai.azure.com/"), tokenCredential);

#region Chat Completion
// Make a Chat Completion request
var chatClient = client.GetChatClient("gpt-35-turbo");
var chatCompletion = (await chatClient.CompleteChatAsync(  
    new ChatMessage[] {
        new SystemChatMessage("You are an AI assistant that helps people find information."),
        new UserChatMessage("What is the capital of Belgium?")
    },
    new ChatCompletionOptions() {
        MaxOutputTokenCount = 800,
        Temperature = (float)0.7
    }
)).Value;
Console.WriteLine($"{chatCompletion.Role}: '{chatCompletion.Content[0].Text}'");  
#endregion

#region Image generation
// Generate an image using the DALL-E model
var generatedImageResult = await client.GetImageClient("dall-e-3").GenerateImageAsync("A giant belgian waffle in a beer glass shape, pixel art style");
var image = generatedImageResult.Value;

// Save the image to a file
var imageFileName = $"waffle-{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
using var httpClient = new HttpClient();
using var s = await httpClient.GetStreamAsync(image.ImageUri);
using var fs = new FileStream(imageFileName, FileMode.OpenOrCreate);
await s.CopyToAsync(fs);
Console.WriteLine($"Image saved to '{imageFileName}'");

// Display the image using the default image viewer
Process.Start(new ProcessStartInfo(imageFileName) { UseShellExecute = true });
#endregion

Console.WriteLine("Merci !");
Console.ReadKey();