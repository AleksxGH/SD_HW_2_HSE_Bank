using Domain.Factories.Interfaces;
using Domain.Entities.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Domain.Factories
{
    /// <summary>
    /// Класс-фабрикa для создания и восстановления категорий операций
    /// </summary>
    public class CategoryFactory : ICategoryFactory
    {
        /// <summary>
        /// Метод создания новой категории
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ICategory CreateCategory(string name, CategoryType type)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Название категории не может быть пустым.", nameof(name));
            }
            return new Category(name, type);
        }

        /// <summary>
        /// Метод восстановления категории
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ICategory RestoreCategory(Guid id, CategoryType type, string name)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Идентификатор категории некорректен.", nameof(id));
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Название категории не может быть пустым.", nameof(name));
            }
            return new Category(id, name, type);
        }
    }
}
