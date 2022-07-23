using System;
using System.Collections.Generic;
using System.Text;

namespace LMS_V2.Shared.Helpers
{
    public static class ConnectionStringHelpers
    {
        public static string GetHerokuConnectionString()
        {
            //string connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            string connectionUrl = "postgres://swikjogljbafpk:659cfc26d22de03c94248ddc3b6293f15481ab11580baf72823d33aa5ae80242@ec2-52-30-159-47.eu-west-1.compute.amazonaws.com:5432/d17gep0hdnjjhm";

            var databaseUri = new Uri(connectionUrl);

            string db = databaseUri.LocalPath.TrimStart('/');
            string[] userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);

            return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
        }
    }
}
