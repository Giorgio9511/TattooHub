using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TattooHub.Application.Interfaces.Services
{
    //Servicio para almacenar archivos (Azure, AWS, etc...)
    //Otro PUERTO para infrastructure
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(
            Stream fileStream,
            string fileName,
            string contentType,
            CancellationToken cancellationToken = default);
        Task<bool> DeleteFileAsync(
            string fileUrl,
            CancellationToken cancellationToken = default);

        Task<Stream> DownloadFileAsync(
            string fileUrl,
            CancellationToken cancellationToken = default);
    }
}
