using GameCatalog.Data;
using GameCatalog.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameCatalog.Controllers
{
    public class GameController : Controller
    {
        public ActionResult Index() {

            GameDAO gameDAO = new GameDAO();
            List<GameModel> games = gameDAO.SelectAll();

            return View("Index", games);
        }

        public ActionResult Details(int id) {

            GameDAO gameDAO = new GameDAO();
            GameModel game = gameDAO.SelectOne(id);

            return View("Details", game);
        }

        public ActionResult Create() {

            GameDAO gameDAO = new GameDAO();

            ViewModel viewModel = new ViewModel();

            viewModel.Game = new GameModel();
            // Get a list of genres:
            viewModel.Genres = gameDAO.SelectGenres();
            // Get a list of platforms:
            viewModel.Platforms = gameDAO.SelectPlatforms();

            return View("Create", viewModel);
        }
    }
}