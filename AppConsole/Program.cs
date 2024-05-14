using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data.Common;

using Configuration;
using Models;
using DbContext;

namespace AppConsole
{
    static class MyLinqExtensions
    {
        public static void Print<T>(this IEnumerable<T> collection)
        {
            collection.ToList().ForEach(item => Console.WriteLine(item));
        }
    }


    class Program
    {
        const int nrItemsSeed = 1000;
        static void Main(string[] args)
        {
            #region run below to test the model only

            Console.WriteLine($"\nSeeding the Model...");
            var _modelList = SeedModel(nrItemsSeed);

            Console.WriteLine($"\nTesting Model...");
            WriteModel(_modelList);
            #endregion


            #region  run below only when Database i created
            Console.WriteLine($"Database type: {csAppConfig.DbSetActive.DbLocation}");
            Console.WriteLine($"Database type: {csAppConfig.DbSetActive.DbServer}");
            Console.WriteLine($"Connection used: {csAppConfig.DbSetActive.DbConnection}");
            Console.WriteLine($"Connection used: {csAppConfig.DbSetActive.DbConnectionString}");
  
            Console.WriteLine($"\nSeeding database...");
            try
            {
                SeedDataBase(_modelList).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: Database could not be seeded. Ensure the database is correctly created");
                Console.WriteLine($"\nError: {ex.Message}");
                Console.WriteLine($"\nError: {ex.InnerException.Message}");
                return;
            }

            Console.WriteLine("\nQuery database...");
            QueryDatabaseAsync().Wait();

            MoreQueryDatabaseAsync().Wait();
            #endregion
       }


        #region Replaced by new model methods
        private static void WriteModel(List<csWine> _modelList)
        {
            Console.WriteLine($"Nr of wine bottles: {_modelList.Count()}");
        }

        private static List<csWine> SeedModel(int nrItems)
        {
            var _seeder = new csSeedGenerator();
            
            //Create a list of wines and winecellars
            var _wines = _seeder.ItemsToList<csWine>(nrItems);

            //roughly 100 bottles per winecellar
            var _wineCellars = _seeder.ItemsToList<csWineCellar>((int) Math.Ceiling((nrItems/100D)));

            //allocate wines to winecellars
            foreach(var _wine in _wines)
            {
                _wine.WineCellar = _seeder.FromList(_wineCellars);
            }

            return _wines;
        }
        #endregion

        #region Update to reflect you new Model
        private static async Task SeedDataBase(List<csWine> _modelList)
        {
            using (var db = csMainDbContext.DbContext())
            {
                #region move the seeded model into the database using EFC
                foreach (var _m in _modelList)
                {
                    db.Wines.Add(_m);
                }
                #endregion

                await db.SaveChangesAsync();
            }
        }

        private static async Task QueryDatabaseAsync()
        {
            Console.WriteLine("--------------");
            using (var db = csMainDbContext.DbContext())
            {
                #region Reading the database using EFC
                var _modelList = await db.Wines.ToListAsync();
                #endregion

                WriteModel(_modelList);
            }
        }


        private static async Task MoreQueryDatabaseAsync()
        {
            Console.WriteLine("------More Query --------");
            using (var db = csMainDbContext.DbContext())
            {
                Console.WriteLine($"5 most expensive wines");
                var list = await db.Wines.OrderByDescending(w => w.Price).Take(5).ToListAsync();
                foreach (var _m in list)
                {
                    Console.WriteLine($"{_m}");
                }

                var _wcc = await db.WineCellars.CountAsync();
                var _wc = await db.Wines.CountAsync();
                Console.WriteLine($"Database contains {_wcc} wine cellars and {_wc} wines");

                Console.WriteLine($"Top 5 cellars");
                var _cellars = await db.WineCellars.Take(5).ToListAsync();
                foreach (var _m in _cellars)
                {
                    Console.WriteLine($"{_m}");
                }
            }
        }
        #endregion
    }
}
