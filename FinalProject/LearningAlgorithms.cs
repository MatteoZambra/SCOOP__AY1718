using System;
using System.IO;

using DataWorkTable;

using Accord.MachineLearning.VectorMachines.Learning;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Kernels;

using MLProj2018;

namespace LearningAlgorithms
{
    class LearningAlgorithmsClass
    {
        private readonly DataObject[] DataSets;
        private DateTime clock;
        private readonly int FlagPartition;
        private readonly int FlagPCAPreprocessing;
        private readonly int FlagPrincipalComponentsNumbers;
        
        public LearningAlgorithmsClass(DataObject[] Data, int Partition, int FlagPCA, int FlagPCN)
        {
            this.DataSets = Data;
            this.FlagPartition = Partition;
            this.FlagPCAPreprocessing = FlagPCA;
            this.FlagPrincipalComponentsNumbers = FlagPCN;
        }


        // Print utility method to export results on files
        public void PrintReport(int[] predicted, double error, string Algrthm)
        {
            string PathFile = InOutStreams.GetPathOut();
            if (FlagPCAPreprocessing == 0)
            {
                PathFile += @"\RealVsPredicted" + Algrthm + ".dat";
            }
            if (FlagPCAPreprocessing == 1)
            {
                string PCN = Convert.ToString(this.FlagPrincipalComponentsNumbers);
                PathFile += @"\RealVsPredicted" + Algrthm + "_" + PCN + "PCs.dat";
            }
            
            StreamWriter write = new StreamWriter(PathFile);
            
            write.Write("RealTag\tPredictedCategory\n\n");
            for (int i = 0; i < predicted.Length; i++)
            {
                write.Write("{0}\t\t{1}\n", DataSets[0].CatIDs[i], predicted[i]);
            }
            
            write.Write("\nError: {0}", error);
            write.Close();

            string FileLog = InOutStreams.GetPathOut() + @"\Error" + Algrthm + "_append.dat";
            StreamWriter writeLog = new StreamWriter(FileLog, true);
            writeLog.WriteLine("{0}\t{1}", error, (DateTime.Now - clock).TotalSeconds);
            writeLog.Close();
        }

        


        public void SVMGaussianKernel()
        {
            Console.WriteLine("SottoProgramma chiamato: GaussianKernel; Kernel = Gaussian.");
            this.clock = DateTime.Now;

            // if sigma is to be set, uncomment the next two lines (and line >> Kernel = Kker in the teacher properties)
            // otherwise, comment the following two lines and uncomment
            // >> UseKernelEstimation = false
            // in such a way, SMO algorithm shall optimize the constrained
            // problem 
            //           min _{w}  || w ||^2 / 2
            //           given y_i (x_i * w + b) >= 1
            // to find the parameters ( w , b ) \in R ^ (d x 1)

            IKernel ker = new Gaussian(0.1);
            Accord.Statistics.Kernels.Gaussian Kker = (Gaussian)ker;
            
            var teacher = new MulticlassSupportVectorLearning<Gaussian>()
            {
                Learner = (p) => new SequentialMinimalOptimization<Gaussian>()
                {
                    // UseComplexityHeuristic = false,
                    Complexity = 3
                },
                Kernel = Kker
            };

            teacher.ParallelOptions.MaxDegreeOfParallelism = 1;

            // NOTE: here <<teacher>> is the MODEL (so with its parameters and optimization)
            // so there KERNEL PARAMETERS are comptued (accordingly to the proper command, 
            // based on an initial guess that has been ESTIMATED)
            // while <<machine>> is the learning algorithm itself, where the MODEL PARAMETERS
            // are indeed computed, using the SMO before set in the teacher properties

            
            // here learning algorithm is taught the training examples to learn 
            // the model parameters ( w , b ) \in R ^ (d + 1)
            var machine = teacher.Learn(DataSets[1].ItemsFeatures, DataSets[1].CatIDs);
            
            // learned being ( w , b ) , we can predict the categories the test examples belong to
            int[] predicted = machine.Decide(DataSets[0].ItemsFeatures);
                        
            // error (zero to one loss) is computed
            double error = new ZeroOneLoss(DataSets[0].CatIDs).Loss(predicted);
            
            // results print
            PrintReport(predicted, error, "Gaussian");

            Console.WriteLine("SottoProgramma GaussianKernel terminato.\nErrore: {0}", error);
            Console.WriteLine("Tempo richiesto per l'operazione: " + (DateTime.Now - clock).TotalSeconds + " secondi.");
        }

        // here the same setting is mantained, being the following another kernelSVM
        public void SVMPolinomialKernel()
        {
            Console.WriteLine("SottoProgramma chiamato: PolynomialKernel; Kernel = Polynomial, degree = 2.");
            this.clock = DateTime.Now;

            var teacher = new MulticlassSupportVectorLearning<Polynomial>()
            {
                Learner = (p) => new SequentialMinimalOptimization<Polynomial>()
                {
                    UseKernelEstimation = false,
                    Kernel = new Polynomial(degree: 2),
                    UseComplexityHeuristic = true
                }
            };

            teacher.ParallelOptions.MaxDegreeOfParallelism = 1;

            var machine = teacher.Learn(DataSets[1].ItemsFeatures, DataSets[1].CatIDs);

            int[] predicted = machine.Decide(DataSets[0].ItemsFeatures);

            double error = new ZeroOneLoss(DataSets[0].CatIDs).Loss(predicted);

            PrintReport(predicted, error, "Polynomial");

            Console.WriteLine("SottoProgramma PolynomialKernel terminato.\nErrore: {0}", error);
            Console.WriteLine("Tempo richiesto per l'operazione: " + (DateTime.Now - clock).TotalSeconds + " secondi.");
        }

        public void TreeLearning()
        {
            Console.WriteLine("SottoProgramma chiamato: TreeLearning.");
            this.clock = DateTime.Now;
            
            var teacher = new C45Learning();            

            DecisionTree TreeAlgorithm = teacher.Learn(DataSets[1].ItemsFeatures, DataSets[1].CatIDs);

            int[] predicted = TreeAlgorithm.Decide(DataSets[0].ItemsFeatures);

            double error = new ZeroOneLoss(DataSets[0].CatIDs).Loss(predicted);

            PrintReport(predicted, error, "Tree");

            Console.WriteLine("SottoProgramma TreeLearning terminato.\nErrore: {0}", error);
            Console.WriteLine("Tempo richiesto per l'operazione: " + (DateTime.Now - clock).TotalSeconds + " secondi.");
        }
    }
}
