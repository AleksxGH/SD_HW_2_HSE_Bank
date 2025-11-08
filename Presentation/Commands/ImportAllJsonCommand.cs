using Spectre.Console;
using Presentation.Commands.Interfaces;
using Application.Import;
using Application.Import.Template;
using Application.Import.Interfaces;
using Infrastructure.Repositories.Interfaces;
using Domain.Entities;
using Domain.Entities.Interfaces;

namespace Presentation.Commands
{
    public class ImportAllJsonCommand : ICommand
    {
        private readonly IBankAccountsRepository _accountsRepo;
        private readonly ICategoriesRepository _categoriesRepo;
        private readonly IOperationsRepository _operationsRepo;
        private readonly JsonImporter _importer;

        public string Name => "Импортировать все данные из JSON";

        public ImportAllJsonCommand(
            IBankAccountsRepository accountsRepo,
            ICategoriesRepository categoriesRepo,
            IOperationsRepository operationsRepo,
            JsonImporter importer)
        {
            _accountsRepo = accountsRepo;
            _categoriesRepo = categoriesRepo;
            _operationsRepo = operationsRepo;
            _importer = importer;
        }

        public void Execute()
        {
            string folderPath;

            // --- Ввод папки пользователем ---
            while (true)
            {
                AnsiConsole.Markup("[white]Введите путь к папке с JSON файлами:[/] ");
                folderPath = Console.ReadLine()?.Trim() ?? "";

                if (!Directory.Exists(folderPath))
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: папка не найдена.[/]");
                    continue;
                }

                string accountsFile = Path.Combine(folderPath, "accounts.json");
                string categoriesFile = Path.Combine(folderPath, "categories.json");
                string operationsFile = Path.Combine(folderPath, "operations.json");

                if (!File.Exists(accountsFile) || !File.Exists(categoriesFile) || !File.Exists(operationsFile))
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: в папке должны быть файлы accounts.json, categories.json, operations.json.[/]");
                    continue;
                }

                break;
            }

            // --- Создаем Visitor ---
            var visitor = new JsonImportVisitor();

            // --- Импортируем все файлы ---
            _importer.ImportAll(
                visitor,
                new[] { Path.Combine(folderPath, "accounts.json") },
                new[] { Path.Combine(folderPath, "categories.json") },
                new[] { Path.Combine(folderPath, "operations.json") }
            );

            // --- Сохраняем данные в репозитории ---
            foreach (IBankAccount account in visitor.Accounts)
                _accountsRepo.Add(account);

            foreach (ICategory category in visitor.Categories)
                _categoriesRepo.Add(category);

            foreach (IOperation operation in visitor.Operations)
                _operationsRepo.Add(operation);

            AnsiConsole.MarkupLine("[green]Импорт завершен успешно![/]");
        }
    }
}
