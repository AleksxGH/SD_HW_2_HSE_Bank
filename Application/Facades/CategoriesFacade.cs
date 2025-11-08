using Domain.Entities.Interfaces;
using Domain.Factories.Interfaces;
using Domain.Enums;
using Infrastructure.Repositories.Interfaces;
using Application.Facades.Interfaces;

namespace Application.Facades
{
    public class CategoriesFacade : ICategoriesFacade
    {
        /// <summary>
        /// Фабрика категорий банковских операций
        /// </summary>
        private readonly ICategoriesFactory _factory;

        /// <summary>
        /// Репозиторий категорий банковских операций
        /// </summary>
        private readonly ICategoriesRepository _repository;

        /// <summary>
        /// Конструктор фасада категорий операций
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="repository"></param>
        public CategoriesFacade(ICategoriesFactory factory, ICategoriesRepository repository)
        {
            _factory = factory;
            _repository = repository;
        }

        /// <summary>
        /// Метод создания новой категории
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ICategory CreateCategory(string name, CategoryType type)
        {
            var category = _factory.CreateCategory(name, type);
            _repository.Add(category);
            return category;
        }

        /// <summary>
        /// Метод восстановления категории
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ICategory RestoreCategory(Guid id, string name, CategoryType type)
        {
            var category = _factory.RestoreCategory(id, type, name);
            _repository.Add(category);
            return category;
        }

        /// <summary>
        /// Метод получения категории по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICategory? GetCategoryById(Guid id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// Метод получения категории по данным о ней
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ICategory? TryGetCategory(string data)
        {
            return _repository.TryGet(data);
        }

        /// <summary>
        /// Метод получения всех категорий
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ICategory> GetAllCategories()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Метод обновления названия категории
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newName"></param>
        public void UpdateCategoryName(Guid id, string newName)
        {
            var category = _repository.GetById(id);
            if (category != null)
            {
                category.Rename(newName);
                _repository.Update(category);
                return;
            }
            throw new InvalidOperationException("Категория не найдена");
        }

        /// <summary>
        /// Метод обновления типа категории
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newBalance"></param>
        public void UpdateCategoryType(Guid id, CategoryType newType)
        {
            var category = _repository.GetById(id);
            if (category != null)
            {
                category.ChangeType(newType);
                _repository.Update(category);
                return;
            }
            throw new InvalidOperationException("Категория не найдена");
        }

        /// <summary>
        /// Метод удаления категории
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteCategory(Guid id)
        {
            if (!_repository.Exists(id))
            {
                return false;
            }
            _repository.Delete(id);
            return true;
        }
    }
}
