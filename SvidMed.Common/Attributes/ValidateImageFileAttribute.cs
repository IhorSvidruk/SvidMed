﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SvidMed.Common.Attributes
{
    public class ValidateImageFileAttribute : ValidationAttribute
    {
        private const int MaxFileLengthInBytes = 1048576; // = (1 * 1024 * 1024) = 1 MB;

        public override bool IsValid(object value)
        {
            // Represents the file sent with the HttpRequest
            var file = value as IFormFile;
            if (file == null)
            {
                return false;
            }

            if (file.Length > MaxFileLengthInBytes)
            {
                return false;
            }

            // Check the image mime types
            return file.ContentType.ToLower() == "image/jpg" || file.ContentType.ToLower() == "image/jpeg" || file.ContentType.ToLower() == "image/png";
        }
    }
}
