using Domain.Entities.Interfaces;
using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Класс банковской операции.
    /// </summary>
    public class Operation : IOperation, IVisitable
    {
        /// <summary>
        /// Уникальный идентификатор операции.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Тип операции (доход / расход).
        /// </summary>
        public OperationType Type { get; private set; }

        /// <summary>
        /// Банковский счёт, связанный с операцией.
        /// </summary>
        public Guid BankAccountId { get; private set; }

        /// <summary>
        /// Сумма операции.
        /// </summary>
        public decimal Amount { get; private set; }

        /// <summary>
        /// Дата и время операции.
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Категория операции.
        /// </summary>
        public Guid CategoryId { get; private set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        /// Конструктор для создания новой операции.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="bankAccountId"></param>
        /// <param name="amount"></param>
        /// <param name="categoryId"></param>
        /// <param name="description"></param>
        public Operation(OperationType type, Guid bankAccountId, decimal amount, Guid categoryId, string? description)
        {
            Id = Guid.NewGuid();
            Type = type;
            BankAccountId = bankAccountId;
            Amount = amount;
            Date = DateTime.Now;
            CategoryId = categoryId;
            Description = description;
        }

        /// <summary>
        /// Конструктор для восстановления операции.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="bankAccountId"></param>
        /// <param name="amount"></param>
        /// <param name="date"></param>
        /// <param name="categoryId"></param>
        /// <param name="description"></param>
        public Operation(Guid id, OperationType type, Guid bankAccountId, decimal amount, DateTime date, Guid categoryId, string? description)
        {
            Id = id;
            Type = type;
            BankAccountId = bankAccountId;
            Amount = amount;
            Date = date;
            CategoryId = categoryId;
            Description = description;
        }


        /// <summary>
        /// Метод для изменения типа операции.
        /// </summary>
        /// <param name="newType"></param>
        public void ChangeType(OperationType newType)
        {
            Type = newType;
        }

        /// <summary>
        /// Метод для изменения идентификатора банковского счёта.
        /// </summary>
        /// <param name="newAccountId"></param>
        public void ChangeBankAccountId(Guid newAccountId)
        {
            BankAccountId = newAccountId;
        }

        /// <summary>
        /// Метод для изменения суммы операции.
        /// </summary>
        /// <param name="newAmount"></param>
        public void ChangeAmount(decimal newAmount)
        {
            Amount = newAmount;
        }

        /// <summary>
        /// Метод для изменения даты операции.
        /// </summary>
        /// <param name="newDate"></param>
        public void ChangeDate(DateTime newDate)
        {
            Date = newDate;
        }

        /// <summary>
        /// Метод для изменения идентификатора категории.
        /// </summary>
        /// <param name="newCategoryId"></param>
        public void ChangeCategoryId(Guid newCategoryId)
        {
            CategoryId = newCategoryId;
        }

        /// <summary>
        /// Метод для изменения описания операции.
        /// </summary>
        /// <param name="newDescription"></param>
        public void ChangeDescription(string? newDescription)
        {
            Description = newDescription;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
