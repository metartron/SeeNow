//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SeeNow.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class post_violations
    {
        public int violation_quid { get; set; }
        public string reporter_id { get; set; }
        public int post_quid { get; set; }
        public string manager_id { get; set; }
        public bool check_flag { get; set; }
        public System.DateTime datetime { get; set; }
        public string violation_reason { get; set; }
    
        public virtual manager manager { get; set; }
        public virtual post post { get; set; }
        public virtual users users { get; set; }
    }
}
