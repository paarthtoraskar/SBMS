using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace IPLab11.Controllers
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path)
            : base(path)
        { }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? headers.ContentDisposition.FileName : "NoName";
            return name.Replace("\"", string.Empty);
            //this is here because Chrome submits files in quotation marks which get treated as part of the filename and get escaped
        }
    }

    public class FilesController : ApiController
    {
        public class FileDesc
        {
            public string Name { get; set; }
            public string Path { get; set; }
            public long Size { get; set; }

            public FileDesc(string n, string p, long s)
            {
                Name = n;
                Path = p;
                Size = s;
            }
        }

        // GET api/files
        public HttpResponseMessage Get()
        {
            string pathInAPIDomain = HttpContext.Current.Server.MapPath("~/App_Data");

            var appRoot = Directory.GetParent(Directory.GetParent(pathInAPIDomain).ToString()).ToString();
            if (!Directory.Exists(appRoot + "\\SBMS" + "\\Products"))
                Directory.CreateDirectory(appRoot + "\\SBMS" + "\\Products");
            var pathInAppDomain = appRoot + "\\SBMS" + "\\Products";

            //string[] files = Directory.GetFiles(pathInAPIDomain);
            string[] files = Directory.GetFiles(pathInAppDomain);

            for (int i = 0; i < files.Count(); ++i)
                files[i] = System.IO.Path.GetFileName(files[i]);
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ObjectContent<IEnumerable<string>>(files, new JsonMediaTypeFormatter());
            return response;
        }

        // GET api/files/5
        public HttpResponseMessage Get(int id)
        {
            string pathInAPIDomain = HttpContext.Current.Server.MapPath("~/App_Data");

            var appRoot = Directory.GetParent(Directory.GetParent(pathInAPIDomain).ToString()).ToString();
            if (!Directory.Exists(appRoot + "\\SBMS" + "\\Products"))
                Directory.CreateDirectory(appRoot + "\\SBMS" + "\\Products");
            var pathInAppDomain = appRoot + "\\SBMS" + "\\Products";

            //string[] files = Directory.GetFiles(pathInAPIDomain);
            string[] files = Directory.GetFiles(pathInAppDomain);

            for (int i = 0; i < files.Count(); ++i)
                files[i] = System.IO.Path.GetFileName(files[i]);

            //string filePath = HttpContext.Current.Server.MapPath("~/App_Data/" + files[id]);
            string filePath = pathInAppDomain + "\\" + files[id];
            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(filePath);

            if (!File.Exists(filePath))
                throw new HttpResponseException(HttpStatusCode.NotFound);

            try
            {
                Stream fileStream = File.Open(filePath, FileMode.Open);

                MemoryStream responseStream = new MemoryStream();
                fileStream.CopyTo(responseStream);
                fileStream.Close();

                responseStream.Position = 0;

                HttpResponseMessage response = new HttpResponseMessage();
                bool fullContent = true;
                response.StatusCode = fullContent ? HttpStatusCode.OK : HttpStatusCode.PartialContent;
                response.Content = new StreamContent(responseStream);
                return response;
            }
            catch (IOException)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        // POST api/files
        public Task<IEnumerable<FileDesc>> Post()
        {
            var folderName = "App_Data";
            var pathInAPIDomain = HttpContext.Current.Server.MapPath("~/" + folderName);

            var appRoot = Directory.GetParent(Directory.GetParent(pathInAPIDomain).ToString()).ToString();
            if (!Directory.Exists(appRoot + "\\SBMS" + "\\Products"))
                Directory.CreateDirectory(appRoot + "\\SBMS" + "\\Products");
            var pathInAppDomain = appRoot + "\\SBMS" + "\\Products";

            var rootUrl = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.AbsolutePath, String.Empty);

            if (Request.Content.IsMimeMultipartContent())
            {
                //var streamProvider = new CustomMultipartFormDataStreamProvider(pathInAPIDomain);
                var streamProvider = new CustomMultipartFormDataStreamProvider(pathInAppDomain);
                var task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith<IEnumerable<FileDesc>>(t =>
                {
                    if (t.IsFaulted || t.IsCanceled)
                    {
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                    }
                    var fileInfo = streamProvider.FileData.Select(i =>
                    {
                        var info = new FileInfo(i.LocalFileName);
                        return new FileDesc(info.Name, rootUrl + "/" + folderName + "/" + info.Name, info.Length / 1024);
                    });
                    return fileInfo;
                });
                return task;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }

        // PUT api/files/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/files/5
        public void Delete(int id)
        {
        }
    }
}