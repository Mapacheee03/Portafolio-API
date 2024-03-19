using System.Text.RegularExpressions;
using Portafolio_API.Models;
using Portafolio_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Portafolio_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PortafolioController : ControllerBase
{

    private readonly ILogger<PortafolioController> _logger;
    private readonly PortafolioServices _portafolioServices;

    public PortafolioController(ILogger<PortafolioController> logger, PortafolioServices portafolioServices)
    {
        _logger = logger;
        _portafolioServices = portafolioServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetDrivers()
    {
        var drivers = await _portafolioServices.GetAsync();
        return Ok(drivers);
    }
    [HttpPost]
    public async Task<IActionResult> InsertPortafolio([FromBody] Portafolio portafolioToInsert)
    {
        if (portafolioToInsert == null)
            return BadRequest();
        if (portafolioToInsert.Name == string.Empty)
            ModelState.AddModelError("Name", "El Portafolio no debe estar vacio");

        await _portafolioServices.InsertPortafolio(portafolioToInsert);

        return Created("Created", true);
    }

    [HttpDelete("ID")]
    public async Task<IActionResult> DeletePortafolio(string idToDelete)
    {
        if (idToDelete == null)
            return BadRequest();
        if (idToDelete == string.Empty)
            ModelState.AddModelError("Id", "No debe dejar el id vacio");

        await _portafolioServices.DeletePortafolio(idToDelete);

        return Ok();
    }

    [HttpPut("DriverToUpdate")]
    public async Task<IActionResult> UpdatePortafolio(Portafolio portafolioToUpdate)
    {
        if (portafolioToUpdate == null)
            return BadRequest();
        if (portafolioToUpdate.Id == string.Empty)
            ModelState.AddModelError("Id", "No debe dejar el id vacio");
        if (portafolioToUpdate.Name == string.Empty)
            ModelState.AddModelError("Name", "No debe dejar el nombre vacio");
        if (portafolioToUpdate.Description == string.Empty)
            ModelState.AddModelError("Description", "No debe dejar la descripcion vacia");
        if (portafolioToUpdate.Team == string.Empty)
            ModelState.AddModelError("Team", "No debe dejar el Team vacio");
        if (portafolioToUpdate.Correo == string.Empty)
            ModelState.AddModelError("Correo", "No debe dejar el Correo vacio");
        if (portafolioToUpdate.FecNac == string.Empty)
            ModelState.AddModelError("FecNac", "No debe dejar el Cumpleaños vacio");
        if (portafolioToUpdate.Telefono == string.Empty)
            ModelState.AddModelError("Telefono", "No debe dejar el Telefono vacio");
        await _portafolioServices.UpdatePortafolio(portafolioToUpdate);

        return Ok();
    }

    [HttpGet("ID")]
    public async Task<IActionResult> GetPortafolioById(string idToSearch)
    {
        var drivers = await _portafolioServices.GetPortafolioById(idToSearch);
        return Ok(drivers);
    }

}
