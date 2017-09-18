using System.IO;
using System.Reflection;

namespace PersonImport
{
    class Program
    {
        static void Main(string[] args)
        {
            // import data.csv file and convert to two output files for frequency and address
            var import = new Import();
            import.ImportFile(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\data.csv");
        }
    }
}
