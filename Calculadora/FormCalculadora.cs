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
        public string Operacao { get; set; }
        public char UltimoCaracter { get; set; }
        public int UltimoIndice { get; set; }

        public FormCalculadora()
        {
            InitializeComponent();
            AtualizarAtributos();
        }


        private void botaoNumerico_Click(object sender, EventArgs e)
        {
            Button botao = (Button)sender;
            lblResultado.Text += botao.Text;
        }

        private void botaoDivMult_Click(object sender, EventArgs e)
        {
            if (Operacao == "" || UltimoCaracter == '+' || UltimoCaracter == '-' || 
                UltimoCaracter == ',' || UltimoCaracter == '^' || UltimoCaracter == '√')
                return;

            Button botao = (Button)sender;
            string operador = botao.Text;

            if (UltimoCaracter == '/' || UltimoCaracter == '*')
                Operacao = Operacao.Substring(0, UltimoIndice);

            if (operador == "/")
                Operacao += '/';
            else
                Operacao += '*';

            lblResultado.Text = Operacao;
        }

        private void botaoSubAdc_Click(object sender, EventArgs e)
        {
            if (UltimoCaracter == ',' || UltimoCaracter == '√')
                return;

            Button botao = (Button)sender;
            string operador = botao.Text;

            if (UltimoCaracter == '-' || UltimoCaracter == '+')
                Operacao = Operacao.Substring(0, UltimoIndice);

            if (operador == "+")
                Operacao += '+';
            else
                Operacao += '-';

            lblResultado.Text = Operacao;
        }

        private void botaoPotencia_Click(object sender, EventArgs e)
        {
            if (UltimoCaracter == ',' || Operacao == "" ||
               UltimoCaracter == '+' || UltimoCaracter == '-' ||
                   UltimoCaracter == '*' || UltimoCaracter == '/' || UltimoCaracter == '√')
                return;

            Button botao = (Button)sender;
            string operador = botao.Text;

            if(UltimoIndice != 0)
            {
                string doisUltimosCaracteres = $"{Operacao[UltimoIndice - 1]}{Operacao[UltimoIndice]}";
                if (UltimoCaracter == '^')
                    Operacao = Operacao.Substring(0, UltimoIndice);
                else if (doisUltimosCaracteres == "^2")
                    Operacao = Operacao.Substring(0, UltimoIndice - 1);
            }
           
            if (operador == "x²")
                Operacao += "^2";
            else
                Operacao += "^";

            lblResultado.Text = Operacao;
        }

        private void botaoRaiz_Click(object sender, EventArgs e)
        {
            if (UltimoCaracter == ',')
                return;

            if (UltimoCaracter == '√')
                Operacao = Operacao.Substring(0, UltimoIndice);

            Operacao += "√";
            lblResultado.Text = Operacao;
        }

        private void btnVirgula_Click(object sender, EventArgs e)
        {
            if (Operacao.Contains(",") || Operacao == "" || UltimoCaracter == '+' ||
                UltimoCaracter == '-' || UltimoCaracter == '*' || UltimoCaracter == '/')
                return;

            Operacao += ',';
            lblResultado.Text = Operacao;
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (Operacao == "")
                return;

            Button button = (Button)sender;
            if (button.Equals(btnApagar))
                Operacao = Operacao.Substring(0, UltimoIndice);
            else if (button.Equals(btnApagarTudo))
                Operacao = "";

            lblResultado.Text = Operacao;
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
        }

        private void AtualizarAtributos()
        {
            Operacao = lblResultado.Text;
            UltimoIndice = lblResultado.Text.Length - 1;
            if (Operacao != "")
                UltimoCaracter = lblResultado.Text[lblResultado.Text.Length - 1];
        }

        /*
        private void FormCalculadora_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }*/
    }
}
