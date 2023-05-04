using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using myContacts5.Models;

namespace myContacts5.Models
{
    public class ContactsViewModel
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}