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
    
    public partial class profile_log
    {
        public string manager_id { get; set; }
        public short profile_id { get; set; }
        public string profile_name { get; set; }
        public string profile_path { get; set; }
        public string status { get; set; }
        public System.DateTime datetime { get; set; }
    
        public virtual manager manager { get; set; }
        public virtual profile profile { get; set; }
    }
}
