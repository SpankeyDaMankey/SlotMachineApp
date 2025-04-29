using System;

class Program
{
    static void Main(string[] args)
    {
        bool cashout = false;
        TripleSlot slot = new();

        while(!cashout)
        {
            slot.DisplayBalance();
            slot.DisplayBetAmount();
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1 - Spin");
            Console.WriteLine("2 - Cash Out");
            Console.WriteLine("3 - Change Bet Amount");
            Console.WriteLine("Enter your choice: ");

            if(int.TryParse(Console.ReadLine(), out int choice))
            {
                switch(choice)
                {
                    case 1:
                        slot.PlaceBet();
                        slot.SpinReels();
                        slot.DisplayReels();
                        slot.CheckWinningCombination();
                        slot.CalculatePayout();
                        break;
                    case 2:
                        cashout = true;
                        Console.WriteLine("You cashed out!");
                        slot.Cashout();
                        break;
                    case 3:
                        slot.ChangeBetAmount();
                        break;
                    default:
                        Console.WriteLine("Invalid Choice!  Choose option 1 or 2");
                        break;
                }
            }
            else Console.WriteLine("Choice entered is not an integer!");

        }
    }
}
