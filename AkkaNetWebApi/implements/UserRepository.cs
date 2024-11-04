using AkkaNetWebApi.Data;
using AkkaNetWebApi.Interfaces;
using AkkaNetWebApi.Models;
using Dapper;
using System;

namespace AkkaNetWebApi.implements
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var sql = "SELECT Name,Email,Age  FROM Users";
            using (var connection = _context.CreateConnection())
            {

                var results= await connection.QueryAsync<User>(sql);

                return results.ToList();
            }            
        } 

        public async Task<User> GetByIdAsync(int id)
        {
            var sql = "SELECT Name,Email,Age  FROM Users where Id=@id";
            using (var connection = _context.CreateConnection())
            {

                var results = await connection.QueryFirstAsync<User>(sql, new { id });

                return results;
            }

        } 

        public async Task<User> AddAsync(User user)
        {
            var sql = "INSERT INTO Users(Name,Email,Age) VALUES(@Name,@Email,0)";
            using (var connection = _context.CreateConnection())
            {

                var results = await connection.ExecuteAsync(sql, user);

                return user;
            }
        }

        public async Task<User> UpdateAsync(User user)
        {
            var sql = "UPDATE Users SET Name=@Name,Email=@Email where Id=@id ";
            using (var connection = _context.CreateConnection())
            {

                var results = await connection.ExecuteAsync(sql, user);

                return user;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE from Users where Id=@id ";
            using (var connection = _context.CreateConnection())
            {
                var results = await connection.ExecuteAsync(sql, new { id });
            }

        }
    }
}
