using System;

namespace BarrierOption
{
    class Program
    {
        static void Main(string[] args)
        {
            var barrier = new BarrierOption(1, 0.01, 0.2, 95, 100, 150, Type.call, Knock.Out);
            Console.WriteLine(barrier.Price);
        }
    }

    class BarrierOption
    {
        private double maturity;
        private double rate;
        private double volatility;
        private double strike;
        private double initialStock;
        private double barrier;
        private Type type;
        private Knock knock;

        public double Price { get; private set; }
        public double Delta { get; private set; }
        public double Gamma { get; private set; }
        public double Theta { get; private set; }
        public double Vega { get; private set; }

        public BarrierOption(double maturity, double rate, double volatility, double strike,
            double initialStock, double barrier, Type type, Knock knock)
        {
            this.maturity = maturity;
            this.rate = rate;
            this.volatility = volatility;
            this.strike = strike;
            this.initialStock = initialStock;
            this.barrier = barrier;
            this.type = type;
            this.knock = knock;

            Price = BarrierOptionPricer();
        }

        private double BarrierOptionPricer()
        {
            var res = 0.0;
            return res;
        }
	}

    enum Type
    {
        call,
        put
    }
    enum Knock
    {
        In,
        Out
    }
}
