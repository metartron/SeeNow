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
    using System.ComponentModel;

    public partial class user_violations
    {
        [DisplayName("檢舉流水號")]
        public int violation_quid { get; set; }
        [DisplayName("檢舉人帳號")]
        public string repoter_id { get; set; }
        [DisplayName("被檢舉帳號")]
        public string report_id { get; set; }
        [DisplayName("管理員帳號")]
        public string manager_id { get; set; }
        [DisplayName("是否處理")]
        public bool check_flag { get; set; }
        [DisplayName("檢舉時間")]
        public System.DateTime datetime { get; set; }
        [DisplayName("檢舉原因")]
        public string violation_reason { get; set; }
    
        public virtual manager manager { get; set; }
        public virtual users users { get; set; }
        public virtual users users1 { get; set; }
    }
}
