namespace SortingAlgorithms
{
    public class ABC_Sorter
    {
        public static char[] ABCSort(string input)
        {
            char temp;    
            string str = input.ToLower();  
            char[] charStr = str.ToCharArray();  
            for(int i=1;i< charStr.Length;i++)  
            {  
                for(int j=0;j< charStr.Length-1;j++)  
                {  
                    if(charStr[j]> charStr[j+1])  
                    {  
                        temp = charStr[j];  
                        charStr[j] = charStr[j + 1];  
                        charStr[j + 1] = temp;  
                    }  
                }  
            }

            return charStr;
        }
    }
}