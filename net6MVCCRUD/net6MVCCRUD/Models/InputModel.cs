namespace net6MVCCRUD.Models
{
    public class InputModel
    {
        /// <summary>圖形驗證碼</summary>
        public string CaptchaCode { get; set; }

        /// <summary>建立支援的存放區為記憶體的資料流</summary>
        public MemoryStream CaptchaMemoryStream { get; set; }

        /// <summary>時間戳</summary>
        public DateTime Timestamp { get; set; }
    }
}
