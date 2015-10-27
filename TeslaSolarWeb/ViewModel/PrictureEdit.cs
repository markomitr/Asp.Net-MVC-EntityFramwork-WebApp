using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TeslaSolarWeb.ViewModel
{
    public class PrictureEdit
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        [DisplayName("Image")]
         public string ImageUrl { get; set; }
         public string ImageUrlSmall { get; set; }
    }
}