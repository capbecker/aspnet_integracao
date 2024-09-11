using Backend.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.@interface
{
    public interface IProdutoService
    {
        Uri getUri(FiltroPaginacao filtro, HttpRequest request);

        Task<RespostaPaginada<List<Produto>>> generatePaginacaoAsync(FiltroPaginacao filtro, HttpRequest request);
    }
}
