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
        public string Resultado { set; get; }

        public CalculadoraMath(string eq)
        {
            Equacao = eq;
        }

        public void CalcularEquacao()
        {
            string eqcCalculo = Equacao;

            if (eqcCalculo.Contains('√'))
                for (int i = 0; i < eqcCalculo.Length; i++)
                {
                    if (eqcCalculo[i] == '√')
                        RealizarCalculoDentroDaEquacao(ref eqcCalculo, i);
                }

            if (eqcCalculo.Contains('^'))
                for (int i = 0; i < eqcCalculo.Length; i++)
                {
                    if (eqcCalculo[i] == '^')
                        RealizarCalculoDentroDaEquacao(ref eqcCalculo, i);
                }

            if (eqcCalculo.Contains('*') || eqcCalculo.Contains('/'))
                for (int i = 0; i < eqcCalculo.Length; i++)
                {
                    if (eqcCalculo[i] == '*' || eqcCalculo[i] == '/')
                    {
                        RealizarCalculoDentroDaEquacao(ref eqcCalculo, i);
                        i = 0;
                    }
                }

            if (eqcCalculo.Contains('+') || eqcCalculo.Contains('-'))
                for (int i = 0; i < eqcCalculo.Length; i++)
                {
                    if (eqcCalculo[i] == '+' || eqcCalculo[i] == '-')
                    {
                        RealizarCalculoDentroDaEquacao(ref eqcCalculo, i);
                        i = 0;
                    }
                }

            Resultado = eqcCalculo;
        }

        public void RealizarCalculoDentroDaEquacao(ref string eqc, int indiceOperador)
        {
            char operador = eqc[indiceOperador];

            var numeros = RetirarNumerosDaEquacao(eqc, indiceOperador);
            double resultado = RealizarCalculo(numeros, operador);

            string serRetirado = $"{numeros.Item1}{operador}{numeros.Item2}";
            string serColocado = resultado.ToString(); 

            if (operador == '√')
            {
                serRetirado = $"√{numeros.Item2}";
                serColocado = $"*{resultado}";

                if (numeros.Item1 == 0)
                    serColocado = $"{resultado}";
            }

            eqc = eqc.Replace(serRetirado, serColocado);
        }

        public Tuple<double,double> RetirarNumerosDaEquacao(string eqc, int indiceOperador)
        {
            string numero1Inverso = "";
            for (int j = indiceOperador - 1; j >= 0; j--)
            {
                if (OPERACOES.Contains(eqc[j]))
                {
                    if (eqc[j] == '-')
                        numero1Inverso += '-';
                    break;
                }
                numero1Inverso += eqc[j];
            }
            string numero1 = new string(numero1Inverso.Reverse().ToArray());

            string numero2 = "";
            for (int j = indiceOperador + 1; j < eqc.Length; j++)
            {
                if ((OPERACOES.Contains(eqc[j]) && j != indiceOperador + 1))
                    break;
                numero2 += eqc[j];
            }

            double num1, num2;
            Double.TryParse(numero1, out num1);
            Double.TryParse(numero2, out num2);
          
            return new Tuple<double, double>(num1, num2);
        }

        public double RealizarCalculo(Tuple<double, double> numeros, char operador)
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
    }
}
