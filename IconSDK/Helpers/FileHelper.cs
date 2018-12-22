using System;
using System.IO;
using System.IO.Compression;

namespace IconSDK.Helpers
{
    public static class FileHelper
    {
        public static byte[] Compress(string path)
        {
            string tempZipFileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".zip";
            ZipFile.CreateFromDirectory(path, tempZipFileName);

            try
            {
                return File.ReadAllBytes(tempZipFileName);
            }
            finally
            {
                File.Delete(tempZipFileName);
            }
        }
    }
}