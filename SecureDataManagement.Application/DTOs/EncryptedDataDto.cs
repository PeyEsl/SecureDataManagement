namespace SecureDataManagement.Application.DTOs
{
    public class EncryptedDataDto
    {
        public int Id { get; set; }
        public string? PlainText { get; set; }
        public string? EncryptedText { get; set; }
        public string? IV { get; set; }
        public string? PublicKey { get; set; }
        public string? DecryptKey { get; set; }
        public string? Algorithm { get; set; }
    }
}
