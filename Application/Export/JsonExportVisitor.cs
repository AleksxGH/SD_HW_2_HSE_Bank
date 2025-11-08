using Domain.Entities.Interfaces;
using Application.Export.Interfaces;
using System.Text.Json;

namespace Application.Export
{
    public class JsonExportVisitor : IExportVisitor
    {
        // Внутренние списки для накопления данных
        private readonly List<IBankAccount> _accounts = [];
        private readonly List<ICategory> _categories = [];
        private readonly List<IOperation> _operations = [];

        // --- Методы Visit ---
        public void Visit(IBankAccount bankAccount)
        {
            if (bankAccount != null)
                _accounts.Add(bankAccount);
        }

        public void Visit(ICategory category)
        {
            if (category != null)
                _categories.Add(category);
        }

        public void Visit(IOperation operation)
        {
            if (operation != null)
                _operations.Add(operation);
        }

        // --- Методы получения JSON ---
        public string GetAccountsExportData()
        {
            return JsonSerializer.Serialize(_accounts, new JsonSerializerOptions { WriteIndented = true });
        }

        public string GetCategoriesExportData()
        {
            return JsonSerializer.Serialize(_categories, new JsonSerializerOptions { WriteIndented = true });
        }

        public string GetOperationsExportData()
        {
            return JsonSerializer.Serialize(_operations, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
