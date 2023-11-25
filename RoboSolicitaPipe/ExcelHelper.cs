using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboSolicitaPipe
{
    public static class ExcelHelper
    {
        public static List<List<string>> LerArquivoExcel(this string caminhoDoArquivo, string nomeDaPlanilha = "")
        {
            List<List<string>> dados = new List<List<string>>();

            FileInfo fileInfo = new FileInfo(caminhoDoArquivo);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage pacote = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet planilha = pacote.Workbook.Worksheets[nomeDaPlanilha];

                if (planilha == null)
                {
                    throw new ArgumentException($"A planilha '{nomeDaPlanilha}' não foi encontrada no arquivo Excel.");
                }

                int totalLinhas = planilha.Dimension.Rows;
                int totalColunas = planilha.Dimension.Columns;

                for (int linha = 2; linha <= totalLinhas; linha++)
                {
                    List<string> linhaAtual = new List<string>();

                    for (int coluna = 1; coluna <= totalColunas; coluna++)
                    {
                        var valor = planilha.Cells[linha, coluna].Text;

                        if (!string.IsNullOrEmpty(valor))
                        {
                            linhaAtual.Add(valor);
                        }
                    }

                    if (linhaAtual.Count > 0)
                    {
                        dados.Add(linhaAtual);
                    }
                }
            }

            return dados;
        }

    }
}
