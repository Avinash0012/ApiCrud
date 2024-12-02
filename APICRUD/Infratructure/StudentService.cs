using APICRUD.Infratructure.Interface;
using APICRUD.Entities_;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace APICRUD.Infratructure
{
    public class StudentService : IStudent
    {
        private readonly string ConnectionString;
        public StudentService(ConnectionHelper connectionHelper)
        {

            ConnectionString = connectionHelper.Default;
        }
 
        public async Task<int> Delete(int id)
        {
            var query = "Delete  from Student where Id = @Id";
            var param = new
            {
                id = id,
            };
            using (var con = new SqlConnection(ConnectionString))
            {
                con.Open();
               
                var i =await con.ExecuteAsync(query, param);
                return i;
            }
        }

        public async Task<Student> Edit(int id)
        {
            var query = "Select * from Student where Id = @Id";
            using (var con = new SqlConnection(ConnectionString))
            {
                con.Open();
                var i = await con.QueryFirstOrDefaultAsync<Student>(query, new { Id = id });
                return i;
            }
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
           
            using (var con = new SqlConnection(ConnectionString))
            {
                con.Open();
                var query = "Select * from Student";
                var list =await con.QueryAsync<Student>(query);
                return list.ToList();
            }
        }
        public async Task<int> Save(Student stu)
        {
            
            var query = "insert into Student (Name , Email,Class)values(@Name,@Email,@Class)";
            if (stu.Id > 0)
            {
                query = "Update Student Set Name= @Name, Email= @Email, Class=@Class where Id = @Id";
            }
            var parameter = new
            {
                stu.Id,
                stu.Name,
                stu.Email,
                stu.Class,
            };
            using (var con = new SqlConnection(ConnectionString))
            {
                var i = await con.ExecuteAsync(query,parameter,commandType:CommandType.Text);
                return i;
            }
        }
    }
}
