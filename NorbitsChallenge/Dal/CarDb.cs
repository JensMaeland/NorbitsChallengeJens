using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Bll;

namespace NorbitsChallenge.Dal
{
    public class CarDb
    {
        private readonly IConfiguration _config;

        public CarDb(IConfiguration config)
        {
            _config = config;
        }

        public int UpdateCar(string licensePlate, string OldLicensePlate, string model, string brand, 
             string description, int tireCount, int companyId)
        {
            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    {
                        command.CommandText = $"update dbo.Car set LicensePlate = '{licensePlate}', Description = '{description}', Model = '{model}', " +
                        $"Brand = '{brand}', TireCount = {tireCount}, CompanyId = {companyId} where LicensePlate like '{OldLicensePlate}'";

                    }

                    return command.ExecuteNonQuery();

                }
            }
        }


        public int CreateCar(int companyId, string licensePlate, string model, string brand, int tireCount, string description = "")
        {

            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"insert into dbo.Car (LicensePlate, Description, Model, Brand, TireCount, CompanyId)" +
                        $"values('{licensePlate}', '{description}', '{model}', '{brand}', {tireCount}, {companyId})";

                    return command.ExecuteNonQuery();
                }
            }
        }

        public int DeleteCar(string licensePlate, int companyId)
        {

            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"delete from dbo.Car where licensePlate = '{licensePlate}' and companyId = {companyId}";

                    return command.ExecuteNonQuery();
                }
            }
        }

        public Car GetCarByLicensePlate(int companyId, string licensePlate)
        {
            var connectionString = _config.GetSection("ConnectionString").Value;
            var car = new Car();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"select * from car where LicensePlate like '%{licensePlate}%' and companyId = {companyId}";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            car.LicensePlate = (string)reader["LicensePlate"];
                            car.Description = (string)reader["Description"];
                            car.Model = (string)reader["Model"];
                            car.Brand = (string)reader["Brand"];
                            car.TireCount = (int)reader["TireCount"];
                            
                            return car;
                        }
                    }
                }
            }
            return car;
        }

        public List<Car> GetAllCarObjects(int companyId)
        {
            var allCars = new List<Car>();

            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"select * from car where companyId = {companyId}";
                    //command.CommandText = $"select * from dbo.car where companyId = {companyId}";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var currentCar = new Car();
                            currentCar.LicensePlate = (string)reader["LicensePlate"];
                            currentCar.Description = (string)reader["Description"];
                            currentCar.Model = (string)reader["Model"];
                            currentCar.Brand = (string)reader["Brand"];
                            currentCar.TireCount = (int)reader["TireCount"];

                            allCars.Add(currentCar);
                        }
                    }
                }
            }

            return allCars;
        }

        public int GetTireCount(int companyId, string licensePlate)
        {
            int result = 0;

            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand {Connection = connection, CommandType = CommandType.Text})
                {
                    command.CommandText = $"select * from car where companyId = {companyId} and licenseplate = '{licensePlate}'";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = (int) reader["tireCount"];
                        }
                    }
                }
            }

            return result;
        }
    }
}
