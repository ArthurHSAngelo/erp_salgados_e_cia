using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace erp_salgados_e_cia
{
    public static class ExportacaoRelatorio
    {
        public static string PedirArquivo(
            string nomeBase,
            out bool pdf)
        {
            pdf = true;

            using (SaveFileDialog janela = new SaveFileDialog())
            {
                janela.Filter =
                    "Documento PDF (*.pdf)|*.pdf|Arquivo CSV (*.csv)|*.csv";

                janela.FilterIndex = 1;

                janela.AddExtension = true;

                janela.FileName =
                    $"{nomeBase}_{DateTime.Now:yyyyMMdd_HHmm}";

                if (janela.ShowDialog() != DialogResult.OK)
                {
                    return null;
                }

                string caminho = janela.FileName;

                string extensao =
                    Path.GetExtension(caminho).ToLowerInvariant();

                if (extensao == ".pdf")
                {
                    pdf = true;
                }
                else if (extensao == ".csv")
                {
                    pdf = false;
                }
                else
                {
                    pdf = janela.FilterIndex == 1;

                    caminho += pdf ? ".pdf" : ".csv";
                }

                return caminho;
            }
        }

        public static void Concluir(string caminho)
        {
            DialogResult resposta = MessageBox.Show(
                "Relatório exportado com sucesso. Deseja abrir o arquivo?",
                "Sucesso",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
            );

            if (resposta != DialogResult.Yes)
            {
                return;
            }

            try
            {
                Process.Start(
                    new ProcessStartInfo(caminho)
                    {
                        UseShellExecute = true
                    }
                );
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Não foi possível abrir o arquivo. Ele foi salvo em: " +
                    caminho,
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        public static void AvisarFalhaAoSalvar()
        {
            MessageBox.Show(
                "Não foi possível salvar o arquivo. Se ele estiver aberto em outro programa, feche-o e tente de novo.",
                "Atenção",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }
    }
}
