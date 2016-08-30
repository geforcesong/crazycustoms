using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace CrazyCustomsWeb.Models.WebModels
{
    public class ErrorObject
    {
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }

        public string ToJson()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(this);
        }
    }
}