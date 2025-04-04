using QLCoffee.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

public class KhoRepository : IKhoRepository
{
    private readonly QuanLyQuanCoffeeEntities _context;

    public KhoRepository(QuanLyQuanCoffeeEntities context)
    {
        _context = context;
    }

    public IEnumerable<KHO> GetAll()
    {
        return _context.KHOes.Include(k => k.NHACUNGCAP).Include(k => k.SANPHAM).ToList();
    }

    public KHO GetById(int id)
    {
        return _context.KHOes.Find(id);
    }

    public void Add(KHO kho)
    {
        _context.KHOes.Add(kho);
        _context.SaveChanges();
    }

    public void Update(KHO kho)
    {
        _context.Entry(kho).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var kho = _context.KHOes.Find(id);
        if (kho != null)
        {
            _context.KHOes.Remove(kho);
            _context.SaveChanges();
        }
    }
}
