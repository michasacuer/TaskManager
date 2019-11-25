namespace TaskManager.WPF.Helpers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using IronPdf;
    using TaskManager.Contracts.Data;
    using TaskManager.Entity;
    using TaskManager.WPF.Models;

    public class ManagerPanelHelper
    {
        private ProjectContract projectContract;

        private RaportContract raportContract;

        public ManagerPanelHelper()
        {
            string bearer = LoggedUser.Instance.User.Bearer;
            this.projectContract = new ProjectContract(bearer);
            this.raportContract = new RaportContract(bearer);
        }

        public async Task<string> GeneratePdf(Project project, string filepath)
        {
            string pdf = await this.GetPdfFtomDatabase(project.Id);
            
            var htmlToPdf = new HtmlToPdf();
            htmlToPdf.RenderHtmlAsPdf(pdf).SaveAs(filepath);

            return filepath;
        }

        public async void PrintRaport(Project project)
        {
            string pdf = await this.GetPdfFtomDatabase(project.Id);
            
            var htmlToPdf = new HtmlToPdf();
            var render = htmlToPdf.RenderHtmlAsPdf(pdf);
            
            var printDocument = render.GetPrintDocument();
            
            var fileDialog = new FileDialog();
            fileDialog.OpenPrinterDialog(printDocument);
        }

        public async Task<List<Project>> GetAllProjectsFromDatabase() => await this.projectContract.GetAllAsync();

        private async Task<string> GetPdfFtomDatabase(int projectId) => await this.raportContract.GetPdfAsync(projectId);
    }
}