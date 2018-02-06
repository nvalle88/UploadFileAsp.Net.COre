# UploadFile

Es una libreria que facilita la subida de archivos en asp net core
## Como Usarlo
```csharp
//para subir un archivo
DataFile file = new DataFile();
var upload1file = await UploadFile.Services.UploadFileService.UploadFile(file, folder, hostingEnvironment.WebRootPath);

//para subir varios archivos
List<IFormFile> files;
var uploadFiles = await UploadFile.Services.UploadFileService.UploadFile(files, folder, hostingEnvironment.WebRootPath);

//las respuesta seran un boleano
```
la clase DataFile para el envio de archivos
---csharp
public class DataFile
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public byte[] FileData { get; set; }
    }
---

![alt tag](https://github.com/nvalle88/ServiciosWeb/blob/master/DS.png)
