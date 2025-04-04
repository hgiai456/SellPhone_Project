using QLCoffee.Models;
using System.Collections.Generic;

public interface IKhoRepository
{
    IEnumerable<KHO> GetAll();
    KHO GetById(int id);
    void Add(KHO kho);
    void Update(KHO kho);
    void Delete(int id);
}
