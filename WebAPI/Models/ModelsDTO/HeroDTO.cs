using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.ModelsDTO
{
  public class HeroDTO
  {
    public string Name { get; set; }
    public int Age { get; set; }
    public float Height { get; set; }
    public string SuperPowers { get; set; }
    public string SuperVillain { get; set; }
    public int HeroPoints { get; set; }
  }
}
