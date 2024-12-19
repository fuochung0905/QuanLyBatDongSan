 public class Employee
{
    public string TenNhanVien { get; set; }
    public string FirstName { get; set; }
    public double Tongluong { get; set; }
    // viết quản lí nhân viên cho gồm có : dev, tester => lương  riêng, nhân viên thời vụ => không có phương thức tính lương
    public double TinhLuong ()
    {
        return Tongluong;
    }
}
public class dev : Employee
{
    
}
public class tester (): Employee
{

}