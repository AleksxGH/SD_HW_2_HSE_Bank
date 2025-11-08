using Domain.Entities.Interfaces;

namespace Application.Export.Interfaces
{
    public interface IExportVisitor : IVisitor
    {
        public string GetAccountsExportData();

        public string GetCategoriesExportData();

        public string GetOperationsExportData();
    }
}
