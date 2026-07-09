using Backend_session9;
using Backend_session9.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_session9
{
    public class Program
    {
        // Static context — EF Core manages the DB connection automatically
        public static EcommerceContext context = new EcommerceContext();


        static void Main(string[] args)
        { }
    }
}
