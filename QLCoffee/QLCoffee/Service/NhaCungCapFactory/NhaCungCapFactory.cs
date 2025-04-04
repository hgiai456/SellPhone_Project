using QLCoffee.Models;

public class NhaCungCapFactory
{
    public static NHACUNGCAP Create(string maNCC, string tenNCC, string diaChi, string soDT)
    {
        return new NHACUNGCAP
        {
            MaNCC = maNCC,
            TenNCC = tenNCC,
            DiaChi = diaChi,
            SoDT = soDT
        };
    }
}
