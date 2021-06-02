using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora
{
    class CalculadoraMath
    {
        public static readonly char[] OPERACOES = { '√', '^', '*', '/', '+', '-'};
        public string Equacao { set; get; }

        public CalculadoraMath(string eq)
        {
            Equacao = eq;
        }

        public string GetResultadoEquacao()
        {
            if (Equacao.Contains('√'))
            {
                for (int i = 0; i < Equacao.Length; i++)
                {
                    if (Equacao[i] == '√')
                        CalcularNaEquacao(Equacao[i], i);
                }
            }
            if (Equacao.Contains('^'))
            {
                for (int i = 0; i < Equacao.Length; i++)
                {
                    if (Equacao[i] == '^')
                        CalcularNaEquacao(Equacao[i], i);
                }
            }
            if (Equacao.Contains('*') || Equacao.Contains('/'))
            {
                for (int i = 0; i < Equacao.Length; i++)
                {
                    if (Equacao[i] == '*' || Equacao[i] == '/')
                    {
                        CalcularNaEquacao(Equacao[i], i);
                        i = 0;
                    }
                }
            }
            if (Equacao.Contains('+') || Equacao.Contains('-'))
            {
                for (int i = 0; i < Equacao.Length; i++)
                {
                    if (Equacao[i] == '+' || Equacao[i] == '-')
                    {
                        CalcularNaEquacao(Equacao[i], i);
                        i = 0;
                    }
                }
            }
            return Equacao;
        }

        /*private void CalcularRaizesNaEquacao()
        {
            int i = 0;
            while(Equacao.Contains("√"))
            {
                if (Equacao[i] == '√')
                {
                    string numero = "";
                    for (int j = i + 1; j < Equacao.Length; j++)
                    {
                        if (OPERACOES.Contains(Equacao[j]))
                            break;

                        numero += Equacao[j];
                    }
                    double raizNum = Math.Sqrt(Convert.ToDouble(numero));

                    string resultado = raizNum.ToString();

                    if (i > 0)
                    {
                        if (!OPERACOES.Contains(Equacao[i - 1]))
                            resultado = $"*{resultado}";
                    }

                    Equacao = Equacao.Replace($"√{numero}", resultado);
                }
                if (Equacao[0] == '*')
                    Equacao = Equacao.Remove(0, 1);
                i++;
            }
        }*/

        private void CalcularNaEquacao(char operador, int indiceOperador)
        {
            var numeros = RetirarNumerosDaEquacao(indiceOperador);
            double resultado = RealizarCalculo(numeros, operador);

            string replace = "";
            if (numeros.Item1 == 0)
                replace = $"{operador}{numeros.Item2}";
            else
                replace = $"{numeros.Item1}{operador}{numeros.Item2}";

            Equacao = Equacao.Replace(replace, resultado.ToString());
        }

        private Tuple<double,double> RetirarNumerosDaEquacao(int indiceOperador)
        {
            string numero1Inverso = "";
            for (int j = indiceOperador - 1; j >= 0; j--)
            {
                if (OPERACOES.Contains(Equacao[j]))
                {
                    if (Equacao[j] == '-')
                        numero1Inverso += '-';
                    break;
                }
                numero1Inverso += Equacao[j];
            }
            string numero1 = new string(numero1Inverso.Reverse().ToArray());

            string numero2 = "";
            for (int j = indiceOperador + 1; j < Equacao.Length; j++)
            {
                if ((OPERACOES.Contains(Equacao[j]) && j != indiceOperador + 1))
                    break;
                numero2 += Equacao[j];
            }

            double num1 = 0; 
            double num2 = 0;
            if (!String.IsNullOrEmpty(numero1))
                num1 = Convert.ToDouble(numero1);

            if (!String.IsNullOrEmpty(numero2))
                num2 = Convert.ToDouble(numero2);

            return new Tuple<double, double>(num1, num2);
        }

        private double RealizarCalculo(Tuple<double, double> numeros, char operador)
        {
            double resultado = 0;
            switch(operador)
            {
                case '√':
                    resultado = Math.Sqrt(numeros.Item2);
                    break;
                case '*':
                    resultado = numeros.Item1 * numeros.Item2;
                    break;
                case '/':
                    resultado = numeros.Item1 / numeros.Item2;
                    break;
                case '^':
                    resultado = Math.Pow(numeros.Item1, numeros.Item2);
                    break;
                case '+':
                    resultado = numeros.Item1 + numeros.Item2;
                    break;
                case '-':
                    resultado = numeros.Item1 - numeros.Item2;
                    break;
            }

            return resultado;
        }

        /*
        private void CalcularPotenciasNaEquacao()
        {
            int i = 0;
            while (Equacao.Contains("^"))
            {
                if (Equacao[i] == '^')
                {
                    string baseInversa = "";
                    for (int j = i - 1; j >= 0; j--)
                    {
                        baseInversa += Equacao[j];

                        if (OPERACOES.Contains(Equacao[j]))
                            break;
                    }
                    string basePot = new string(baseInversa.Reverse().ToArray());

                    string expoentePot = "";
                    for (int j = i + 1; j < Equacao.Length; j++)
                    {
                        if ((OPERACOES.Contains(Equacao[j]) && j != i + 1))
                            break;

                        expoentePot += Equacao[j];
                    }

                    double potencia = Math.Pow(Convert.ToDouble(basePot), Convert.ToDouble(expoentePot));
                    string a = potencia.ToString();
                    Equacao = Equacao.Replace($"{basePot}^{expoentePot}", potencia.ToString());
                }
                i++;
            }
        }   

        private void CalcularMultiplicaçãoNaEquacao()
        {
            int i = 0;
            while(Equacao.Contains("*"))
            {
                if (Equacao[i] == '*')
                {
                    string numero1Inverso = "";
                    for (int j = i - 1; j >= 0; j--)
                    {
                        numero1Inverso += Equacao[j];

                        if (OPERACOES.Contains(Equacao[j]))
                            break;
                    }
                    string numero1 = new string(numero1Inverso.Reverse().ToArray());

                    string numero2 = "";
                    for (int j = i + 1; j < Equacao.Length; j++)
                    {
                        if ((OPERACOES.Contains(Equacao[j]) && j != i + 1))
                            break;

                        numero2 += Equacao[j];
                    }

                    double resultado = Convert.ToDouble(numero1) * Convert.ToDouble(numero2);
                    Equacao = Equacao.Replace($"{numero1}*{numero2}", resultado.ToString());
                }
                i++;
            }
        }*/
    }
}
