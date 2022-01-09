using System;
using System.IO;

namespace ExternalSortingAlgorithms
{
    public static class DirectMergeSort
    {
        public static void MakeFile(string path, int length) { 
            var rnd = new Random();
            var file = new StreamWriter(path); 
 
            for (var i = 0; i < length; i++) file.WriteLine(rnd.Next(100)); 
 
            file.Close(); 
        } 
        
        public static void SortFile(string file) { 
            var i = 1; 
            while(SplitFile(file, "A.txt", "B.txt", i)) { 
                MergeFiles("A.txt", "B.txt", file, i); 
                i *= 2; 
            }
        } 
        
        private static bool SplitFile(string originPath, string firstOutput, string secondOutput, int step) 
        { 
            if (File.Exists(firstOutput)) File.Delete(firstOutput); 
            if (File.Exists(secondOutput))File.Delete(secondOutput); 
 
            StreamReader origin = new StreamReader(originPath); 
            StreamWriter[] file = new StreamWriter[] { new StreamWriter(firstOutput), new StreamWriter(secondOutput) }; 
 
            string line; 
            int i,j = 0; 
            for (i = 0; true; i++) { 
                line = origin.ReadLine(); 
                if (line == null) break; 
 
                file[(i / step) % 2].WriteLine(line); 
                if ((i / step) % 2 == 0) j++; 
            } 
 
            origin.Close(); 
            file[0].Close(); 
            file[1].Close();

            return !(j >= i - 1);
        } 
        
        private static void MergeFiles(string firstFile, string secondFile, string resultFile, int step) { 
            if (File.Exists(resultFile)) File.Delete(resultFile); 
 
            StreamWriter result = new StreamWriter(resultFile); 
            StreamReader[] file = new StreamReader[] { new StreamReader(firstFile), new StreamReader(secondFile) }; 
 
            string[] line = new string[2] { file[0].ReadLine(), file[1].ReadLine() }; 
            int[] pos = new int[2]; 
            while(true) 
            { 
                if (pos[0] >= step || line[0] == null) 
                { 
                    while (pos[1] < step && line[1] != null) 
                    { 
                        result.WriteLine(line[1]); 
                        line[1] = file[1].ReadLine(); 
                        pos[1]++; 
                    } 
 
                    pos = new int[2]; 
                    if (line[0] == null && line[1] == null) break; 
                } 
                else if (pos[1] >= step || line[1] == null) 
                { 
                    while (pos[0] < step && line[0] != null) 
                    { 
                        result.WriteLine(line[0]); 
                        line[0] = file[0].ReadLine(); 
                        pos[0]++; 
                    } 
 
                    pos = new int[2]; 
                } 
                else 
                { 
                    if (int.Parse(line[0]) < int.Parse(line[1])) 
                    { 
                        result.WriteLine(line[0]); 
                        line[0] = file[0].ReadLine(); 
                        pos[0]++; 
                    } 
                    else 
                    { 
                        result.WriteLine(line[1]); 
                        line[1] = file[1].ReadLine(); 
                        pos[1]++; 
                    } 
                } 
            } 
 
            result.Close(); 
            file[0].Close(); 
            file[1].Close(); 
        }
    }
}