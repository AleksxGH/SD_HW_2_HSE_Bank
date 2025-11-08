using Spectre.Console;
using Presentation.Commands.Interfaces;
using Application.Export.Template;
using Infrastructure.Repositories.Interfaces;

namespace Presentation.Commands
{
    public class ExportJsonCommand : ICommand
    {
        private readonly ExporterTemplate _exporter;
        private readonly IBankAccountsRepository _accountsRepo;
        private readonly ICategoriesRepository _categoriesRepo;
        private readonly IOperationsRepository _operationsRepo;

        public string Name => "Экспортировать данные в JSON";

        public ExportJsonCommand(
            ExporterTemplate exporter,
            IBankAccountsRepository accountsRepo,
            ICategoriesRepository categoriesRepo,
            IOperationsRepository operationsRepo)
        {
            _exporter = exporter;
            _accountsRepo = accountsRepo;
            _categoriesRepo = categoriesRepo;
            _operationsRepo = operationsRepo;
        }

        public void Execute()
        {
            while (true)
            {
                AnsiConsole.Markup("[white]Введите путь к папке для экспорта данных:[/] ");
                string? input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: путь не может быть пустым.[/]");
                    continue;
                }

                try
                {
                    if (!System.IO.Directory.Exists(input))
                    {
                        System.IO.Directory.CreateDirectory(input);
                    }

                    _exporter.OutputFolder = input;
                    break;
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLineInterpolated($"[red]Ошибка: невозможно создать папку. {ex.Message}[/]");
                }
            }

            _exporter.ExportAll(
                _accountsRepo.GetAll(),
                _categoriesRepo.GetAll(),
                _operationsRepo.GetAll()
            );

            AnsiConsole.MarkupLine("[green]Данные успешно экспортированы![/]");
            AnsiConsole.WriteLine($"Путь к папке с данными: {_exporter.OutputFolder}");
        }

    }
}
