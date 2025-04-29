using Dafny;
class TripleSlot : SlotMachine<Symbol>
{
    private Symbol[] reels;

    private CombinationTable combinationTable;

    private CombinationType combinationType;

    private TS._ISymbol ConvertSymbol(Symbol s)
    {
        if(s == Symbol.cherry) 
        {
            return TS.Symbol_cherry.create_cherry();
        }
        else if(s == Symbol.lemon)
        {
            return TS.Symbol_lemon.create_lemon();
        }
        else if(s == Symbol.orange)
        {
            return TS.Symbol_orange.create_orange();
        }
        else if(s == Symbol.plum)
        {
            return TS.Symbol_plum.create_plum();
        }
        else if(s == Symbol.bell)
        {
            return TS.Symbol_bell.create_bell();
        }
        else if(s == Symbol.bar)
        {
            return TS.Symbol_bar.create_bar();
        }
        else
        {
            return TS.Symbol_seven.create_seven();
        }

    }

    private TS._ICombinationType ConvertCombinationType(CombinationType c)
    {
        if(c == CombinationType.triple)
        {
            return TS.CombinationType_Triple.create_Triple();
        }
        else if(c == CombinationType.doublet)
        {
            return TS.CombinationType_Double.create_Double();
        }
        else
        {
            return TS.CombinationType_None.create_None();
        }
    }

    public TripleSlot()
    {
        reels = new Symbol[3];

        combinationTable = new CombinationTable();

        Reels.Add(Symbol.cherry, 0.2);
        Reels.Add(Symbol.lemon, 0.2);
        Reels.Add(Symbol.orange, 0.2);
        Reels.Add(Symbol.plum, 0.2);
        Reels.Add(Symbol.bell, 0.05);
        Reels.Add(Symbol.bar, 0.05);
        Reels.Add(Symbol.seven, 0.1);
    }
    public override void SpinReels()
    {
        double spinValue1 = random.Next(100);
        double spinValue2 = random.Next(100);
        double spinValue3 = random.Next(100);

        bool lock1 = false;
        bool lock2 = false;
        bool lock3 = false;

        double accumulation1 = 0;
        double accumulation2 = 0;
        double accumulation3 = 0;

        foreach (Symbol s in Reels.Keys)
        {
            if(!lock1 && spinValue1 <= accumulation1 + Reels[s])
            {
                reels[0] = s;
                lock1 = true;
            }
            else accumulation1 += Reels[s] * 100;

            if(!lock2 && spinValue2 <= accumulation2 + Reels[s])
            {
                reels[1] = s;
                lock2 = true;
            }
            else accumulation2 += Reels[s] * 100;

            if(!lock3 && spinValue3 <= accumulation3 + Reels[s])
            {
                reels[2] = s;
                lock3 = true;
            }
            else accumulation3 += Reels[s] * 100;

            if(lock1 && lock2 && lock3) break;
        }
    }
    
    public override void DisplayReels()
    {
        Console.WriteLine("+----------+----------+----------+");
        Console.WriteLine($"| {reels[0], -10} | {reels[1], -10} | {reels[2], -10} |");
        Console.WriteLine("+----------+----------+----------+");
    }
    
    public override void CheckWinningCombination()
    {
        Symbol reel1 = reels[0];
        Symbol reel2 = reels[1];
        Symbol reel3 = reels[2];

        TS._ISymbol reel1D = ConvertSymbol(reel1);
        TS._ISymbol reel2D = ConvertSymbol(reel2);
        TS._ISymbol reel3D = ConvertSymbol(reel3);

        if(TS.TripleSlotD.CheckTripleCombination(reel1D, reel2D, reel3D))
        {
            combinationType = CombinationType.triple;

            combination = (int)TS.TripleSlotD.getCombination(reel1D, combinationTable.getCombination(reel1));


        }
        else if(TS.TripleSlotD.CheckDoubleCombination(reel1D, reel2D, reel3D))
        {
            combinationType = CombinationType.doublet;

            if(reel1 == reel2)
            {
                combination = (int)TS.TripleSlotD.getCombination(reel1D, combinationTable.getCombination(reel2));
            }
            else if(reel2 == reel3)
            {
                combination = (int)TS.TripleSlotD.getCombination(reel2D, combinationTable.getCombination(reel3));
            }
            else
            {
                combination = (int)TS.TripleSlotD.getCombination(reel1D, combinationTable.getCombination(reel3));
            }

        }
        else
        {
            combinationType = CombinationType.none;
            combination = 0;
        }


    }

    public override void CalculatePayout()
    {
        TS._ICombinationType combinationTypeD = ConvertCombinationType(combinationType);

        int multiplier  = (int)TS.TripleSlotD.getMultiplier(combination, combinationTypeD);

        BigRational BRwinnings = TS.TripleSlotD.CalculatePayout(new BigRational(betAmount), multiplier);

        double winnings = (double)BRwinnings.num/(double)BRwinnings.den;

        Console.WriteLine("You won: " + winnings);
        balance += winnings;
    }

    
}
