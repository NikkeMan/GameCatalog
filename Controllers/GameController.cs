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

        public ActionResult AddNew() {

            GameDAO gameDAO = new GameDAO();

            ViewModel viewModel = new ViewModel {
                // Get a list of genres:
                Genres = gameDAO.SelectGenres(),
                // Get a list of platforms:
                Platforms = gameDAO.SelectPlatforms()
            };

            return View("AddNew", viewModel);
        }

        public ActionResult Insert(ViewModel model) {
            GameDAO gameDAO = new GameDAO();

            ////Get selected genres in Genres list:
            //List<SelectListItem> selectedGenres = gameDAO.GetSelectedGenres();

            //// Get selected platforms in Platforms list:
            //List<SelectListItem> selectedPlatforms = gameDAO.GetSelectedPlatforms();

            //ViewModel newModel = new ViewModel {
            //    Title = model.Title,
            //    Developer = model.Developer,
            //    ReleaseDate = model.ReleaseDate
            //};

            try {
                gameDAO.InsertNew(model);

                return View("Success");
            }
            catch (Exception e) {
                throw e;
            }
        }
    }
}