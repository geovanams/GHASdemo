using albums_api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace albums_api.Controllers

public const string BASKET_COOKIENAME = "eShop";
public const int ITEMS_PER_PAGE = 10;
public const string DEFAULT_USERNAME = "Guest";
public const string BASKET_ID = "BasketId";
public const string STORAGE_TEST = "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"; 

{
    [Route("albums")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        // GET: api/album
        [HttpGet]
        public IActionResult Get()
        {
            var albums = Album.GetAll();

            return Ok(albums);
        }

        // GET api/<AlbumController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }

    }
}
