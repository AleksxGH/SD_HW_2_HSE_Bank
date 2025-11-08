using Domain.Entities.Interfaces;
using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Класс категории банковской операции.
    /// </summary>
    public class Category : ICategory
    {
        /// <summary>
        /// Уникальный идентификатор категории.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Название категории.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Тип категории (доход / расход).
        /// </summary>
        public CategoryType Type { get; private set; }

        /// <summary>
        /// Конструктор создания категории.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public Category(string name, CategoryType type)
        {
            Id = Guid.NewGuid();
            Name = name;
            Type = type;
        }

        /// <summary>
        /// Конструктор восстановления категории.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public Category(Guid id, string name, CategoryType type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

        /// <summary>
        /// Изменяет название категории.
        /// </summary>
        public void Rename(string newName)
        {
            Name = newName;
        }

        /// <summary>
        /// Изменяет тип категории (доход / расход).
        /// </summary>
        public void ChangeType(CategoryType newType)
        {
            Type = newType;
        }
    }
}
