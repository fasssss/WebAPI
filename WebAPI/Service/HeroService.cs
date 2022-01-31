using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Service.Interfaces;
using MongoDB.Driver.GridFS;
using MongoDB.Driver;
using WebAPI.Models.ModelsDTO;
using WebAPI.Models.ModelsDB;
using AutoMapper;
using MongoDB.Bson;

namespace WebAPI.Service
{
  public class HeroService: IHeroService
  {
    private IMongoCollection<Hero> _Heroes { get; set; }
    private IMongoDatabase _Database { get; set; }
    private Mapper _mapper;
    public HeroService(IMongoDatabase database)
    {
      _Database = database;
      _Heroes = _Database.GetCollection<Hero>("Heroes");
      var config = new MapperConfiguration(cfg => cfg.CreateMap<Hero, HeroDTO>());
      _mapper = new Mapper(config);
    }

    public async Task<IEnumerable<HeroDTO>> GetHeroesAsync()
    {
      var heroesListDTO = _mapper.Map<IEnumerable<HeroDTO>>(await _Heroes.Find(_ => true).ToListAsync());
      return heroesListDTO;
    }

    public async Task<HeroDTO> GetHeroAsync(string name)
    {
      var heroDTO = _mapper.Map<HeroDTO>(await _Heroes.Find(x => x.Name == name).FirstOrDefaultAsync());
      return heroDTO;
    }

    public async Task<HeroDTO> AddHeroAsync(HeroDTO newHero)
    {
        if (_mapper.Map<HeroDTO>(await _Heroes.Find(x => x.Name == newHero.Name).FirstOrDefaultAsync()) != null)
        {
          throw(new ArgumentException("A hero with same name already exists"));
        }

        await _Heroes.InsertOneAsync(
          new Hero{ Id = "",
            Name = newHero.Name,
            Age = newHero.Age,
            Height = newHero.Height,
            SuperPowers = newHero.SuperPowers,
            SuperVillain = newHero.SuperVillain,
            HeroPoints = newHero.HeroPoints
            }
          );

      return newHero;
    }

    public async Task<HeroDTO> UpdateHeroAsync(HeroDTO updatedHero)
    {
      var alreadyExisted = _mapper.Map<HeroDTO>(await _Heroes.Find(x => x.Name == updatedHero.Name).FirstOrDefaultAsync());
      if (alreadyExisted == null)
      {
        throw (new ArgumentNullException("Error. Hero wasn't found"));
      }

      if ((await _Heroes.UpdateOneAsync(x => x.Name == updatedHero.Name,
        new BsonDocument("$set", new BsonDocument{
          { "Age", updatedHero.Age },
          { "Height", updatedHero.Height },
          { "SuperPowers", updatedHero.SuperPowers },
          { "SuperVillain", updatedHero.SuperVillain },
          { "HeroPoints", updatedHero.HeroPoints }
        }))).ModifiedCount == 0)
      {
        throw (new ArgumentException("Error occured when update operation was processing"));
      }

      return updatedHero;
    }

    public async Task<HeroDTO> RemoveHeroAsync(string name)
    {
      var hero = _mapper.Map<HeroDTO>(await _Heroes.Find(x => x.Name == name).FirstOrDefaultAsync());
      if (hero == null)
      {
        throw (new ArgumentNullException("Error. Hero wasn't found"));
      }

      if ((await _Heroes.DeleteOneAsync(x => x.Name == name)).DeletedCount == 0)
      {
        throw (new ArgumentException("Error occured when remove operation was processing"));
      }

      return hero;
    }
  }
}
