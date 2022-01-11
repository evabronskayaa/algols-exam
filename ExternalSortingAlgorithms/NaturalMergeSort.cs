using System;
using System.Collections.Generic;
using System.IO;

namespace ExternalSortingAlgorithms
{
    public class NaturalMergeSort
    {
        public static void DoPolypathNaturalSort(string filePath, int fileCount)
        {
            while(DivideFile(filePath, fileCount))
            {
                MergeFiles(filePath, fileCount);
            }
        }
        
        private static bool DivideFile(string originFilePath, int fileCount)
        {
            StreamReader file = new StreamReader(originFilePath);

            for (int i = 0; i < fileCount; i++)
                if (File.Exists(i + ".txt"))
                    File.Delete(i + ".txt");

            StreamWriter[] resultFiles = new StreamWriter[fileCount];
            for (int i = 0; i < fileCount; i++)
                resultFiles[i] = new StreamWriter(i + ".txt");

            string line = file.ReadLine();
            int curNum = int.MinValue;
            int curFileNum = 0;
            bool isSorted = true;
            while (line != null)
            {
                var lastNum = curNum;
                curNum = Int32.Parse(line);

                if (lastNum > curNum)
                {
                    curFileNum = (curFileNum + 1) % fileCount;
                    isSorted = false;
                }
                    
                resultFiles[curFileNum].WriteLine(line);
                
                line = file.ReadLine();
            }

            file.Close();
            for (int i = 0; i < fileCount; i++)
                resultFiles[i].Close();

            return !isSorted;
        }
        
        private static void MergeFiles(string resultFilePath, int fileCount)
        {
            if (File.Exists(resultFilePath))
                File.Delete(resultFilePath);
            StreamWriter resultFile = new StreamWriter(resultFilePath);

            StreamReader[] file = new StreamReader[fileCount];
            for (int i = 0; i < fileCount; i++)
                file[i] = new StreamReader(i + ".txt");

            LinkedList<int> curFileNums = new LinkedList<int>();
            string[] lines = new string[fileCount];
            int[] nums = new int[fileCount];
            int[] lastNums = new int[fileCount];
            for (int i = 0; i < fileCount; i++)
            {
                lines[i] = file[i].ReadLine();
                if (lines[i] != null)
                {
                    nums[i] = Int32.Parse(lines[i]);
                    curFileNums.AddLast(i);
                }
            }

            while (curFileNums.Count > 0)
            {
                while (curFileNums.Count > 0)
                {
                    int fileNum = GetFileNumWithMinNum(curFileNums,  nums);

                    resultFile.WriteLine(lines[fileNum]);
                    lines[fileNum] = file[fileNum].ReadLine();
                    if (lines[fileNum] == null)
                    {
                        curFileNums.Remove(fileNum);
                    }
                    else
                    {
                        lastNums[fileNum] = nums[fileNum];
                        nums[fileNum] = Int32.Parse(lines[fileNum]);
                        if (lastNums[fileNum] > nums[fileNum])
                            curFileNums.Remove(fileNum);
                    }
                }

                for (int i = 0; i < fileCount; i++)
                    if (lines[i] != null)
                        curFileNums.AddLast(i);
            }

            resultFile.Close();
            for (int i = 0; i < fileCount; i++)
                file[i].Close();
        }
        
        private static int GetFileNumWithMinNum(LinkedList<int> curFileNums, int[] nums)
        {
            int fileNum = -1;
            int minNum = int.MaxValue;
            foreach(int i in curFileNums)
            {
                if (nums[i] < minNum)
                {
                    minNum = nums[i];
                    fileNum = i;
                }
            }

            return fileNum;
        }
    }
}