using System;

namespace OpenAiTest
{
	public class Client
	{
        public string? OrininalPromt { get; set; }

        const double Temperature = 0.5;

        private string systemPrompt = "You are Gordon Ramsay and a very helpful and cheerful assistant";

        public async Task SendToAPI()
        {
            var sender = new Supermarket();

            OrininalPromt = GeneratePrompt();

            try
            {
                Console.WriteLine("Prompt: " + OrininalPromt);
                Console.WriteLine();
                Console.WriteLine("Hit any Key to start...");
                Console.ReadKey();

                var response = await sender.SendMessageToGpt3(OrininalPromt, Temperature, systemPrompt);
                Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private string GeneratePrompt()
        {
            string criteria = " The total calories for all the ingredients in the recipe must not be more than 300. ";
            string currentItems = "eggs and flour";
            string dietRequirements = " Do not include baking powder in any recipe. ";

            string mainMsg = "I am walking through a supermarket shopping. " +
                "I have " + currentItems + " in my basket. " +
                "Please suggest 1 recipe I could make with these ingredients. " + criteria +
                "Also, if I added two more ingredients tell me what else could I make.  " + dietRequirements +
                "Please show the calories for each of the items in the recipes. ";

            string responseTarget = " Please phrase your response in a way that will encourage a shopper in ASDA buy the additional ingredients ";

            return mainMsg + criteria + responseTarget;
        }


        public async Task SendFollowUpMessage()
        {
            string prompt = OrininalPromt + " Please now show the previously returned recipes without any instructions. ";
            string outputToTable = " Please output the recipes to HTML in a Table.  ";
            var sender = new Supermarket();

            try
            {
                Console.WriteLine("Followup Prompt: " + prompt);
                Console.WriteLine();
                Console.WriteLine("Hit any Key to send...");
                Console.ReadKey();

                var response = await sender.SendFollowUpMessage(prompt + outputToTable, Temperature, systemPrompt);

                Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

