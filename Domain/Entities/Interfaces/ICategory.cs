using Domain.Enums;

namespace Domain.Entities.Interfaces
{
    /// <summary>
    /// Интерфейс категории операции.
    /// </summary>
    public interface ICategory
    {
        /// <summary>
        /// Уникальный идентификатор категории.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Название категории.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Тип категории (доход / расход).
        /// </summary>
        CategoryType Type { get; }

        /// <summary>
        /// Изменяет название категории.
        /// </summary>
        void Rename(string newName);

        /// <summary>
        /// Изменяет тип категории (доход / расход).
        /// </summary>
        void ChangeType(CategoryType newType);
    }
}
