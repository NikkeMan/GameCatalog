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
    public class GamesController : Controller
    {
        public ActionResult Index() {
            GameDAO gameDAO = new GameDAO();
            // Get a list of all games:
            List<GameModel> models = gameDAO.SelectAll();
            return View("Index", models);
        }
        public ActionResult Details(GameModel model) {
            return View("Details", model);
        }
        public ActionResult Add() {
            GameDAO gameDAO = new GameDAO();
            GameModel model = new GameModel {
                Genres = gameDAO.SelectAllGenres(),
                Platforms = gameDAO.SelectAllPlatforms()
            };

            ViewBag.Title = "Add a new game";
            return View("GameForm", model);
        }
        public ActionResult Edit(GameModel model) {
            GameDAO gameDAO = new GameDAO();

            model.SelectedGenres = gameDAO.SelectGameGenres(model.GameID);
            model.SelectedPlatforms = gameDAO.SelectGamePlatforms(model.GameID);
            model.Genres = gameDAO.SelectAllGenres();
            model.Platforms = gameDAO.SelectAllPlatforms();

            ViewBag.Title = "Edit game";
            return View("GameForm", model);
        }
        public ActionResult SaveChanges(GameModel model) {
            GameDAO gameDAO = new GameDAO();

            // Make sure none of the values are empty:
            if (gameDAO.CheckIfEmptyForm(model)) {
                if (model.GameID == -1) {
                    // Add new game to the database:
                    try {
                        gameDAO.Create(model);
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
                    // Update existing game:
                    try {
                        gameDAO.Update(model);
                        ViewBag.ResultTitle = "Success!";
                        ViewBag.ResultMessage = "The changes have been saved to the database.";
                        return View("Result");
                    }
                    catch (Exception e) {
                        ViewBag.ResultTitle = "Failed...";
                        ViewBag.ResultMessage = e.Message;
                        return View("Result");
                    }
                }
            }
            else {
                // Invalid form:
                ViewBag.ResultTitle = "Failed...";
                ViewBag.ResultMessage = "Trying to add to the database with an invalid form.";
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