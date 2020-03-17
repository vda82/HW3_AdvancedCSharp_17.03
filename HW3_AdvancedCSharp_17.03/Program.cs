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

            string path = @"C:\Users\Vitaliy_Dryha\Desktop\C# Anton";
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor("txt");

            SearchStart searchStart = new SearchStart();
            SearchFinish searchFinish = new SearchFinish();

            fileSystemVisitor.SearchStarted += searchStart.OnSearchStarted;
            fileSystemVisitor.SearchFinished += searchFinish.OnSearchFinished;

            foreach (var dirOrFile in fileSystemVisitor.GetAllDirectoriesAndFiles(path).Where(dir=>dir.Contains(fileSystemVisitor.Filter)))
            {
                Console.WriteLine(dirOrFile);
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

        public delegate void SearchFinishedEventHandler(object source, EventArgs args);

        public event SearchStartedEventHandler SearchFinished;

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
            OnSearchFinished();          
        }

        protected virtual void OnSearchStarted()
        {
            if (SearchStarted != null)
            {
                SearchStarted(this, EventArgs.Empty);
            }
        }

        protected virtual void OnSearchFinished()
        {
            if (SearchFinished != null)
            {
                SearchFinished(this, EventArgs.Empty);
            }
        }

    }

    public class SearchStart
    {
        public void OnSearchStarted(object source, EventArgs args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("-----Search Started-----");
            Console.ResetColor();
        }
    }

    public class SearchFinish
    {
        public void OnSearchFinished(object source, EventArgs args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("-----Search Finished-----", Console.ForegroundColor);
            Console.ResetColor();
        }
    }
}



