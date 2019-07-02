using System;
using System.Data;
using System.IO;
using System.Collections.Generic;

using Accord.Statistics;
using Accord.Statistics.Analysis;
using Accord.Statistics.Models.Regression.Linear;
using Accord.Math;
using Accord.Math.Decompositions;
using Accord.Math.Comparers;
using Accord.Controls;

using Accord.IO;
using MLProj2018;

namespace DataWorkTable
{
    public class DataObject
    {
        private readonly int N;
        private readonly int M;
        public double[][] ItemsFeatures { get; set; }
        public string[] Categories { get; set; }
        public int[] CatIDs { get; set; }
        public Dictionary<int, string> CatsKey = new Dictionary<int, string>();
        
        public DataObject(int N, int M)
        {
            this.N = N;
            this.M = M;

            ItemsFeatures = new double[N][];
            Categories = new string[N];
            CatIDs = new int[N];

            for (int i = 0; i < ItemsFeatures.Length; i++)
            {
                ItemsFeatures[i] = new double[M - 1];
            }
        }
    }

    public class DataSetImport
    {
        public DataObject FeaturesImport()
        {
            CsvReader read = new CsvReader(InOutStreams.GetPathIn(), true);
            DataTable table = read.ToTable();

            int N = table.Rows.Count;
            int M = table.Columns.Count;

            // istanziazione della classe DataObject
            // facendo così si inizializzano la matrice ItemsFeats
            // e i vettori dei label (a 0.0 e 0)
            DataObject DataSet = new DataObject(N, M);

            int i = 0;
            foreach (DataRow r in table.Rows)
            {
                for (int j = 0; j < M; j++)
                {
                    if (j < M - 1)
                    {
                        DataSet.ItemsFeatures[i][j] = Double.Parse((string)r[j]);
                    }
                    else if (j == M - 1)
                    {
                        DataSet.Categories[i] = ((string)r[j]);
                    }
                }
                i++;
            }

            read.Close();

            // una volta letto il dataset, convertiamo le categorie in label sempre
            // interi, ma ordinati da 0 a 5, in base all'ordine di apparimento dall'inizio
            CategorizeAsInts(DataSet);

            return DataSet;
        }

        public void CategorizeAsInts(DataObject DataSet)
        {
            // nella cartella Drop lasciamo un'annotazione su come sono
            // state tradotte le categorie originali
            StreamWriter write = new StreamWriter(InOutStreams.GetPathOut() + @"\Key.KEY");
            int global = 0;
            int i = 0, j = 0, count;
            DataSet.CatIDs[0] = global;
            DataSet.CatsKey[global] = DataSet.Categories[i];

            write.Write("{0} in DataObject CatsID = {1} in original DataSet Categories\n", global, DataSet.Categories[0]);

            for (i = 1; i < DataSet.Categories.Length; i++)
            {
                count = 0;
                for (j = 0; j < i; j++)
                {
                    if (DataSet.Categories[i] == DataSet.Categories[j])
                    {
                        DataSet.CatIDs[i] = DataSet.CatIDs[j];
                    }
                    else
                    {
                        count++;
                    }
                }
                if (count == i)
                {
                    global++;
                    DataSet.CatIDs[i] = global;
                    DataSet.CatsKey[global] = (string)DataSet.Categories[i];
                    write.Write("{0} in DataObject CatsID = {1} in original DataSet Categories\n", global, DataSet.Categories[i]);
                }
            }

            write.Close();
        }
    }


    class DataPartition
    {
        private readonly int DimTestSet;
        private readonly int DimTrainSet;
        private readonly int FlagPartition;

        public DataPartition(DataObject DataSet, int _flag)
        {
            double NTST; // = ((double) 0.1) * DataSet.ItemsFeats.Length;
            double NTRN; // = ((double) 0.9) * DataSet.ItemsFeats.Length;
            // this.DimTestSet = (int)NTST;
            // this.DimTrainSet = (int)NTRN;
            this.FlagPartition = _flag;

            if (this.FlagPartition == 0)
            {
                NTST = ((double)0.1) * DataSet.ItemsFeatures.Length;
                NTRN = ((double)0.9) * DataSet.ItemsFeatures.Length;
                this.DimTestSet = (int)NTST;
                this.DimTrainSet = (int)NTRN;
            }
            else if (this.FlagPartition == 1)
            {
                NTST = ((double)(1.0 / 3.0)) * DataSet.ItemsFeatures.Length;
                NTRN = ((double)(2.0 / 3.0)) * DataSet.ItemsFeatures.Length;
                this.DimTestSet = (int)NTST;
                this.DimTrainSet = (int)NTRN;
            }

            // KEY: [0] è il test, cioè il 10% del totale
            //      [1] è il train, il restante 90%.
        }

