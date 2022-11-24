using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using System.Net;
using vProAssociados.Models;
using System.Text;
using System.Threading.Tasks;

namespace vProAssociados.Repository
{
    public class AssociadoRep
    {
        HttpClient cliente = null;

        public AssociadoRep()
        {
            cliente = new HttpClient();

            string link = ConfigurationManager.AppSettings["ApiUrl"];
            cliente.BaseAddress = new Uri(link);
            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
        }



        public List<Associado> SelecionarTodos()
        {
            var reposta = cliente.GetAsync($@"api/SelecionarTodos").Result;

            List<Associado> ListaAssociados = new List<Associado>();

            if (reposta.StatusCode == HttpStatusCode.OK)
            {
                ListaAssociados = JsonConvert.DeserializeObject<List<Associado>>(reposta.Content.ReadAsStringAsync().Result);
            }

            return ListaAssociados;
        }

        public Associado SelecionarPorId(int Id)
        {

            Associado associados = new Associado();
            try
            {
                HttpClient cliente = new HttpClient();

                string link = ConfigurationManager.AppSettings["ApiUrl"];
                cliente.BaseAddress = new Uri(link);
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var reposta = cliente.GetAsync($@"api/SelecionarPorId?Id={Id.ToString()}").Result;


                if (reposta.StatusCode == HttpStatusCode.OK)
                {
                    associados = JsonConvert.DeserializeObject<Associado>(reposta.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                associados.Id = -1;
                associados.Nome = "Erro:" + ex.Message;
            }

            return associados;
        }

        public Retorno Alterar(Associado associado)
        {
            var Json = new StringContent(JsonConvert.SerializeObject(associado), Encoding.UTF8, "application/json");

            var reposta = cliente.PostAsync($@"api/Alterar", Json);

            Retorno retorno = new Retorno();

            if (reposta.Result.StatusCode == HttpStatusCode.OK)
            {    
                retorno = JsonConvert.DeserializeObject<Retorno>(reposta.Result.Content.ReadAsStringAsync().Result);
            }

            return retorno;
        }

        public Retorno Incluir(Associado associado)
        {
            var Json = new StringContent(JsonConvert.SerializeObject(associado), Encoding.UTF8, "application/json");

            var reposta = cliente.PostAsync($@"api/Incluir", Json);

            Retorno retorno = new Retorno();

            if (reposta.Result.StatusCode == HttpStatusCode.OK)
            { 
                retorno = JsonConvert.DeserializeObject<Retorno>(reposta.Result.Content.ReadAsStringAsync().Result);
            }

            return retorno;
        }



    }
}