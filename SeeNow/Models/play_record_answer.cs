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

    public partial class play_record_answer
    {
        [DisplayName("遊戲紀錄流水號")]
        public int play_guid { get; set; }
        [DisplayName("帳號")]
        public string account { get; set; }
        [DisplayName("答題答案")]
        public int select_answer { get; set; }
        [DisplayName("遊戲題目流水號")]
        public int quiz_guid { get; set; }
    
        public virtual play_record play_record { get; set; }
    }
}