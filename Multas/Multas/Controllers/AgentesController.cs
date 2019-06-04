using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Multas.Models;

namespace Multas.Controllers
{
    [Authorize] //Obriga a que os utilizadores estejam autenticados 

    public class AgentesController : Controller
    {
        private MultasDB db = new MultasDB();

        // GET: Agentes
        [Authorize(Roles = "RecursosHumanos, Agentes")] //Além de autenticado,
                                                        //só os utilizadores do tipo RecursosHumanos ou Agentes têm acesso 
                                                        //só precisa de pertencer a uma delas.. 
                                                        //»»»»»»»»»»»»»»»»»»»»»»»»»««««««««««««««««««««««««

        //Exemplo de uma situação em que é necessário ser GestorMultas e Agente . 
        //[Authorize(Roles = "RecursosHumanos")]
        //  [Authorize(Roles = "Agentes")]
        public ActionResult Index()
        {

            Session["Metodo"] = "";

            return View(db.Agentes.ToList());
        }



        // GET: Agentes/Details/5        

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Agentes agente = db.Agentes.Find(id);

            if (agente == null)
            {
                return RedirectToAction("Index");
            }

            Session["Metodo"] = "";

            return View(agente);
        }

        // GET: Agentes/Create
        /// <summary>
        /// mostra a view para carregar os dados de um novo Agente
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "RecursosHumanos, Agentes")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agentes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        /// <summary>
        /// recolhe os dados da View, sobre um novo Agente
        /// </summary>
        /// <param name="agente">dados do novo Agente</param>
        /// <param name="fotografia">ficheiro com a foto do novo Agente</param>
        /// <returns></returns>
        [Authorize(Roles = "RecursosHumanos")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,Esquadra")] Agentes agente,
                                    HttpPostedFileBase fotografia)
        {

            // vars auxiliares
            string caminho = "";
            bool ficheiroValido = false;


            /// 1º será que foi enviado um ficheiro?
            if (fotografia == null)
            {
                // atribuir uma foto por defeito ao Agente
                agente.Fotografia = "no_image_user_profile.png";
            }
            else
            {
                /// 2º será que o ficheiro, se foi fornecido, é do tipo correto?
                string mimeType = fotografia.ContentType;
                if (mimeType == "image/jpeg" || mimeType == "image/png")
                {
                    // o ficheiro é do tipo correto

                    /// 3º qual o nome a atribuir ao ficheiro?
                    Guid g;
                    g = Guid.NewGuid(); // obtem os dados para o nome do ficheiro
                                        // e qual a extensão do ficheiro?
                    string extensao = Path.GetExtension(fotografia.FileName).ToLower();
                    // montar o novo nome
                    string nomeFicheiro = g.ToString() + extensao;
                    // onde guardar o ficheiro?
                    caminho = Path.Combine(Server.MapPath("~/imagens/"), nomeFicheiro);

                    /// 4º como o associar ao novo Agente?
                    agente.Fotografia = nomeFicheiro;

                    // marcar o ficheiro como válido
                    ficheiroValido = true;
                }
                else
                {
                    // o ficheiro fornecido não é válido
                    // atribuo a imagem por defeito ao Agente
                    agente.Fotografia = "no_image_user_profile.png";
                }
            }

            /// confronta os dados q vêm da view com a forma que os dados devem ter.
            /// ie, valida os dados com o Modelo
            if (ModelState.IsValid)
            {
                try
                {
                    db.Agentes.Add(agente);
                    db.SaveChanges();

                    /// 5º como o guardar no disco rígido? e onde?
                    if (ficheiroValido) fotografia.SaveAs(caminho);

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    throw;
                }

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
        /// <summary>
        /// mostra na view os dados de um agente para porterior, eventual, remoção
        /// </summary>
        /// <param name="id">identificador do agente a remover</param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {

            // o ID do agente não foi fornecido
            // não é possível procurar o Agente
            // o que devo fazer?
            if (id == null)
            {
                ///  opção por defeito do 'template'
                ///  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                /// e não há ID do Agente, uma de duas coisas aconteceu:
                ///   - há um erro nos links da aplicação
                ///   - há um 'chico experto' a fazer asneiras no URL

                /// redireciono o utilzador para o ecrã incial
                return RedirectToAction("Index");
            }


            // procura os dados do Agentes, cujo ID é fornecido
            Agentes agente = db.Agentes.Find(id);

            /// se o agente não fôr encontrado
            if (agente == null)
            {
                // ou há um erro,
                // ou há um 'chico experto'...
                //   return HttpNotFound();

                /// redireciono o utilzador para o ecrã incial
                return RedirectToAction("Index");
            }

            /// para o caso do utilizador alterar, de forma fraudulenta,
            /// os dados do Agente, vamos guardá-los internamente
            /// Para isso, vou guardar o valor do ID do Agente
            /// - guardar o ID do Agente num cookie cifrado
            /// - guardar o ID numa var. de sessão 
            ///      (quem estiver a usar o Asp .Net Core já não tem esta ferramenta...)
            /// - outras opções...
            Session["IdAgente"] = agente.ID;
            Session["Metodo"] = "Agentes/Delete";

            // envia para a View os dados do Agente encontrado
            return View(agente);
        }





        // POST: Agentes/Delete/5
        /// <summary>
        /// concretizar a operação de remoção de um agente
        /// </summary>
        /// <param name="id"> identificador do agente</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {

            if (id == null)
            {
                // se entrei aqui,é porque é pq há um erro
                // nao se sabe o ID do agente a remover
                return RedirectToAction("Index");
            }

            // avaliar se o ID do agente que é fornecido
            // é o mesmo ID do agente que foi apresentado no ecrã
            if (id != (int)Session["IdAgente"])
            {
                // há um ataque!
                // redirecionar para a página de Index
                return RedirectToAction("Index");
            }

            // avaliar se o método é o que é esperado
            string operacao = "Agentes/Delete";
            if (operacao != (string)Session["Metodo"])
            {
                // há um ataque!
                // redirecionar para a página de Index
                return RedirectToAction("Index");
            }

            // procura os dados do Agente, na BD
            Agentes agente = db.Agentes.Find(id);

            if (agente == null)
            {
                // não foi possível encontrar o Agente
                return RedirectToAction("Index");
            }

            try
            {
                db.Agentes.Remove(agente);
                db.SaveChanges();
            }
            catch (Exception)
            {
                // captura a excessão e processa o código para resolver o problema
                // pode haver mais do que um 'catch' associado a um 'try'

                // enviar mensagem de erro para o utilizador
                ModelState.AddModelError("", "Ocorreu um erro com a eliminação do Agente "
                                            + agente.Nome +
                                            ". Provavelmente relacionado com o facto do " +
                                            "agente ter emitido multas...");
                // devolver os dados do Agente à View
                return View(agente);
            }

            // redireciona o interface para a view INDEX associada ao controller Agentes
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