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
        public FormCalculadora()
        {
            InitializeComponent();
        }

        private void botaoNumerico_Click(object sender, EventArgs e)
        {
            Button botao = (Button)sender;
            lblResultado.Text += botao.Text;
        }

        private void lblResultado_SizeChanged(object sender, EventArgs e)
        {
            if(lblResultado.Size.Width >= 250 && lblResultado.Font.Size >= 15)
            {
                string name = lblResultado.Font.Name;
                float size = lblResultado.Font.Size * (float)0.90;
                lblResultado.Font = new Font(name, size);
            }
        }

        private void btnVirgula_Click(object sender, EventArgs e)
        {
            if (!lblResultado.Text.Contains(",") && lblResultado.Text != "")
            {
                char ultimoCaracter = lblResultado.Text[lblResultado.Text.Length - 1];

                if(ultimoCaracter != '+' && ultimoCaracter != '-'
                    && ultimoCaracter != '*' && ultimoCaracter != '/')
                {
                    lblResultado.Text += ',';
                }
            }
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            if(lblResultado.Text != "")
            {
                Button button = (Button)sender;
                if (button.Equals(btnApagar))
                {
                    string resultado = lblResultado.Text;
                    lblResultado.Text = resultado.Substring(0, resultado.Length - 1);
                }
                else if (button.Equals(btnApagarTudo))
                    lblResultado.Text = "";
            }
        }
    }
}
