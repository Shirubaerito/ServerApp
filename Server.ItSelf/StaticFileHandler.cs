using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Server.ItSelf
{
    public class ControllersHandler : IHandler
    {
        private readonly Assembly _assembly;
        public ControllersHandler(Assembly controllersAssembly) 
        {
            this._assembly = controllersAssembly;
        }

        public void Handle(Stream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class StaticFileHandler : IHandler
    {
        private readonly string _path;
        public StaticFileHandler(string path)
        {
            _path = path;
        }


        public void Handle(Stream networkStream)
        {
            using (var reader = new StreamReader(networkStream))
            using (var writer = new StreamWriter(networkStream))
            {
                var firstLine = reader.ReadLine();
                for (string line = null; line != string.Empty; line = reader.ReadLine()) ;

                var request = RequestParser.Parse(firstLine);

                var filePath = Path.Combine(_path, request.Path.Substring(1));

                if (!File.Exists(filePath))
                {
                    ResponseWriter.WriteStatus(HttpStatusCode.NotFound, networkStream);
                }
                else

                {
                    ResponseWriter.WriteStatus(HttpStatusCode.OK, networkStream);
                    using (var fileStream = File.OpenRead(filePath))
                    {
                        fileStream.CopyTo(networkStream);
                    }
                }
               Console.WriteLine(filePath);
                //writer.WriteLine("Hello from server");
            }
        }
    }
}