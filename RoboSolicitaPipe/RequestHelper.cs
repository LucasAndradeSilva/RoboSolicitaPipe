using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboSolicitaPipe
{
    public static class RequestHelper
    {
        public static async Task<TResposta> CreateRequest<TResposta, TCorpo>(
        string url, HttpMethod metodo, TCorpo corpo = default)
        {
            using (HttpClient cliente = new HttpClient())
            {
                // Configurar a URL da requisição
                cliente.BaseAddress = new Uri(url);

                // Configurar o corpo da requisição (se houver)
                string conteudoCorpo = "";
                if (corpo != null)
                {
                    conteudoCorpo = corpo.SerializeObjectToJsonAsync();
                }

                HttpContent conteudoHttp = conteudoCorpo != null ?
                    new StringContent(conteudoCorpo, Encoding.UTF8, "application/json") :
                    null;

                // Criar a requisição HTTP
                HttpRequestMessage requisicao = new HttpRequestMessage
                {
                    Method = metodo,
                    Content = conteudoHttp
                };

                // Enviar a requisição e obter a resposta
                HttpResponseMessage resposta = await cliente.SendAsync(requisicao);

                // Verificar se a resposta foi bem-sucedida
                resposta.EnsureSuccessStatusCode();

                // Ler e desserializar o conteúdo da resposta
                string conteudoResposta = await resposta.Content.ReadAsStringAsync();
                TResposta resultado = conteudoResposta.DeserializeJson<TResposta>();

                return resultado;
            }
        }
    }
}
