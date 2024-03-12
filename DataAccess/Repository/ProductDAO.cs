using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Product> GetProductList()
        {
            var members = new List<Product>();
            try
            {
                using var context = new SaleManagementContext();
                members = context.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return members;
        }
        public Product GetProductByID(int ProductID)
        {
            Product mem = null;
            try
            {
                using var context = new SaleManagementContext();
                mem = context.Products.SingleOrDefault(c => c.ProductId == ProductID);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return mem;
        }
        public List<Product> Filter(String name, string unitprice, string unitinstock, string id)
        {
            List<Product> mem = new List<Product>();
            try
            {
                using var context = new SaleManagementContext();
                var members = context.Products.ToList();
                if (!String.IsNullOrWhiteSpace(name))
                {
                    foreach(var x in members)
                    {
                        if (x.ProductName.Contains(name))
                        {
                            mem.Add(x);
                        }
                    }
                }
                if (!String.IsNullOrWhiteSpace(unitprice))
                {
                    foreach (var x in members)
                    {
                        if (x.UnitPrice == decimal.Parse(unitprice))
                        {
                            mem.Add(x);
                        }
                    }
                }
                if (!String.IsNullOrWhiteSpace(unitinstock))
                {
                    foreach (var x in members)
                    {
                        if (x.UnitsInStock == decimal.Parse(unitinstock))
                        {
                            mem.Add(x);
                        }
                    }
                }
                if (!String.IsNullOrWhiteSpace(id))
                {
                    mem.Add(GetProductByID(int.Parse(id)));
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }
        public List<Product> GetProductByName(String name)
        {
            List<Product> mem = new List<Product>();
            try
            {
                using var context = new SaleManagementContext();
                var members = context.Products.ToList();
                foreach(var x in members) 
                { 
                    if (x.ProductName.Contains(name))
                    {
                        mem.Add(x);
                    }
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }
        public List<Product> GetProductByUnitPrice(decimal param)
        {
            List<Product> mem = new List<Product>();
            try
            {
                using var context = new SaleManagementContext();
                var members = context.Products.ToList();
                foreach (var x in members)
                {
                    if (x.UnitPrice == param)
                    {
                        mem.Add(x);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }
        public List<Product> GetProductByUnitInStock(int param)
        {
            List<Product> mem = new List<Product>();
            try
            {
                using var context = new SaleManagementContext();
                var members = context.Products.ToList();
                foreach (var x in members)
                {
                    if (x.UnitsInStock == param)
                    {
                        mem.Add(x);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }
        public void AddNew(Product Product)
        {
            try
            {
                Product mem = GetProductByID(Product.ProductId);
                if (mem == null)
                {
                    using var context = new SaleManagementContext();
                    context.Products.Add(Product);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The product is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Product Product)
        {
            try
            {
                Product mem = GetProductByID(Product.ProductId);
                if(mem != null)
                {
                    using var context = new SaleManagementContext();
                    context.Products.Update(Product);
                    context.SaveChanges();
                }else { throw new Exception("The product does not already exist."); }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Remove(int ProductId)
        {
            try
            {
                Product mem = GetProductByID(ProductId);
                if( mem != null)
                {
                    using var context = new SaleManagementContext();
                    context.Products.Remove(mem);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The product does not already exist.");
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Product> Filter(int a, int b)
        {
            var members = new List<Product>();
            var fil = new List<Product>();
            try
            {
                using var context = new SaleManagementContext();
                members = context.Products.ToList();
                for(int i = 0; i < members.Count(); i++)
                {
                    if ((members[i].UnitPrice >= a && members[i].UnitPrice <=b) || (members[i].UnitsInStock>=a && members[i].UnitsInStock <= b))
                    {
                        fil.Add(members[i]);
                    }
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return fil;
        }
    }
}
