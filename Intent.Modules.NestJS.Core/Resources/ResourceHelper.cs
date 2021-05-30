using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Intent.Modules.NestJS.Core.Resources
{
    public static class ResourceHelper
    {
        public const string ZipFileName = "nestjs-bootstrap";
        const string ZipFileResourcePath = "Intent.Modules.NestJS.Core" + ".Resources." + ZipFileName + ".zip";

        public static readonly IReadOnlyCollection<string> ImageFiles = new string[]
        {
            "favicon.ico",
            "icon.jpg",
            "icon.png"
        };

        public static void ReadQuickstartFileContents(Action<ZipArchive> readContentsAction)
        {
            var zipFileStream = GetFileFromResource(ZipFileResourcePath);
            using (var archive = new ZipArchive(zipFileStream, ZipArchiveMode.Read))
            {
                readContentsAction(archive);
            }
        }

        private static Stream GetFileFromResource(string resourcePath)
        {
            var stream = typeof(ResourceHelper).Assembly.GetManifestResourceStream(resourcePath);
            return stream;
        }
    }
}