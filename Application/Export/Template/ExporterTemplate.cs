using Application.Export.Interfaces;
using Domain.Entities.Interfaces;

namespace Application.Export.Template
{
    public abstract class ExporterTemplate
    {
        public string OutputFolder = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\.."));

        // Шаблонный метод
        public void ExportAll(
            IEnumerable<IBankAccount> accounts,
            IEnumerable<ICategory> categories,
            IEnumerable<IOperation> operations)
        {
            var visitor = CreateVisitor(); // конкретный visitor в наследнике

            // Посещаем объекты
            foreach (var account in accounts)
                visitor.Visit(account);

            foreach (var category in categories)
                visitor.Visit(category);

            foreach (var operation in operations)
                visitor.Visit(operation);

            // Сохраняем или возвращаем результат
            SaveExport(visitor);
        }

        // Фабричный метод для создания конкретного visitor
        protected abstract IExportVisitor CreateVisitor();

        // Метод сохранения результата (конкретный формат)
        protected abstract void SaveExport(IExportVisitor visitor);
    }

}
