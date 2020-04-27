using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad10_2
{
    class Program
    {
        static int[][] matrix;
        static List<int[]> covered = new List<int[]>();
        static int count = 0,sizei,sizek;
        static void Main(string[] args)
        {
            //ввод
            Console.WriteLine("Введите кол-во строк матрицы");
            int i = Convert.ToInt32(Console.ReadLine());
            sizei = i;
            Console.WriteLine("Введите кол-во столбцов матрицы");
            int k = Convert.ToInt32(Console.ReadLine());
            sizek = k;
            matrix = new int[i][];
            Console.WriteLine("Введите матрицу построчно");
            for(int n =0;n<i;n++)
            {
                matrix[n] = Console.ReadLine().Split().Select(int.Parse).ToArray();
            }
            //начинаем обход
            for (int ii = 0; ii < sizei; ii++)
                for (int kk = 0; kk < sizek; kk++)
                    Obhod(ii, kk);
            Console.WriteLine("Кол-в зон - " + count);
            Console.ReadKey();
        }
        static void Obhod(int i, int k)
        {
            
            if (matrix[i][k]==1)
                return;
            //делаем проход по зоне с закрашиваниеем
            Deepsearch(i,k);
            //увеличем кол-во зон, если там остался 0
            count++;
           
        }

        private static void Deepsearch(int i, int k)
        {
            //закрашивем
            matrix[i][k] = 1;
            //проверяем, можем ли мы закрасить соседние элементы
            if (CheckState(-1, i, k, true))
                Deepsearch(i - 1, k);
            if (CheckState(1, i, k, true))
                Deepsearch(i + 1, k);
            if (CheckState(-1, i, k, false))
                Deepsearch(i, k - 1);
            if (CheckState(1, i, k, false))
                Deepsearch(i, k + 1);
        }
        private static bool CheckState(int pi,int i,int k,bool IsI)
        {
            
                
            if (IsI)
            {
                if (i + pi >= 0 && i + pi < sizei && matrix[i + pi][k] == 0)
                    return true;
            }
            else
            {
                if (k + pi >= 0 && k + pi < sizek && matrix[i][k + pi] == 0)
                    return true;
            }
            return false;
        }
    }
}
