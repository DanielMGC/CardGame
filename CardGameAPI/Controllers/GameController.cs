using CardGameAPI.Entities;
using CardGameAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace CardGameAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpPost]
        [Route("start")]
        public IActionResult CreateGame()
        {
            var result = gameService.CreateGame();
            if (!result.result.Success)
                return BadRequest(result.result.Message);

            return Content(JsonSerializer.Serialize(result.game, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }

        [HttpGet]
        [Route("game")]
        public IActionResult GetGame(int gameId)
        {
            var result = gameService.GetGame(gameId);
            if (!result.result.Success)
                return BadRequest(result.result.Message);

            return Content(JsonSerializer.Serialize(result.game, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }

        [HttpDelete]
        [Route("end")]
        public IActionResult DeleteGame(int gameId)
        {
            var result = gameService.DeleteGame(gameId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Content(JsonSerializer.Serialize(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }

        [HttpPost]
        [Route("add-deck")]
        public IActionResult AddDeck(int gameId)
        {
            var result = gameService.AddDeck(gameId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Content(JsonSerializer.Serialize(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }

        [HttpPost]
        [Route("add-player")]
        public IActionResult AddPlayer(int gameId, string name)
        {
            var result = gameService.AddPlayer(gameId, name);
            if (!result.Success)
                return BadRequest(result.Message);

            return Content(JsonSerializer.Serialize(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }

        [HttpDelete]
        [Route("remove-player")]
        public IActionResult RemovePlayer(int gameId, string name)
        {
            var result = gameService.RemovePlayer(gameId, name);
            if (!result.Success)
                return BadRequest(result.Message);

            return Content(JsonSerializer.Serialize(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }

        [HttpGet]
        [Route("players")]
        public IActionResult GetPlayers(int gameId)
        {
            var result = gameService.GetPlayers(gameId);
            if (!result.result.Success)
                return BadRequest(result.result.Message);

            return Content(JsonSerializer.Serialize(result.players, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }

        [HttpPut]
        [Route("deal")]
        public IActionResult DealCards(int gameId, string playerName, int numberOfCards)
        {
            var result = gameService.DealCards(gameId, playerName, numberOfCards);
            if (!result.Success)
                return BadRequest(result.Message);

            return Content(JsonSerializer.Serialize(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }

        [HttpGet]
        [Route("undealt")]
        public IActionResult GetUndealtCards(int gameId)
        {
            var result = gameService.GetUndealtCards(gameId);
            if (!result.result.Success)
                return BadRequest(result.result.Message);

            string json = $"{{\"totals\":\"{result.totals}\"}}";
            return Content(json);
        }

        [HttpGet]
        [Route("undealt-by-suit")]
        public IActionResult GetUndealtCardsBySuit(int gameId)
        {
            var result = gameService.GetUndealtCardsBySuit(gameId);
            if (!result.result.Success)
                return BadRequest(result.result.Message);

            string json = $"{{\"totals\":\"{result.totals}\"}}";
            return Content(json);
        }

        [HttpPut]
        [Route("shuffle")]
        public IActionResult ShuffleCards(int gameId)
        {
            var result = gameService.Shuffle(gameId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Content(JsonSerializer.Serialize(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }
    }
}
