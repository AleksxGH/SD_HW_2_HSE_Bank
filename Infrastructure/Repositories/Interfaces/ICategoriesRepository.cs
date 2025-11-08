using Domain.Entities.Interfaces;

namespace Infrastructure.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория категорий операций
    /// </summary>
    public interface ICategoriesRepository
    {
        /// <summary>
        /// Метод добавления категории в репозиторий
        /// </summary>
        /// <param name="category"></param>
        void Add(ICategory category);

        /// <summary>
        /// Метод обновления категории в репозитории
        /// </summary>
        /// <param name="category"></param>
        void Update(ICategory category);

        /// <summary>
        /// Метод удаления категории из репозитория
        /// </summary>
        /// <param name="categoryId"></param>
        void Delete(Guid categoryId);

        /// <summary>
        /// Метод получения категории по идентификатору
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        ICategory? GetById(Guid categoryId);

        /// <summary>
        /// Метод попытки получения категории по данным
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        ICategory? TryGet(string data);

        /// <summary>
        /// Метод получения всех категорий из репозитория
        /// </summary>
        /// <returns></returns>
        IEnumerable<ICategory> GetAll();

        /// <summary>
        /// Метод проверки существования категории по идентификаторуы
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        bool Exists(Guid categoryId);

        /// <summary>
        /// Метод сохранения изменений в репозитории
        /// </summary>
        void SaveChanges();
    }
}
