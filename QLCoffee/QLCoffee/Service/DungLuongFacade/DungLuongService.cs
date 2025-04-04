using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCoffee.Service.DungLuongFacade
{
    public class DungLuongService
    {
        private readonly QuanLyQuanCoffeeEntities _db;

        public DungLuongService()
        {
            _db = new QuanLyQuanCoffeeEntities();
        }

        public List<DUNGLUONG> GetAll()
        {
            return _db.DUNGLUONGs.ToList();
        }

        public DUNGLUONG GetById(int id)
        {
            return _db.DUNGLUONGs.Find(id);
        }

        public bool Create(DUNGLUONG dUNGLUONG)
        {
            if (dUNGLUONG == null) return false;
            _db.DUNGLUONGs.Add(dUNGLUONG);
            _db.SaveChanges();
            return true;
        }

        public bool Update(DUNGLUONG dUNGLUONG)
        {
            if (dUNGLUONG == null) return false;
            _db.Entry(dUNGLUONG).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var dUNGLUONG = _db.DUNGLUONGs.Find(id);
            if (dUNGLUONG == null) return false;
            _db.DUNGLUONGs.Remove(dUNGLUONG);
            _db.SaveChanges();
            return true;
        }
    }
}
