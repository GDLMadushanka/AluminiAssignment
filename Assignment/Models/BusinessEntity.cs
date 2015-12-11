using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment.Models
{
    public class BusinessEntity
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailName { get; set; }
        public string Mobile { get; set; }
        public string NIC { get; set; }
        public string YearOfLeaving { get; set; }
        public string LastClass { get; set; }
        public string Address { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string Occupation { get; set; }
        public string ProfessionalQualifications { get; set; }
        public string ActivitiesDuringSchool { get; set; }
        public string AdmissionNumber { get; set; }
        public Nullable<System.DateTime> DateOfAdmission { get; set; }
        public bool Approved { get; set; }
    }
}