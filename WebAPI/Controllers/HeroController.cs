using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.ModelsDTO;
using WebAPI.Service.Interfaces;
using Newtonsoft;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
  [ApiController]
  [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
  public class HeroController : ControllerBase
  {
    private IHeroService _heroHandler;

    public HeroController(IHeroService heroHandler)
    {
      _heroHandler = heroHandler;
    }

    // GET: api/<HeroController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HeroDTO>>> Get()
    {
      try
      {
        IEnumerable<HeroDTO> heroes = await _heroHandler.GetHeroesAsync();
        return Ok(heroes);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // POST api/<HeroController>
    [HttpPost]
    public async Task<ActionResult<HeroDTO>> PostHero(HeroDTO hero)
    {
      try
      {
        var insertedHero = await _heroHandler.AddHeroAsync(hero);
        return new ObjectResult(insertedHero);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // DELETE api/<HeroController>/5
    [HttpDelete("{name}")]
    public async Task<ActionResult<HeroDTO>> Delete(string name)
    {
      try
      {
        var hero = await _heroHandler.RemoveHeroAsync(name);
        return hero;
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPatch]
    public async Task<ActionResult<HeroDTO>> Update(HeroDTO hero)
    {
      try
      {
        var updatedHero = await _heroHandler.UpdateHeroAsync(hero);
        return updatedHero;
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}
