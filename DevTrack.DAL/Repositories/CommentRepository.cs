using DevTrack.DAL.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevTrack.DAL.Repositories
{
    public class CommentRepository : BaseRepository
    {
        private readonly CommentMapper commentMapper = new CommentMapper();

        public CommentRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            var comments = new List<Comment>();

            try
            {
                using (var connection = connectionFactory.CreateConnection())
                {
                    string query = "SELECT * FROM comments";
                    using var command = new MySqlCommand(query, connection);

                    await connection.OpenAsync();
                    using var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        comments.Add(commentMapper.MapFromReader(reader));
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error getting all comments: {ex.Message}");
                return new List<Comment>();
            }

            return comments;
        }

        public async Task CreateCommentAsync(Comment comment)
        {
            try
            {
                using (var connection = connectionFactory.CreateConnection())
                {
                    string query = "INSERT INTO comments (ProjectID, TaskID, UserID, CommentText, CommentDate) " +
                                   "VALUES (@ProjectID, @TaskID, @UserID, @CommentText, @CommentDate)";
                    using var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProjectID", comment.ProjectID);
                    command.Parameters.AddWithValue("@TaskID", comment.TaskID);
                    command.Parameters.AddWithValue("@UserID", comment.UserID);
                    command.Parameters.AddWithValue("@CommentText", comment.CommentText);
                    command.Parameters.AddWithValue("@CommentDate", comment.CommentDate);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error creating comment: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            try
            {
                using (var connection = connectionFactory.CreateConnection())
                {
                    string query = "UPDATE comments SET ProjectID = @ProjectID, TaskID = @TaskID, UserID = @UserID, " +
                                   "CommentText = @CommentText, CommentDate = @CommentDate " +
                                   "WHERE CommentID = @CommentID";
                    using var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProjectID", comment.ProjectID);
                    command.Parameters.AddWithValue("@TaskID", comment.TaskID);
                    command.Parameters.AddWithValue("@UserID", comment.UserID);
                    command.Parameters.AddWithValue("@CommentText", comment.CommentText);
                    command.Parameters.AddWithValue("@CommentDate", comment.CommentDate);
                    command.Parameters.AddWithValue("@CommentID", comment.CommentID);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error updating comment: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            try
            {
                using (var connection = connectionFactory.CreateConnection())
                {
                    string query = "DELETE FROM comments WHERE CommentID = @CommentID";
                    using var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CommentID", commentId);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error deleting comment: {ex.Message}");
                throw;
            }
        }
    }
}