namespace SecureDataManagement.Web.Models.ViewModels
{
    public class DecryptedDataViewModel
    {
        public int Id { get; set; }
        public string? PlainText { get; set; }
        public string? DecryptedText { get; set; }
        public string? Algorithm { get; set; }
        public int EncryptedDataId { get; set; }
    }
}
