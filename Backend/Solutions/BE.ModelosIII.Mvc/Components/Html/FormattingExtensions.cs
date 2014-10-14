using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BE.ModelosIII.Mvc.Components.Html
{
    public static class FormattingExtensions
    {
        public static string ToJsBool(this bool source)
        {
            return source ? "1" : string.Empty;
        }

        public static string ToYesNo(this bool source)
        {
            return source ? "Si" : "No";
        }

        public static string ToCurrency(this decimal? source)
        {
            return !source.HasValue ? String.Empty : source.Value.ToCurrency();
        }

        public static string ToCurrency(this decimal source)
        {
            return source.ToString("C2");
        }

        public static string ToCurrency(this double source)
        {
            return source.ToString("C2");
        }

        public static string ToCurrencyWithoutDecimals(this double source)
        {
            return source.ToString("C0");
        }

        public static string ToMonth(this int source)
        {
            if (source < 1 || source > 12)
                throw new ArgumentOutOfRangeException("source");

            var date = new DateTime(2000, source, 1);
            string valueLower = date.ToString("MMM.");
            return valueLower.First().ToString(CultureInfo.InvariantCulture).ToUpper() + valueLower.Substring(1);
        }

        public static string ToLongDate(this DateTime source)
        {
            return string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", source);
        }

        public static string ToDate(this DateTime source)
        {
            return string.Format("{0:dd-MMM-yyyy}", source);
        }

        public static string ToTimeAgo(this DateTime dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;

            if (timeSpan <= TimeSpan.FromSeconds(60))
                return string.Format("Hace {0} segundos", timeSpan.Seconds);

            if (timeSpan <= TimeSpan.FromMinutes(60))
                return timeSpan.Minutes > 1 ? String.Format("Hace {0} minutos", timeSpan.Minutes) : "Hace un minuto";

            if (timeSpan <= TimeSpan.FromHours(24))
                return timeSpan.Hours > 1 ? String.Format("Hace {0} horas", timeSpan.Hours) : "Hace una hora";

            if (timeSpan <= TimeSpan.FromDays(30))
                return timeSpan.Days > 1 ? String.Format("Hace {0} días", timeSpan.Days) : "Ayer";

            if (timeSpan <= TimeSpan.FromDays(28))
                return timeSpan.Days > 7 ? String.Format("Hace {0} semanas", timeSpan.Days) : "Hace una semana";

            if (timeSpan <= TimeSpan.FromDays(365))
                return timeSpan.Days > 30 ? String.Format("Hace {0} meses", timeSpan.Days / 30) : "Hace un mes";

            return timeSpan.Days > 365 ? String.Format("Hace {0} años", timeSpan.Days / 365) : "Hace un año";
        }

        public static string ToPercent(this double source)
        {
            //if (source < 0 || source > 100)
            //    throw new ArgumentOutOfRangeException("source");

            return (source / 100).ToString("0%");
        }

        public static string RemoveDiacritics(this string source)
        {
            string standardFormD = source.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in standardFormD)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        public static string ToUrlFriendlyString(this string name)
        {
            var myRegex = new Regex(@"^[ a-z\d]+");
            return myRegex.Match(name.RemoveDiacritics().ToLowerInvariant()).Value.Replace(' ', '-');
        }

        public static string ToSiteName(this string name)
        {
            var myRegex = new Regex(@"^[ a-z\d]+");
            return myRegex.Match(name.RemoveDiacritics().ToLowerInvariant()).Value.Replace(' ', '-');
        }

        public static string ToLocalizedLabel(this DayOfWeek day, IFormatProvider formatProvider)
        {
            //Cualquier cultura devuelve es

            if (day == DayOfWeek.Monday)
            {
                return "Lunes";
            }
            if (day == DayOfWeek.Tuesday)
            {
                return "Martes";
            }
            if (day == DayOfWeek.Wednesday)
            {
                return "Miércoles";
            }
            if (day == DayOfWeek.Thursday)
            {
                return "Jueves";
            }
            if (day == DayOfWeek.Friday)
            {
                return "Viernes";
            }
            if (day == DayOfWeek.Saturday)
            {
                return "Sábado";
            }
            if (day == DayOfWeek.Sunday)
            {
                return "Domingo";
            }

            return string.Empty;
        }
    }
}