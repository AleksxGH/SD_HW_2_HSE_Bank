using Domain.Entities.Interfaces;
using Domain.Enums;

namespace Domain.Factories.Interfaces
{
    public interface IOperationFactory
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
        IOperation CreateOperation(OperationType type, Guid accountId, decimal amount, Guid categoryId, string? description = null);

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
        IOperation RestoreOperation(Guid id, OperationType type, Guid accountId, decimal amount, 
            DateTime date, Guid categoryId, string? description = null);
    }
}
