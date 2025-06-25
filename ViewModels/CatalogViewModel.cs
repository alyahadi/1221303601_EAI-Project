using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AandN_Website.ViewModels
{
    public class CatalogViewModel
    {
        public string Category { get; set; }
        public IntegrationServiceRef.CatalogItem[] Items { get; set; }
    }
}