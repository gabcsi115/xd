using HalakAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HalakAPI.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class HorgaszokController : Controller
    {
        [HttpGet("All")]
        public IActionResult GetAll ()
        {
            using (var context = new HalakContext())
            {
                try
                {
                    return Ok(context.Horgaszoks.ToList());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("ById")]
        public IActionResult GetById(int id)
        {
            using (var context = new HalakContext())
            {
                try
                {
                    if (context.Horgaszoks.Select(h => h.Id).Contains(id))
                    {
                        var result = context.Horgaszoks.FirstOrDefault(h => h.Id == id);
                        return Ok(result);
                    }
                    else
                    {
                        return NotFound("Nem létező horgász");
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("Uj")]
        public async Task<IActionResult> PostUj(Horgaszok horgasz)
        {
            using (var context = new HalakContext())
            {
                try
                {
                    await context.Horgaszoks.AddAsync(horgasz);
                    await context.SaveChangesAsync();
                    return Ok("Sikeres rögzítés");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut("Modosit")]
        public IActionResult Put(Horgaszok horgasz)
        {
            using (var context = new HalakContext())
            {
                try
                {
                    if (context.Horgaszoks.Select(h => h.Id).Contains(horgasz.Id))
                    {
                        context.Horgaszoks.Update(horgasz);
                        context.SaveChanges();
                        return Ok("Sikeres módosítás");
                    }
                    else
                    {
                        return NotFound("Nem létező horgász");
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpDelete("Torolo/{azon}")]
        public IActionResult DeleteById(int id, string azon)
        {
            if (azon != Program.azonosito)
            {
                using (var context = new HalakContext())
                {
                    try
                    {
                        if (context.Horgaszoks.Select(h => h.Id).Contains(id))
                        {
                            Horgaszok torlendo = new() { Id = id };
                            context.Horgaszoks.Remove(torlendo);
                            context.SaveChanges();
                            return Ok("Sikeres törlés");
                        }
                        else
                        {
                            return NotFound("Nem létező horgász");
                        }

                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }
            else
            {
                return StatusCode(401, "Nem egedélyezett művelet");
            }
        }

    }
}
