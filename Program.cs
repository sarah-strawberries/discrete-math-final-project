namespace discrete_math_final_project;

class Program
{
    public static void Main(string[] args)
    {
        string userInput;
        int n = 0;
        Console.WriteLine("Please enter a nonzero integer to be factored.");
        userInput = Console.ReadLine();

        while (n == 0)
        {
            try
            {
                n = Int32.Parse(userInput);
                if (n == 0)
                {
                    Console.WriteLine("Input integer must not be 0. Please try again.");
                    userInput = Console.ReadLine();
                }
            }
            catch
            {
                Console.WriteLine("Input must be an integer. Please try again.");
                userInput = Console.ReadLine();
            }
        }
    }
}