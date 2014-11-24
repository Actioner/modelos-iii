namespace BE.ModelosIII.Mvc.Models.Report
{
    public class GenerationReportModel
    {
        public int RunId { get; set; }
        public int Number { get; set; }
        public double Best { get; set; }
        public double Average { get; set; }
        public double Worst { get; set; }
    }
}