using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Laba8
{
    class Program
    {
        static int count;
        static int flowNumber = 1;

        static string writeANumberWithADot(double number)
        {
            string s = Convert.ToString(number);
            string result = "";

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ',')
                    result += '.';
                else
                    result += s[i];
            }

            return result;
        }

        static void writeToNotebook(List<InfoFlows> InfoFlows)
        {
            string filename;
            Console.WriteLine("Введите имя файла:");
            filename = Console.ReadLine();
            StreamWriter sw = new StreamWriter(File.Open(@"" + filename + ".txt", FileMode.CreateNew), Encoding.Unicode);

            sw.WriteLine("{");
            sw.WriteLine("  \"Flows\": [");
            sw.WriteLine("    {");
            sw.WriteLine("      \"Flows\": [");

            for (int i = 0; i < InfoFlows.Count; i++)
            {
                sw.WriteLine("        {");
                sw.WriteLine("          \"Id\": \"" + InfoFlows[i].Id + "\",");
                sw.WriteLine("          \"SourceId\": \"" + InfoFlows[i].SourceId + "\",");
                sw.WriteLine("          \"DestinationId\": \"" + InfoFlows[i].DestinationId + "\",");
                sw.WriteLine("          \"IsExcluded\": false,");
                sw.WriteLine("          \"Value\": " + writeANumberWithADot(InfoFlows[i].Value) + ",");
                sw.WriteLine("          \"Tolerance\": " + writeANumberWithADot(InfoFlows[i].Tolerance) + ",");
                sw.WriteLine("          \"UseTecnologicRanges\": false");

                if (i < InfoFlows.Count - 1)
                    sw.WriteLine("        },");
                else
                    sw.WriteLine("        }");
            }

            sw.WriteLine("      ],");
            sw.WriteLine("      \"Delta_error\": 0.05");
            sw.WriteLine("    }");
            sw.WriteLine("  ]");
            sw.WriteLine("}");

            sw.Close();

        }

        static InfoFlows drawNewFlow(int nodeName, double value, int inputnode, int outputnode) ///i, additional, -1, i
        {
            MathCalculations calculations = new MathCalculations();
            double tolerance = calculations.getRandom(0, value / 10);
            double bias = calculations.getRandom(0, value / 10 - tolerance);
            int sign = calculations.getRandom(0, 2);

            InfoFlows infoFlows = new InfoFlows();

            infoFlows.Id = Convert.ToString(flowNumber);
            if (inputnode != -1)
                infoFlows.SourceId = Convert.ToString(inputnode);
            else
                infoFlows.SourceId = "";
            if (outputnode != -1)
                infoFlows.DestinationId = Convert.ToString(outputnode);
            else
                infoFlows.DestinationId = "";
            infoFlows.IsExcluded = false;
            if (sign == 1)
            {
                infoFlows.Value = value + bias;
            }
            else
            {
                infoFlows.Value = value - bias;
            }
            infoFlows.Tolerance = tolerance;
            infoFlows.UseTecnologicRanges = false;

            flowNumber++;
            return infoFlows;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество узлов: ");
            count = Convert.ToInt32(Console.ReadLine());

            List<InfoFlows> InfoFlows = new List<InfoFlows>();

            double currentInput = 0, currentOutput = 0;

            MathCalculations calculations = new MathCalculations(); 
            double input = calculations.getRandom(100, 999999.999999);
            double output = 0;
            currentInput += input;

            for (int i = 1; i <= count; i++)
            {
                //инициализируем дополнительный поток
                double additional = calculations.getRandom(100, 999999.999999);

                
                if (additional < input)
                {
                    
                    int sign = calculations.getRandom(-1, 2);
                    if (sign == -1)
                    {
                        currentOutput += additional;
                        InfoFlows.Add(drawNewFlow(i, additional, -1, i));

                    }
                    else if (sign == 1)
                    {
                        currentInput += additional;
                        InfoFlows.Add(drawNewFlow(i, additional, i, -1));
                    }
                }
                //иначе
                else
                {
                    int sign = calculations.getRandom(0, 2);
                    if (sign == 1)
                    {
                        currentInput += additional;
                        InfoFlows.Add(drawNewFlow(i, additional, i, -1));
                    }
                }

                
                output = currentInput - currentOutput;

                //добавим потоки в список
                if (i > 1)
                    InfoFlows.Add(drawNewFlow(i, input, i, i - 1));
                else
                    InfoFlows.Add(drawNewFlow(i, input, i, -1));

                //изменим значения переменных для следующего узла
                currentInput = output;
                currentOutput = 0;
                input = output;
            }
            InfoFlows.Add(drawNewFlow(count + 1, input, -1, count));

            writeToNotebook(InfoFlows);

            string a = Console.ReadLine();

        }
    }

    class MathCalculations
    {
        private Random rnd = new Random();

        
        public Int32 getRandom(Int32 iMin, Int32 iMax)
        {
            return rnd.Next(iMin, iMax);
        }

        public double getRandom(double dMin, double dMax)
        {
            return rnd.NextDouble() * (dMax - dMin) + dMin;
        }
    }
}
