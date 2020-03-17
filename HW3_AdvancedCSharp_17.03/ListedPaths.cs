using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_AdvancedCSharp_17._03
{
    public class ListedPaths
    {
        public delegate void SearchStartedEventHandler(object source, EventArgs args);

        public event SearchStartedEventHandler SearchStarted;

        public List<string> GetListOfFilesAndDirectories()
        {
            OnSearchStarted();
            List<string> allDirsFilesList = new List<string>();
            List<string> filteredFiles = new List<string>();
            string path = @"C:\Users\Vitaliy_Dryha\Desktop\C# Anton";
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor("txt");
            foreach (var dirOrFile in fileSystemVisitor.GetAllDirectoriesAndFiles(path))
            {
                allDirsFilesList.Add(dirOrFile);
            }
            
            IEnumerable<string> query = from file in (List<string>)allDirsFilesList
                                 where file.Contains(fileSystemVisitor.Filter)
                                 select file;

            foreach (var item in query)
            {
                filteredFiles.Add(item);
            }

            return filteredFiles;
        }
        
        protected virtual void OnSearchStarted()
        {
            if (SearchStarted!=null)
            {
                SearchStarted(this, EventArgs.Empty);
            }
        }
    }
}
