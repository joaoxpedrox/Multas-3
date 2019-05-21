using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Multas.Models;

namespace Multas.Controllers
{
    public class AgentesController : Controller
    {
        private MultasDB db = new MultasDB();

        // GET: Agentes
        public ActionResult Index()
        {
            return View(db.Agentes.ToList());
        }

        // GET: Agentes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index"); 
            }
            Agentes agentes = db.Agentes.Find(id);
            if (agentes == null)
            {
                return RedirectToAction("Index");
            }
            Session["Metodo"] = ""; 
            return View(agentes);
        }

        // GET: Agentes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agentes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,Esquadra")] Agentes agente,
                                   HttpPostedFileBase fotografia
        )
        {
            /// foi fornecido um ficheiro?
            /// é uma imagem (fotografia)?
            /// se é fotografia, 
            ///     guardar a imagem e 
            ///     associar ao agente
            /// se não é imagem, ou se não existir ficheiro
            ///     atribuir ao agente uma 'imagem por defeito'


            if (ModelState.IsValid)
            {
                db.Agentes.Add(agente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agente);
        }
        
        // GET: Agentes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agentes agentes = db.Agentes.Find(id);
            if (agentes == null)
            {
                return HttpNotFound();
            }
            return View(agentes);
        }

        // POST: Agentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Esquadra,Fotografia")] Agentes agentes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agentes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agentes);
        }

        // GET: Agentes/Delete/5
        public ActionResult Delete(int? id)
        {

            /// o ID é nulo se:
            ///   - há um erro no programa
            ///   - há um 'xico experto' a tentar a sua sorte
            /// redireciono o utilizador para a página de INDEX
            if (id == null)
            {
                return RedirectToAction("Index");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // procura os dados do Agente associado ao ID fornecido
            Agentes agente = db.Agentes.Find(id);

            /// o 'agente' é nulo se:
            ///   - há um erro no programa
            ///   - há um 'xico experto' a tentar a sua sorte
            /// redireciono o utilizador para a página de INDEX
            if (agente == null)
            {
                return RedirectToAction("Index");
                //return HttpNotFound();
            }

            /// para evitar 'trocas' maliciosa do 'agente'
            /// guardar o ID do agente, para futura comparação
            /// - num cookie cifrado
            /// - numa var. de sessão (não funciona no Asp .Net Core)
            /// - noutro recurso válido...
            Session["IdAgente"] = agente.ID;
            Session["acao"] = "Agentes/Delete";

            //  envia para a View os dados do Agente encontrado
            return View(agente);
        }

        // POST: Agentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {

            /// o ID é nulo se:
            ///   - há um erro no programa
            ///   - há um 'xico experto' a tentar a sua sorte
            /// redireciono o utilizador para a página de INDEX
            if (id == null)
            {
                return RedirectToAction("Index");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // será que o ID do agente que aqui á fornecido
            // é o ID do agente apresentado no ecrã?
            if (id != (int)Session["IdAgente"] ||
               (string)Session["acao"] != "Agentes/Delete")
            {
                // redireciono o utilizador para a página de INDEX
                return RedirectToAction("Index");
            }


            // limpar o valor das Var. Sessão, porque não preciso mais delas
            Session["IdAgente"] = "";
            Session["acao"] = "";

            // procurar os dados do Agente a remover
            Agentes agente = db.Agentes.Find(id);
            //   Agentes agente = db.Agentes.Find((int)Session["IdAgente"]);

            /// o 'agente' é nulo se:
            ///   - há um erro no programa
            ///   - há um 'xico experto' a tentar a sua sorte
            /// redireciono o utilizador para a página de INDEX
            if (agente == null)
            {
                return RedirectToAction("Index");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                // remove os dados do Agente do Modelo
                db.Agentes.Remove(agente);
                // consolida a remoção da BD
                db.SaveChanges();
            }
            catch (Exception)
            {
                // prepara mensagem de erro a ser enviada para o utilizador
                ModelState.AddModelError("", "ocorreu um erro com a remoção do agente " +
                                             agente.Nome +
                                             ". Provavelmente existem multas associadas" +
                                             " a esse agente.");
                // reenviar os dados do Agente para a View
                return View(agente);
            }

            // redireciona para a página INDEX
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}