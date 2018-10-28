using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

//http://www.linhadecodigo.com.br/artigo/3712/testando-servicos-web-api-com-postman.aspx

namespace WebAPI.Controllers
{
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        private static List<UsuarioModel> listaUsuarios = new List<UsuarioModel>();

        [AcceptVerbs("POST")]
        [Route("usuario")]
        public string CadastrarUsuario(UsuarioModel usuario)
        {
            UsuarioModel buscaUsuario = listaUsuarios.FirstOrDefault(x => x.id == usuario.id);

            if(buscaUsuario != null)
            {
                return "Já existe um usuário cadastrado com este ID";
            }
            else
            {
                listaUsuarios.Add(usuario);

                return "Usuário cadastrado com sucesso!";
            }
            
        }

        [AcceptVerbs("PUT")]
        [Route("usuario")]
        public string AlterarUsuario(UsuarioModel usuario)
        {
            UsuarioModel buscaUsuario = listaUsuarios.FirstOrDefault(x => x.id == usuario.id);

            if (buscaUsuario != null)
            {
                listaUsuarios.Where(n => n.id == usuario.id)
                         .Select(s =>
                         {
                             s.email = usuario.email;
                             s.nome = usuario.nome;
                             s.sobrenome = usuario.sobrenome;
                             s.endereco = usuario.endereco;
                             s.nascimento = usuario.nascimento;
                             
                             return s;

                         }).ToList();
                return "Usuário alterado com sucesso!";
            }
            else return "Usuário não encontrado";

        }

        [AcceptVerbs("DELETE")]
        [Route("usuario/{codigo}")]
        public string ExcluirUsuario(int codigo)
        {
            try
            {
                UsuarioModel usuario = listaUsuarios.Where(n => n.id == codigo)
                                                .Select(n => n)
                                                .First();
                if (usuario != null)
                {
                    listaUsuarios.Remove(usuario);

                    return "Registro excluido com sucesso!";
                }
                return "Registro não encontrado";
            }
            catch
            {
                return "Registro não encontrado";
            }
            
            
            
        }

        [AcceptVerbs("GET")]
        [Route("usuario/{codigo}")]
        public UsuarioModel ConsultarUsuarioPorCodigo(int codigo)
        {

            UsuarioModel usuario = listaUsuarios.Where(n => n.id == codigo)
                                                .Select(n => n)
                                                .FirstOrDefault();
            return usuario != null ? usuario : new UsuarioModel();

        }

        [AcceptVerbs("GET")]
        [Route("usuario")]
        public List<UsuarioModel> ConsultarUsuarios()
        {
            return listaUsuarios;
        }
    }
}
