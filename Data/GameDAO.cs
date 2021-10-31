using GameCatalog.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameCatalog.Data {
    public class GameDAO {

		//Connection string for a local database named games_db: 
        private readonly string connectionString = "Data Source=.;Initial Catalog=games_db;Integrated Security=True; MultipleActiveResultSets=true";

        public List<GameModel> SelectAll() {
            List<GameModel> returnList = new List<GameModel>();

			using(SqlConnection connection = new SqlConnection(connectionString)) {

				// SQL query that selects all the data from the database:
				string sqlQuery = @"SELECT game.*, g.genres, p.platforms FROM game
								LEFT JOIN (SELECT game.game_ID, STRING_AGG(genre.name, ', ')
									WITHIN GROUP (ORDER BY genre.name) AS genres FROM game 
									INNER JOIN game_genre
									ON game.game_ID = game_genre.game_ID
									INNER JOIN genre
									ON genre.genre_ID = game_genre.genre_ID
									GROUP BY game.game_ID) AS g
								ON game.game_ID = g.game_ID

								LEFT JOIN (SELECT game.game_ID, STRING_AGG(platform.name, ', ')
									WITHIN GROUP (ORDER BY platform.name) AS platforms FROM game 
									INNER JOIN game_platform
									ON game.game_ID = game_platform.game_ID
									INNER JOIN platform
									ON platform.platform_ID = game_platform.platform_ID
									GROUP BY game.game_ID) AS p
								ON game.game_ID = p.game_ID
								;";

				SqlCommand command = new SqlCommand(sqlQuery, connection);

				// Open connection:
				connection.Open();

				// Read data: 
				SqlDataReader reader = command.ExecuteReader();
				if(reader.HasRows) {
					while(reader.Read()) {
                        // Create new GameModel object:
                        GameModel game = new GameModel {
                            GameID = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Developer = reader.GetString(2),
                            ReleaseDate = reader.GetDateTime(3),
                            GameGenres = reader.GetString(4),
                            GamePlatforms = reader.GetString(5)
                        };

                        // Add the object to the return list:
                        returnList.Add(game);
                    }
                }
			}
            return returnList;
        }

		public GameModel SelectOne(int gameID) {
			GameModel gameModel = new GameModel();

			using (SqlConnection connection = new SqlConnection(connectionString)) {

				// SQL query that returns an entry from the database based on game_ID:
				string sqlQuery = @"SELECT game.*, g.genres, p.platforms FROM game
						JOIN(SELECT game.game_ID, STRING_AGG(genre.name, ', ')
							WITHIN GROUP(ORDER BY genre.name) AS genres FROM game
							INNER JOIN game_genre
							ON game.game_ID = game_genre.game_ID
							INNER JOIN genre
							ON genre.genre_ID = game_genre.genre_ID
							GROUP BY game.game_ID) AS g
						ON game.game_ID = g.game_ID

						JOIN(SELECT game.game_ID, STRING_AGG(platform.name, ', ')
							WITHIN GROUP(ORDER BY platform.name) AS platforms FROM game
							INNER JOIN game_platform
							ON game.game_ID = game_platform.game_ID
							INNER JOIN platform
							ON platform.platform_ID = game_platform.platform_ID
							GROUP BY game.game_ID) AS p
						ON game.game_ID = p.game_ID
						WHERE game.game_ID = @game_ID";

				SqlCommand command = new SqlCommand(sqlQuery, connection);

				command.Parameters.Add("@game_ID", System.Data.SqlDbType.Int).Value = gameID;

				// Open connection:
				connection.Open();

				// Read data: 
				SqlDataReader reader = command.ExecuteReader();
				if (reader.HasRows) {
					while (reader.Read()) {
						gameModel.GameID = reader.GetInt32(0);
						gameModel.Title = reader.GetString(1);
						gameModel.Developer = reader.GetString(2);
						gameModel.ReleaseDate = reader.GetDateTime(3);
						gameModel.GameGenres = reader.GetString(4);
						gameModel.GamePlatforms = reader.GetString(5);
					}
				}
			}
			return gameModel;
		}

		public List<SelectListItem> SelectGenres() {
			List<SelectListItem> genres = new List<SelectListItem>();

			using (SqlConnection connection = new SqlConnection(connectionString)) {

				// SQL query that returns all genres:
				string sqlQuery = @"SELECT * FROM genre ORDER BY name";

				SqlCommand command = new SqlCommand(sqlQuery, connection);

				// Open connection:
				connection.Open();

				// Read data: 
				SqlDataReader reader = command.ExecuteReader();
				if (reader.HasRows) {
					while (reader.Read()) {
                        // Create new SelectListItem object and add it to the list:
                        genres.Add(new SelectListItem { Value = reader.GetInt32(0).ToString(), Text = reader.GetString(1) });
					}
				}
			}
			return genres;
        }

		public List<SelectListItem> SelectPlatforms() {
			List<SelectListItem> platforms = new List<SelectListItem>();

			using (SqlConnection connection = new SqlConnection(connectionString)) {

				// SQL query that returns all platforms:
				string sqlQuery = @"SELECT * FROM platform ORDER BY name";

				SqlCommand command = new SqlCommand(sqlQuery, connection);

				// Open connection:
				connection.Open();

				// Read data: 
				SqlDataReader reader = command.ExecuteReader();
				if (reader.HasRows) {
					while (reader.Read()) {
                        // Create new SelectListItem object and add it to the list:
                        platforms.Add(new SelectListItem { Value = reader.GetInt32(0).ToString(), Text = reader.GetString(1) });
                        //platforms.Add(new PlatformModel {
                        //	PlatformID = reader.GetInt32(0),
                        //	Name = reader.GetString(1)
                        //});
                    }
				}
			}
			return platforms;
		}

		public List<int> SelectGameGenres(int gameID) {
			List<int> genreIDs = new List<int>();

			using (SqlConnection connection = new SqlConnection(connectionString)) {

				// SQL query that returns all game genres:
				string sqlQuery = @"SELECT genre_ID from game_genre WHERE game_ID = @gameID";

				SqlCommand command = new SqlCommand(sqlQuery, connection);
				command.Parameters.Add("@gameID", System.Data.SqlDbType.Int).Value = gameID;

				// Open connection:
				connection.Open();

				// Read data: 
				SqlDataReader reader = command.ExecuteReader();
				if (reader.HasRows) {
					while (reader.Read()) {
						genreIDs.Add(reader.GetInt32(0));
					}
				}
			}

			return genreIDs;
		}

		public List<int> SelectGamePlatforms(int gameID) {
			List<int> platformIDs = new List<int>();

			using (SqlConnection connection = new SqlConnection(connectionString)) {

				// SQL query that returns all game platforms:
				string sqlQuery = @"SELECT platform_ID from game_platform WHERE game_ID = @gameID";

				SqlCommand command = new SqlCommand(sqlQuery, connection);
				command.Parameters.Add("@gameID", System.Data.SqlDbType.Int).Value = gameID;

				// Open connection:
				connection.Open();

				// Read data: 
				SqlDataReader reader = command.ExecuteReader();
				if (reader.HasRows) {
					while (reader.Read()) {
						platformIDs.Add(reader.GetInt32(0));
					}
				}
			}

			return platformIDs;
		}

		public void Create(GameModel model) {
			// Insert title, developer & releaseDate to the 'game' table:
			string sqlQuery1 = @"INSERT INTO game(title, developer, release_date) 
									VALUES (@title, @developer, @release_date)";

			// Get the game_id of newly created entry:
			string sqlQuery2 = @"SELECT game_id FROM game WHERE title = @title";

			// Insert each selected genre to the 'game_genre' table using game_id:
			string sqlQuery3 = @"INSERT INTO game_genre (game_ID, genre_ID) VALUES (@game_ID, @genre_ID)";

			// Insert each selected platform to the 'game_platform' table using game_id:
			string sqlQuery4 = @"INSERT INTO game_platform (game_ID, platform_ID) VALUES (@game_ID, @platform_ID)";

			using (SqlConnection connection = new SqlConnection(connectionString)) {
				// Open connection:
				connection.Open();

				// 1st query:
				using (SqlCommand command = new SqlCommand(sqlQuery1, connection)) {
					command.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 50).Value = model.Title;
					command.Parameters.Add("@developer", System.Data.SqlDbType.VarChar, 50).Value = model.Developer;
					command.Parameters.Add("@release_date", System.Data.SqlDbType.Date).Value = model.ReleaseDate.ToShortDateString();
					command.ExecuteNonQuery();
				}

				// 2nd query:
				using (SqlCommand command = new SqlCommand(sqlQuery2, connection)) {
					command.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 50).Value = model.Title;
					SqlDataReader reader = command.ExecuteReader();
					if (reader.HasRows) {
						while (reader.Read()) {
							model.GameID = reader.GetInt32(0);
						}
					}
				}

				// 3rd query:
				using (SqlCommand command = new SqlCommand(sqlQuery3, connection)) {
					command.Parameters.Add("@game_ID", System.Data.SqlDbType.Int).Value = model.GameID;
					command.Parameters.Add("@genre_ID", System.Data.SqlDbType.Int);

					foreach (int genreID in model.SelectedGenres.ToArray<int>()) {
						command.Parameters["@genre_ID"].Value = genreID;
						command.ExecuteNonQuery();
					}
				}

				// 4th query:
				using (SqlCommand command = new SqlCommand(sqlQuery4, connection)) {
					command.Parameters.Add("@game_ID", System.Data.SqlDbType.Int).Value = model.GameID;
					command.Parameters.Add("@platform_ID", System.Data.SqlDbType.Int);

					foreach (int platformID in model.SelectedPlatforms.ToArray<int>()) {
						command.Parameters["@platform_ID"].Value = platformID;
						command.ExecuteNonQuery();
					}
				}
			}
		}

		public void Update(GameModel model) {
			// Update game table:
			string sqlQuery1 = @"UPDATE game SET title = @title, developer = @developer, release_date = @releaseDate
								WHERE game_ID = @gameID";

			// Delete old values from game_genre: 
			string sqlQuery2 = @"DELETE FROM game_genre WHERE game_ID = @gameID";

			// Add new values to game_genre:
			string sqlQuery3 = @"INSERT INTO game_genre (game_ID, genre_ID) VALUES (@gameID, @genreID)";

			// Delete old values from game_genre: 
			string sqlQuery4 = @"DELETE FROM game_platform WHERE game_ID = @gameID";

			// Add new values to game_genre:
			string sqlQuery5 = @"INSERT INTO game_platform (game_ID, platform_ID) VALUES (@gameID, @platformID)";

			using (SqlConnection connection = new SqlConnection(connectionString)) {
				// Open connection:
				connection.Open();

				// 1st query:
				using (SqlCommand command = new SqlCommand(sqlQuery1, connection)) {
					command.Parameters.Add("@gameID", System.Data.SqlDbType.Int).Value = model.GameID;
					command.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 50).Value = model.Title;
					command.Parameters.Add("@developer", System.Data.SqlDbType.VarChar, 50).Value = model.Developer;
					command.Parameters.Add("@releaseDate", System.Data.SqlDbType.Date).Value = model.ReleaseDate.ToShortDateString();
					command.ExecuteNonQuery();
				}

				// 2nd query:
				using (SqlCommand command = new SqlCommand(sqlQuery2, connection)) {
					command.Parameters.Add("@gameID", System.Data.SqlDbType.Int).Value = model.GameID;
					command.ExecuteNonQuery();
				}

				// 3rd query:
				using (SqlCommand command = new SqlCommand(sqlQuery3, connection)) {
					foreach (int genreID in model.SelectedGenres) {
						command.Parameters.Clear();
						command.Parameters.Add("@gameID", System.Data.SqlDbType.Int).Value = model.GameID;
						command.Parameters.Add("@genreID", System.Data.SqlDbType.Int).Value = genreID;
						command.ExecuteNonQuery();
					}
				}

				// 4nd query:
				using (SqlCommand command = new SqlCommand(sqlQuery4, connection)) {
					command.Parameters.Add("@gameID", System.Data.SqlDbType.Int).Value = model.GameID;
					command.ExecuteNonQuery();
				}

				// 5nd query:
				using (SqlCommand command = new SqlCommand(sqlQuery5, connection)) {
					foreach (int platformID in model.SelectedPlatforms) {
						command.Parameters.Clear();
						command.Parameters.Add("@gameID", System.Data.SqlDbType.Int).Value = model.GameID;
						command.Parameters.Add("@platformID", System.Data.SqlDbType.Int).Value = platformID;
						command.ExecuteNonQuery();
					}
				}
			}
		}

		public void Delete(int gameID) {
			// Delete all game entries from 'game_genre' table:
			string sqlQuery1 = @"DELETE FROM game_genre WHERE game_ID = @gameID";

			// Delete all game entries from 'game_platform' table:
			string sqlQuery2 = @"DELETE FROM game_platform WHERE game_ID = @gameID";

			// Delete the game from 'game' table:
			string sqlQuery3 = @"DELETE FROM game WHERE game_ID = @gameID";

			using (SqlConnection connection = new SqlConnection(connectionString)) {
				// Open connection:
				connection.Open();

				// 1st query:
				using (SqlCommand command = new SqlCommand(sqlQuery1, connection)) {
					command.Parameters.Add("@gameID", System.Data.SqlDbType.Int).Value = gameID;
					command.ExecuteNonQuery();
				}

				// 2nd query:
				using (SqlCommand command = new SqlCommand(sqlQuery2, connection)) {
					command.Parameters.Add("@gameID", System.Data.SqlDbType.Int).Value = gameID;
					command.ExecuteNonQuery();
				}

				// 3rd query:
				using (SqlCommand command = new SqlCommand(sqlQuery3, connection)) {
					command.Parameters.Add("@gameID", System.Data.SqlDbType.Int).Value = gameID;
					command.ExecuteNonQuery();
				}
			}
		}

		public bool CheckIfExists(string gameTitle) {
			using (SqlConnection connection = new SqlConnection(connectionString)) {
				// SQL query that returns an entry from the database based on title:
				string sqlQuery = @"SELECT title FROM game WHERE title = @title";

				SqlCommand command = new SqlCommand(sqlQuery, connection);

				command.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 50).Value = gameTitle;

				// Open connection:
				connection.Open();

				try {
					SqlDataReader reader = command.ExecuteReader();
					if (reader.HasRows) {
						// Game already exists in database:
						return true;
					}
				}
				catch (NullReferenceException) {
					// Nothing was found:
				}
			}
			return false;
		}

		public bool CheckIfEmptyForm(GameModel model) {
			// When trying to add to the database, make sure none of the values are empty/unassigned:
			if (model.Title == "" || model.Developer == "" || model.ReleaseDate == DateTime.MinValue || !model.SelectedGenres.Any() || !model.SelectedPlatforms.Any()) {
				return false;
            }
			else return true;
        }
    }
}