// ReportingRepository.cs
using DevTrack.DAL.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevTrack.DAL.Repositories
{
    public class ReportingRepository : BaseRepository
    {
        public ReportingRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        // Example: Get a report of project progress
        public async Task<List<ProjectProgressReport>> GetProjectProgressReportAsync()
        {
            // ... (Existing code for GetProjectProgressReportAsync) ...
        }

        // Example: Get a report of user workload
        public async Task<List<UserWorkloadReport>> GetUserWorkloadReportAsync()
        {
            var reports = new List<UserWorkloadReport>();

            try
            {
                using (var connection = connectionFactory.CreateConnection())
                {
                    // Construct your SQL query for the report
                    string query = @"
                        SELECT u.UserName, COUNT(t.TaskID) AS TotalTasks
                        FROM users u
                        LEFT JOIN tasks t ON u.UserID = t.AssignedTo
                        GROUP BY u.UserID, u.UserName";

                    using var command = new MySqlCommand(query, connection);

                    await connection.OpenAsync();
                    using var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        reports.Add(new UserWorkloadReport
                        {
                            UserName = reader.GetString("UserName"),
                            TotalTasks = reader.GetInt32("TotalTasks")
                        });
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error generating user workload report: {ex.Message}");
                return new List<UserWorkloadReport>();
            }

            return reports;
        }

        // Example: Get a report of overdue tasks
        public async Task<List<OverdueTaskReport>> GetOverdueTasksReportAsync()
        {
            var reports = new List<OverdueTaskReport>();

            try
            {
                using (var connection = connectionFactory.CreateConnection())
                {
                    // Construct your SQL query for the report
                    string query = @"
                        SELECT t.TaskName, t.DueDate, p.ProjectName
                        FROM tasks t
                        JOIN projects p ON t.ProjectID = p.ProjectID
                        WHERE t.DueDate < CURDATE() AND t.Status <> 'Completed'";

                    using var command = new MySqlCommand(query, connection);

                    await connection.OpenAsync();
                    using var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        reports.Add(new OverdueTaskReport
                        {
                            TaskName = reader.GetString("TaskName"),
                            DueDate = reader.GetDateTime("DueDate"),
                            ProjectName = reader.GetString("ProjectName")
                        });
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error generating overdue tasks report: {ex.Message}");
                return new List<OverdueTaskReport>();
            }

            return reports;
        }

        // ... (Add other methods for different reports) ...
    }
}