using Domain.Entities.Interfaces;

namespace Application.Facades.Interfaces
{
    /// <summary>
    /// Интерфейс фасада для управления банковскими счетами.
    /// </summary>
    public interface IBankAccountsFacade
    {
        /// <summary>
        /// Метод создания нового банковского счета.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="initialBalance"></param>
        /// <returns></returns>
        IBankAccount CreateBankAccount(string name, decimal initialBalance);

        /// <summary>
        /// Метод восстановления банковского счета.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="balance"></param>
        /// <returns></returns>
        IBankAccount RestoreBankAccount(Guid id, string name, decimal balance);

        /// <summary>
        /// Метод получения банковского счета по идентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IBankAccount? GetBankAccountById(Guid id);

        /// <summary>
        /// Метод получения банковского счета по данным о нем.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IBankAccount? TryGetBankAccount(string name);

        /// <summary>
        /// Метод получения всех банковских счетов.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IBankAccount> GetAllBankAccounts();

        /// <summary>
        /// Метод обновления названия банковского счета.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newName"></param>
        void UpdateBankAccountName(Guid id, string newName);

        /// <summary>
        /// Метод обновления баланса банковского счета.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newBalance"></param>
        void UpdateBankAccountBalance(Guid id, decimal newBalance);

        /// <summary>
        /// Метод удаления банковского счета.
        /// </summary>
        /// <param name="id"></param>
        bool DeleteBankAccount(Guid id);
    }
}
