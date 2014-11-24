namespace BE.ModelosIII.Infrastructure.ApplicationServices
{
    public interface IPdfManager
    {
        byte[] GetGenerationReportContent(Models.GenerationReport.ReportInfo info);
    }
}