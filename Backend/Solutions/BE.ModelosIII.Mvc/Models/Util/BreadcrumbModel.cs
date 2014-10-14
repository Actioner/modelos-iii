namespace BE.ModelosIII.Mvc.Models.Util
{
    public class BreadcrumbModel
    {
        public string Text { get; set; }
        public string Link { get; set; }
        public BreadcrumbModel NextBreadcrumb { get; set; }

        public BreadcrumbModel Add(string text, string link = null)
        {
            var newBreadcrumb = new BreadcrumbModel(text, link);
            var current = this;

            while (current.NextBreadcrumb != null)
            {
                current = current.NextBreadcrumb;
            }
            current.NextBreadcrumb = newBreadcrumb;
            return this;
        }

        public BreadcrumbModel(string text, string link = null)
        {
            this.Text = text;
            this.Link = link ?? "#";
        }
    }
}