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

            flowLayoutPanelHistorico.Location = new Point(80, -2);
        }

        //Métodos de captura dos eventos pressionar os botões
        private void botaoVirtual_Click(object sender, EventArgs e)
        {
            Button botao = (Button)sender;
            char tecla = botao.Text[0];
            switch(tecla)
            {
                case '1': case '2': case '3': case '4': case '5':
                case '6': case '7': case '8': case '9': case '0':
                    botaoNumerico(tecla);
                    break;
                case '+': case '-':
                    botaoSubAdc(tecla);
                    break;
                case '*': case '/':
                    botaoDivMult(tecla);
                    break;
                case 'C':
                    botaoApagar(true);
                    break;
                case '⌫':
                    botaoApagar();
                    break;
                case '√':
                    botaoRaiz();
                    break;
                case ',':
                    botaoVirgula();
                    break;
                case '=':
                    botaoResultado();
                    break;
            }

            if(botao.Text == "x²" || botao.Text == "x^y")
                botaoPotencia(botao.Text);
        }

        private void botaoTeclado_Click(object sender, KeyPressEventArgs e)
        {
           char tecla = e.KeyChar;
           switch (tecla)
            {
                case '1': case '2': case '3': case '4': case '5':
                case '6': case '7': case '8': case '9': case '0':
                    botaoNumerico(tecla);
                    break;
                case '+': case '-':
                    botaoSubAdc(tecla);
                    break;
                case '*': case '/':
                    botaoDivMult(tecla);
                    break;
                case '\b':
                    botaoApagar();
                    break;
                case ',':
                    botaoVirgula();
                    break;
                case '\n':
                    botaoResultado();
                    break;
            }
        }

        private void btnHistorico_Click(object sender, EventArgs e)
        {
            bool visivel = flowLayoutPanelHistorico.Visible;

            if (visivel)
                flowLayoutPanelHistorico.Visible = false;
            else
                flowLayoutPanelHistorico.Visible = true;
        }

        //funcionalidades dos botões
        private void botaoNumerico(char numero)
        {
            lblResultado.Text += numero;
        }

        private void botaoDivMult(char operador)
        {
            if (!Char.IsNumber(UltimoCaracter))
            {
                if (Equacao == "" || UltimoCaracter == operador || (UltimoCaracter != '*' && UltimoCaracter != '/'))
                    return;
                
                Equacao = Equacao.Substring(0, UltimoIndice);
            }
            Equacao += operador;
            lblResultado.Text = Equacao;
        }

        private void botaoSubAdc(char operador)
        {
            if (!Char.IsNumber(UltimoCaracter))
            {
                if (UltimoCaracter == operador || UltimoCaracter == ',' || UltimoCaracter == '√')
                    return;
                else if (UltimoCaracter == '-' || UltimoCaracter == '+')
                    Equacao = Equacao.Substring(0, UltimoIndice);
            }
            
            Equacao += operador;
            lblResultado.Text = Equacao;
        }

        private void botaoPotencia(string operador)
        {
            if (!Char.IsNumber(UltimoCaracter))
            {
                if (UltimoCaracter == '^')
                    Equacao = Equacao.Substring(0, UltimoIndice);

                return;
            }

            if (operador == "x²")
                Equacao += "^2";
            else
                Equacao += '^';

            lblResultado.Text = Equacao;
        }

        private void botaoRaiz()
        {
            if (UltimoCaracter == ',' || UltimoCaracter == '√')
                return;

            lblResultado.Text += "√";
        }

        private void botaoVirgula()
        {
            if (!Char.IsNumber(UltimoCaracter))
                return;

            string numero = "";
            for (int i = Equacao.Length - 1; i >= 0; i--) 
            {
                if (CalculadoraMath.OPERACOES.Contains(Equacao[i]))
                    break;

                numero += Equacao[i];
            }

            if(!numero.Contains(','))
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

        private void botaoResultado()
        {
            if (!EquacaoEhValidaParaCalculo())
                return;

            try
            {
                CalculadoraMath calculo = new CalculadoraMath(Equacao);
                calculo.CalcularEquacao();

                lblEquacao.Text = lblResultado.Text;
                lblResultado.Text = calculo.Resultado;

                AdicionarItemNoHistorico(lblEquacao.Text, lblResultado.Text);
            }
            catch
            {
                lblEquacao.Text = "";
                lblResultado.Text = "Erro";
            }
        }

        private bool EquacaoEhValidaParaCalculo()
        {
            bool temOperador = false;
            foreach (char c in Equacao)
            {
                if (CalculadoraMath.OPERACOES.Contains(c))
                    temOperador = true;
            }

            bool restricao = !String.IsNullOrEmpty(Equacao) &&
                             Equacao != "Erro" &&
                             temOperador &&
                             !(Equacao.Length == 1 && CalculadoraMath.OPERACOES.Contains(Equacao[0]));

            return restricao;
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

        private void AdicionarItemNoHistorico(string eqc, string res)
        {
            Label lblEqc = new Label()
            {
                Text = eqc,
                ForeColor = Color.DarkRed,
                Font = new Font(Font.FontFamily.Name, 10)
            };

            Label lblRes = new Label()
            {
                Text = res,
                ForeColor = Color.DarkRed,
                Font = new Font(Font.FontFamily.Name, 15)
             };

            Label lblLine = new Label()
            {
                Text = "",
                BorderStyle = BorderStyle.FixedSingle,
                AutoSize = false,
                Height = 1
            };

            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Controls.Add(lblEqc);
            panel.Controls.Add(lblRes);
            panel.Controls.Add(lblLine);

            panel.Size = new Size(100, 50);

            flowLayoutPanelHistorico.Controls.Add(panel);
        }
    }
}
