using Application.Facades.Interfaces;
using Domain.Entities.Interfaces;
using Domain.Factories.Interfaces;
using Infrastructure.Repositories.Interfaces;

namespace Application.Facades
{
    public class BankAccountsFacade : IBankAccountsFacade
    {
        /// <summary>
        /// Фабрика банковских счетов
        /// </summary>
        private readonly IBankAccountsFactory _factory;

        /// <summary>
        /// Репозиторий банковских счетов
        /// </summary>
        private readonly IBankAccountsRepository _repository;

        /// <summary>
        /// Конструктор фасада банковских счетов
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="repository"></param>
        public BankAccountsFacade(IBankAccountsFactory factory, IBankAccountsRepository repository)
        {
            _factory = factory;
            _repository = repository;
        }

        /// <summary>
        /// Метод создания нового банковского счета.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="initialBalance"></param>
        /// <returns></returns>
        public IBankAccount CreateBankAccount(string name, decimal initialBalance)
        {
            var account = _factory.CreateAccount(name, initialBalance);
            _repository.Add(account);
            return account;
        }

        /// <summary>
        /// Метод восстановления банковского счета.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="balance"></param>
        /// <returns></returns>
        public IBankAccount RestoreBankAccount(Guid id, string name, decimal balance)
        {
            var account = _factory.RestoreAccount(id, name, balance);
            _repository.Add(account);
            return account;
        }

        /// <summary>
        /// Метод получения банковского счета по идентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBankAccount? GetBankAccountById(Guid id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// Метод получения банковского счета по данным о нем.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBankAccount? TryGetBankAccount(string data)
        {
            return _repository.TryGet(data);
        }

        /// <summary>
        /// Метод получения всех банковских счетов.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IBankAccount> GetAllBankAccounts()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Метод обновления названия банковского счета.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newName"></param>
        public void UpdateBankAccountName(Guid id, string newName)
        {
            var account = _repository.GetById(id);
            if (account != null)
            {
                account.Rename(newName);
                _repository.Update(account);
                return;
            }
            throw new ArgumentException("Счет не найден.");
        }

        /// <summary>
        /// Метод обновления баланса банковского счета.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newBalance"></param>
        public void UpdateBankAccountBalance(Guid id, decimal newBalance)
        {
            var account = _repository.GetById(id);
            if (account != null)
            {
                account.UpdateBalance(newBalance);
                _repository.Update(account);
                return;
            }
            throw new ArgumentException("Счет не найден.");
        }

        /// <summary>
        /// Метод удаления банковского счета.
        /// </summary>
        /// <param name="id"></param>
        public bool DeleteBankAccount(Guid id)
        {
            if (!_repository.Exists(id))
            {
                return false;
            }
            _repository.Delete(id);
            return true;
        }
    }
}
