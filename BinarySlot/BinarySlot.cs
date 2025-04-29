using Dafny;


public enum Coin
{
    heads,
    tails
}

public class BinarySlot : SlotMachine <Coin>
{
    private Coin reel;

    private BS._ICoin ConvertCoin(Coin c)
    {
        return c == Coin.heads ? BS.Coin_heads.create_heads() : BS.Coin_tails.create_tails();
    }

    public BinarySlot()
    {
        Reels.Add(Coin.heads, 0.5);
        Reels.Add(Coin.tails, 0.5);
    }

    public override void SpinReels()
    {
        double spinValue = random.Next(2);

        if(spinValue < Reels[Coin.heads])
        {
            reel = Coin.heads;
        }
        else reel = Coin.tails;
    }

    public override void DisplayReels()
    {
        Console.WriteLine(reel);
    }

    public override void CheckWinningCombination()
    {
        BS._ICoin bob = ConvertCoin(this.reel);
        combination = (int)BS.BinarySlotD.CheckWinningCombination(bob);
    }
    
    public override void CalculatePayout()
    {
        BigRational BRwinnings = BS.BinarySlotD.CalculatePayout(this.combination, new BigRational(this.betAmount));

        double winnings = (double)BRwinnings.num/(double)BRwinnings.den;

        Console.WriteLine("You won: " + winnings); 
        balance += winnings;
    }

    
}

