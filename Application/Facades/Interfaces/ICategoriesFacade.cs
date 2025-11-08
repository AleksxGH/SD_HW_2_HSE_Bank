using Domain.Entities.Interfaces;
using Domain.Enums;

namespace Application.Facades.Interfaces
{
    /// <summary>
    /// Интерфейс фасада для управления категориями операций.
    /// </summary>
    public interface ICategoriesFacade
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
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        ICategory RestoreCategory(Guid id, string name, CategoryType type);

        /// <summary>
        /// Метод получения категории по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ICategory? GetCategoryById(Guid id);

        /// <summary>
        /// Метод получения категории по данным о ней
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ICategory? TryGetCategory(string name);

        /// <summary>
        /// Метод получения всех категорий
        /// </summary>
        /// <returns></returns>
        IEnumerable<ICategory> GetAllCategories();

        /// <summary>
        /// Метод обновления названия категории
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newName"></param>
        void UpdateCategoryName(Guid id, string newName);

        /// <summary>
        /// Метод обновления типа категории
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newBalance"></param>
        void UpdateCategoryType(Guid id, decimal newBalance);

        /// <summary>
        /// Метод удаления категории
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteCategory(Guid id);
    }
}
