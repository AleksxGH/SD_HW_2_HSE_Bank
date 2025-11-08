using Application.Import.Interfaces;
using Application.Import.Template;
using Domain.Entities;
using Domain.Entities.Interfaces;
using System.Text.Json;

namespace Application.Import
{
    public class JsonImporter : ImporterTemplate
    {
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        protected override void LoadAccounts(IImportVisitor visitor, IEnumerable<string> filePaths)
        {
            foreach (var file in filePaths)
            {
                if (!File.Exists(file)) continue;

                var json = File.ReadAllText(file);
                var accounts = JsonSerializer.Deserialize<List<BankAccount>>(json, _jsonOptions);
                if (accounts == null) continue;

                foreach (var account in accounts)
                    visitor.Visit(account);
            }
        }

        protected override void LoadCategories(IImportVisitor visitor, IEnumerable<string> filePaths)
        {
            foreach (var file in filePaths)
            {
                if (!File.Exists(file)) continue;

                var json = File.ReadAllText(file);
                var categories = JsonSerializer.Deserialize<List<Category>>(json, _jsonOptions);
                if (categories == null) continue;

                foreach (var category in categories)
                    visitor.Visit(category);
            }
        }

        protected override void LoadOperations(IImportVisitor visitor, IEnumerable<string> filePaths)
        {
            foreach (var file in filePaths)
            {
                if (!File.Exists(file)) continue;

                var json = File.ReadAllText(file);
                var operations = JsonSerializer.Deserialize<List<Operation>>(json, _jsonOptions);
                if (operations == null) continue;

                foreach (var operation in operations)
                    visitor.Visit(operation);
            }
        }
    }
}
