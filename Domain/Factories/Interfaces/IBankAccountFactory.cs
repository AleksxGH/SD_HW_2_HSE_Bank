using Domain.Entities.Interfaces;

namespace Domain.Factories.Interfaces
{
    /// <summary>
    /// Интерфейс фабрики банковских счетов
    /// </summary>
    public interface IBankAccountFactory
    {
        /// <summary>
        /// Создать новый банковский счёт
        /// </summary>
        /// <param name="name">Название счёта</param>
        /// <param name="balance">Баланс</param>
        IBankAccount CreateAccount(string name, decimal balance);

        /// <summary>
        /// Восстановить банковский счёт из данных
        /// </summary>
        /// <param name="id">Идентификатор счёта</param>
        /// <param name="name">Название счёта</param>
        /// <param name="balance">Баланс</param>
        IBankAccount RestoreAccount(Guid id, string name, decimal balance);
    }
}
