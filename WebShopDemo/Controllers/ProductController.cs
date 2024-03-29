﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopDemo.Abstraction;
using WebShopDemo.Domain;
using WebShopDemo.Models.Brand;
using WebShopDemo.Models.Category;
using WebShopDemo.Models.Product;

namespace WebShopDemo.Controllers
{
    [Authorize(Roles="Administrator")]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;

        public ProductController(IProductService productService, ICategoryService categoryService, IBrandService brandService)
        {
            this.productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
        }

        [AllowAnonymous]
        public ActionResult Index(string searchStringCategoryName, string searchStringBrandName)
        {
            List<ProductIndexVM> products = productService.GetProducts(searchStringCategoryName, searchStringBrandName)
                .Select(product => new ProductIndexVM
                { 
                Id = product.Id,
                ProductName = product.ProductName,
                BrandId = product.BrandId,
                BrandName = product.Brand.BrandName,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.CategoryName,
                Picture = product.Picture,
                Quantity = product.Quantity,
                Price = product.Price,
                Discount = product.Discount,
                }).ToList();
            return this.View(products);
        }
        public ActionResult Create()
        {
            var product = new ProductCreateVM();
            product.Brands = _brandService.GetBrands()
                .Select(x => new BrandPairVM()
                {
                    Id = x.Id,
                    Name = x.BrandName
                }).ToList();
            product.Categories = _categoryService.GetCategories()
                .Select(x => new CategoryPairVM()
                {
                    Id = x.Id,
                    Name=x.CategoryName
                }).ToList();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] ProductCreateVM product)
        {
            if(ModelState.IsValid)
            {
                var createdId = productService.Create(product.ProductName,
                    product.BrandId, product.CategoryId, product.Picture,
                    product.Quantity, product.Price, product.Discount);

                if(createdId)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            Product product = productService.GetProductById(id);
                if(product == null)
            { return NotFound(); }

            ProductEditVM updatedProduct = new ProductEditVM()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Picture = product.Picture,
                Quantity = product.Quantity,
                Price = product.Price,
                Discount = product.Discount
            };

            updatedProduct.Brands = _brandService.GetBrands()
                .Select(b => new BrandPairVM()
                {
                    Id = b.Id,
                    Name = b.BrandName
                }).ToList();

            updatedProduct.Categories = _categoryService.GetCategories()
                .Select(c => new CategoryPairVM()
                {
                    Id = c.Id,
                    Name = c.CategoryName
                }).ToList();
            return View(updatedProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductEditVM product)
        {
            if (ModelState.IsValid)
            {
                var updated = productService.Update(id, product.ProductName , product.BrandId ,
                    product.CategoryId , product.Picture , product.Quantity ,
                    product.Price , product.Discount );

                if (updated)
                {
                    return this.RedirectToAction("Index");
                }
            }
            return View(product);
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Product item = productService.GetProductById(id);
            if (item==null)
            {
                return NotFound();
            }
            ProductDetailsVM product = new ProductDetailsVM()
            {
                Id=item.Id,
                ProductName = item.ProductName,
                BrandId = item.BrandId,
                BrandName = item.Brand.BrandName,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.CategoryName,
                Picture = item.Picture,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item.Discount
            };
            return View(product);
        }

        public ActionResult Delete(int id)
        {
            Product product = productService.GetProductById(id);
            if(product==null)
            { return NotFound();}

            ProductDeleteVM productDelete = new ProductDeleteVM()
            {
                Id=product.Id,
                ProductName=product.ProductName,
                BrandId=product.BrandId,
                BrandName=product.Brand.BrandName,
                CategoryId=product.CategoryId,
                CategoryName=product.Category.CategoryName,
                Picture = product.Picture,
                Quantity=product.Quantity,
                Price=product.Price,
                Discount=product.Discount
            };
            return View(productDelete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = productService.RemoveById(id);
            if (deleted)
            {
                return this.RedirectToAction("Index");
            }
            else { return View(); }
        }
    }
}
