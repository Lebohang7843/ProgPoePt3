using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ClaimsManagementApp.Services
{
    public class FileService : IFileService
    {
        private readonly string _uploadPath = "Uploads";
        private readonly long _maxFileSize = 5 * 1024 * 1024; // 5MB
        private readonly string[] _allowedExtensions = { ".pdf", ".docx", ".xlsx" };

        public FileService()
        {
            if (!Directory.Exists(_uploadPath))
                Directory.CreateDirectory(_uploadPath);
        }

        public async Task<string> SaveFileAsync(IFormFile file, string claimId)
        {
            if (!ValidateFile(file))
                throw new InvalidOperationException("File validation failed");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(_uploadPath, fileName);

            // Encrypt file before saving
            await EncryptAndSaveFileAsync(file, filePath);

            return fileName;
        }

        public async Task<(byte[] content, string contentType, string fileName)> GetFileAsync(string storedFileName)
        {
            var filePath = Path.Combine(_uploadPath, storedFileName);
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found");

            var decryptedContent = await DecryptFileAsync(filePath);
            var contentType = GetContentType(storedFileName);

            return (decryptedContent, contentType, storedFileName);
        }

        public bool ValidateFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            if (file.Length > _maxFileSize)
                return false;

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
                return false;

            return true;
        }

        private async Task EncryptAndSaveFileAsync(IFormFile file, string filePath)
        {
            using var aes = Aes.Create();

            // Use a properly sized key (exactly 32 bytes for AES-256)
            // "Your32ByteEncryptionKey1234" is exactly 32 bytes in UTF-8
            string encryptionKey = "Your32ByteEncryptionKey1234";
            aes.Key = Encoding.UTF8.GetBytes(encryptionKey);

            // Generate a random IV for better security
            aes.GenerateIV();

            using var inputStream = file.OpenReadStream();
            using var outputStream = new FileStream(filePath, FileMode.Create);

            // Write the IV to the beginning of the file
            await outputStream.WriteAsync(aes.IV, 0, aes.IV.Length);

            using var cryptoStream = new CryptoStream(outputStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            await inputStream.CopyToAsync(cryptoStream);

            // Ensure the crypto stream is flushed and closed
            cryptoStream.FlushFinalBlock();
        }

        private async Task<byte[]> DecryptFileAsync(string filePath)
        {
            using var aes = Aes.Create();

            // Use the same key as encryption
            string encryptionKey = "Your32ByteEncryptionKey1234";
            aes.Key = Encoding.UTF8.GetBytes(encryptionKey);

            using var inputStream = new FileStream(filePath, FileMode.Open);

            // Read the IV from the beginning of the file
            byte[] iv = new byte[16];
            int bytesRead = await inputStream.ReadAsync(iv, 0, iv.Length);
            if (bytesRead != 16)
            {
                throw new InvalidOperationException("Could not read IV from file");
            }
            aes.IV = iv;

            using var cryptoStream = new CryptoStream(inputStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using var memoryStream = new MemoryStream();
            await cryptoStream.CopyToAsync(memoryStream);

            return memoryStream.ToArray();
        }

        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".pdf" => "application/pdf",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream"
            };
        }
    }
}