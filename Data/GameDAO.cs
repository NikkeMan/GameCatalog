using GameCatalog.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GameCatalog.Data {
    public class GameDAO {

		//Connection string for a local database named games_db: 
        private readonly string connectionString = "Data Source=.;Initial Catalog=games_db;Integrated Security=True";

        public List<GameModel> SelectAll() {
            List<GameModel> returnList = new List<GameModel>();

			using(SqlConnection connection = new SqlConnection(connectionString)) {

				// SQL query that selects all the data from the database:
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
						ON game.game_ID = p.game_ID";

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
                            Genres = reader.GetString(4),
                            Platforms = reader.GetString(5)
                        };

                        // Add the object to the return list:
                        returnList.Add(game);
                    }
                }
			}
            return returnList;
        }

		public GameModel SelectOne(int game_ID) {
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

				command.Parameters.Add("@game_ID", System.Data.SqlDbType.Int).Value = game_ID;

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
						gameModel.Genres = reader.GetString(4);
						gameModel.Platforms = reader.GetString(5);
					}
				}
			}
			return gameModel;
		}

		public List<GenreModel> SelectGenres() {
			List<GenreModel> genres = new List<GenreModel>();

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
						// Create new GenreModel object and add it to the list::
						GenreModel genre = new GenreModel {
							GenreID = reader.GetInt32(0),
							Name = reader.GetString(1)
						};
						genres.Add(genre);
					}
				}
			}
			return genres;
        }

		public List<PlatformModel> SelectPlatforms() {
			List<PlatformModel> platforms = new List<PlatformModel>();

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
						// Create new PlatformModel object and add it to the list:
						PlatformModel platform = new PlatformModel {
							PlatformID = reader.GetInt32(0),
							Name = reader.GetString(1)
						};
						platforms.Add(platform);
					}
				}
			}
			return platforms;
		}
	}
}