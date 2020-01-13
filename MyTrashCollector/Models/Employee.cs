using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyTrashCollector.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Zip { get; set; }

        public string DesiredDayToView { get; set; }
    }
}