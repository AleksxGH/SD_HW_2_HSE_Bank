using Domain.Factories.Interfaces;
using Domain.Entities.Interfaces;
using Domain.Entities;

namespace Domain.Factories
{
    /// <summary>
    /// Класс-фабрика для создания и восстановления банковских счетов
    /// </summary>
    public class BankAccountFactory : IBankAccountFactory
    {
        /// <summary>
        /// Создать новый банковский счёт
        /// </summary>
        /// <param name="name">Название счёта</param>
        /// <param name="balance">Баланс</param>
        public IBankAccount CreateAccount(string name, decimal balance)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Название счёта не может быть пустым.", nameof(name));
            }
            return new BankAccount(name, balance);
        }

        /// <summary>
        /// Восстановить банковский счёт из данных
        /// </summary>
        /// <param name="id">Идентификатор счёта</param>
        /// <param name="name">Название счёта</param>
        /// <param name="balance">Баланс</param>
        public IBankAccount RestoreAccount(Guid id, string name, decimal balance)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Идентификатор банковского счета некорректен.", nameof(id));
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Название счёта не может быть пустым.", nameof(name));
            }
            return new BankAccount(id, name, balance);
        }
    }
}
