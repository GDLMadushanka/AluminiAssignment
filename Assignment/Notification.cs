//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Assignment
{
    using System;
    using System.Collections.Generic;
    
    public partial class Notification
    {
        public int notificationID { get; set; }
        public string UserName { get; set; }
        public bool Caught { get; set; }
        public string Destination { get; set; }
        public string NotificationText { get; set; }
        public Nullable<int> ProposalID { get; set; }
        public Nullable<int> MeetingID { get; set; }
        public Nullable<int> AmmendID { get; set; }
    }
}
