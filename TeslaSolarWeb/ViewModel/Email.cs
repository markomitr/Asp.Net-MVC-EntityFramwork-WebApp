using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeslaSolarWeb.ViewModel
{
    public class EmailMsg
    {
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Subject { get; set; }

        [Required,DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public bool IsSent { get; set; }

        public string Poraka { get; set; }
        public string ErrorMsg { get; set; }
    }
}