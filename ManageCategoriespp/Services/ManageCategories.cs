using ManageCategoriesApp.Data.EF;
using ManageCategoriesApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCategoriesApp.Services
{
    public sealed class ManageCategories
    {
        //Using Singleton Pattern
        private static ManageCategories instance = null;
        private static readonly object instanceLock = new object();
        private ManageCategories() { }
        public static ManageCategories Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ManageCategories();
                    }
                    return instance;
                }
            }
        }

        //----------------------------------------------------
        public List<Category> GetCategories()
        {
            List<Category> categories;
            try
            {
                using MyStockDBContext stock = new MyStockDBContext();
                categories = stock.Categories.ToList();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return categories;
        }

        public void InsertCategory(Category category)
        {
            try
            {
                using MyStockDBContext stock = new MyStockDBContext();
                stock.Categories.Add(category);
                stock.SaveChanges();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateCategory(Category category)
        {
            using MyStockDBContext stock = new MyStockDBContext();

            var existingCategory = stock.Categories.Find(category.CategoryID);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
                stock.SaveChanges();
            }
            else
            {
                throw new Exception("Category not found");
            }
        }


        public void DeleteCategory(Category category)
        {
            using MyStockDBContext stock = new MyStockDBContext();

            var existingCategory = stock.Categories.Find(category.CategoryID);
            if (existingCategory != null)
            {
                stock.Categories.Remove(existingCategory);
                stock.SaveChanges();
            }
            else
            {
                throw new Exception("Category not found");
            }
        }



    }
}