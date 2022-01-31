using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.ModelsDTO;

namespace WebAPI.Service.Interfaces
{
  public interface IHeroService
  {
    Task<IEnumerable<HeroDTO>> GetHeroesAsync();
    Task<HeroDTO> GetHeroAsync(string name);
    Task<HeroDTO> AddHeroAsync(HeroDTO newHero);
    Task<HeroDTO> RemoveHeroAsync(string name);
    Task<HeroDTO> UpdateHeroAsync(HeroDTO hero);
  }
}