        public DataObject[] TestSetExtraction(DataObject DataSet)
        {
            // così vengono create due istanze di DataObject, matrice e vettori
            // inizializzati
            DataObject[] SetsOfData = new DataObject[2];

            Console.WriteLine("Dimensioni Test e Train: {0}, {1}", this.DimTestSet, this.DimTrainSet);

            SetsOfData[0] = new DataObject(this.DimTestSet, DataSet.ItemsFeatures[0].Length);
            SetsOfData[1] = new DataObject(this.DimTrainSet, DataSet.ItemsFeatures[0].Length);

            int[] RandomIndexes = new int[SetsOfData[0].ItemsFeatures.Length];

            Random rand = new Random(); // seed?

            // Generiamo 0.1 * N - 1 interi casuali, compresi tra 0 e 
            // N, in modo che ognuno di questi è la riga del dataset che 
            // dobbiamo tenere da parte
            for (int k = 0; k < SetsOfData[0].ItemsFeatures.Length; k++)
            {
                RandomIndexes[k] = rand.Next(0, DataSet.ItemsFeatures.Length - 1);
            }

            int i = 0;
            foreach (int IndexRand in RandomIndexes)
            {
                SetsOfData[0].ItemsFeatures[i] = DataSet.ItemsFeatures[IndexRand];
                SetsOfData[0].Categories[i] = DataSet.Categories[IndexRand];
                SetsOfData[0].CatIDs[i] = DataSet.CatIDs[IndexRand];
                i++;
            }


            int count = 0, global = 0;
            for (int I = 0; I < DataSet.ItemsFeatures.Length; I++)
            {
                count = 0;
                foreach (int IndRand in RandomIndexes)
                {
                    if (I == IndRand)
                    {
                        count++;
                    }
                }
                if (count == 0 && global < SetsOfData[1].ItemsFeatures.Length)
                {
                    SetsOfData[1].ItemsFeatures[global] = DataSet.ItemsFeatures[I];
                    SetsOfData[1].Categories[global] = DataSet.Categories[I];
                    SetsOfData[1].CatIDs[global] = DataSet.CatIDs[I];
                    global++;
                }
            }

            SetsOfData[0].CatsKey = DataSet.CatsKey;
            SetsOfData[1].CatsKey = DataSet.CatsKey;
            
            return SetsOfData;
        }


        public void PrintPartition(DataObject[] SetsData)
        {
            string FilePath = InOutStreams.GetPathOut();
            FilePath += @"\DataPartition.dat";
            StreamWriter write = new StreamWriter(FilePath);
            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < SetsData[k].ItemsFeatures.Length; i++)
                {
                    for (int j = 0; j < SetsData[k].ItemsFeatures[i].Length; j++)
                    {
                        write.Write("{0}\t", SetsData[k].ItemsFeatures[i][j]);
                    }
                    write.Write("{0}\n", SetsData[k].CatIDs[i]);
                }
                write.Write("\n\n\n");
            }
            write.Close();
        }
    }

    

    class DataProcessing
    {
        // rationale behind the choice of static fields given in Main() where 
        // the user is asked whether preprocessing the data
        private static int FlagProcessing = 0;
        private static int PrincipalComponentsNumber = 0;

        // accessor methods for the static (FLAGs) fields above
        // those need not to be associated 
        public static void SetFlagProcessing(int _FlagProcessing)
        {
            FlagProcessing = _FlagProcessing;
        }

        public static int GetFlagProcessing()
        {
            return FlagProcessing;
        }

        public static int GetPrincipalComponentsNumber()
        {
            return PrincipalComponentsNumber;
        }


        public DataObject DoThePCA(DataObject DataSet)
        {
            DataObject NewData = new DataObject(DataSet.ItemsFeatures.Length, DataSet.ItemsFeatures[0].Length)
            {
                ItemsFeatures = DataSet.ItemsFeatures
            };

            var pca = new PrincipalComponentAnalysis()
            {
                Method = PrincipalComponentMethod.Center,
                Whiten = true
            };

            MultivariateLinearRegression transform = pca.Learn(DataSet.ItemsFeatures);

            double[] MeansVector = new double[DataSet.ItemsFeatures[0].Length];
            MeansVector = Measures.Mean(DataSet.ItemsFeatures, 0);

            double[][] CovarianceMatrix = Measures.Covariance(DataSet.ItemsFeatures, MeansVector);
            double[,] CovMat = new double[CovarianceMatrix.Length, CovarianceMatrix.Length];

            for (int i = 0; i < CovarianceMatrix.Length; i++)
            {
                for (int j = 0; j < CovarianceMatrix[0].Length; j++)
                {
                    CovMat[i, j] = CovarianceMatrix[i][j];
                }
            }

            var evd = new EigenvalueDecomposition(CovMat);
            double[] eigenval = evd.RealEigenvalues;
            double[,] eigenvec = evd.Eigenvectors;

            eigenvec = Matrix.Sort(eigenval, eigenvec, new GeneralComparer(ComparerDirection.Descending, true));

            Console.WriteLine("Numero di componenti principali: ");
            ScatterplotBox.Show(eigenval);
            PrincipalComponentsNumber = Convert.ToInt32(Console.ReadLine());

            pca.NumberOfOutputs = PrincipalComponentsNumber;

            double[][] DataProjected = pca.Transform(DataSet.ItemsFeatures);

            if (PrincipalComponentsNumber == 2)
            {
                ScatterplotBox.Show(DataProjected, DataSet.CatIDs);
            }

            NewData.ItemsFeatures = DataProjected;
            NewData.CatIDs = DataSet.CatIDs;

            return NewData;
        }
    }
}
