using System.ComponentModel;

namespace BE.ModelosIII.Mvc.Models.Util
{
    public class ActionModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public IconType IconType { get; set; }
        public object RouteValues { get; set; }

        public ActionModel(string name, string action, IconType iconType, string id = null)
        {
            Id = id;
            Name = name;
            Action = action;
            IconType = iconType;
        }
    }

    public enum IconType
    {
        [Description("icon-pencil")]
        Edit = 0,
        [Description("icon-check")]
        Create = 1,
        [Description("icon-eye-open")]
        View = 2,
        [Description("icon-trash")]
        Delete = 2,
    }
}