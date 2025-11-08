using Domain.Factories.Interfaces;
using Domain.Entities.Interfaces;
using Domain.Entities;
using Domain.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Factories
{
    /// <summary>
    /// Класс-фабрика для создания и восстановления банковских операций
    /// </summary>
    public class OperationFactory : IOperationFactory
    {
        /// <summary>
        /// Метод создания новой операции
        /// </summary>
        /// <param name="type"></param>
        /// <param name="accountId"></param>
        /// <param name="amount"></param>
        /// <param name="categoryId"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public IOperation CreateOperation(OperationType type, Guid accountId, decimal amount, Guid categoryId, string? description = null)
        {
            if (accountId == Guid.Empty)
            {
                throw new ArgumentException("Идентификатор банковского счета некорректен.", nameof(accountId));
            }
            if (amount <= 0)
            {
                throw new ArgumentException("Сумма операции должна быть положительной.", nameof(amount));
            }
            if (categoryId == Guid.Empty)
            {
                throw new ArgumentException("Идентификатор категории некорректен.", nameof(categoryId));
            }
            return new Operation(type, accountId, amount, categoryId, description);
        }

        /// <summary>
        /// Метод восстановления операции
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="accountId"></param>
        /// <param name="amount"></param>
        /// <param name="date"></param>
        /// <param name="categoryId"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public IOperation RestoreOperation(Guid id, OperationType type, Guid accountId, decimal amount,
            DateTime date, Guid categoryId, string? description = null)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Идентификатор опeрации некорректен.", nameof(id));
            }
            if (accountId == Guid.Empty)
            {
                throw new ArgumentException("Идентификатор банковского счета некорректен.", nameof(accountId));
            }
            if (amount <= 0)
            {
                throw new ArgumentException("Сумма операции должна быть положительной.", nameof(amount));
            }
            if (date > DateTime.Now)
            {
                throw new ArgumentException("Дата операции некорректна.", nameof(date));
            }
            if (categoryId == Guid.Empty)
            {
                throw new ArgumentException("Идентификатор категории некорректен.", nameof(categoryId));
            }
            return new Operation(id, type, accountId, amount, date, categoryId, description);
        }
    }
}
