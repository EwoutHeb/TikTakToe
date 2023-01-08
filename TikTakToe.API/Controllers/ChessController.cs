﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TikTakToe.Core.Games;
using TikTakToe.Core.Players;
using TikTakToe.Core.Boards;
using TikTakToe.Core.Enums;

namespace TikTakToe.API.Controllers {
    [Route("api/")]
    [ApiController]
    public class ChessController : ControllerBase {
        private IGame _game;

        public ChessController() {
            _game = new Game(new User(), new User(), new Board());
        }

        [HttpPost]
        [Route("newgame")]
        [Route("newgame/{player1Class}/{player2Class}")]
        public IActionResult NewGame(string? player1Class = "User", string? player2Class = "User") {
            Player player1 = GetPlayer(player1Class);
            Player player2 = GetPlayer(player2Class);

            var _game = new Game(player1, player2, new Board());

            return Ok(_game.ToString());
        }

        private Player GetPlayer(string playerClassNAme) {
            switch(playerClassNAme) {
                case "User":
                    return new User();
                case "AISingle":
                    return new AISingle();
            }
            return new User();
        }

        [HttpPost]
        [Route("MakeMove/{position}/{move}")]
        public IActionResult MakeMove(int position, Squares? move) {
            _game.MakeMove(position);
            return Ok();
        }

        [HttpGet]
        [Route("score")]
        public IActionResult Score() {
            return Ok(_game.GetScore());
        }

        [HttpGet]
        [Route("board")]
        public IActionResult Board() {
            return Ok(_game.GetBoardString());
        }
    }
}
