using System;
using MathNet.Numerics.Distributions;

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
        private double dividend;
        private Type type;
        private Knock knock;

        public double Price { get; private set; }
        public double Delta { get; private set; }
        public double Gamma { get; private set; }
        public double Theta { get; private set; }
        public double Vega { get; private set; }

        public BarrierOption(double maturity, double rate, double volatility, double strike,
            double initialStock, double barrier, Type type, Knock knock, double dividend = 0)
        {
            this.maturity = maturity;
            this.rate = rate;
            this.volatility = volatility;
            this.strike = strike;
            this.initialStock = initialStock;
            this.barrier = barrier;
            this.dividend = dividend;
            this.type = type;
            this.knock = knock;

            Price = BarrierOptionPricer();
        }

        private double BarrierOptionPricer()
        {
            Func<double, double> CDF = x => Normal.CDF(0, 1, x);
            Func<double, double> PDF = x => Normal.PDF(0, 1, x);

            var res = 0.0;
            var dfq = Math.Exp(-dividend * maturity);
            var dfr = Math.Exp(-rate * maturity);
            var z = volatility * Math.Sqrt(maturity);
            var l = (rate - dividend + Math.Pow(volatility, 2) / 2) / Math.Pow(volatility, 2);
            var y = Math.Log(Math.Pow(barrier, 2) / (initialStock * strike)) / z + l * z;

            if (barrier < initialStock & type == Type.call & barrier < strike)
            {
                switch (knock)
                {
                    case Knock.In: res = initialStock * dfq * Math.Pow(barrier / initialStock, 2 * l) * CDF(y)
                            - strike * dfr * Math.Pow(barrier / initialStock, 2 * l - 2) * CDF(y + z);
                        break;
                    case Knock.Out:

                }
            }
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
