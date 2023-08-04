var gpt = new OpenAiTest.Client();

await gpt.SendToAPI();

await gpt.SendFollowUpMessage();

Console.ReadKey();
