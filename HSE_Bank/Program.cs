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
            services.AddSingleton<IMenu, MainMenu>();          // главное меню
            services.AddSingleton<MainMenu>();
            services.AddTransient<ImportMenu>();
            services.AddTransient<BankAccountsMenu>();
            services.AddTransient<CategoriesMenu>();
            services.AddTransient<OperationsMenu>();
            services.AddTransient<ExportMenu>();

            // --- Команды ---
            services.AddTransient<CreateBankAccountCommand>();
            services.AddTransient<ICommand>(sp =>
                new TimedCommandDecorator(sp.GetRequiredService<CreateBankAccountCommand>()));

            // --- Собираем DI-контейнер ---
            var provider = services.BuildServiceProvider();

            // --- Получаем зависимости ---
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

            // Добавляем команды (через DI)
            var createAccountCommand = provider.GetRequiredService<ICommand>();
            accountsMenu.AddCommand(createAccountCommand);

            // --- Запуск ---
            mainMenu.Show();
        }
    }
}
