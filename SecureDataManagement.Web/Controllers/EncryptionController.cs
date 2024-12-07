using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecureDataManagement.Application.DTOs;
using SecureDataManagement.Application.Interfaces;
using SecureDataManagement.Application.Services;
using SecureDataManagement.Web.Models.ViewModels;

namespace SecureDataManagement.Web.Controllers
{
    public class EncryptionController : Controller
    {
        #region Ctor

        private readonly IEncryptionService _encryptionService;
        private readonly IDecryptionService _decryptionService;
        private readonly IAESService _aesService;
        private readonly IRSAService _rsaService;
        private readonly IHashingService _hashingService;
        private readonly IKeyDerivationService _keyDerivationService;
        private readonly IDataProtectionService _dataProtectionService;
        private readonly IHMACService _hmacService;
        private readonly IMapper _mapper;

        public EncryptionController(
            IEncryptionService encryptionService,
            IDecryptionService decryptionService,
            IAESService aesService,
            IRSAService rsaService,
            IHashingService hashingService,
            IKeyDerivationService keyDerivationService,
            IDataProtectionService dataProtectionService,
            IHMACService hmacService,
            IMapper mapper)
        {
            _encryptionService = encryptionService;
            _decryptionService = decryptionService;
            _aesService = aesService;
            _rsaService = rsaService;
            _hashingService = hashingService;
            _keyDerivationService = keyDerivationService;
            _dataProtectionService = dataProtectionService;
            _hmacService = hmacService;
            _mapper = mapper;
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var encrypt = await _encryptionService.GetAllAsync() ?? Enumerable.Empty<EncryptedDataDto>();

                var model = new EncryptRequestViewModel
                {
                    EncryptedDatas = _mapper.Map<IEnumerable<EncryptedDataViewModel>>(encrypt)
                };

                return View(model);
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while retrieving encrypted data. Please try again..";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Encrypt(EncryptRequestViewModel request)
        {
            if (string.IsNullOrEmpty(request.PlainText) || string.IsNullOrEmpty(request.Algorithm))
            {
                ModelState.AddModelError("", "Please provide valid data.");
                return View(nameof(Index));
            }

            try
            {
                var hmacKey = HMACService.GenerateRandomKey();
                (string publicKey, string privateKey) = _rsaService.GenerateKeys();

                EncryptedDataDto record = new EncryptedDataDto
                {
                    PlainText = request.PlainText,
                    Algorithm = request.Algorithm,
                };

                switch (request.Algorithm.ToUpper())
                {
                    case "AES":
                        var aesResult = _aesService.Encrypt(request.PlainText);
                        record.EncryptedText = aesResult.EncryptedText;
                        record.IV = aesResult.IV;
                        record.DecryptKey = aesResult.Key;
                        break;
                    case "RSA":
                        record.EncryptedText = _rsaService.Encrypt(request.PlainText, publicKey);
                        record.PublicKey = publicKey;
                        record.DecryptKey = privateKey;
                        break;
                    case "HASHING":
                        record.EncryptedText = _hashingService.GenerateHash(request.PlainText);
                        break;
                    case "KEYDERIVATION":
                        var derivationResult = _keyDerivationService.GenerateKey(request.PlainText);
                        record.EncryptedText = derivationResult.DerivedKey;
                        record.DecryptKey = derivationResult.Salt;
                        break;
                    case "DATAPROTECTION":
                        record.EncryptedText = _dataProtectionService.Protect(request.PlainText);
                        break;
                    case "HMAC":
                        record.EncryptedText = _hmacService.GenerateSignature(request.PlainText);
                        record.DecryptKey = hmacKey;
                        break;
                    default:
                        ModelState.AddModelError("", "Invalid algorithm type selected.");
                        return View(nameof(Index));
                }

                var result = await _encryptionService.AddAsync(record);
                if (result)
                {
                    TempData["SuccessMessage"] = "Encryption successful!";
                    return RedirectToAction(nameof(Index));
                }

                TempData["ErrorMessage"] = "Encryption failed!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while encrypting the plain text. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Decrypt(int? id)
        {
            if (!id.HasValue)
            {
                TempData["ErrorMessage"] = "Encrypt ID is null!";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var encrypted = await _encryptionService.GetByIdAsync(id.Value);
                if (encrypted == null)
                {
                    TempData["ErrorMessage"] = $"No encryption data was found for {id.Value}.";
                    return RedirectToAction(nameof(Index));
                }

                string? decryptedText = null;
                bool isVerified = false;

                switch (encrypted.Algorithm?.ToUpperInvariant())
                {
                    case "AES":
                        decryptedText = _aesService.Decrypt(encrypted.EncryptedText!, encrypted.IV!, encrypted.DecryptKey!);
                        break;
                    case "RSA":
                        decryptedText = _rsaService.Decrypt(encrypted.EncryptedText!, encrypted.DecryptKey!);
                        break;
                    case "HASHING":
                        isVerified = _hashingService.VerifyHash(encrypted.PlainText!, encrypted.EncryptedText!);
                        break;
                    case "KEYDERIVATION":
                        isVerified = _keyDerivationService.VerifyKey(encrypted.PlainText!, encrypted.EncryptedText!, encrypted.DecryptKey!);
                        break;
                    case "DATAPROTECTION":
                        decryptedText = _dataProtectionService.Unprotect(encrypted.EncryptedText!);
                        break;
                    case "HMAC":
                        _hmacService.SetKey(encrypted.DecryptKey!);
                        isVerified = _hmacService.VerifySignature(encrypted.PlainText!, encrypted.EncryptedText!);
                        break;
                    default:
                        ModelState.AddModelError("", "Invalid algorithm type selected.");
                        return View(nameof(Index));
                }

                var dto = new DecryptedDataDto
                {
                    PlainText = encrypted.PlainText,
                    EncryptedDataId = encrypted.Id,
                    DecryptedText = isVerified ? "Verified" : decryptedText,
                };

                var decrypt = await _decryptionService.GetByEncryptedIdAsync(dto.EncryptedDataId);
                if (decrypt == null)
                {
                    var regResult = await _decryptionService.AddAsync(dto);
                    if (!regResult)
                    {
                        TempData["ErrorMessage"] = "Failed to register decryption data.";
                        return RedirectToAction(nameof(Index));
                    }
                }

                var model = _mapper.Map<DecryptedDataViewModel>(decrypt ?? dto);
                model.Algorithm = encrypted.Algorithm;

                return View(model);
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while decrypting the plain text. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> DeleteEncrypt(int? id)
        {
            if (!id.HasValue)
            {
                TempData["ErrorMessage"] = "Encrypt ID is null!";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var encrypted = await _encryptionService.GetByIdAsync(id.Value);
                if (encrypted == null)
                {
                    TempData["ErrorMessage"] = $"No encryption data was found for {id.Value}.";
                    return RedirectToAction(nameof(Index));
                }

                await _encryptionService.DeleteAsync(id.Value);

                TempData["SuccessMessage"] = "رمزنگاری با موفقیت حذف شد.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the encryption. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> DeleteDecrypt(int? id)
        {
            if (!id.HasValue)
            {
                TempData["ErrorMessage"] = "Decrypt ID is null!";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var decrypted = await _decryptionService.GetByIdAsync(id.Value);
                if (decrypted == null)
                {
                    TempData["ErrorMessage"] = $"No decryption data was found for {id.Value}.";
                    return RedirectToAction(nameof(Index));
                }

                await _decryptionService.DeleteAsync(id.Value);

                TempData["SuccessMessage"] = "رمزگشایی با موفقیت حذف شد.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the decryption. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
