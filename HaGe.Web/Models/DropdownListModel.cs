using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaGe.Web.Models
{
    public class DropdownListModel
    {
        public DropdownListModel()
        {
            Items = new List<SelectListItem>();
            Required = false;
            HideSearch = true;
            IsMultiple = false;
            CodeListId = "";
        }
        public string Name { get; set; }
        public string PlaceHolder { get; set; }
        public string CssClass { get; set; }
        public string Control { get; set; }
        public List<SelectListItem> Items { get; set; }
        public bool Required { get; set; }
        public bool IsMultiple { get; set; }
        public string ModalId { get; set; }
        public bool HideSearch { get; set; }
        public bool AllowClear { get; set; }
        public bool Tags { get; set; }
        public string CodeListId { get; set; }

    }
}
