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
    
    public partial class quizzes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public quizzes()
        {
            this.play_record = new HashSet<play_record>();
            this.post = new HashSet<post>();
            this.quiz_answer = new HashSet<quiz_answer>();
            this.quiz_violations = new HashSet<quiz_violations>();
            this.use_record = new HashSet<use_record>();
            this.users = new HashSet<users>();
        }
    
        public int quiz_guid { get; set; }
        public string type_id { get; set; }
        public string tittle_text { get; set; }
        public string title_img_path { get; set; }
        public string title_video_path { get; set; }
        public short difficulty_id { get; set; }
        public int time { get; set; }
        public short score { get; set; }
        public int energy { get; set; }
        public bool visible { get; set; }
        public int like_num { get; set; }
        public int quiz_group { get; set; }
    
        public virtual difficulty_level difficulty_level { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<play_record> play_record { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<post> post { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_answer> quiz_answer { get; set; }
        public virtual quiz_group quiz_group1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_violations> quiz_violations { get; set; }
        public virtual type type { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<use_record> use_record { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<users> users { get; set; }
    }
}
