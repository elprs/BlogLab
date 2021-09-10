using BlogLabModels.Photo;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogLab.Repository
{
    public class PhotoRepository : IPhotoRepository
    {

        private readonly IConfiguration _config;
        public PhotoRepository(IConfiguration config )
        {
            _config = config;
        }
        public async Task<int> DeleteAsync(int photoId)
        {
            int affectedRows = 0;
            //Open a  connection 
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {


                // Execution of stored procedure that allows to insert an account
                await connection.OpenAsync();

                affectedRows = await connection.ExecuteAsync(
                    "Photo_Delete", 
                    new { PhotoId = photoId }, 
                    commandType: CommandType.StoredProcedure);
            }

            return affectedRows; 
        }

        public async Task<List<Photo>> GetAllByUserIdAsync(int applicationUserId)
        {
            IEnumerable<Photo> photos;

            //Open a  connection 
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {


                // Execution of stored procedure that allows to insert an account
                await connection.OpenAsync();

                photos = await connection.QueryAsync<Photo>(
                    "Photo_GetByUserId",
                    new { applicationUserId = applicationUserId },
                    commandType: CommandType.StoredProcedure);
            }

            return photos.ToList();
        }

        public async Task<Photo> GetAsync(int photoId)
        {

            Photo photo; 
            //Open a  connection 
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {


                // Execution of stored procedure that allows to insert an account
                await connection.OpenAsync();

                photo = await connection.QueryFirstOrDefaultAsync<Photo>(
                    "Photo_Get",
                    new { PhotoId = photoId },
                    commandType: CommandType.StoredProcedure);
            }

            return photo; 
        }

        public async Task<Photo> InsertAsync(PhotoCreate photoCreate, int applicationUserId)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("PublicId", typeof(string));
            dataTable.Columns.Add("ImageUrl", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));

            dataTable.Rows.Add(photoCreate.PublicId, photoCreate.ImageUrl, photoCreate.Description);

            int newPhotoId;


            //Open a  connection 
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {


                // Execution of stored procedure that allows to insert an account
                await connection.OpenAsync();

                newPhotoId = await connection.ExecuteAsync(
                    "Photo_Delete",
                    new { Photo = dataTable.AsTableValuedParameter("dbo.PhotoType") },
                    commandType: CommandType.StoredProcedure);
            }

            Photo photo = await GetAsync(newPhotoId);

            return photo; 
        }
    }
}
