using Dafny;

public abstract class SlotMachine<T> where T : notnull
{   
    protected double balance;
    protected double betAmount;
    protected Dictionary<T, double> Reels = [];

    protected Random random;
    protected int combination;

    public SlotMachine()
    {
        balance = 1000;
        betAmount = 10;
        random = new Random();
    }

    public abstract void SpinReels();
    public abstract void DisplayReels();
    public abstract void CalculatePayout();
    public abstract void CheckWinningCombination();
    public void PlaceBet() 
    {
        BigRational BRbet = SM.SlotMachineD.PlaceBet(new BigRational(this.balance), new BigRational(this.betAmount));
        double bet = (double)BRbet.num/(double)BRbet.den;
        this.balance -= bet;
    }

    public void ChangeBetAmount()
    {
        bool valid = false;
        
        while(!valid)
        {
            Console.WriteLine("Choose bet amount: ");
        
            Console.WriteLine("1: 10");
            Console.WriteLine("2: 20");
            Console.WriteLine("3: 50");
            Console.WriteLine("4: 100");

            if(int.TryParse(Console.ReadLine(), out int choice))
            {
                switch(choice)
                {
                    case 1:
                        betAmount = 10;
                        valid = true;
                        break;
                    case 2:
                        betAmount = 20;
                        valid = true;
                        break;
                    case 3: 
                        betAmount = 50;
                        valid = true;
                        break;
                    case 4:     
                        betAmount = 100;
                        valid = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Choice!  Choose option 1, 2, 3, or 4");
                        break;
                }
            }
        }
        
    }

    public void DisplayBetAmount()
    {
        Console.WriteLine("Current Bet Amount Setting: " + betAmount);
    }

    public void DisplayBalance()
    {
        Console.WriteLine("Balance: " + balance);
    }

    public void Cashout()
    {
        Console.WriteLine("Cashout Amount: " + balance);
    }

}







