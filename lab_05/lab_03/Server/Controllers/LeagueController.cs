using AutoMapper;
using lab_03.BL.Exceptions;
using lab_03.BL.Models;
using lab_03.BL.Services;
using lab_03.Server.DTO;
using Microsoft.AspNetCore.Mvc;

namespace lab_03.Server.Controllers
{
    [ApiController]
    [Route("api/v1/leagues")]
    public class LeagueController : ControllerBase
    {
        private ILogger<LeagueController> logger;
        private IMapper mapper;
        private LeagueService leagueService;
        public LeagueController(ILogger<LeagueController> logger, IMapper mapper, LeagueService leagueService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.leagueService = leagueService;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult Create([FromBody] RawLeagueDto rawleagueDto)
        {
            try
            {
                leagueService.InsertLeague(mapper.Map<RawLeagueDto, League>(rawleagueDto));
                return Ok();
            }
            catch (LeagueExistException ex)
            {
                logger.LogError("league have been exists");
                return Conflict();
            }
            catch (Exception ex)
            {
                logger.LogError("server error");
                throw;
            }
        }
        //[HttpGet]
        //[ProducesResponseType(200)]
        ////[Route("api/v1/leagues/idUser")]
        //public IActionResult Get([FromQuery] int idUser)
        //{
        //    var leagues = leagueService.GetByUser(idUser);
        //    return Ok(leagues.Select(l => mapper.Map<League, LeagueDto>(l)).ToList());
        //}


        [HttpGet("{idLeague}/clubs")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult GetClubByIdLeague(int idLeague)
        {
            try
            {
                var clubs = leagueService.GetClubsByIdLeague(idLeague);
                return Ok(clubs);
            }
            catch (Exception ex)
            {
                logger.LogError("server error");
                throw;
            }
        }

        [HttpGet("{idLeague}/matches")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult GetMatchesByIdLeague(int idLeague)
        {
            try
            {
                var matches = leagueService.GetMatchesByIdLeague(idLeague);
                return Ok(matches);
            }
            catch (Exception ex)
            {
                logger.LogError("server error");
                throw;
            }
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Update([FromBody] LeagueDto leagueDto)
        {
            try
            {
                leagueService.ModifyLeague(mapper.Map<LeagueDto, League>(leagueDto));
                return Ok();
            }
            catch (LeagueNotFoundException ex)
            {
                logger.LogError("server error");
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError("server error");
                throw;
            }
        }

        [HttpGet("{idLeague}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetLeague(int idLeague)
        {
            try
            {
                var league = leagueService.GetLeague(idLeague);
                return Ok(league);
            }
            catch (LeagueNotFoundException ex)
            {
                logger.LogError("league not exists");
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError("server error");
                throw;
            }
        }

        [HttpDelete("{idLeague}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Detele(int idLeague)
        {
            try
            {
                leagueService.DeleteLeague(idLeague);
                return Ok();
            }
            catch (LeagueNotFoundException ex)
            {
                logger.LogError("league not found");
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError("server error");
                throw;
            }
        }

    }
}
