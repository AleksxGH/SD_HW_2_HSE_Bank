using Domain.Entities.Interfaces;

namespace Infrastructure.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория банковских операций
    /// </summary>
    public interface IOperationsRepository
    {
        /// <summary>
        /// Метод добавления операции в репозиторий
        /// </summary>
        /// <param name="operation"></param>
        void Add(IOperation operation);

        /// <summary>
        /// Метод обновления операции в репозитории
        /// </summary>
        /// <param name="operation"></param>
        void Update(IOperation operation);

        /// <summary>
        /// Метод удаления операции из репозитория
        /// </summary>
        /// <param name="operationId"></param>
        void Delete(Guid operationId);

        /// <summary>
        /// Метод получения операции по идентификатору
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        IOperation? GetById(Guid operationId);

        /// <summary>
        /// Метод проверки существования операции по идентификатору
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        bool Exists(Guid operationId);

        /// <summary>
        /// Метод получения всех операций из репозитория
        /// </summary>
        /// <returns></returns>
        IEnumerable<IOperation> GetAll();

        /// <summary>
        /// Метод получения всех операций по идентификатору банковского счёта
        /// </summary>
        /// <param name="bankAccountId"></param>
        /// <returns></returns>
        IEnumerable<IOperation> GetOperationsByBankAccountId(Guid bankAccountId);

        /// <summary>
        /// Метод сохранения изменений в репозитории
        /// </summary>
        void SaveChanges();

    }
}
