namespace discrete_math_final_project;

class Program
{
    public static void Main(string[] args)
    {
        EllipticCurveInZMod curve; 
        bool programIsRunning = true;
        string userInput;
        int n = 0;

        while (programIsRunning)
        {
            Console.WriteLine("Welcome, user! This program demonstrates Lenstra's Elliptic Curve Factorization. This algorithm is especially helpful for finding factors of large integers.\n");
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
            // do stuff with curve

            curve = new EllipticCurveInZMod(n);
            Console.WriteLine($"Random elliptic curve in Z mod {n}Z:\n");
            Console.WriteLine(curve.ToString());

            Console.WriteLine("Would you like to factor another number? (y/n)");
            userInput = Console.ReadLine().ToLower();
            while (!(userInput == "y") && !(userInput == "n"))
            {
                Console.WriteLine("Please type \"y\" or \"n\" to indicate yes or no. Would you like to factor another number?");
                userInput = Console.ReadLine();
            }
            if (userInput == "y") 
            {
                n = 0; // reset n so that input validation works
            }
            if (userInput == "n")
            {
                Console.WriteLine("\nThanks for using this program; we hope you enjoyed this demonstration of Lenstra's Elliptic Curve Factorization! Have a nice day!");
                programIsRunning = false;
            }
        }
    }
}