using Domain.Entities.Interfaces;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using System.Xml.Linq;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Класс репозитория банковских счетов в памяти
    /// </summary>
    public class InMemoryBankAccountsRepository : IBankAccountsRepository
    {
        /// <summary>
        /// Контейнер для хранения банковских счётов в виде словаря
        /// </summary>
        private readonly Dictionary<Guid, IBankAccount> _accounts;

        /// <summary>
        /// Конструктор репозитория банковских счетов в памяти
        /// </summary>
        public InMemoryBankAccountsRepository()
        {
            _accounts =[];
        }

        /// <summary>
        /// Метод добавления банковского счёта в репозиторий
        /// </summary>
        /// <param name="bankAccount"></param>
        public void Add(IBankAccount bankAccount)
        {
            _accounts[bankAccount.Id] = bankAccount;
        }

        /// <summary>
        /// Метод обновления банковского счёта в репозитории
        /// </summary>
        /// <param name="bankAccount"></param>
        public void Update(IBankAccount bankAccount)
        {
            _accounts[bankAccount.Id] = bankAccount;
        }

        /// <summary>
        /// Метод удаления банковского счёта из репозитория
        /// </summary>
        /// <param name="bankAccountId"></param>
        public void Delete(Guid bankAccountId)
        {
            _accounts.Remove(bankAccountId);
        }

        /// <summary>
        /// Метод получения банковского счёта по идентификатору
        /// </summary>
        /// <param name="bankAccountId"></param>
        /// <returns></returns>
        public IBankAccount? GetById(Guid bankAccountId)
        {
            _ = _accounts.TryGetValue(bankAccountId, out var account);
            return account;
        }

        /// <summary>
        /// Метод попытки получения банковского счёта по данным
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IBankAccount? TryGet(string data)
        {
            _ = Guid.TryParse(data, out var id);
            var account = GetById(id);
            if (account != null)
            {
                return account;
            }
            return _accounts.Values.FirstOrDefault(acc => acc.Name.Equals(data, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Метод получения всех банковских счётов из репозитория
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IBankAccount> GetAll()
        {
            return _accounts.Values;
        }

        /// <summary>
        /// Метод проверки существования банковского счёта по идентификатору
        /// </summary>
        /// <param name="bankAccountId"></param>
        /// <returns></returns>
        public bool Exists(Guid bankAccountId)
        {
            if (bankAccountId == Guid.Empty)
            {
                return false;
            }
            if (_accounts.ContainsKey(bankAccountId))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод сохранения изменений в репозитории
        /// </summary>
        public void SaveChanges()
        {
            // В данном случае, так как репозиторий в памяти, изменения сохранять не нужно. 
        }
    }
}
