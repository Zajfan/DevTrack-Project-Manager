﻿// ProjectMapperHelpers.cs
using DevTrack.DAL.Models;

internal static class ProjectMapperHelpers
{
    public static Project MapFromReader(MySqlDataReader reader)
    {
        return new Project
        {
            ProjectID = reader.GetInt32("ProjectID"),
            ProjectName = reader.GetString("ProjectName"),
            // ... map other properties
        };
    }
}