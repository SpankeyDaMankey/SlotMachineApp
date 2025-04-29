

public enum Symbol
{
    cherry,
    lemon,
    orange,
    plum,
    bell,
    bar,
    seven
}

public enum CombinationType
{
    triple,
    doublet,
    none
}

public class CombinationTable
{
    private Dictionary<Symbol, int> table_combination;
    private Dictionary<int, int> table_multiplier_triple;
    private Dictionary<int, int> table_multiplier_double;


    public CombinationTable()
    {
        table_combination = new Dictionary<Symbol, int>();

        table_multiplier_triple = new Dictionary<int, int>();
        table_multiplier_double = new Dictionary<int, int>();

        addTableCombination();

        addTableMultiplierTriple();
        addTableMultiplierDouble();
    }

    private void addTableCombination()
    {
        table_combination.Add(Symbol.cherry, 1);
        table_combination.Add(Symbol.lemon, 2);
        table_combination.Add(Symbol.orange, 3);
        table_combination.Add(Symbol.plum, 4);
        table_combination.Add(Symbol.bell, 5);
        table_combination.Add(Symbol.bar, 6);
        table_combination.Add(Symbol.seven, 7);
    }

    private void addTableMultiplierTriple()
    {
        table_multiplier_triple.Add(1, 5);
        table_multiplier_triple.Add(2, 5);
        table_multiplier_triple.Add(3, 5);
        table_multiplier_triple.Add(4, 5);
        table_multiplier_triple.Add(5, 10);
        table_multiplier_triple.Add(6, 15);
        table_multiplier_triple.Add(7, 25);
    }

    private void addTableMultiplierDouble()
    {
        table_multiplier_double.Add(1, 2);
        table_multiplier_double.Add(2, 1);
        table_multiplier_double.Add(3, 1);
        table_multiplier_double.Add(4, 1);
        table_multiplier_double.Add(5, 3);
        table_multiplier_double.Add(6, 4);
        table_multiplier_double.Add(7, 7);
    }

    public int getCombination(Symbol symbol)
    {
        return table_combination[symbol];
    }

    public int getMultiplierTriple(int combination)
    {
        return table_multiplier_triple[combination];
    }

    public int getMultiplierDouble(int combination)
    {
        return table_multiplier_double[combination];
    }
}