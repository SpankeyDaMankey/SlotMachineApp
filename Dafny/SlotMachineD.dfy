

module {:extern "SM"} SM {
  class SlotMachineD {
    // Helper method for runtime checks
    static method RuntimeRequires(cond: bool, message: string) 
    {
      expect cond, message;
    }

    static method PlaceBet(balance: real, bet: real) returns (betAmount: real)
      requires bet >= 0.0      // Static verification
      requires balance >= 0.0
      requires bet <= balance  // Static verification
      ensures betAmount == if bet <= balance then bet else balance
    {
        RuntimeRequires(bet >= 0.0, "Invalid bet amount, must be non-negative");  // Runtime check
        RuntimeRequires(balance >= 0.0, "Invalid balance, must be non-negative");  // Runtime check
        RuntimeRequires(bet <= balance, "Bet amount exceeds balance");  // Runtime check

      if bet <= balance {
        betAmount := bet;
      } else {
        betAmount := balance;
      }
    }
  }
}
//#########################################################################################################

module {:extern "BS"} BS 
{
    datatype Coin = heads | tails
    

    class BinarySlotD
    {
        static method RuntimeRequires(cond: bool, message: string) 
        {
            expect cond, message;
        }

        static method CheckWinningCombination(reel : Coin) returns (combination: int)
        ensures combination == (if reel == heads then 1 else 0)
        {
            if reel == heads 
            {
                combination := 1;
            }
            else
            {
                combination := 0;  
            }

            RuntimeRequires(combination == (if reel == heads then 1 else 0), "Invalid combination");
        }

        static method CalculatePayout(combination: int, bet: real) returns (payout: real)
        requires bet >= 0.0
        ensures payout == (if combination == 1 then 2.0 * bet else 0.0) 
        ensures payout >= 0.0
        {
            RuntimeRequires(bet >= 0.0, "Invalid bet amount, must be non-negative");  // Pre-Condition check
            if combination == 1
            {
                payout := 2.0 * bet;
            }
            else
            {
                payout := 0.0;
            }
            RuntimeRequires(payout == (if combination == 1 then 2.0 * bet else 0.0), "Invalid payout calculation");  // Post-Condition check
            RuntimeRequires(payout >= 0.0, "Invalid payout amount, must be non-negative");  // Post-Condition check

        }

    }
}
//#########################################################################################################

module {:extern "TS"} TS
{
    datatype Symbol = cherry | lemon | orange | plum | bell | bar | seven
    datatype CombinationType = Triple | Double | None

    class TripleSlotD
    {
        static method RuntimeRequires(cond: bool, message: string) 
        {
            expect cond, message;
        }

        static method CheckTripleCombination(reel1: Symbol, reel2: Symbol, reel3: Symbol) returns (isTriple: bool)
        ensures isTriple == (if reel1 == reel2 && reel2 == reel3 then true else false)
        {
            if reel1 == reel2 && reel2 == reel3
            {
                isTriple := true;
            }
            else
            {
                isTriple := false;
            }
        }

        static method CheckDoubleCombination(reel1: Symbol, reel2: Symbol, reel3: Symbol) returns (isDouble: bool)
        ensures isDouble == ((reel1 == reel2 && reel2 != reel3) || (reel2 == reel3 && reel3 != reel1) || (reel1 == reel3 && reel3 != reel2))
        {
            if reel1 == reel2 || reel2 == reel3 || reel1 == reel3
            {
                if !(reel1 == reel2 && reel2 == reel3)
                {
                    isDouble := true;
                }
                else
                {
                    isDouble := false;
                }
            }
            else
            {
                isDouble := false;
            }
        }

        static method getCombination(reel: Symbol, combination: int) returns (c: int)
        requires combination == (
            if reel == cherry then 1
            else if reel == lemon then 2
            else if reel == orange then 3
            else if reel == plum then 4
            else if reel == bell then 5
            else if reel == bar then 6
            else 7
        )
            ensures c == combination
        {
            RuntimeRequires(combination == (
                if reel == cherry then 1
                else if reel == lemon then 2
                else if reel == orange then 3
                else if reel == plum then 4
                else if reel == bell then 5
                else if reel == bar then 6
                else 7
            ), "getCombination message: Invalid combination");

            if reel == cherry { c := 1; }
            else if reel == lemon { c := 2; }
            else if reel == orange { c := 3; }
            else if reel == plum { c := 4; }
            else if reel == bell { c := 5; }
            else if reel == bar { c := 6; }
            else { c := 7; }
        }

        static method getMultiplier(combination: int, cType: CombinationType) returns (multiplier: int)
        requires combination == 0 || combination == 1 || combination == 2 || combination == 3 || combination == 4 || combination == 5 || combination == 6 || combination == 7
        requires cType == Triple || cType == Double || cType == None
        ensures multiplier == (
            if cType == Triple then
                if combination == 1 then 10
                else if combination == 2 then 10
                else if combination == 3 then 10
                else if combination == 4 then 10
                else if combination == 5 then 25
                else if combination == 6 then 50
                else if combination == 7 then 100
                else 0
            else if cType == Double then
                if combination == 1 then 2
                else if combination == 2 then 1
                else if combination == 3 then 1
                else if combination == 4 then 1
                else if combination == 5 then 5
                else if combination == 6 then 10
                else if combination == 7 then 15
                else 0
            else if cType == None then 0
            else 0 // Default case
        )
        {
            RuntimeRequires(cType == Triple || cType == Double || cType == None, "getMultiplier message: Invalid combination type");
            RuntimeRequires(combination == 0 || combination == 1 || combination == 2 || combination == 3 || combination == 4 || combination == 5 || combination == 6 || combination == 7, "getMultiplier message: Invalid combination");
            if cType == Triple
            {
                if combination == 1 { multiplier := 10; }
                else if combination == 2 { multiplier := 10; }
                else if combination == 3 { multiplier := 10; }
                else if combination == 4 { multiplier := 10; }
                else if combination == 5 { multiplier := 25; }
                else if combination == 6 { multiplier := 50; }
                else if combination == 7 { multiplier := 100; }
                else { multiplier := 0; } // Default case
            }
            else if cType == Double
            {
                if combination == 1 { multiplier := 2; }
                else if combination == 2 { multiplier := 1; }
                else if combination == 3 { multiplier := 1; }
                else if combination == 4 { multiplier := 1; }
                else if combination == 5 { multiplier := 5; }
                else if combination == 6 { multiplier := 10; }
                else if combination == 7 { multiplier := 15; }
                else { multiplier := 0; } // Default case
            }
            else if cType == None
            {
                multiplier := 0; // Default case
            }
            else
            {
                multiplier := 0; // Default case
            }
        
        }

        static method CalculatePayout(bet: real, multiplier: int) returns (payout: real)
        requires bet >= 0.0
        requires multiplier >= 0
        ensures payout >= 0.0
        {
            RuntimeRequires(bet >= 0.0, "Invalid bet amount, must be non-negative");  // Pre-Condition check
            RuntimeRequires(multiplier >= 0, "Invalid multiplier, must be non-negative");  // Pre-Condition check

            payout := bet * (multiplier as real);
        }
    }
}
