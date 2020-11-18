using Project_Parts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Parts.DAL
{
    public interface IPartsRepositories
    {
        bool Insert(CarParts[] parts);
    }
    public class PartsRepositories : IPartsRepositories
    {
        public CarDbContext db = null;
        public PartsRepositories(CarDbContext db) { this.db = db; }
        public bool Insert(CarParts[] parts)
        {
            db.CarParts.AddRange(parts);
            return db.SaveChanges()>0;
        }
    }
}
