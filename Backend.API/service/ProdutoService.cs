using Backend.API.@interface;
using Backend.API.Models;
using Backend.API.repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Backend.API.Controllers
{
    public class ProdutoService : IProdutoService
    {
        public readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }


        public Uri getUri(FiltroPaginacao filtro, HttpRequest request) {   
            StringBuilder url = new StringBuilder();
            url.Append(request.HttpContext.Request.Scheme)// https
                .Append("://")
                .Append(request.HttpContext.Request.Host) // localhost:7277
                .Append(request.Path);                    // /api/produto/buscarProdutos

            ArrayList parametros = new ArrayList();
            if (filtro==null)
                return new Uri(url.ToString());
            else
            {
                if (filtro.pageNumber!=null)
                {
                    parametros.Add("pageNumber=" + filtro.pageNumber);
                }
                if (filtro.pageSize != null)
                {
                    parametros.Add("pageSize=" + filtro.pageSize);
                }
            }
            url.Append("?");
            url.Append(string.Join("&", parametros.ToArray()));
            return new Uri(url.ToString());
        }

        public async Task<RespostaPaginada<List<Produto>>> generatePaginacaoAsync(FiltroPaginacao filtro, HttpRequest request)
        {
            FiltroPaginacao paginacao = new FiltroPaginacao(filtro.pageNumber, filtro.pageSize);
            List<Produto> listaProdutos = await _produtoRepository.findAll(paginacao);
            int totalRegistros = await _produtoRepository.countAll();
            int totalPaginas = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRegistros) / Convert.ToDecimal(paginacao.pageSize)));
            int proximaPagina = paginacao.pageNumber == 1 ? 1 : paginacao.pageNumber - 1;
            int anteriorPagina = paginacao.pageNumber == totalPaginas ? totalPaginas : paginacao.pageNumber + 1;

            Uri UriPrimeiraPagina = getUri(new FiltroPaginacao(1, paginacao.pageSize), request);
            Uri UriUltimaPagina = getUri(new FiltroPaginacao(totalPaginas, paginacao.pageSize), request);
            Uri UriProximaPagina = getUri(new FiltroPaginacao(proximaPagina, paginacao.pageSize), request);
            Uri UriAnteriorPagina = getUri(new FiltroPaginacao(anteriorPagina, paginacao.pageSize), request);

            return new RespostaPaginada<List<Produto>>(
               listaProdutos, true, null, "Sucesso", paginacao.pageNumber, paginacao.pageSize,
           UriPrimeiraPagina, UriUltimaPagina, totalPaginas, totalRegistros, UriAnteriorPagina, UriProximaPagina);
        }
    }
}
