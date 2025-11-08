using Infrastructure.Repositories.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Commands.Decorators;
using Presentation.Commands;
using Presentation.Menu;
using Domain.Factories.Interfaces;
using Domain.Factories;
using Application.Facades.Interfaces;
using Application.Facades;
using Presentation.Menu.Interfaces;
using Presentation.Commands.Interfaces;
using Application.Export.Template;
using Application.Export;

namespace HSE_Bank
{
    public class Program
    {
        public static void Main()
        {
            var services = new ServiceCollection();

            // --- Регистрация фабрик ---
            services.AddSingleton<IBankAccountsFactory, BankAccountsFactory>();
            services.AddSingleton<ICategoriesFactory, CategoriesFactory>();
            services.AddSingleton<IOperationsFactory, OperationsFactory>();

            // --- Репозитории ---
            services.AddSingleton<IBankAccountsRepository, InMemoryBankAccountsRepository>();
            services.AddSingleton<ICategoriesRepository, InMemoryCategoriesRepository>();
            services.AddSingleton<IOperationsRepository, InMemoryOperationsRepository>();

            // --- Фасады ---
            services.AddSingleton<IBankAccountsFacade, BankAccountsFacade>();
            services.AddSingleton<ICategoriesFacade, CategoriesFacade>();
            services.AddSingleton<IOperationsFacade, OperationsFacade>();

            // --- Меню ---
            services.AddSingleton<MainMenu>();
            services.AddSingleton<IMenu>(sp => sp.GetRequiredService<MainMenu>());
            services.AddTransient<ImportMenu>();
            services.AddTransient<BankAccountsMenu>();
            services.AddTransient<CategoriesMenu>();
            services.AddTransient<OperationsMenu>();
            services.AddTransient<ExportMenu>();

            // --- Экспорт ---
            services.AddTransient<JsonExporter>(sp =>
            {
                string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Exports");
                return new JsonExporter(outputFolder);
            });

            services.AddTransient<ExportJsonCommand>(sp =>
            new ExportJsonCommand(
                sp.GetRequiredService<JsonExporter>(), // конкретный класс
                sp.GetRequiredService<IBankAccountsRepository>(),
                sp.GetRequiredService<ICategoriesRepository>(),
                sp.GetRequiredService<IOperationsRepository>()
            ));


            // --- Команды и декораторы ---
            services.AddTransient<CreateBankAccountCommand>();
            services.AddTransient<ViewAllBankAccountsCommand>();
            services.AddTransient<RenameBankAccountCommand>();
            services.AddTransient<DeleteBankAccountCommand>();

            services.AddTransient<CreateCategoryCommand>();
            services.AddTransient<ViewAllCategoriesCommand>();
            services.AddTransient<RenameCategoryCommand>();
            services.AddTransient<ChangeCategoryTypeCommand>();
            services.AddTransient<DeleteCategoryCommand>();

            services.AddTransient<CreateOperationCommand>();
            services.AddTransient<ViewAllOperationsCommand>();
            services.AddTransient<ChangeOperationTypeCommand>();
            services.AddTransient<ChangeOperationsAccountCommand>();
            services.AddTransient<ChangeOperationsAmountCommand>();

            // Регистрируем декорированные команды как ICommand
            services.AddTransient<ICommand>(sp => new TimedCommandDecorator(sp.GetRequiredService<CreateBankAccountCommand>()));
            services.AddTransient<ICommand>(sp => new TimedCommandDecorator(sp.GetRequiredService<ViewAllBankAccountsCommand>()));
            services.AddTransient<ICommand>(sp => new TimedCommandDecorator(sp.GetRequiredService<RenameBankAccountCommand>()));
            services.AddTransient<ICommand>(sp => new TimedCommandDecorator(sp.GetRequiredService<DeleteBankAccountCommand>()));

            services.AddTransient<ICommand>(sp => new TimedCommandDecorator(sp.GetRequiredService<CreateCategoryCommand>()));
            services.AddTransient<ICommand>(sp => new TimedCommandDecorator(sp.GetRequiredService<ViewAllCategoriesCommand>()));
            services.AddTransient<ICommand>(sp => new TimedCommandDecorator(sp.GetRequiredService<RenameCategoryCommand>()));
            services.AddTransient<ICommand>(sp => new TimedCommandDecorator(sp.GetRequiredService<ChangeCategoryTypeCommand>()));
            services.AddTransient<ICommand>(sp => new TimedCommandDecorator(sp.GetRequiredService<DeleteCategoryCommand>()));

            services.AddTransient<ICommand>(sp => new TimedCommandDecorator(sp.GetRequiredService<CreateOperationCommand>()));
            services.AddTransient<ICommand>(sp => new TimedCommandDecorator(sp.GetRequiredService<ViewAllOperationsCommand>()));
            services.AddTransient<ICommand>(sp => new TimedCommandDecorator(sp.GetRequiredService<ChangeOperationTypeCommand>()));
            services.AddTransient<ICommand>(sp => new TimedCommandDecorator(sp.GetRequiredService<ChangeOperationsAccountCommand>()));
            services.AddTransient<ICommand>(sp => new TimedCommandDecorator(sp.GetRequiredService<ChangeOperationsAmountCommand>()));

            services.AddTransient<ICommand>(sp => new TimedCommandDecorator(sp.GetRequiredService<ExportJsonCommand>()));

            var provider = services.BuildServiceProvider();

            // --- Получаем меню ---
            var mainMenu = provider.GetRequiredService<MainMenu>();
            var importMenu = provider.GetRequiredService<ImportMenu>();
            var accountsMenu = provider.GetRequiredService<BankAccountsMenu>();
            var categoriesMenu = provider.GetRequiredService<CategoriesMenu>();
            var operationsMenu = provider.GetRequiredService<OperationsMenu>();
            var exportMenu = provider.GetRequiredService<ExportMenu>();

            // --- Конфигурируем структуру меню ---
            mainMenu.AddSection(importMenu);
            mainMenu.AddSection(accountsMenu);
            mainMenu.AddSection(categoriesMenu);
            mainMenu.AddSection(operationsMenu);
            mainMenu.AddSection(exportMenu);

            // --- Получаем все декорированные команды ---
            var decoratedCommands = provider.GetServices<ICommand>().ToList();

            // Добавляем нужные команды в меню
            accountsMenu.AddCommand(decoratedCommands.First(c => c.Name == "Создать новый банковский счет"));
            accountsMenu.AddCommand(decoratedCommands.First(c => c.Name == "Просмотреть все банковские счета"));
            accountsMenu.AddCommand(decoratedCommands.First(c => c.Name == "Переименовать банковский счет"));
            accountsMenu.AddCommand(decoratedCommands.First(c => c.Name == "Удалить банковский счет"));

            categoriesMenu.AddCommand(decoratedCommands.First(c => c.Name == "Создать новую категорию"));
            categoriesMenu.AddCommand(decoratedCommands.First(c => c.Name == "Просмотреть все категории"));
            categoriesMenu.AddCommand(decoratedCommands.First(c => c.Name == "Переименовать категорию"));
            categoriesMenu.AddCommand(decoratedCommands.First(c => c.Name == "Изменить тип категории"));
            categoriesMenu.AddCommand(decoratedCommands.First(c => c.Name == "Удалить категорию"));

            operationsMenu.AddCommand(decoratedCommands.First(c => c.Name == "Создать новую банковскую операцию"));
            operationsMenu.AddCommand(decoratedCommands.First(c => c.Name == "Просмотреть все банковские операции"));
            operationsMenu.AddCommand(decoratedCommands.First(c => c.Name == "Изменить счет операции"));
            operationsMenu.AddCommand(decoratedCommands.First(c => c.Name == "Изменить тип операции"));
            operationsMenu.AddCommand(decoratedCommands.First(c => c.Name == "Изменить сумму операции"));

            exportMenu.AddCommand(decoratedCommands.First(c => c.Name == "Экспортировать данные в JSON"));

            // --- Запуск ---
            mainMenu.Show();
        }
    }
}
