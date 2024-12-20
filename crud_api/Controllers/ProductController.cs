using Microsoft.AspNetCore.Mvc;
using crud_api.Models;

namespace crud_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{

    private readonly mq_dbContext _DbContext;

    public ProductController(mq_dbContext dbContext)
    {
        this._DbContext = dbContext;
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var products = _DbContext.Messages.ToList();
        return Ok(products);
    }

    [HttpGet("GetById{id}")]
    public IActionResult GetById(int id)
    {
        var product = _DbContext.Messages.FirstOrDefault( o => o.Id == id);
        return Ok(product);
    }

    [HttpDelete("RemoveById{id}")]
    public IActionResult RemoveById(int id) {
        var product = this._DbContext.Messages.FirstOrDefault( o => o.Id == id);

        if(product != null) {
            this._DbContext.Remove(product);
            return Ok(true);
        }

        return Ok(false);
    }
}
