using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_AdvancedCSharp_17._03
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> allDirsFilesList = new List<string>();
            string path = @"C:\Users\Vitaliy_Dryha\Desktop\C# Anton";
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor("txt");



            Start start = new Start();
            fileSystemVisitor.SearchStarted += start.OnSearchStarted;



            foreach (var dirOrFile in fileSystemVisitor.GetAllDirectoriesAndFiles(path))
            {
                allDirsFilesList.Add(dirOrFile);
            }

            IEnumerable<string> query = from file in allDirsFilesList
                                        where file.Contains(fileSystemVisitor.Filter)
                                        select file;

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }
    }

    public class FileSystemVisitor
    {
        private string filter;
        public string Filter { get; set; }
        public FileSystemVisitor(string filter)
        {
            this.Filter = filter;
        }

        public delegate void SearchStartedEventHandler(object source, EventArgs args);

        public event SearchStartedEventHandler SearchStarted;


        public IEnumerable<string> GetAllDirectoriesAndFiles(string path)
        {
            OnSearchStarted();

            string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                yield return file;
            }

            string[] dirs = Directory.GetDirectories(path);
            foreach (var dir in dirs)
            {
                yield return dir;
            }
            OnSearchStarted();          
        }

        protected virtual void OnSearchStarted()
        {
            if (SearchStarted != null)
            {
                SearchStarted(this, EventArgs.Empty);
            }
        }

    }

    public class Start
    {
        public void OnSearchStarted(object source, EventArgs args)
        {
            Console.WriteLine("Search Started");
        }
    }
}



