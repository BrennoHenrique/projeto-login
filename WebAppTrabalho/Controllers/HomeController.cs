using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppTrabalho.Models;

namespace WebAppTrabalho.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastro()
        {
            ViewData["Message"] = "Cadastre-se";
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(string nome, string email, string senha, string confSenha)
        {
            Controle controle = new Controle();
            controle.Cadastrar(nome, email, senha, confSenha);

            ViewData["Message"] = controle.Menssagem;

            return View();
        }

        [HttpPost]
        public IActionResult SecondPage(string email, string senha)
        {
            Controle controle = new Controle();

            controle.AutenticarUsuario(email, senha);

            if (controle.VerificaAutenticacao(email, senha))
            {
                controle.AutenticarUsuario(email, senha);

                ViewData["Nome"] = controle.Nome;
                return View();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
