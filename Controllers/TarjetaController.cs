using System;
using System.Text.Json;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Mvc;

namespace ApiTarjetas.Controllers;
[ApiController]
[Route("[controller]")]
public class TarjetaController : ControllerBase 
{
    private readonly string _filePath = "Tarjeta.bin";

    [HttpPost]
    public IActionResult SaveTarjeta([FromBody] Tarjeta tarjeta)
    {
        try
        {
            // Read the existing Tarjeta.json file
            if (!System.IO.File.Exists("Tarjeta.json"))
            {
                var tarjetas = new List<Tarjeta>();
                var jsonString = JsonSerializer.Serialize(tarjetas);
                System.IO.File.WriteAllText("Tarjeta.json", jsonString);
            }
            else
            {
                var jsonString = System.IO.File.ReadAllText("Tarjeta.json");

                // Deserialize the json string to a list of tarjetas
                List<Tarjeta> tarjetas = JsonSerializer.Deserialize<List<Tarjeta>>(jsonString);
                int newId = tarjetas.Max(x => x.id) + 1;
                tarjeta.id = newId;
                // Add the new tarjeta to the list
                tarjetas.Add(tarjeta);

                // Serialize the updated list of tarjetas
                jsonString = JsonSerializer.Serialize(tarjetas);

                // Save the json string to the Tarjeta.json file
                System.IO.File.WriteAllText("Tarjeta.json", jsonString);
            }
              var jsonStringDespuesAgrega = System.IO.File.ReadAllText("Tarjeta.json");

                // Deserialize the json string to a list of tarjetas
                List<Tarjeta> tarjetasDespuesAgrega = JsonSerializer.Deserialize<List<Tarjeta>>(jsonStringDespuesAgrega);
                return Ok(tarjetasDespuesAgrega);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    [HttpGet]
    public IActionResult GetAllTarjetas()
    {
        try
        {
            // Read the Tarjeta.json file
            var jsonString = System.IO.File.ReadAllText("Tarjeta.json");

            // Deserialize the json string to a list of tarjetas
            List<Tarjeta> tarjetas = JsonSerializer.Deserialize<List<Tarjeta>>(jsonString);

            return Ok(tarjetas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete]
    public IActionResult DeleteTarjeta(int id)
    {
        try
        {
            // Read the existing Tarjeta.json file
            if (!System.IO.File.Exists("Tarjeta.json"))
            {
                return NotFound("No se encontraron tarjetas para eliminar.");
            }
            else
            {      
                var jsonString = System.IO.File.ReadAllText("Tarjeta.json");
                // Deserialize the json string to a list of tarjetas
                List<Tarjeta> tarjetas = JsonSerializer.Deserialize<List<Tarjeta>>(jsonString);
                var tarjeta = tarjetas.FirstOrDefault(x => x.id == id);
                if (tarjeta == null)
                {
                    return NotFound("La tarjeta no se encuentra en la lista");
                }
                tarjetas.Remove(tarjeta);
                // Serialize the updated list of tarjetas
                jsonString = JsonSerializer.Serialize(tarjetas);
                // Save the json string to the Tarjeta.json file
                System.IO.File.WriteAllText("Tarjeta.json", jsonString);
            }
            return Ok(new { message = "Tarjeta eliminada con éxito." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}