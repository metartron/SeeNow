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
    using System.ComponentModel.DataAnnotations;

    public partial class quiz_group
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public quiz_group()
        {
            this.quizzes = new HashSet<quizzes>();
        }

        [DisplayName("遊戲題組編號")]
        [Required(ErrorMessage = "欄位不可空白")]
        public int quiz_group1 { get; set; }
        [DisplayName("遊戲題組名稱")]
        [Required(ErrorMessage = "欄位不可空白")]
        [StringLength(510, ErrorMessage = "欄位長度510字內")]
        public string group_name { get; set; }
        [DisplayName("老師帳號")]
        [Required(ErrorMessage = "欄位不可空白")]
        [StringLength(20, ErrorMessage = "欄位長度20字內")]
        public string account { get; set; }
        [DisplayName("題庫類別編號")]
        [Required(ErrorMessage = "欄位不可空白")]
        public int category_id { get; set; }
    
        public virtual category category { get; set; }
        public virtual users users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quizzes> quizzes { get; set; }
    }
}
