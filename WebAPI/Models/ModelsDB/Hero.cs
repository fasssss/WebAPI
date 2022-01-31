using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.ModelsDB
{
  public class Hero
  {
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [Display(Name = "HeroName")]
    public string Name { get; set; }
    [Display(Name = "Age")]
    public int Age { get; set; }
    [Display(Name = "Height")]
    public float Height { get; set; }
    [Display(Name = "SuperPowers")]
    public string SuperPowers { get; set; }
    [Display(Name = "SuperVillain")]
    public string SuperVillain { get; set; }
    [Display(Name = "HeroPoints")]
    public int HeroPoints { get; set; }
  }
}
