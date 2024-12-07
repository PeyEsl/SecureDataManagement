namespace SecureDataManagement.Application.DTOs
{
    public class DecryptedDataDto
    {
        public int Id { get; set; }
        public string? PlainText { get; set; }
        public string? DecryptedText { get; set; }
        public int EncryptedDataId { get; set; }
    }
}
