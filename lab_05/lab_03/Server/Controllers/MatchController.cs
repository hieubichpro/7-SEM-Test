using AutoMapper;
using lab_03.BL.Exceptions;
using lab_03.BL.Models;
using lab_03.BL.Services;
using lab_03.Server.DTO;
using Microsoft.AspNetCore.Mvc;

namespace lab_03.Server.Controllers
{
    [ApiController]
    [Route("api/v1/matches")]
    public class MatchController : ControllerBase
    {
        private ILogger<MatchController> _logger;
        private IMapper _mapper;
        private MatchService _matchService;
        public MatchController(IMapper mapper, ILogger<MatchController> logger, MatchService matchService)
        {
            _mapper = mapper;
            _logger = logger;
            _matchService = matchService;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Create([FromBody] MatchDto matchDto)
        {
            try
            {
                _matchService.InsertMatch(_mapper.Map<MatchDto, Match>(matchDto));
                return Ok();
            }
            catch (MatchExistsException ex)
            {
                _logger.LogError("error while create match");
                return Conflict();
            }
            catch (Exception ex)
            {
                _logger.LogError("server error");
                throw;
            }
        }
        //[HttpGet]
        //[ProducesResponseType(200)]
        //public IActionResult GetAll([FromQuery] int idleague)
        //{
        //    var matches = _matchService.GetMatches(idleague);
        //    return Ok(matches.Select(m => _mapper.Map<Match, MatchDto>(m)).ToList());
        //}


        [HttpGet("{idMatch}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetMatch(int idMatch)
        {
            try
            {
                var match = _matchService.GetMatch(idMatch);
                return Ok(_mapper.Map<Match, MatchDto>(match));
            }
            catch (MatchNotFoundException ex)
            {
                _logger.LogError("match not found");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("server error");
                throw;
            }
        }
        [HttpDelete("{idMatch}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult DeteleMatch(int idmatch)
        {
            try
            {
                _matchService.DeleteMatch(idmatch);
                return Ok();
            }
            catch (MatchNotFoundException ex)
            {
                _logger.LogError("not found");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("error server");
                throw;
            }
        }
        [HttpPatch("{idMatch}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Update(int idMatch, [FromBody] RawMatchDto rawmatchdto)
        {
            try
            {
                _matchService.EnterScore(idMatch, _mapper.Map<RawMatchDto, Match>(rawmatchdto));
                return Ok();
            }
            catch (MatchNotFoundException ex)
            {
                _logger.LogError("match not found");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("server error");
                throw;
            }
        }
    }
}
