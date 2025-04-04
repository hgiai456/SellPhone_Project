using System;
using QLCoffee.Models;

public sealed class DatabaseSingleton
{
    // Lazy đảm bảo chỉ tạo instance khi cần
    private static readonly Lazy<QuanLyQuanCoffeeEntities> instance =
        new Lazy<QuanLyQuanCoffeeEntities>(() => new QuanLyQuanCoffeeEntities());

    // Constructor private để không ai có thể khởi tạo từ ngoài
    private DatabaseSingleton() { }

    // Property để truy cập instance duy nhất
    public static QuanLyQuanCoffeeEntities Instance => instance.Value;
}