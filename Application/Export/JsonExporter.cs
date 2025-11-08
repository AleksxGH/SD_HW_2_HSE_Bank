using Application.Export.Interfaces;
using Application.Export.Template;

namespace Application.Export
{
    public class JsonExporter : ExporterTemplate
    {
        public JsonExporter(string outputFolder)
        {
            OutputFolder = outputFolder;
        }

        protected override IExportVisitor CreateVisitor()
        {
            return new JsonExportVisitor();
        }

        protected override void SaveExport(IExportVisitor visitor)
        {
            int i = 1;
            string exportFolder;
            do
            {
                exportFolder = Path.Combine(OutputFolder, $"Export_{i}");
                i++;
            } while (Directory.Exists(exportFolder));

            Directory.CreateDirectory(exportFolder);

            File.WriteAllText(Path.Combine(exportFolder, "accounts.json"), visitor.GetAccountsExportData());
            File.WriteAllText(Path.Combine(exportFolder, "categories.json"), visitor.GetCategoriesExportData());
            File.WriteAllText(Path.Combine(exportFolder, "operations.json"), visitor.GetOperationsExportData());
        }

    }

}
