using Application.Import.Interfaces;
using Domain.Entities.Interfaces;
using System.Collections.Generic;

namespace Application.Import.Template
{
    public abstract class ImporterTemplate
    {
        protected abstract void LoadAccounts(IImportVisitor visitor, IEnumerable<string> filePaths);
        protected abstract void LoadCategories(IImportVisitor visitor, IEnumerable<string> filePaths);
        protected abstract void LoadOperations(IImportVisitor visitor, IEnumerable<string> filePaths);

        public void ImportAll(IImportVisitor visitor, IEnumerable<string> accountFiles, IEnumerable<string> categoryFiles, IEnumerable<string> operationFiles)
        {
            LoadAccounts(visitor, accountFiles);
            LoadCategories(visitor, categoryFiles);
            LoadOperations(visitor, operationFiles);
        }
    }
}
