using Domain.Entities.Interfaces;
using Domain.Enums;

namespace Domain.Factories.Interfaces
{
    /// <summary>
    /// Интерфейс фабрики категорий операций
    /// </summary>
    public interface ICategoryFactory
    {
        /// <summary>
        /// Метод создания новой категории
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        ICategory CreateCategory(string name, CategoryType type);

        /// <summary>
        /// Метод восстановления категории
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        ICategory RestoreCategory(Guid id, CategoryType type, string name);
    }
}
