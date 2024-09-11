using Backend.API.@interface;
using Backend.API.Models;
using Backend.API.repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Numerics;

namespace Backend.API.Controllers
{
    [ApiController]
    [Route("/api/produto")]
    public class ProdutoController : Controller
    {
        public readonly IProdutoRepository _produtoRepository;
        public readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoRepository produtoRepository, IProdutoService produtoService)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
        }

        [HttpGet("buscarProdutos")]
        public async Task<IActionResult> buscarProdutos([FromQuery] FiltroPaginacao filtro) {

            /*------------------------------------------------------
             * HttpContext = Microsoft.AspNetCore.Http.DefaultHttpContext ("sozinho")
             *              Ou Microsoft.AspNetCore.Http, dependendo do contexto (com .ALGUMACOISA)
             *  .Request =  Microsoft.AspNetCore.Http.DefaultHttpRequest
             *  .Response = Microsoft.AspNetCore.Http.DefaultHttpResponse
             *  .Connection = Microsoft.AspNetCore.Http.DefaultConnectionInfo
             *  .Features = Microsoft.AspNetCore.Server.Kestrel.Core.Internal
             *              .Http.Http1Connection`1[Microsoft.AspNetCore.Hosting.HostingApplication+Context]
             *  .Items = Microsoft.AspNetCore.Http.ItemsDictionary
             *  
             *  os objetos HttpContext.Request, .Response e etc pode ser chamado sem o HttpContext
             *------------------------------------------------------*/

            RespostaPaginada<List<Produto>> respostaPaginada = await _produtoService.generatePaginacaoAsync(filtro, Request);
            //_produtoService.generatePaginacaoAsync(filtro, Request);
            return Ok(respostaPaginada);
        }

        /*[HttpGet("buscarProdutos")]
        //--Dependendo da demanda, pode ser interessante a paginaçao ser feita no frontend ou backend--//
        public async Task<IActionResult> buscarProdutos([FromQuery] FiltroPaginacao filtro) {
            FiltroPaginacao paginacao = new FiltroPaginacao(filtro.pageNumber, filtro.pageSize);
            List<Produto> listaProdutos = await _produtoRepository.findAll(paginacao);
            //return Ok(new Response<List<Produto>>(listaProdutos));
            return Ok(new RespostaPaginada<List<Produto>>(listaProdutos, paginacao.pageNumber, paginacao.pageSize));
        }*/

        /*[HttpGet("buscarProdutos")]
        public async Task<ActionResult<List<Produto>>> buscarProdutos()
        {
            List<Produto> listaProdutos = await _produtoRepository.findAll();
            return Ok(new Resposta<List<Produto>>(listaProdutos));
        }*/


        [HttpGet("obterProduto/{id}")]
        public async Task<IActionResult> obterProduto(long id)
        {
            Produto produto = await _produtoRepository.findById(id);
            if (produto == null)
            {
                return NotFound(new Resposta<String>("Produto não encontrado"));
            }
            return Ok(new Resposta<Produto>(produto));
        }


        [HttpPost("salvarProduto")]
        public async Task<IActionResult> salvarProduto(Produto produto)
        {
            _produtoRepository.insert(produto);
            if (await _produtoRepository.SaveAllAsync())
            {
                return Ok(new Resposta<String>("Produto cadastrado"));
            }
            return BadRequest(new Resposta<String>("Ocorreu um problema ao inserir"));
        }

        [HttpDelete("excluirProduto/{id}")]
        public async Task<IActionResult> excluirProduto(long id)
        {
            Produto produto = await _produtoRepository.findById(id);
            Console.Write(produto == null);
            if (produto != null)
            {
                _produtoRepository.delete(produto);
                if (await _produtoRepository.SaveAllAsync())
                {
                    return Ok(new Resposta<String>("Produto removido"));
                }
                return BadRequest(new Resposta<String>("Ocorreu um problema ao remover"));                
            }
            return NotFound(new Resposta<String>("Produto não encontrado"));
        }

        [HttpPut("atualizarProduto")]
        public async Task<IActionResult> atualizarProduto(Produto produto)            
        {
            _produtoRepository.update(produto);
            if (await _produtoRepository.SaveAllAsync())
            {
                return Ok(new Resposta<String>("Produto alterado"));
            }
            return BadRequest(new Resposta<String>("Ocorreu um problema ao alterar"));
        }
    }
}
