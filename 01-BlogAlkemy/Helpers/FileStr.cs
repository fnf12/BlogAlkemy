using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _01_BlogAlkemy.Helpers
{
    public class FileStr
    {
        public FileStr()
        {
        }
        public static Boolean create(string path,string folder,string filename, IFormFile file){
            try
            {
                var Ruta = System.IO.Path.Combine(path, folder, filename);
                var filestream = new System.IO.FileStream(Ruta, System.IO.FileMode.Create);
                file.CopyTo(filestream);
                filestream.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex.Message);
                return false;
            }
        }

        public static Boolean delete(string path, string folder, string file)
        {
            try
            {
                var ruta = System.IO.Path.Combine(path, folder, file);
                System.IO.File.Delete(ruta);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
