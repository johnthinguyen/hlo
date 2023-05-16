using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebResxLanguage.Models
{
    public class ElementV2Model
    {
        [Required]
        public string Lang { get; set; }

        [Required]
        [AllowHtml]
        public List<ElementDetailModel> Detail { get; set; }
    }
    public class ElementDetailModel
    {
        [Required]
        public string Key { get; set; }

        [Required]
        [AllowHtml]
        public string Name { get; set; }
    }


    public class ElementModel
    {
        [Required]
        public string Key { get; set; }

        [Required]
        [AllowHtml]
        public string En { get; set; }

        [Required]
        [AllowHtml]
        public string Km { get; set; }

        [Required]
        [AllowHtml]
        public string Th { get; set; }

        [Required]
        [AllowHtml]
        public string Vi { get; set; }

        [Required]
        [AllowHtml]
        public string Zh { get; set; }

        [Required]
        [AllowHtml]
        public string My { get; set; }

        [Required]
        [AllowHtml]
        public string Ms { get; set; }

        [Required]
        [AllowHtml]
        public string Fil { get; set; }
    }
}