using Domain.Entities.Interfaces;

namespace Domain.Entities
{
    /// <summary>
    /// Класс банковского счета.
    /// </summary>
    public class BankAccount : IBankAccount
    {
        /// <summary>
        /// Уникальный идентификатор счёта.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Название счёта.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Текущий баланс счёта (может быть отрицательным).
        /// </summary>
        public decimal Balance { get; private set; }

        /// <summary>
        /// Конструктор создания банковского счёта.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="initialBalance"></param>
        public BankAccount(string name, decimal initialBalance = 0)
        {
            Id = Guid.NewGuid();
            Name = name;
            Balance = initialBalance;
        }

        /// <summary>
        /// Конструктор восстановления банковского счёта.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="initialBalance"></param>
        public BankAccount(Guid id, string name, decimal initialBalance = 0)
        {
            Id = id;
            Name = name;
            Balance = initialBalance;
        }

        /// <summary>
        /// Обновляет баланс счёта.
        /// </summary>
        /// <param name="newBalance">Новое значение баланса.</param>
        public void UpdateBalance(decimal newBalance)
        {
            Balance = newBalance;
        }

        /// <summary>
        /// Переименовывает счёт.
        /// </summary>
        /// <param name="newName">Новое имя счёта.</param>
        public void Rename(string newName)
        {
            Name = newName;
        }

        /// <summary>
        /// Вносит деньги на счёт.
        /// </summary>
        /// <param name="amount">Сумма пополнения.</param>
        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        /// <summary>
        /// Списывает деньги со счёта.
        /// </summary>
        /// <param name="amount">Сумма списания.</param>
        public void Withdraw(decimal amount)
        {
            Balance -= amount;
        }
    }
}
