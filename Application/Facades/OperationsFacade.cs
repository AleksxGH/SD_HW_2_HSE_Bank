using Domain.Entities.Interfaces;
using Domain.Factories.Interfaces;
using Domain.Enums;
using Infrastructure.Repositories.Interfaces;
using Application.Facades.Interfaces;
using Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Facades
{
    public class OperationsFacade : IOperationsFacade
    {
        /// <summary>
        /// Фабрика банковских операций
        /// </summary>
        private readonly IOperationsFactory _factory;

        /// <summary>
        /// Репозиторий банковских операций
        /// </summary>
        private readonly IOperationsRepository _operationsRepository;

        /// <summary>
        /// Репозиторий банковских счетов
        /// </summary>
        private readonly IBankAccountsRepository _accountsRepository;

        /// <summary>
        /// Репозиторий категорий банковских операций
        /// </summary>
        private readonly ICategoriesRepository _categoriesRepository;

        /// <summary>
        /// Конструктор фасада банковских операций
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="operationsRepository"></param>
        /// <param name="accountsRepository"></param>
        /// <param name="categoriesRepository"></param>
        public OperationsFacade(IOperationsFactory factory, IOperationsRepository operationsRepository, 
            IBankAccountsRepository accountsRepository, ICategoriesRepository categoriesRepository)
        {
            _factory = factory;
            _operationsRepository = operationsRepository;
            _accountsRepository = accountsRepository;
            _categoriesRepository = categoriesRepository;
        }

        /// <summary>
        /// Метод применения операции
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public bool ApplyOperation(IOperation operation)
        {
            var account = _accountsRepository.GetById(operation.BankAccountId);
            if (account == null) return false;
            if (operation.Type == OperationType.Income)
            {
                account.Deposit(operation.Amount);
            }
            else
            {
                account.Withdraw(operation.Amount);
            }
            _accountsRepository.Update(account);
            return true;
        }

        /// <summary>
        /// Метод создания новой операции
        /// </summary>
        /// <param name="name"></param>
        /// <param name="initialBalance"></param>
        /// <returns></returns>
        public IOperation CreateOperation(OperationType type, Guid accountId, decimal amount, Guid categoryId, string? description = null)
        {
            if (!_accountsRepository.Exists(accountId))
                throw new ArgumentException("Банковский счет не найден.");
            if (!_categoriesRepository.Exists(categoryId))
                throw new ArgumentException("Категория не найдена.");
            var operation = _factory.CreateOperation(type, accountId, amount, categoryId, description);
            _operationsRepository.Add(operation);
            ApplyOperation(operation);
            return operation;
        }

        /// <summary>
        /// Метод восстановления операции
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="balance"></param>
        /// <returns></returns>
        public IOperation RestoreOperation(Guid id, OperationType type, Guid accountId, decimal amount, 
            DateTime date, Guid categoryId, string? description = null)
        {
            if (!_accountsRepository.Exists(accountId))
                throw new ArgumentException("Банковский счет не найден.");
            if (!_categoriesRepository.Exists(categoryId))
                throw new ArgumentException("Категория не найдена.");
            var operation = _factory.RestoreOperation(id, type, accountId, amount, date, categoryId, description);
            _operationsRepository.Add(operation);
            return operation;
        }

        /// <summary>
        /// Метод получения операции по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IOperation? GetOperationById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }
            return _operationsRepository.GetById(id);
        }

        /// <summary>
        /// Метод получения всех операций
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOperation> GetAllOperations()
        {
            return _operationsRepository.GetAll();
        }

        /// <summary>
        /// Метод получения всех операций по идентификатору банковского счета
        /// </summary>
        /// <param name="bankAccountId"></param>
        /// <returns></returns>
        public IEnumerable<IOperation> GetOperationsByAccountId(Guid bankAccountId)
        {
            if (bankAccountId == Guid.Empty)
                throw new ArgumentException("Идентификатор банковского счета некорректен.");
            if (_accountsRepository.GetById(bankAccountId) == null)
                throw new ArgumentException("Банковский счет не найден.");

            return _operationsRepository.GetOperationsByBankAccountId(bankAccountId);
        }

        private void RecalculateBankAccountBalance(Guid bankAccountId)
        {
            var account = _accountsRepository.GetById(bankAccountId) ?? throw new ArgumentException($"Счет не найден.");
            var operations = _operationsRepository.GetOperationsByBankAccountId(bankAccountId);

            decimal newBalance = 0;
            foreach (var operation in operations)
            {
                if (operation.Type == OperationType.Income)
                    newBalance += operation.Amount;
                else if (operation.Type == OperationType.Expense)
                    newBalance -= operation.Amount;
            }

            account.UpdateBalance(newBalance);
            _accountsRepository.Update(account);
        }


        /// <summary>
        /// Метод обновления типа операции
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newName"></param>
        public void UpdateOperationType(Guid id, OperationType newName)
        {
            var operation = _operationsRepository.GetById(id);
            if (operation != null)
            {
                operation.ChangeType(newName);
                _operationsRepository.Update(operation);
                RecalculateBankAccountBalance(operation.BankAccountId);
                return;
            }
            throw new ArgumentException("Операция не найдена.");

        }

        /// <summary>
        /// Метод обновления идентификатора банковского счета операции
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="bankAccountId"></param>

        public void UpdateOperationAccountId(Guid operationId, Guid bankAccountId)
        {
            if (!_accountsRepository.Exists(bankAccountId))
                throw new ArgumentException("Банковский счет не найден.");

            var operation = _operationsRepository.GetById(operationId);
            if (operation != null)
            {
                var oldBankAccountId = operation.BankAccountId;
                operation.ChangeBankAccountId(bankAccountId);
                _operationsRepository.Update(operation);
                RecalculateBankAccountBalance(oldBankAccountId);
                RecalculateBankAccountBalance(operation.BankAccountId);
                return;
            }
            throw new ArgumentException("Операция не найдена.");
        }

        /// <summary>
        /// Метод обновления суммы операции
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newAmount"></param>
        public void UpdateOperationAmount(Guid id, decimal newAmount)
        {
            if (newAmount < 0)
                throw new ArgumentException("Сумма операции не может быть отрицательной.");

            var operation = _operationsRepository.GetById(id);
            if (operation != null)
            {
                operation.ChangeAmount(newAmount);
                _operationsRepository.Update(operation);
                RecalculateBankAccountBalance(operation.BankAccountId);
                return;
            }
            throw new ArgumentException("Операция не найдена.");
        }

        /// <summary>
        /// Метод обновления даты операции
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newDate"></param>
        public void UpdateOperationDate(Guid id, DateTime newDate)
        {
            if (newDate > DateTime.Now)
                throw new ArgumentException("Дата операции некорректна.");

            var operation = _operationsRepository.GetById(id);
            if (operation != null)
            {
                operation.ChangeDate(newDate);
                _operationsRepository.Update(operation);
                return;
            }
            throw new ArgumentException("Операция не найдена.");
        }

        /// <summary>
        /// Метод обновления категории операции
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newCategoryId"></param>
        public void UpdateOperationCategoryId(Guid id, Guid newCategoryId)
        {
            if (!_categoriesRepository.Exists(newCategoryId))
                throw new ArgumentException("Категория не найдена.");

            var operation = _operationsRepository.GetById(id);
            if (operation != null)
            {
                operation.ChangeCategoryId(newCategoryId);
                _operationsRepository.Update(operation);
                return;
            }
            throw new ArgumentException("Операция не найдена.");
        }

        /// <summary>
        /// Метод обновления описания операции
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newDescription"></param>
        public void UpdateOperationDescription(Guid id, string? newDescription)
        {
            var operation = _operationsRepository.GetById(id);
            if (operation != null)
            {
                operation.ChangeDescription(newDescription);
                _operationsRepository.Update(operation);
                return;
            }
            throw new ArgumentException("Операция не найдена.");
        }

        /// <summary>
        /// Метод для удаления операции из репозиторияы
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteOperation(Guid id)
        {
            if (_operationsRepository.Exists(id))
            {
                _operationsRepository.Delete(id);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод проверки существования операции по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool OperationExists(Guid id)
        {
            return _operationsRepository.Exists(id);
        }

        /// <summary>
        /// Метод проверки существования банковского счета
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IBankAccount? TryGetBankAccount(string data)
        {
            return _accountsRepository.TryGet(data);
        }

        /// <summary>
        /// Метод проверки существования категории
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ICategory? TryGetCategory(string data)
        {
            return _categoriesRepository.TryGet(data);
        }
    }
}
