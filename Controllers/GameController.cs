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
            // Get a list of all games:
            List<GameModel> games = gameDAO.SelectAll();
            return View("Index", games);
        }

        public ActionResult Details(int gameID) {
            GameDAO gameDAO = new GameDAO();
            // Get a game from database using gameID:
            GameModel game = gameDAO.SelectOne(gameID);
            return View("Details", game);
        }

        public ActionResult AddNew() {
            GameDAO gameDAO = new GameDAO();
            ViewModel viewModel = new ViewModel {
                // Get a list of all genres:
                Genres = gameDAO.SelectGenres(),
                // Get a list of all platforms:
                Platforms = gameDAO.SelectPlatforms()
            };
            return View("AddNew", viewModel);
        }

        public ActionResult Insert(ViewModel model) {
            GameDAO gameDAO = new GameDAO();

            //Check if game already exists in the database:
            bool gameExistsInDB = gameDAO.CheckIfExists(model.Title);

            if (gameExistsInDB == false) {
                // Add the game to the database:
                try {
                    gameDAO.InsertNew(model);
                    ViewBag.ResultTitle = "Success!";
                    ViewBag.ResultMessage = "The game was added to the database.";
                    return View("Result");
                }
                catch (Exception e) {
                    ViewBag.ResultTitle = "Failed...";
                    ViewBag.ResultMessage = e.Message;
                    return View("Result");
                }
            }
            else {
                // Game already exists in database:
                ViewBag.ResultTitle = "Failed...";
                ViewBag.ResultMessage = "The game you tried to add already exists in the database.";
                return View("Result");
            }
        }

        public ActionResult Delete(int gameID) {
            GameDAO gameDAO = new GameDAO();
            try {
                gameDAO.Delete(gameID);
                ViewBag.ResultTitle = "Success!";
                ViewBag.ResultMessage = "The game was removed from the database.";
                return View("Result");
            }
            catch (Exception e) {
                ViewBag.ResultTitle = "Failed...";
                ViewBag.ResultMessage = e.Message;
                return View("Result");
            }
        }
    }
}