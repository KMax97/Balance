using WebApplicationBalance2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CaclLibrary;

namespace WebApplicationBalance2.Services
{

    public struct Answer
    {
        public double[] solution;
        public bool isBalanced;
    }
    public class Converter
    {
        private List<InfoFlows> InfoFlows;
        private List<Limit> limit;

        private double[,] x0; //нач значение value
        private double[,] W; // матрица ограничений
        private double[,] I; //диагональная матрица измерителей
        private List<String> flowsName;
        private List<String> nodesName;
        bool isBalancing = true;
        String message = "Баланс сошелся!";
        bool uk=true;
        private double[] result = null;

        private double delta;
        public Converter(BalanceInput balanceInput)
        {
            InfoFlows = new List<InfoFlows>();
            limit = new List<Limit>();

            foreach (Flow flow in balanceInput.Flows)
            {
                if (flow.Flows != null)
                {
                    delta = flow.Delta_error;
                    foreach (InfoFlows infoFlows in flow.Flows)
                    {
                        InfoFlows.Add(new Models.InfoFlows
                        {
                            Id = infoFlows.Id,
                            SourceId = infoFlows.SourceId,
                            DestinationId = infoFlows.DestinationId,
                            IsExcluded = infoFlows.IsExcluded,
                            Value = infoFlows.Value,
                            Tolerance = infoFlows.Tolerance,
                            UseTecnologicRanges = infoFlows.UseTecnologicRanges,
                            TecnologicRanges = infoFlows.TecnologicRanges
                        });
                    }

                    if (flow.Limitations != null)
                    {
                        foreach (Limit limitation in flow.Limitations)
                        {
                            limit.Add(new Limit
                            {
                                A = limitation.A,
                                Sign = limitation.Sign,
                                B = limitation.B
                            });
                        }
                    }
                }
                else
                {
                    uk = false;
                    isBalancing = false;
                    message = "Не найдено ни одного объекта!";
                }
            }
        }

