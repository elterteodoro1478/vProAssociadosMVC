using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using vProAssociados.Models;
using vProAssociados.Repository;

namespace vProAssociados.Controllers
{
    public class HomeController : Controller
    {   
        public ActionResult Index()
        {            
            
            if (Session["CPF"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (Session["UsuarioMaster"].ToString() == "SIM")
                {
                    return RedirectToAction("List");
                }
                else
                {
                    return RedirectToAction("Edit", new { Id = Convert.ToInt32(Session["Id"].ToString()) });
                }                    
            }            
        }


        public async Task<ActionResult> List()
        {
            List<Associado> Lista = new AssociadoRep().SelecionarTodos();

            return View(Lista.ToList());
        }

        // GET: Associadoes/Details/5
        

        // GET: Associadoes/Create
        public ActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> Details(int Id = 0)
        {
            Associado associado = new AssociadoRep().SelecionarPorId(Id);

            return View(associado);
        }

        // GET: Associadoes/Edit/5
        public async Task<ActionResult> Edit(int Id = 0)
        {
            Associado associado = new AssociadoRep().SelecionarPorId(Id);
            
            return View(associado);
        }

        
        public JsonResult SalvarDados(Associado associado)
        {

            if (associado.Id > 1 )
            {
                Retorno ret = new AssociadoRep().Alterar(associado);

                if (ret.Status == "OK")
                    return Json(new { retorno = ret.Mensagem, UsuarioMaster = "NAO" });
                else 
                  return Json(new { Erro = ret.Mensagem  , UsuarioMaster = "NAO" });
            }
            else
            {
                Retorno ret = new AssociadoRep().Incluir(associado);

                if (ret.Status == "OK")
                    return Json(new { retorno = ret.Mensagem, UsuarioMaster = "NAO" });
                else
                    return Json(new { Erro = ret.Mensagem, UsuarioMaster = "NAO" });
            }
            
            //return View(associado);
        }



    }
}