using System;

namespace SlotMachineApp.SlotMachine
{
    public class SlotSymbol
    {
        public char Symbol { get; }

        public string Name { get; }

        public float Coefficient { get; }

        public float Probability { get; }

        public SlotSymbol(char symbol, string name, float coefficient, float probability)
        {
            this.Symbol = symbol;
            this.Name = name;
            this.Coefficient = coefficient;
            this.Probability = probability;
        }
    }
}