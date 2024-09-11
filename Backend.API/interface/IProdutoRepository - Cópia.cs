using Backend.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.@interface
{
    public interface IProdutoService
    {
        Uri getUri(String url, [FromQuery] FiltroPaginacao filtro);        
    }
}
