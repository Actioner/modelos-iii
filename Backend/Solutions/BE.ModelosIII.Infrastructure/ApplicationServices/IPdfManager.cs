namespace BE.ModelosIII.Infrastructure.ApplicationServices
{
    public interface IPdfManager
    {
        byte[] GetMostSoldHourContent(Models.MostSoldHour.ReportInfo info);
        byte[] GetMostSoldMovieContent(Models.MostSoldMovie.ReportInfo info);
    }
}