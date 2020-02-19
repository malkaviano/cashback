using Domain.Models;

namespace Domain.Interfaces
{
    public interface ICashbackStrategy
    {
        void Apply(Sales sales);
    }
}