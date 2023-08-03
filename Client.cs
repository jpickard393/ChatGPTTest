using System;

namespace OpenAiTest
{
	public class Client
	{
        const double Temperature = 0.5;
		
        public async Task SendToAPI()
        {
            ///string systemPrompt = "You are an expert in diet and nutrition, and a very helpful and cheerful assistant. You have an excellent knowledge of food and cooking. ";
            string systemPrompt = "You are Gordon Ramsay and a very helpful and cheerful assistant";

            var sender = new Supermarket();

            string prompt = GeneratePrompt();

            try
            {
                Console.WriteLine("Prompt: " + prompt);
                Console.WriteLine();
                Console.WriteLine("Hit any Key to start...");
                Console.ReadKey();

                var response = await sender.SendMessageToGpt3(prompt, Temperature, systemPrompt);
                Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private string GeneratePrompt()
        {
            string criteria = " The total calories for all the ingredients in the recipie must not be more than 300. ";
            string currentItems = "eggs and flour";
            string dietRequirements = " Do not include baking powder in any recipie. ";

            string mainMsg = "I am walking through a supermarket shopping. " +
                "I have " + currentItems + " in my basket. " +
                "Please suggest 1 recipie I could make with these ingredients. " + criteria +
                "Also, if I added two more ingredients tell me what else could I make.  " + dietRequirements +
                "Please show the calories for each of the items in the recipes. ";

            string responseTarget = " Please phrase your response in a way that will encourage a shopper in ASDA buy the additional ingredients ";

            return mainMsg + criteria + responseTarget;
        }
    }
}

