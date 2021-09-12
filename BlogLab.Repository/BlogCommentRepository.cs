using BlogLabModels.BlogComment;
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
    public class BlogCommentRepository : IBlogCommentRepository
    {

        private readonly IConfiguration _config;

        public BlogCommentRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task<int> DeleteAsync(int blogCommentId)
        {

            int affectedRows = 0;
            //Open a  connection 
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {


                // Execution of stored procedure that allows to insert an account
                await connection.OpenAsync();

                affectedRows = await connection.ExecuteAsync(
                    "BlogComment_Delete",
                    new { BlogCommentId = blogCommentId },
                    commandType: CommandType.StoredProcedure);
            }

            return affectedRows;
        }

        public async Task<List<BlogComment>> GetAllAsync(int blogId)
        {
            IEnumerable<BlogComment> blogComments;

            //Open a  connection 
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {


                // Execution of stored procedure that allows to insert an account
                await connection.OpenAsync();

                blogComments = await connection.QueryAsync<BlogComment>(
                    "BlogComment_GetAll",
                    new { BlogId = blogId },
                    commandType: CommandType.StoredProcedure);
            }

            return blogComments.ToList();
        }

        public async Task<BlogComment> GetAsync(int blogCommentId)
        {

            BlogComment blogComment;
            //Open a  connection 
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {


                // Execution of stored procedure that allows to insert an account
                await connection.OpenAsync();

                blogComment = await connection.QueryFirstOrDefaultAsync<BlogComment>(
                    "BlogComment_Get",
                    new { BlogCommentId = blogCommentId },
                    commandType: CommandType.StoredProcedure);
            }

            return blogComment;
        }

        public async Task<BlogComment> UpsertAsynt(BlogCommentCreate blogCommentCreate, int applicationUserId)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("BlogCommentId", typeof(int));
            dataTable.Columns.Add("ParentBlogCommentId", typeof(int));
            dataTable.Columns.Add("BlogId", typeof(int));
            dataTable.Columns.Add("Content", typeof(string));



            dataTable.Rows.Add(blogCommentCreate.BlogCommentId, blogCommentCreate.ParentBlogCommentId, blogCommentCreate.BlogId, blogCommentCreate.Content);

            int? newBlogCommentId;


            //Open a  connection 
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {


                // Execution of stored procedure that allows to insert an account
                await connection.OpenAsync();

                newBlogCommentId = await connection.ExecuteAsync(
                    "BlogComment_Upsert",
                    new { BlogComment = dataTable.AsTableValuedParameter("dbo.BlogCommentType") },
                    commandType: CommandType.StoredProcedure);
            }

            newBlogCommentId = newBlogCommentId ?? blogCommentCreate.BlogCommentId;

            BlogComment blogCommment = await GetAsync(newBlogCommentId.Value);

            return blogCommment;
        }
    }
}
