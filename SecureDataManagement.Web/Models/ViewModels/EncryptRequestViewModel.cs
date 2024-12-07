using System.ComponentModel.DataAnnotations;

namespace SecureDataManagement.Web.Models.ViewModels
{
    public class EncryptRequestViewModel
    {
        [Required]
        [MaxLength(1000)]
        public string? PlainText { get; set; }

        [Required]
        public string? Algorithm { get; set; }
        public IEnumerable<EncryptedDataViewModel>? EncryptedDatas { get; set; }
    }
}
