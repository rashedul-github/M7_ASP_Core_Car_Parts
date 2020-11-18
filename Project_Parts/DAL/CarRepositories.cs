using Project_Parts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Project_Parts.DAL
{
    public interface ICarRepositories
    {
        List<Car> GetAll();
        bool Insert(Car c);
        bool Update(Car car, bool childIncluded = false);
        bool Delete(int id);
        bool InsertParts(CarParts p);
        Car Get(int id);
        Car GetById(int id);
    }
    public class CarRepositories : ICarRepositories
    {
        CarDbContext db = null;
        public CarRepositories(CarDbContext db) { this.db = db; }

        public bool Delete(int id)
        {
            Car c = new Car { CarId = id };
            db.Entry(c).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }

        public Car Get(int id)
        {
            return db.Cars
                .Include(g => g.CarParts)
                .FirstOrDefault(s => s.CarId == id);
        }

        public List<Car> GetAll()
        {
            return db.Cars.Include(x => x.CarParts).ToList();
        }

        public Car GetById(int id)
        {
            return db.Cars.FirstOrDefault(x => x.CarId == id);
        }

        public bool Insert(Car c)
        {
            db.Cars.Add(c);
            return db.SaveChanges() > 0;
        }

        public bool InsertParts(CarParts p)
        {
            db.CarParts.Add(p);
            return db.SaveChanges() > 0;
        }

        public bool Update(Car car, bool childIncluded = false)
        {
            var org = db.Cars.Include(x => x.CarParts).First(x => x.CarId == car.CarId);
            org.CarModel = car.CarModel;
            org.Type = car.Type;
            org.Price = car.Price;
            org.IsAvailable = car.IsAvailable;
            if (car.CarParts != null && car.CarParts.Count > 0)
            {
                var parts = car.CarParts.ToArray();
                for (var i= 0; i < parts.Length; i++)
                {
                    var temp = org.CarParts.FirstOrDefault(y => y.CarPartsId == parts[i].CarPartsId);
                    if(temp != null)
                    {
                        temp.PartsName = parts[i].PartsName;
                        temp.Stock = parts[i].Stock;
                        temp.Price = parts[i].Price;
                    }
                    else
                    {
                        org.CarParts.Add(parts[i]);
                    }
                }
                foreach(var p in org.CarParts)
                {
                    var tmp = car.CarParts.FirstOrDefault(j => j.CarPartsId == p.CarPartsId);
                    if (tmp == null)
                        db.Entry(p).State = EntityState.Deleted;
                }
            }
             return db.SaveChanges() > 0;
        }
    }
}
