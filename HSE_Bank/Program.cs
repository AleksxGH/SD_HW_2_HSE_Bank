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

            // --- Команды и декораторы ---
            services.AddTransient<CreateBankAccountCommand>();
            services.AddTransient<ViewAllBankAccountsCommand>();
            services.AddTransient<RenameBankAccountCommand>();
            services.AddTransient<DeleteBankAccountCommand>();

            // Регистрируем декорированные команды как ICommand
            services.AddTransient<ICommand>(sp =>
                new TimedCommandDecorator(sp.GetRequiredService<CreateBankAccountCommand>()));
            services.AddTransient<ICommand>(sp =>
                new TimedCommandDecorator(sp.GetRequiredService<ViewAllBankAccountsCommand>()));
            services.AddTransient<ICommand>(sp =>
                new TimedCommandDecorator(sp.GetRequiredService<RenameBankAccountCommand>()));
            services.AddTransient<ICommand>(sp =>
                new TimedCommandDecorator(sp.GetRequiredService<DeleteBankAccountCommand>()));

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

            // --- Запуск ---
            mainMenu.Show();
        }
    }
}