        public BalanceOutput Calculate1()
        {
            if (uk)
            {
                int tecnologicRangeCount = 0;
                flowsName = new List<string>();
                nodesName = new List<string>();

                //создаем список названий потоков (стрелок)
                for (int i = 0; i < InfoFlows.Count; i++)
                {
                    for (int j = 0; j < flowsName.Count; j++)
                    {
                        if (InfoFlows[i].Id.CompareTo(flowsName[j]) == 0)
                            return new BalanceOutput() {IsBalancing = false, Message = "Поток с id = '" + InfoFlows[i].Id + "' уже существует в списке!", Flows = null };
                        else if (InfoFlows[i].Id.CompareTo("") == 0)
                            return new BalanceOutput() {IsBalancing = false, Message = "Поток не может иметь в качестве названия пустую строку", Flows = null };
                    }

                    flowsName.Add(InfoFlows[i].Id);
                    if (InfoFlows[i].UseTecnologicRanges)
                        tecnologicRangeCount += 2;
                }

                //создаем список названий узлов
                bool uk1, uk2;
                for (int i = 0; i < InfoFlows.Count; i++)
                {
                    uk1 = true;
                    for (int j = 0; j < nodesName.Count; j++)
                        if (InfoFlows[i].SourceId ==null || InfoFlows[i].SourceId.CompareTo(nodesName[j]) == 0)
                        {
                            uk1 = false;
                            break;
                        }
                    if (uk1 && InfoFlows[i].SourceId.CompareTo("")!=0) nodesName.Add(InfoFlows[i].SourceId);

                    uk2 = true;
                    for (int j = 0; j < nodesName.Count; j++)
                        if (InfoFlows[i].DestinationId == null || InfoFlows[i].DestinationId.CompareTo(nodesName[j]) == 0)
                        {
                             uk2 = false;
                             break;
                        }
                    if (uk2 && InfoFlows[i].DestinationId.CompareTo("") != 0) nodesName.Add(InfoFlows[i].DestinationId);
                }

                ICalculate calculate = new Calculator();
                //массив исходных данных;
                x0 = new double[InfoFlows.Count, 1];
                for (int i = 0; i < InfoFlows.Count; i++)
                    x0[i, 0] = InfoFlows[i].Value;

                //заполение матрицы I (наличие измерителя у потока)
                I = new double[InfoFlows.Count, InfoFlows.Count];
                for (int i = 0; i < InfoFlows.Count; i++)
                {
                    if (InfoFlows[i].IsExcluded) I[i, i] = 0;
                    else I[i, i] = 1;
                }

                //заполение матрицы W (дельты)
                W = new double[InfoFlows.Count, InfoFlows.Count];
                for (int i = 0; i < InfoFlows.Count; i++)
                {
                    if (InfoFlows[i].Tolerance == 0) W[i, i] = 0;
                    else W[i, i] = Math.Pow(InfoFlows[i].Tolerance, -2);
                }

                //Создание матрицы H(Q)
                double[,] Q = calculate.Multiplication(I, W);

                //Создание матрицы d
                double[,] d1 = calculate.Multiplication(Q, x0);
                int f1 = d1.GetLength(0);
                int f2 = d1.GetLength(1);
                for (int i = 0; i < f1; i++)
                    for (int j = 0; j < f2; j++)
                        d1[i, j] *= -1;
                double[] d = new double[InfoFlows.Count];
                for (int i = 0; i < InfoFlows.Count; i++)
                    d[i] = d1[i, 0];

                //Создание ограничений
                double[,] A = new double[nodesName.Count + tecnologicRangeCount + limit.Count, InfoFlows.Count];
                double[] b = new double[nodesName.Count + tecnologicRangeCount + limit.Count];
                string[] sign = new string[nodesName.Count + tecnologicRangeCount + limit.Count];

                //ограничения по сумме входящих и выходящих (nodesName.Count)
                for (int i = 0; i < nodesName.Count; i++)
                {
                    for (int j = 0; j < InfoFlows.Count; j++)
                    {
                        if (InfoFlows[j].SourceId !=null && InfoFlows[j].SourceId.CompareTo(nodesName[i]) == 0 && InfoFlows[j].SourceId.CompareTo("") != 0)
                            A[i, j] = 1;
                        else if (InfoFlows[j].DestinationId !=null && InfoFlows[j].DestinationId.CompareTo(nodesName[i]) == 0 && InfoFlows[j].DestinationId.CompareTo("") != 0)
                            A[i, j] = -1;
                    }
                    b[i] = 0;
                    sign[i] = "=";
                }
                int current = nodesName.Count;

                //дополнительные огрничения
                for (int i = 0; i < limit.Count; i++)
                    if (limit[i].Sign.CompareTo("=") == 0)
                    {
                        for (int j = 0; j < InfoFlows.Count; j++)
                            for (int t=0; t<limit[i].A.Count; t++)
                                if (limit[i].A[t].FlowId.CompareTo(InfoFlows[j].Id)==0)
                                    A[current, j] = limit[i].A[t].Coefficient;
                        b[current] = limit[i].B;
                        sign[current] = "=";
                        current++;
                    }
                for (int i = 0; i < limit.Count; i++)
                    if (limit[i].Sign.CompareTo("=") != 0)
                    {
                        for (int j = 0; j < InfoFlows.Count; j++)
                            for (int t = 0; t < limit[i].A.Count; t++)
                                if (limit[i].A[t].FlowId.CompareTo(InfoFlows[j].Id) == 0)
                                    A[current, j] = limit[i].A[t].Coefficient;
                        b[current] = limit[i].B;
                        sign[current] = limit[i].Sign;
                        current++;
                    }

                //ограничения TecnologicRanges
                for (int i=0; i<InfoFlows.Count;i++)
                {
                    if (InfoFlows[i].UseTecnologicRanges)
                    {
                        A[current, i] = 1;
                        A[current+1, i] = 1;
                        b[current] = InfoFlows[i].TecnologicRanges[0].MinValue;
                        b[current + 1] = InfoFlows[i].TecnologicRanges[0].MaxValue;
                        sign[current] = ">=";
                        sign[current + 1] = "<=";
                        current += 2;
                    }
                }

                Answer answer;
                //answer.solution = calculate.Calc(Q, d, A, b, sign).solution;
                answer.isBalanced = calculate.Calc(Q, d, A, b, sign).isBalanced;

                result = calculate.Calc(Q, d, A, b, sign).solution;
                // result = calculate.Calc(Q, d, A, b, sign);
                if (answer.isBalanced == false) return new BalanceOutput() { IsBalancing = false, Message = "Баланс не сходится после Calc", Flows = result };

                for (int i=0; i<nodesName.Count; i++)
                {
                    double res=0;
                    for (int j=0; j< InfoFlows.Count; j++)
                    {
                        res += A[i, j] * result[j];
                    }
                    if (!(res>=(b[i]-delta) && res<=(b[i]+delta)))
                    {
                        return new BalanceOutput() {IsBalancing = false, Message = "баланс не сходится в узле "+ nodesName[i], Flows = result };
                    }
                }

                for (int i = nodesName.Count; i< limit.Count; i++)
                {
                    double res = 0;
                    for (int j = 0; j < InfoFlows.Count; j++)
                    {
                        res += A[i, j] * result[j];
                    }
                    if (sign[i].Equals("=") && res<b[i] && res>b[i])
                        return new BalanceOutput() {IsBalancing = false, Message = "баланс не сходится. Не выполняется дополнительное ограничение о равенстве " + b[i], Flows = result };
                    if ((sign[i].Equals("<=") || sign[i].Equals("<")) && res > b[i]+delta)
                        return new BalanceOutput() { IsBalancing = false, Message = "баланс не сходится. Не выполняется дополнительное ограничение о >= " + b[i], Flows = result };
                    if ((sign[i].Equals(">=") || sign[i].Equals(">")) && res < b[i]-delta)
                        return new BalanceOutput() { IsBalancing = false, Message = "баланс не сходится. Не выполняется дополнительное ограничение о <= " + b[i], Flows = result };
                }

                for (int i= nodesName.Count+ limit.Count; i<A.GetLength(0); i=i+2)
                {
                    double res1=0, res2=0;
                    int flow=-1;
                    for (int j=0; j<InfoFlows.Count; j++)
                    {
                        if (A[i, j] != 0) flow = j+1;
                        res1+= A[i, j] * result[j];
                        res2+= A[i+1, j] * result[j];
                    }
                    if (res1<b[i] &&res2>b[i+1])
                        return new BalanceOutput() { IsBalancing = false, Message = "баланс не сходится. Не выполняются ограничения TecnologicRanges для потока " + flow, Flows = result };
                }
            }
            BalanceOutput outputFlow = new BalanceOutput() { IsBalancing = isBalancing, Message = message, Flows = result };
            return outputFlow;
        }
    }
}