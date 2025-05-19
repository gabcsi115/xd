using HalakAPI.DTOs;
using HalakAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;

namespace HalakAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HalakController : ControllerBase
    {
        [HttpGet("ByHorgaszId")]
        public IActionResult GetByHorgaszId (int id )
        {
            using (var context = new HalakContext())
            {
                try
                {
                    if (context.Horgaszoks.Select(h => h.Id).Contains(id))
                    {
                        var result = context.Fogasoks.Include(f => f.Horgasz)
                            .Include(f => f.Hal)
                            .Where(f => f.HorgaszId == id)
                            .Select(f => f.Hal)
                            .ToList();
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

        [HttpGet("ByHorgaszName")]
        public IActionResult GetByHorgaszName(string name)
        {
            using (var context = new HalakContext())
            {
                try
                {
                    if (context.Horgaszoks.Select(h => h.Nev).Contains(name))
                    {
                        var result = context.Fogasoks.Include(f => f.Horgasz)
                            .Include(f => f.Hal)
                            .Where(f => f.Horgasz.Nev == name)
                            .Select(f => f.Hal)
                            .ToList();
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

        [HttpGet("MeretFolott")]
        public IActionResult GetMeretFolott (decimal hatar)
        {
            using (var context = new HalakContext())
            {
                try
                {
                    var result = context.Halaks.Include(h => h.To)
                        .Where(h => h.MeretCm >= hatar)
                        .Select(h => new MeretToDTO()
                        {
                            Faj = h.Faj,
                            MeretCm = h.MeretCm,
                            ToNev = h.To.Nev
                            
                        }).ToList();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
