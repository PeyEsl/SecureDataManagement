namespace SecureDataManagement.Core.Entities
{
    public class DecryptedData
    {
        public int Id { get; set; }                     // کلید اصلی
        public string? PlainText { get; set; }          // متن ورودی کاربر
        public string? DecryptedText { get; set; }      // متن رمزگشایی‌شده (اختیاری)
        public DateTime CreatedAt { get; set; }         // زمان ایجاد
        public int EncryptedDataId { get; set; }        // کلید خارجی جدول رمزنگاری
        public EncryptedData? EncryptedData { get; set; }
    }
}
