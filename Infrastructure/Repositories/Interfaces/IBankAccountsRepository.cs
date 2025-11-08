using Domain.Entities.Interfaces;

namespace Infrastructure.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория банковских счетов
    /// </summary>
    public interface IBankAccountsRepository
    {
        /// <summary>
        /// Метод добавления банковского счёта в репозиторий
        /// </summary>
        /// <param name="bankAccount"></param>
        void Add(IBankAccount bankAccount);

        /// <summary>
        /// Метод обновления банковского счёта в репозитории
        /// </summary>
        /// <param name="bankAccount"></param>
        void Update(IBankAccount bankAccount);

        /// <summary>
        /// Метод удаления банковского счёта из репозитория
        /// </summary>
        /// <param name="bankAccountId"></param>
        void Delete(Guid bankAccountId);

        /// <summary>
        /// Метод получения банковского счёта по идентификатору
        /// </summary>
        /// <param name="bankAccountId"></param>
        /// <returns></returns>
        IBankAccount? GetById(Guid bankAccountId);

        /// <summary>
        /// Метод попытки получения банковского счёта по данным
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        IBankAccount? TryGet(string data);

        /// <summary>
        /// Метод получения всех банковских счётов из репозитория
        /// </summary>
        /// <returns></returns>
        IEnumerable<IBankAccount> GetAll();

        /// <summary>
        /// Метод проверки существования банковского счёта по идентификатору
        /// </summary>
        /// <param name="bankAccountId"></param>
        /// <returns></returns>
        bool Exists(Guid bankAccountId);

        /// <summary>
        /// Метод сохранения изменений в репозитории
        /// </summary>
        void SaveChanges();
    }
}
