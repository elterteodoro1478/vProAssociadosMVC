using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using vProAssociados.Models;

namespace vProAssociados.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();           

        }

        
        public  JsonResult Logar(string CPF = "", string Placa = "")
        {
            Associado associado = SelecionarPorCpfPlaca(CPF, Placa);

            if (associado.Id > 0)
            {
                Session["CPF"] = CPF;
                Session["Placa"] = Placa;
                Session["Id"] = associado.Id;
                Session["UsuarioMaster"] = associado.UsuarioMaster ? "SIM" : "NAO";

                return Json(new { retorno = "OK", UsuarioMaster = associado.UsuarioMaster ? "SIM" :"NAO"  });
            }
            else
            {
                if (associado.Id == -1)
                    return Json(new { Erro = "Erro na pesquisa, erro: "+ associado.Nome , UsuarioMaster = "NAO" });
                else
                    return Json(new { Erro = "Associado não encontrado!", UsuarioMaster = "NAO" });
            }            
        }


        public Associado SelecionarPorCpfPlaca(string CPF, string Placa)
        {

            Associado associados = new Associado();
            try
            {
                HttpClient cliente = new HttpClient();

                string link = ConfigurationManager.AppSettings["ApiUrl"];
                cliente.BaseAddress = new Uri(link);
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var reposta = cliente.GetAsync($@"api/SelecionarPorCpfPlaca?cpf={CPF}&placa={Placa}").Result;


                if (reposta.StatusCode == HttpStatusCode.OK)
                {
                    associados = JsonConvert.DeserializeObject<Associado>(reposta.Content.ReadAsStringAsync().Result);
                }
            }
            catch(Exception ex)
            {
                associados.Id = -1;
                associados.Nome = "Erro:" + ex.Message;
            }
           

            return associados;
        }


    }
}
