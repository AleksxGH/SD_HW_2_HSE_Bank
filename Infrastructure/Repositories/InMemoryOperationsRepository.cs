using Domain.Entities.Interfaces;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Класс репозитория операций, реализующий хранение в памяти
    /// </summary>
    public class InMemoryOperationsRepository : IOperationsRepository
    {
        private readonly List<IOperation> _operations;
        public InMemoryOperationsRepository()
        {
            _operations = [];
        }

        /// <summary>
        /// Метод добавления операции в репозиторий
        /// </summary>
        /// <param name="operation"></param>
        public void Add(IOperation operation)
        {
            _operations.Add(operation);
        }

        /// <summary>
        /// Метод обновления операции в репозитории
        /// </summary>
        /// <param name="operation"></param>
        public void Update(IOperation operation)
        {
            var index = _operations.FindIndex(op => op.Id == operation.Id);
            if (index != -1)
            {
                _operations[index] = operation;
            }
            else
            {
                Add(operation);
            }
        }

        /// <summary>
        /// Метод удаления операции из репозитория
        /// </summary>
        /// <param name="operationId"></param>
        public void Delete(Guid operationId)
        {
            var index = _operations.FindIndex(op => op.Id == operationId);
            if (index != -1)
            {
                _operations.RemoveAt(index);
            }
        }

        /// <summary>
        /// Метод получения операции по идентификатору
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public IOperation? GetById(Guid operationId)
        {
            return _operations.FirstOrDefault(op => op.Id == operationId);
        }

        /// <summary>
        /// Метод проверки существования операции по идентификатору
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public bool Exists(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                return false;
            }
            if (GetById(operationId) != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод получения всех операций из репозитория
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOperation> GetAll()
        {
            return _operations;
        }

        /// <summary>
        /// Метод получения всех операций по идентификатору банковского счёта
        /// </summary>
        /// <param name="bankAccountId"></param>
        /// <returns></returns>
        public IEnumerable<IOperation> GetOperationsByBankAccountId(Guid bankAccountId)
        {
            var operations = _operations.Where(op => op.BankAccountId == bankAccountId);
            return operations;
        }

        /// <summary>
        /// Метод сохранения изменений в репозитории
        /// </summary>
        public void SaveChanges()
        {
            // В In-Memory репозитории нет необходимости сохранять изменения
        }
    }
}
