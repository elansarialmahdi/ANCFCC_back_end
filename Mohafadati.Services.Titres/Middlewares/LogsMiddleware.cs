using Mohafadati.Services.Titres.Models.Dto;

namespace Mohafadati.Services.Titres.Middlewares
{
    public class LogsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string FilePath;
        public LogsMiddleware(RequestDelegate next)
        { 
            _next = next;
            FilePath = "C:\\Users\\AdminLachmachi\\Desktop\\Logs.txt";
        }
        public async Task InvokeAsync(HttpContext context)
        {
           
           
            context.Request.EnableBuffering();
            string bodyAsString = "";
            using (var reader = new StreamReader(
                context.Request.Body,
                encoding: System.Text.Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                bufferSize: 1024,
                leaveOpen: true))
            {
                bodyAsString = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0; 
            }


            string RequestLog = $"--> Requete faite le {DateTime.Now} -- Par: {context.Connection.RemoteIpAddress?.ToString()} -- Path: {context.Request.Path} -- Methode: {context.Request.Method} -- Contenu: {bodyAsString}{Environment.NewLine}";
            await File.AppendAllTextAsync(FilePath, RequestLog);





            var originalResponseBody = context.Response.Body;
            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;
                await _next(context);

                memoryStream.Position = 0;
                string responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

                memoryStream.Position = 0;
                await memoryStream.CopyToAsync(originalResponseBody);

                string ResponseLog = $"<-- Reponse faite le {DateTime.Now} -- Code: {context.Response.StatusCode} -- Contenu: {responseBody}{Environment.NewLine}";
                await File.AppendAllTextAsync(FilePath, ResponseLog);
                
            }





            
        }
    }
}
