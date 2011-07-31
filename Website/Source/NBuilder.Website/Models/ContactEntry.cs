using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NBuilderWebsite.Models
{
    public class ContactEntry
    {
        [Required]
        public string Name { get; set; }

        [Required]
        // DataTypeAttribute.IsValid is hard coded to return true!!!! What a big steaming pile.

        //[DataType(DataType.EmailAddress,ErrorMessage = "Must be a valid email address")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "You must provide a valid e-mail address")]
        public string EmailAddress { get; set; }

        [Required]
        public string Message { get; set; }
    }
}