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

namespace HSE_Bank
{
    public class Program
    {
        public static void Main()
        {
            var services = new ServiceCollection();

            // Регистрируем фабрики
            services.AddSingleton<IBankAccountsFactory, BankAccountsFactory>();
            services.AddSingleton<ICategoriesFactory, CategoriesFactory>();
            services.AddSingleton<IOperationsFactory, OperationsFactory>();

            //Регистрируем репозитории
            services.AddSingleton<IBankAccountsRepository, InMemoryBankAccountsRepository>();
            services.AddSingleton<ICategoriesRepository, InMemoryCategoriesRepository>();
            services.AddSingleton<IOperationsRepository, InMemoryOperationsRepository>();

            // Регистрируем фасады
            services.AddSingleton<IBankAccountsFacade, BankAccountsFacade>();
            services.AddSingleton<ICategoriesFacade, CategoriesFacade>();
            services.AddSingleton<IOperationsFacade, OperationsFacade>();

            var provider = services.BuildServiceProvider();


            var accounts = provider.GetRequiredService<IBankAccountsFacade>();
            var categories = provider.GetRequiredService<ICategoriesFacade>();
            var operations = provider.GetRequiredService<IOperationsFacade>();

            // Меню
            var menu = new Menu();

            // Добавляем команды с декоратором
            menu.AddCommand(new TimedCommandDecorator(new CreateBankAccountCommand(accounts)));


            // Запуск
            menu.Run();
        }
    }
}
