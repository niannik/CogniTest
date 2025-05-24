﻿using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces;

public interface IFileManager
{
    public Task<string> SaveFileAsync(IFormFile file, string folder, CancellationToken cancellationToken);
    public void Delete(string relativePath);

    public static class Folders
    {
        public const string Attachments = "Attachments";
        public const string Pictures = "Pictures";
        public const string Audio = "Audio";
    }
}
