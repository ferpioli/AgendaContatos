using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgendaContatos
{
    public partial class frmAgendaContatos : Form  
    {
        private operacaoEnum acao;
        public frmAgendaContatos()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir?","Pergunta",MessageBoxButtons.YesNo)== DialogResult.Yes)
            {
                int indiceExcluido = lbxContatos.SelectedIndex;
                lbxContatos.SelectedIndex = 0;
                lbxContatos.Items.RemoveAt(indiceExcluido);
                List<Contato> contatoList = new List<Contato>();
                foreach (Contato contato in lbxContatos.Items)
                {
                    contatoList.Add(contato);
                }
                ManipuladordeArquivos.EscreverArquivo(contatoList);
                CarregarListaContatos();
                LimparCampos();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(false);
            AlterarBotoesIncluirAlterarExcluir(true);
            AlterarEstadoDosCampos(false);
        }

        private void frmAgendaContatos_Shown(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar (false);
            AlterarBotoesIncluirAlterarExcluir(true);
            CarregarListaContatos();
            AlterarEstadoDosCampos(false);
        }

        private void AlterarBotoesSalvarECancelar(bool estado)
        {
            btnSalvar.Enabled = estado;
            btnCancelar.Enabled = estado;
        }

        private void AlterarBotoesIncluirAlterarExcluir(bool estado)
        {
            btnIncluir.Enabled = estado;
            btnAlterar.Enabled = estado;
            btnExcluir.Enabled = estado;
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(true);
            AlterarBotoesIncluirAlterarExcluir(false);
            AlterarEstadoDosCampos(true);
            acao = operacaoEnum.INCLUIR;

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(true);
            AlterarBotoesIncluirAlterarExcluir(false);
            AlterarEstadoDosCampos(true);
            acao = operacaoEnum.ALTERAR;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (acao == operacaoEnum.INCLUIR)
            {
                Contato contato = new Contato
                {
                    Nome = txbNome.Text,
                    Email = txbEmail.Text,
                    NumeroTelefone = txbNumeroTelefone.Text
                };
                List<Contato> contatoList = new List<Contato>();
                foreach (Contato contatoDaLista in lbxContatos.Items)
                {
                    contatoList.Add(contatoDaLista);
                }
                contatoList.Add(contato);
                ManipuladordeArquivos.EscreverArquivo(contatoList);
            }else
            {
                Contato contato = new Contato
                {
                    Nome = txbNome.Text,
                    Email = txbEmail.Text,
                    NumeroTelefone = txbNumeroTelefone.Text
                };
                List<Contato> contatoList = new List<Contato>();
                foreach (Contato contatoDaLista in lbxContatos.Items)
                {
                    contatoList.Add(contatoDaLista);
                }
                int indice = lbxContatos.SelectedIndex;
                contatoList.RemoveAt(indice);
                contatoList.Insert(indice, contato);
                ManipuladordeArquivos.EscreverArquivo(contatoList);

            }

            CarregarListaContatos();
            AlterarBotoesIncluirAlterarExcluir(true);
            AlterarBotoesSalvarECancelar(false);
            LimparCampos();
            AlterarEstadoDosCampos(false);
        }
        private void CarregarListaContatos()
        {
            lbxContatos.Items.Clear();
            lbxContatos.Items.AddRange(ManipuladordeArquivos.LerArquivo().ToArray());
            lbxContatos.SelectedIndex = 0;
        }

        private void LimparCampos()
        {
            txbNome.Text = "";
            txbEmail.Text = "";
            txbNumeroTelefone.Text = "";
        }
        private void  AlterarEstadoDosCampos(bool estado)
        {
            txbNome.Enabled = estado;
            txbEmail.Enabled = estado;
            txbNumeroTelefone.Enabled = estado;
        }

        private void lbxContatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Contato contato = (Contato)lbxContatos.Items[lbxContatos.SelectedIndex];
            txbNome.Text = contato.Nome;
            txbEmail.Text = contato.Email;
            txbNumeroTelefone.Text = contato.NumeroTelefone;
        }
    }
}
