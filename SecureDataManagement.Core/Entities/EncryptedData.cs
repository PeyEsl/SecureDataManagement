namespace SecureDataManagement.Core.Entities
{
    public class EncryptedData
    {
        public int Id { get; set; }                     // کلید اصلی
        public string? PlainText { get; set; }          // متن ورودی کاربر
        public string? EncryptedText { get; set; }      // متن رمزنگاری‌شده
        public string? IV { get; set; }                 // Initial Vector برای الگوریتم‌هایی مثل AES
        public string? PublicKey { get; set; }          // کلید عمومی برای الگوریتم‌هایی مثل RSA
        public string? DecryptKey { get; set; }          // کلید رمزگشایی
        public string? Algorithm { get; set; }          // نوع الگوریتم (AES, RSA, و غیره)
        public DateTime CreatedAt { get; set; }         // زمان ایجاد
    }
}
