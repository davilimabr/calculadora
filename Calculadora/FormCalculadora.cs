using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora
{
    public partial class FormCalculadora : Form
    {
        public string Equacao { get; set; }
        public char UltimoCaracter { get; set; }
        public int UltimoIndice { get; set; }

        public FormCalculadora()
        {
            InitializeComponent();
            AtualizarAtributos();
        }

        //Métodos de captura dos eventos pressionar os botões
        private void botaoVirtual_Click(object sender, EventArgs e)
        {
            Button botao = (Button)sender;
            string tecla = botao.Text;
            switch(tecla)
            {
                case "1": case "2": case "3": case "4": case "5":
                case "6": case "7": case "8": case "9": case "0":
                    botaoNumerico(tecla);
                    break;
                case "+": case "-":
                    botaoSubAdc(tecla);
                    break;
                case "*": case "/":
                    botaoDivMult(tecla);
                    break;
                case "x²": case "x^y":
                    botaoPotencia(tecla);
                    break;
                case "C":
                    botaoApagar(true);
                    break;
                case "⌫":
                    botaoApagar();
                    break;
                case "√":
                    botaoRaiz();
                    break;
                case ",":
                    botaoVirgula();
                    break;
                case "=":
                    botaoResultado();
                    break;
            }
        }

        private void botaoTeclado_Click(object sender, KeyPressEventArgs e)
        {
           string tecla = $"{e.KeyChar}";
           switch (tecla)
            {
                case "1": case "2": case "3": case "4": case "5":
                case "6": case "7": case "8": case "9": case "0":
                    botaoNumerico(tecla);
                    break;
                case "+": case "-":
                    botaoSubAdc(tecla);
                    break;
                case "*": case "/":
                    botaoDivMult(tecla);
                    break;
                case "\b":
                    botaoApagar();
                    break;
                case ",":
                    botaoVirgula();
                    break;
                case "\n":
                    botaoResultado();
                    break;
            }
        }

        //funcionalidades dos botões
        private void botaoResultado()
        {
            string equacao = lblResultado.Text;

            CalculadoraMath calculo = new CalculadoraMath(equacao);
            string resultado = calculo.GetResultadoEquacao();

            lblEquacao.Text = lblResultado.Text;
            lblResultado.Text = resultado;
        }

        private void botaoNumerico(string numero)
        {
            lblResultado.Text += numero;
        }

        private void botaoDivMult(string operador)
        {
            if (Equacao == "" || UltimoCaracter == '+' || UltimoCaracter == '-' ||
               UltimoCaracter == ',' || UltimoCaracter == '^' || UltimoCaracter == '√')
                return;

            if (UltimoCaracter == '/' || UltimoCaracter == '*')
                Equacao = Equacao.Substring(0, UltimoIndice);

            Equacao += operador;
            lblResultado.Text = Equacao;
        }

        private void botaoSubAdc(string operador)
        {
            if (UltimoCaracter == ',' || UltimoCaracter == '√')
                return;

            if (UltimoCaracter == '-' || UltimoCaracter == '+')
                Equacao = Equacao.Substring(0, UltimoIndice);

            Equacao += operador;
            lblResultado.Text = Equacao;
        }

        private void botaoPotencia(string operador)
        {
            if (UltimoCaracter == ',' || Equacao == "" ||
               UltimoCaracter == '+' || UltimoCaracter == '-' ||
                   UltimoCaracter == '*' || UltimoCaracter == '/' || UltimoCaracter == '√')
                return;

            if(UltimoIndice != 0)
            {
                string doisUltimosCaracteres = $"{Equacao[UltimoIndice - 1]}{Equacao[UltimoIndice]}";
                if (UltimoCaracter == '^')
                    Equacao = Equacao.Substring(0, UltimoIndice);
                else if (doisUltimosCaracteres == "^2")
                    Equacao = Equacao.Substring(0, UltimoIndice - 1);
            }
           
            if (operador == "x²")
                Equacao += "^2";
            else
                Equacao += "^";

            lblResultado.Text = Equacao;
        }

        private void botaoRaiz()
        {
            if (UltimoCaracter == ',')
                return;

            if (UltimoCaracter == '√')
                Equacao = Equacao.Substring(0, UltimoIndice);

            lblResultado.Text += "√";
        }

        private void botaoVirgula()
        {
            if (Equacao.Contains(",") || Equacao == "" || UltimoCaracter == '+' ||
                UltimoCaracter == '-' || UltimoCaracter == '*' || UltimoCaracter == '/')
                return;

            lblResultado.Text += ',';
        }

        private void botaoApagar(bool apagarTudo = false)
        {
            if (Equacao == "")
                return;

            if (apagarTudo)
                Equacao = "";
            else
                Equacao = Equacao.Substring(0, UltimoIndice);

            lblEquacao.Text = "";
            lblResultado.Text = Equacao;
        }


        private void lblResultado_SizeChanged(object sender, EventArgs e)
        {
            if (lblResultado.Size.Width >= 250 && lblResultado.Font.Size >= 15)
            {
                string nome = lblResultado.Font.Name;
                float tamanho = lblResultado.Font.Size * (float)0.90;
                lblResultado.Font = new Font(nome, tamanho);
            }
        }

        private void lblResultado_TextChanged(object sender, EventArgs e)
        {
            AtualizarAtributos();

            if(Equacao == "")
            {
                string nome = lblResultado.Font.Name;
                lblResultado.Font = new Font(nome, 25);
            }
        }

        private void AtualizarAtributos()
        {
            Equacao = lblResultado.Text;
            UltimoIndice = (lblResultado.Text.Length > 0) ? lblResultado.Text.Length - 1 : 0;
            if (Equacao != "")
                UltimoCaracter = lblResultado.Text[lblResultado.Text.Length - 1];
        }
    }
}
