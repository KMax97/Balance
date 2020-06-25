using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaclLibrary
{

    public interface ICalculate
    {
        double[,] Multiplication(double[,] matrix1, double[,] matrix2);
        Answer Calc(double[,] Q, double[] d, double[,] A, double[] b, string[] sign);
    }
}
