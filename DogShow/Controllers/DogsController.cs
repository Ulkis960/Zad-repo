﻿using DogShow.Data;
using DogShow.Domain;
using DogShow.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogShow.Controllers
{
    public class DogsController : Controller
    {
        private readonly ApplicationDbContext context;
        public DogsController (ApplicationDbContext context )
        {
            this.context = context;
        }
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(DogCreateViewModel bindingModel)
        {
            if(ModelState.IsValid)
            {
                Dog dogFromDb = new Dog
                {
                    Name = bindingModel.Name,
                    Age = bindingModel.Age,
                    Breed = bindingModel.Breed,
                    Picture = bindingModel.Picture,
                };

                context.Dogs.Add(dogFromDb);
                context.SaveChanges();

                return this.RedirectToAction("Success");
            }

            return this.View();

            
        }
        public IActionResult Success()
        {
            return this.View();
        }

        public IActionResult All (string searchStringBreed, string searchStringName)
        {
            List<DogAllViewModel> dogs = context.Dogs
                .Select(dogFromDb => new DogAllViewModel
                {
                    Id=dogFromDb.Id,
                    Name= dogFromDb.Name,
                    Age= dogFromDb.Age,
                    Breed= dogFromDb.Breed,
                    Picture= dogFromDb.Picture
                }
                ).ToList();
            if (!String.IsNullOrEmpty(searchStringBreed))
            {
                dogs = dogs.Where(x => x.Breed.ToLower() == searchStringBreed.ToLower())
                    .ToList();
            }

            if (!String.IsNullOrEmpty(searchStringName))
            {
                dogs = dogs.Where(x => x.Name.ToLower() == searchStringName.ToLower())
                    .ToList();
            }

            

            return View(dogs);
                
        }


        public IActionResult Sort()
        {
            List<DogAllViewModel> dogs = context.Dogs
                .Select(dogFromDb => new DogAllViewModel
                {
                    Id = dogFromDb.Id,
                    Name = dogFromDb.Name,
                    Age = dogFromDb.Age,
                    Breed = dogFromDb.Breed,
                    Picture = dogFromDb.Picture
                }
                ).OrderBy(x=> x.Name).ToList();
          
            return View(dogs);

        }


        public IActionResult Edit(int? id)
        {
            if (id ==null)
            {
                return NotFound();
            }

            Dog item = context.Dogs.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            DogCreateViewModel dog = new DogCreateViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                Breed = item.Breed,
                Picture = item.Picture

            };
            return View(dog);
        }
        [HttpPost]

        public IActionResult Edit(DogCreateViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                Dog dog = new Dog
                {
                    Id = bindingModel.Id,
                    Name = bindingModel.Name,
                    Age = bindingModel.Age,
                    Breed = bindingModel.Breed,
                    Picture = bindingModel.Picture
                };
                context.Dogs.Update(dog);
                context.SaveChanges();
                return this.RedirectToAction("All");

            }
            return View(bindingModel);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Dog item = context.Dogs.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            DogCreateViewModel dog = new DogCreateViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                Breed = item.Breed,
                Picture = item.Picture

            };
            return View(dog);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Dog item = context.Dogs.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            context.Dogs.Remove(item);
            context.SaveChanges();
            return this.RedirectToAction("All", "Dogs");

        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Dog item = context.Dogs.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            DogDetailsViewModel dog = new DogDetailsViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                Breed = item.Breed,
                Picture = item.Picture

            };
            return View(dog);
        }

    }
}
