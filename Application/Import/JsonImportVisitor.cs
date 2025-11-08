using Application.Import.Interfaces;
using Domain.Entities.Interfaces;
using Domain.Entities;
using System.Text.Json;

namespace Application.Import
{
    public class JsonImportVisitor : IImportVisitor
    {
        public List<IBankAccount> Accounts { get; } = [];
        public List<ICategory> Categories { get; } = [];
        public List<IOperation> Operations { get; } = [];

        public void Visit(IBankAccount bankAccount)
        {
            Accounts.Add(bankAccount);
        }

        public void Visit(ICategory category)
        {
            Categories.Add(category);
        }

        public void Visit(IOperation operation)
        {
            Operations.Add(operation);
        }
    }
}
