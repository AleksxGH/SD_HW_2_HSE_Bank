using Domain.Entities.Interfaces;
using Domain.Enums;

namespace Application.Facades.Interfaces
{
    /// <summary>
    /// Интерфейс фасада для управления операциями.
    /// </summary>
    public interface IOperationsFacade
    {
        /// <summary>
        /// Метод применения операции
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        bool ApplyOperation(IOperation operation);

        /// <summary>
        /// Метод создания новой операции
        /// </summary>
        /// <param name="name"></param>
        /// <param name="initialBalance"></param>
        /// <returns></returns>
        IOperation CreateOperation(string name, decimal initialBalance);

        /// <summary>
        /// Метод восстановления операции
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="balance"></param>
        /// <returns></returns>
        IOperation RestoreOperation(Guid id, string name, decimal balance);

        /// <summary>
        /// Метод получения операции по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IOperation? GetOperationById(Guid id);

        /// <summary>
        /// Метод получения всех операций
        /// </summary>
        /// <returns></returns>
        IEnumerable<IOperation> GetAllOperations();

        /// <summary>
        /// Метод получения всех операций по идентификатору банковского счета
        /// </summary>
        /// <param name="bankAccountId"></param>
        /// <returns></returns>
        IEnumerable<IOperation> GetOperationsByAccountId(Guid bankAccountId);

        /// <summary>
        /// Метод обновления типа операции
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newName"></param>
        void UpdateOperationType(Guid id, OperationType newName);

        /// <summary>
        /// Метод обновления идентификатора банковского счета операции
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="bankAccountId"></param>

        void UpdateOperationAccountId(Guid operationId, Guid bankAccountId);

        /// <summary>
        /// Метод обновления суммы операции
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newAmount"></param>
        void UpdateOperationAmount(Guid id, decimal newAmount);

        /// <summary>
        /// Метод обновления даты операции
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newDate"></param>
        void UpdateOperationDate(Guid id, DateTime newDate);

        /// <summary>
        /// Метод обновления категории операции
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newCategoryId"></param>
        void UpdateOperationCategoryId(Guid id, Guid newCategoryId);

        /// <summary>
        /// Метод обновления описания операции
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newDescription"></param>
        void UpdateOperationDescription(Guid id, string? newDescription);

        /// <summary>
        /// Метод для удаления операции из репозиторияы
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteOperation(Guid id);
    }
}
