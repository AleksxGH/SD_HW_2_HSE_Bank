using Domain.Enums;

namespace Domain.Entities.Interfaces
{
    public interface IOperation
    {
        /// <summary>
        /// Уникальный идентификатор операции.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Тип операции (доход / расход).
        /// </summary>
        OperationType Type { get; }

        /// <summary>
        /// Банковский счёт, связанный с операцией.
        /// </summary>
        Guid BankAccountId { get; }

        /// <summary>
        /// Сумма операции.
        /// </summary>
        decimal Amount { get; }

        /// <summary>
        /// Дата и время операции.
        /// </summary>
        DateTime Date { get; }

        /// <summary>
        /// Категория операции.
        /// </summary>
        Guid CategoryId { get; }

        /// <summary>
        /// Описание
        /// </summary>
        string? Description { get; }

        /// <summary>
        /// Метод для изменения типа операции.
        /// </summary>
        /// <param name="newType"></param>
        void ChangeType(OperationType newType);

        /// <summary>
        /// Метод для изменения идентификатора банковского счёта.
        /// </summary>
        /// <param name="newAccountId"></param>
        void ChangeBankAccountId(Guid newAccountId);

        /// <summary>
        /// Метод для изменения суммы операции.
        /// </summary>
        /// <param name="newAmount"></param>
        void ChangeAmount(decimal newAmount);

        /// <summary>
        /// Метод для изменения даты операции.
        /// </summary>
        /// <param name="newDate"></param>
        void ChangeDate(DateTime newDate);

        /// <summary>
        /// Метод для изменения идентификатора категории.
        /// </summary>
        /// <param name="newCategoryId"></param>
        void ChangeCategoryId(Guid newCategoryId);

        /// <summary>
        /// Метод для изменения описания операции.
        /// </summary>
        /// <param name="newDescription"></param>
        void ChangeDescription(string? newDescription);
    }
}
