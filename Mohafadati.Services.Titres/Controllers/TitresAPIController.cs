using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Mohafadati.Services.Titres.Data;
using Mohafadati.Services.Titres.Models;
using Mohafadati.Services.Titres.Models.Dto;

namespace Mohafadati.Services.Titres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitresAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        public TitresAPIController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public object Get()
        {
            try
            {
                IEnumerable<Titre> objList = _db.Titres.ToList();
                return objList;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        [HttpPost]
        public ActionResult Post([FromBody] TitreDto obj)
        {

            try 
            {
                List<Titre> TitreContent = _db.Titres.ToList();
                Titre? titre = TitreContent.Find((element)=> element.ConservationFonciere.Equals(obj.ConservationFonciere)
                                                             && element.Indice.Equals(obj.Indice) 
                                                             && element.IndiceSpecial.Equals(obj.IndiceSpecial)
                                                             && element.NumeroTitre == obj.NumeroTitre);
                
                if (titre is null)
                {
                    throw new Exception("Ce titre n'existe pas!");
                }
                return Ok(obj);

            }
            catch (Exception ex) 
            {
                return NotFound( new {message = ex.Message} );
            }



            
            //    List<Titre> objList = _db.Titres.ToList();
            //Titre? T = objList.Find((element) => element.ConservationFonciere.Equals(obj.ConservationFonciere) 
            //&& element.Indice.Equals(obj.Indice) 
            //&& element.IndiceSpecial.Equals(obj.IndiceSpecial)
            //&& element.NumeroTitre == obj.NumeroTitre);

            
            //if (T != null)
            //{
            //    TitreDto Titre = new()
            //    {
            //        ConservationFonciere = T.ConservationFonciere,
            //        Indice = T.Indice,
            //        IndiceSpecial = T.IndiceSpecial,
            //        NumeroTitre = T.NumeroTitre
            //    };
            //    return Titre;
            //}
            //else return null;
            

        }
    }
}
