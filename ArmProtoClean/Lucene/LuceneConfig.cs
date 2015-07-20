using System;
using System.IO;
using Lucene.Net.Index;
using Lucene.Net.Store;

namespace ArmProtoClean.Lucene
{

    internal static class LuceneConfig
    {

        // properties
        // To get bin not debug: Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        //To get path of Solution/project:Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        public static string LuceneDir = Environment.CurrentDirectory + "\\LuceneIndex";
        public const int HitsLimit = 100;

        private static FSDirectory _directoryTemp;

        public static FSDirectory Directory
        {
            get
            {
                if (_directoryTemp == null)
                    _directoryTemp = FSDirectory.Open(new DirectoryInfo(LuceneDir));
                if (IndexWriter.IsLocked(_directoryTemp))
                    IndexWriter.Unlock(_directoryTemp);
                var lockFilePath = Path.Combine(LuceneDir, "write.lock");
                if (File.Exists(lockFilePath))
                    File.Delete(lockFilePath);
                return _directoryTemp;
            }
        }

    }

}