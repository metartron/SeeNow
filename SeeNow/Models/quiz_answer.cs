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

    public partial class quiz_answer
    {
        [DisplayName("遊戲答案流水號")]
        public int quiz_guid { get; set; }
        [DisplayName("答案編號")]
        public int answer_id { get; set; }
        [DisplayName("型態編號")]
        public string type_id { get; set; }
        [DisplayName("文字答案")]
        public string answer_text { get; set; }
        [DisplayName("圖片答案")]
        public string answer_img_path { get; set; }
        [DisplayName("是否正確")]
        public bool is_correct { get; set; }
    
        public virtual quizzes quizzes { get; set; }
        public virtual type type { get; set; }
    }
}
