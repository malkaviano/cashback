using Domain.Interfaces;

namespace Domain.Cashback
{
    /*
        Para até 1.000 reais em compras, o revendedor(a) receberá 10% de cashback do valor vendido no período;
        Entre 1.000 e 1.500 reais em compras, o revendedor(a) receberá 15% de cashback do valor vendido no período;
        Acima de 1.500 reais em compras, o revendedor(a) receberá 20% de cashback do valor vendido no período.
    */

    public class CashbackDefaultStrategy : ICashbackStrategy
    {
        public (int percentage, decimal cashback) CashbackValue(decimal value)
        {
            int percentage;

            if (value <= 1000)
            {
                percentage = 10;
            }
            else if (value > 1000 && value <= 1500)
            {
                percentage = 15;
            }
            else
            {
                percentage = 20;
            }

            var factor = (percentage / 100m);

            return (percentage: percentage, cashback: value * factor);
        }
    }
}