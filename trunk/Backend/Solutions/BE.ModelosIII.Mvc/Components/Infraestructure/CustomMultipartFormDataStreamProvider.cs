using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;

namespace BE.ModelosIII.Mvc.Components.Infraestructure
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path)
            : base(path)
        {
        }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            string name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? headers.ContentDisposition.FileName.Replace("\"", string.Empty) : "NoName";
            string extension = Path.GetExtension(name);
            string fileName = GetRandomFileName();

            return fileName + extension;
        }

        private string GetRandomFileName()
        {
            var bytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

           return BitConverter.ToString(bytes).Replace("-", string.Empty).ToLowerInvariant();
        }
    }
}