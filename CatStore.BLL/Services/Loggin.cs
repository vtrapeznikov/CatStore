using System.IO;

namespace CatStore.BLL.Services
{
    public class Loggin
    {
        public static void CreateLog(string message)
        {
            File.AppendAllText(System.Web.HttpContext.Current.Server.MapPath(@"\Logs\log.txt"), message + "\n");
        }
    }
}
