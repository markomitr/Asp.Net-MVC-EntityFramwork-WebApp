using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeslaSolarWeb.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        [DisplayName("Image")]    
        public string ImageUrl { get; set; }
        public string ImageUrlSmall
        {
            get
            {
                string [] delovi = this.ImageUrl.Split('.');
                string pateka = "";
                if (delovi.Length == 2)
                {
                    pateka = delovi[0] + "_small" + "." + delovi[1];
                }

                return pateka;
            }
        }
    }
}