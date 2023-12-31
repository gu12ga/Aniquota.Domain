﻿using Aniquota.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Policy;

namespace Aniquota.Domain.Controllers
{
    public class ClienteController : Controller
    {
        private ISession session;
        public const string SessionKey = "_idCliente";
        private AplicaController aplicaController;
        private ProdutoController produtoController;

        public ClienteController()
        {

            aplicaController = new AplicaController();
            produtoController = new ProdutoController();

        }

        public IActionResult InserirCliente(string cpf, string nome, string senha, string email)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                return BadRequest("O CPF do cliente é obrigatório.");
            }
            if (string.IsNullOrWhiteSpace(nome))
            {
                return BadRequest("O Nome do cliente é obrigatório.");
            }
            if (string.IsNullOrWhiteSpace(senha))
            {
                return BadRequest("O Senha do cliente é obrigatório.");
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("O Email do cliente é obrigatório.");
            }

            using (var contexto = new Contexto())
            {

                var cliente = new Cliente
                {
                    CPF = cpf,
                    Nome = nome,
                    Senha = senha,
                    Email = email
                };

                contexto.Cliente.Add(cliente);
                contexto.SaveChanges();

            }

            return Ok("Cliente criado com sucesso.");
        }

        public IActionResult ExcluirCliente(int idCliente)
        {
            using (var contexto = new Contexto())
            {
                var cliente = contexto.Cliente.Find(idCliente);

                if (cliente == null)
                {
                    return NotFound("Cliente não encontrado.");
                }

                contexto.Cliente.Remove(cliente);
                contexto.SaveChanges();
            }

            return Ok("Cliente excluído com sucesso.");
        }

        public IActionResult AtualizarCliente(int idCliente, string cpf, string nome, string senha, string email)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                return BadRequest("O CPF do cliente é obrigatório.");
            }
            if (string.IsNullOrWhiteSpace(nome))
            {
                return BadRequest("O Nome do cliente é obrigatório.");
            }
            if (string.IsNullOrWhiteSpace(senha))
            {
                return BadRequest("O Senha do cliente é obrigatório.");
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("O Email do cliente é obrigatório.");
            }

            using (var contexto = new Contexto())
            {
                var cliente = contexto.Cliente.Find(idCliente);

                if (cliente == null)
                {
                    return NotFound("Cliente não encontrado.");
                }

                cliente.CPF = cpf;
                cliente.Nome = nome;
                cliente.Senha = senha;
                cliente.Email = email;

                contexto.SaveChanges();
            }

            return Ok("Cliente atualizado com sucesso.");
        }

        public IActionResult LoginView()
        {
            return View();
        }

        public IActionResult InicialView()
        {

            
            List<AplicaProdutoClienteModel> lista1 = aplicaController.ListaCliente(HttpContext.Session);
            List<Produto> lista2 = produtoController.ListaProduto();

            
            MinhaPaginaViewModel viewModel = new MinhaPaginaViewModel
            {
                Lista1 = lista1,
                Lista2 = lista2
            };

            
            return View(viewModel);
            
        }

        public IActionResult Login(string cpf, string senha)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                return BadRequest("O CPF do cliente é obrigatório.");
            }
            if (string.IsNullOrWhiteSpace(senha))
            {
                return BadRequest("O Senha do cliente é obrigatório.");
            }

            using (var contexto = new Contexto())
            {
                var cliente = contexto.Cliente.FirstOrDefault(c => c.CPF == cpf);

                if (cliente == null)
                {

                    return NotFound("Credenciais inválidas. Tente novamente.");
                }

                if (cliente.Senha != senha)
                {

                    return NotFound("Senha inválida. Tente novamente.");
                }

                HttpContext.Session.SetString(SessionKey, cliente.IdCliente.ToString());

                return RedirectToAction("InicialView", "Cliente");

        }

        }


    }
}
