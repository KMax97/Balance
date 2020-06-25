using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using System.Threading.Tasks;
using Accord.Math.Optimization;

namespace CaclLibrary
{

    public struct Answer
    {
        public double[] solution;
        public bool isBalanced;
    }

    
    public class Calculator : ICalculate
    {
        public Answer Calc(double[,] Q, double[] d, double[,] A, double[] b, string[] sign)
        {
            Answer answer;
            answer.isBalanced = false;
            answer.solution = null;
            //A матрица ограничений для потоков
            double[] solution;
            var constraints = new List<LinearConstraint>();
            for (int i = 0; i < A.GetLength(0); i++)
            {
                int n = 0;
                for (int j = 0; j < A.GetLength(1); j++)
                    if (A[i, j] != 0) n++;
                int[] index = new int[n];
                double[] combine = new double[n];
                int y = 0, j1 = 0;
                for (y = 0; y < A.GetLength(1); y++)
                    if (A[i, y] != 0)
                    {
                        index[j1] = y;
                        combine[j1] = A[i, y];
                        j1++;
                    }

                if (sign[i].CompareTo("=") == 0)
                {
                    constraints.Add(
                        new LinearConstraint(numberOfVariables: n)
                        {
                            VariablesAtIndices = index,
                            CombinedAs = combine,
                              //ShouldBe = ConstraintType.EqualTo,
                              Value = b[i]
                        });
                }
                if (sign[i].CompareTo("<") == 0 || sign[i].CompareTo("<=") == 0)
                {
                    constraints.Add(
                        new LinearConstraint(numberOfVariables: n)
                        {
                            VariablesAtIndices = index,
                            CombinedAs = combine,
                            ShouldBe = ConstraintType.LesserThanOrEqualTo,
                            Value = b[i]
                        });
                }
                if (sign[i].CompareTo(">") == 0 || sign[i].CompareTo(">=") == 0)
                {
                    constraints.Add(
                        new LinearConstraint(numberOfVariables: n)
                        {
                            VariablesAtIndices = index,
                            CombinedAs = combine,
                            ShouldBe = ConstraintType.GreaterThanOrEqualTo,
                            Value = b[i]
                        });
                }
            }
            try
            {
                var solver = new GoldfarbIdnani(
                        function: new QuadraticObjectiveFunction(Q, d),
                        constraints: constraints);

                //проверить эту переменную на true или false
                bool success = solver.Minimize();

                
                answer.solution = solver.Solution;
                answer.isBalanced = success;

                //solution = solver.Solution;
            }
            catch (Exception ex)
            {
                //solution = null;
                answer.solution = null; 
            }
            return answer;
            //return solution;
        }
        public double[,] Multiplication(double[,] matrix1, double[,] matrix2)
        {
            var a = Matrix<double>.Build.DenseOfArray(matrix1);
            var b = Matrix<double>.Build.DenseOfArray(matrix2);

            return (a * b).ToArray();
        }
    }
}
