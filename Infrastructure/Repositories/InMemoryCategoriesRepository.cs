using Domain.Entities;
using Domain.Entities.Interfaces;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Класс репозитория категорий операций, реализующий хранение в памяти
    /// </summary>
    public class InMemoryCategoriesRepository : ICategoriesRepository
    {
        /// <summary>
        /// Контейнер для хранения категорий в памяти в виде словаря
        /// </summary>
        private readonly Dictionary<Guid, ICategory> _categories;

        /// <summary>
        /// Конструктор репозитория категорий операций в памяти
        /// </summary>
        public InMemoryCategoriesRepository()
        {
            _categories = [];
        }

        /// <summary>
        /// Метод добавления категории в репозиторий
        /// </summary>
        /// <param name="category"></param>
        public void Add(ICategory category)
        {
            _categories[category.Id] = category;
        }

        /// <summary>
        /// Метод обновления категории в репозитории
        /// </summary>
        /// <param name="category"></param>
        public void Update(ICategory category)
        {
            _categories[category.Id] = category;
        }

        /// <summary>
        /// Метод удаления категории из репозитория
        /// </summary>
        /// <param name="categoryId"></param>
        public void Delete(Guid categoryId)
        {
            _categories.Remove(categoryId);
        }

        /// <summary>
        /// Метод получения категории по идентификатору
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public ICategory? GetById(Guid categoryId)
        {
            _ = _categories.TryGetValue(categoryId, out var category);
            return category;
        }

        /// <summary>
        /// Метод попытки получения категории по данным
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ICategory? TryGet(string data)
        {
            _ = Guid.TryParse(data, out var id);
            var category = GetById(id);
            if (category != null)
            {
                return category;
            }
            return _categories.Values.FirstOrDefault(c => c.Name.Equals(data, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Метод получения всех категорий из репозитория
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ICategory> GetAll()
        {
            return _categories.Values;
        }

        /// <summary>
        /// Метод проверки существования категории по идентификаторуы
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool Exists(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                return false;
            }
            if (_categories.ContainsKey(categoryId))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод сохранения изменений в репозитории
        /// </summary>
        public void SaveChanges()
        {
            /// В данном случае, так как репозиторий в памяти, изменения сохранять не нужно.
        }
    }
}
