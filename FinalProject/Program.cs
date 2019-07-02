using System;
using System.Collections.Generic;
using System.IO;

using DataWorkTable;
using LearningAlgorithms;

namespace MLProj2018
{
    // static class to manage the input and output stream
    static class InOutStreams
    {
        private static string PathIn = Directory.GetCurrentDirectory();
        private static string PathOut = Directory.GetCurrentDirectory();
        private static string PathTemp = Directory.GetCurrentDirectory();

        public static void GoToFolder()
        {
            bool DirectoryDetected = false;
            while (!DirectoryDetected) // as long as we've not jumped to the folder where InOut lays, we continue to rise level
            {
                PathTemp = Directory.GetParent(PathTemp).ToString();
                if (Directory.Exists(PathTemp + @"\InOut"))
                {
                    DirectoryDetected = true;
                }
            }
            PathTemp += @"\InOut";
            PathIn = PathTemp + @"\Source\";
            PathOut = PathTemp + @"\Drop\";
        }
        
        // in order to make the paths private (so that to protect them, since are crucial for the program to run propertly),
        // getter methods are necessary to access the path from everywhere in the code
        public static string GetPathIn()
        {
            return PathIn;
        }

        public static string GetPathOut()
        {
            return PathOut;
        }

        // we create the directory related to the chosen input
        public static void DirectoryManager(int FlagDataSet, Dictionary<int,string> dict)
        {
            if (!Directory.Exists(PathOut += dict[FlagDataSet]))
            {                
                Directory.CreateDirectory(PathOut);
            }

            PathIn = PathIn + dict[FlagDataSet] + ".csv";
        }

        // according to whether the partition, the preprocessing, the proper subfolder is created
        // NOTE: being Paths fields static, simply calling InOutStreams.(..) we can access them,
        // being here in their same class scope
        public static void DirectoryCreation(int FlagPartition, int FlagPrincipalComponents, int PrincipalComponentsNumber)
        {
            string FilePath = PathOut;
            if (!Directory.Exists(FilePath + @"\DataPartitions"))
            {
                Directory.CreateDirectory(FilePath + @"\DataPartitions");
            }
            FilePath += @"\DataPartitions";

            if (FlagPartition == 0)
            {
                if (!Directory.Exists(FilePath + @"\10percentTestSet"))
                {
                    Directory.CreateDirectory(FilePath + @"\10percentTestSet");
                }
                FilePath += @"\10percentTestSet";
                InOutStreams.PathOut = FilePath;
                
            }
            else if (FlagPartition == 1)
            {
                if (!Directory.Exists(FilePath + @"\33percentTestSet"))
                {
                    Directory.CreateDirectory(FilePath + @"\33percentTestSet");
                }
                FilePath += @"\33percentTestSet";
                InOutStreams.PathOut = FilePath;
                
            }
            PathOut = FilePath;
        }
    }

    

    class Program
    {
        // main program, which calls whatever is needed for the analysis
        static void Main(string[] args)
        {
            DateTime GlobalClock = DateTime.Now;
            Console.WriteLine("I N I Z I O _ P R O G R A M M A.\n\n\n");

            // a dictionary is used to match input files names and integers that are required to the user
            Dictionary<int, string> dict = new Dictionary<int, string>
            {
                { 0, "WineDS" },
                { 1, "BioMechDS" },
                { 2, "IrisDS" }
            };

            // being InOutStream static, it need no instantiation
            InOutStreams.GoToFolder();
            Console.WriteLine(InOutStreams.GetPathOut());

            Console.WriteLine("Scelta del DataSet:\nWine DataSet: premere 0\nBioMechanical DataSet: premere 1\nIris DataSet: premere 2");
            int FlagDataSet = Convert.ToInt32(Console.ReadLine());
            
            InOutStreams.DirectoryManager(FlagDataSet, dict);

            // is worth note that even if DataObject is the heart of the data modelling,  prior instantiation
            // of the class containing the methds for import is necessary, then the DataObject is instantiated
            // and filled in with the data read by the DataSetImport methods
            DataSetImport DSImp = new DataSetImport();
            DataObject DataSet = DSImp.FeaturesImport();

            Console.WriteLine("\n\nPreprocessare i dati? (PCA):\nNo: premere 0\nSi: premere 1");
            int FlagProcessing = Convert.ToInt32(Console.ReadLine());
            if (FlagProcessing == 1)
            {
                DataProcessing process = new DataProcessing();
                DataSet = process.DoThePCA(DataSet);
                DataProcessing.SetFlagProcessing(FlagProcessing);
            }
            // NOTE: in DataWorkTable.DataProcessing two static field are located. Here, according to the
            // user choice to preprocess or not, the if () statement above is considered (or not), BUT to
            // the information about data preprocessing choice must be in any case remembered, thus, in the 
            // if() body the DataProcessing class is instantiated, and eventually the static field about choice 
            // and number of PCs stored, but if the if() is not executed, such choices are not even present,
            // and they are needed in the following. So the static field initialised to 0 is anyway present
            // and solves this problem

            Console.WriteLine("\n\nPartizione del DataSet?");
            Console.WriteLine("TrainingSet = 90% del DataSet e TestSet = 10% del DataSet: premere 0");
            Console.WriteLine("TrainingSet = 66% del DataSet e TestSet = 33% del DataSet: premere 1");
            int FlagPartition = Convert.ToInt32(Console.ReadLine());

            InOutStreams.DirectoryCreation(FlagPartition, DataProcessing.GetFlagProcessing(), DataProcessing.GetPrincipalComponentsNumber());
            Console.WriteLine("\n\nDrop folder: \n" + InOutStreams.GetPathOut());

            Console.WriteLine("\n\nSingle run o ciclo? (inserire 1 per single run, altrimenti numero di iterazioni)");
            int iter = Convert.ToInt32(Console.ReadLine());
                        
            for (int i = 0; i < iter; i++)
            {
                if (iter > 1)
                {
                    Console.WriteLine("Iterazione {0} / {1}", i + 1, iter);
                }
                
                DataPartition Partition = new DataPartition(DataSet, FlagPartition);
                DataObject[] SetsData = Partition.TestSetExtraction(DataSet);
            
                Partition.PrintPartition(SetsData);
            
                Console.WriteLine("\n\n");
            
                LearningAlgorithmsClass la = new LearningAlgorithmsClass(SetsData, FlagPartition, DataProcessing.GetFlagProcessing(), DataProcessing.GetPrincipalComponentsNumber());
                
                // given all the previous choices, the whole learning machinery is deployed
                la.SVMGaussianKernel();
                Console.WriteLine("\n\n");
                la.SVMPolinomialKernel();
                Console.WriteLine("\n\n");
                la.TreeLearning();
                Console.WriteLine("\n\n");
            }
            



            Console.WriteLine("\n\n");
            Console.WriteLine("\n\n\nTempo di esecuzione: " + (DateTime.Now - GlobalClock).TotalSeconds + " secondi.");
            Console.WriteLine("\nF I N E _ P R O G R A M M A.");
            Console.WriteLine("Premi qualsiasi tasto per uscire.");
            Console.ReadKey();
        }
    }
}
