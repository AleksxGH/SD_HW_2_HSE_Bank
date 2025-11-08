namespace Domain.Entities.Interfaces
{
    public interface IBankAccount
    {
        /// <summary>
        /// Интерфейс банковского счёта.
        /// Определяет базовые операции для управления балансом и данными счёта.
        /// </summary>
        public interface IBankAccount
        {
            /// <summary>
            /// Уникальный идентификатор счёта.
            /// </summary>
            Guid Id { get; }

            /// <summary>
            /// Название счёта.
            /// </summary>
            string Name { get; }

            /// <summary>
            /// Текущий баланс счёта (может быть отрицательным).
            /// </summary>
            decimal Balance { get; }

            /// <summary>
            /// Обновляет баланс счёта.
            /// </summary>
            /// <param name="newBalance">Новое значение баланса.</param>
            void UpdateBalance(decimal newBalance);

            /// <summary>
            /// Переименовывает счёт.
            /// </summary>
            /// <param name="newName">Новое имя счёта.</param>
            void Rename(string newName);

            /// <summary>
            /// Вносит деньги на счёт.
            /// </summary>
            /// <param name="amount">Сумма пополнения.</param>
            void Deposit(decimal amount);

            /// <summary>
            /// Списывает деньги со счёта.
            /// </summary>
            /// <param name="amount">Сумма списания.</param>
            void Withdraw(decimal amount);
        }
    }
}
