using Domain.Interfaces;
using Domain.Models;
using System;

namespace Domain.Cashback
{
    /*
        Para até 1.000 reais em compras, o revendedor(a) receberá 10% de cashback do valor vendido no período;
        Entre 1.000 e 1.500 reais em compras, o revendedor(a) receberá 15% de cashback do valor vendido no período;
        Acima de 1.500 reais em compras, o revendedor(a) receberá 20% de cashback do valor vendido no período.
    */

    public class CashbackDefaultStrategy : ICashbackStrategy
    {
        private readonly DateTime startDate;
        private readonly DateTime endDate;

        public CashbackDefaultStrategy(DateTime startDate, DateTime endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public void Apply(Sales sales)
        {
            if (sales.Data < startDate || sales.Data > endDate)
            {
                // Avoids update inconcistencies, also fraud
                sales.CashbackValue = 0;
                sales.CashbackPercentage = 0;
                return;
            }

            int percentage;
            decimal value = sales.Value;

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

            sales.CashbackValue = value * factor;
            sales.CashbackPercentage = percentage;
        }
    }
}