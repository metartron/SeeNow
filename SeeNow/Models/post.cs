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

    public partial class post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public post()
        {
            this.post_violations = new HashSet<post_violations>();
        }

        [DisplayName("貼文流水號")]
        public int post_quid { get; set; }
        [DisplayName("遊戲題目流水號")]
        public int quiz_guid { get; set; }
        [DisplayName("內容")]
        public string content { get; set; }
        [DisplayName("發文時間")]
        public System.DateTime datetime { get; set; }
        [DisplayName("是否鎖定")]
        public bool lock_flag { get; set; }
        [DisplayName("帳號")]
        public string account { get; set; }
    
        public virtual users users { get; set; }
        public virtual quizzes quizzes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<post_violations> post_violations { get; set; }
    }
}
