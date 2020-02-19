namespace Domain.Interfaces
{
    public interface ICashbackStrategy
    {
        (int percentage, decimal cashback) CashbackValue(decimal value);
    }
}