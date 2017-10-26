﻿using Bytes2you.Validation;
using System;
using System.Web.Mvc;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;
using System.Linq;
using TravelAlbum.Web.Models.InputViewModels;
using System.IO;
using TravelAlbum.Data.Contracts;
using TravelAlbum.Web.Models.TravelModels;
using System.Web;
using TravelAlbum.Web.Models;

namespace TravelAlbum.Web.Controllers
{
    public class TravelsController : Controller
    {
        private readonly ITravelService travelService;

        private readonly ITravelTranslationalInfoService travelTranslationalService;

        private readonly ITravelImageService travelImageService;

        public TravelsController(ITravelService travelService, ITravelTranslationalInfoService travelTranslationalService, ITravelImageService travelImageService)
        {
            Guard.WhenArgument(travelService, "travelService").IsNull().Throw();
            Guard.WhenArgument(travelTranslationalService, "travelTranslationalService").IsNull().Throw();
            Guard.WhenArgument(travelImageService, "travelImageService").IsNull().Throw();

            this.travelService = travelService;
            this.travelTranslationalService = travelTranslationalService;
            this.travelImageService = travelImageService;
        }

        [ValidateAntiForgeryTokenAttribute]
        public ActionResult SearchTravels(TravelSearchInputViewModel inputModel)
        {
            TravelTranslationalInfo travelByTitle = travelService.GetTravelByTitle(inputModel.Search);

            return RedirectToAction("Details", "Travels", new { id = travelByTitle.TravelId });
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            Travel travel = this.travelService.GetById(id);
            TravelTranslationalInfo travelTranslationalInfo = travel.TranslatedTravels.FirstOrDefault(x => x.TravelId == travel.TravelId);
            TravelImage travelImage = travel.TravelImages.FirstOrDefault();

            String imageData = Convert.ToBase64String(travelImage.Content);
            TravelViewModel travelViewModel = new TravelViewModel()
            {
                Title = travelTranslationalInfo.Title,
                Description = travelTranslationalInfo.Description,
                ImageData = imageData
            };            

            return this.View(travelViewModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Add(CreateTravelInputModel travelForAdding)
        {
            Travel newTravel = new Travel
            {
                TravelId = Guid.NewGuid(),
                CreatedOn = DateTime.Now,
                StartDate = new DateTime(2017, 08, 10),
                EndDate = DateTime.Now
            };

            travelService.Add(newTravel);

            TravelTranslationalInfo newBgTravelInfo = new TravelTranslationalInfo()
            {
                TravelTranslationalInfoId = Guid.NewGuid(),
                Title = travelForAdding.bgTitle,
                Description = travelForAdding.bgDescription,
                Travel = newTravel,
                Language = travelForAdding.Language
            };

            travelTranslationalService.Add(newBgTravelInfo);

            TravelTranslationalInfo newEnTravelInfo = new TravelTranslationalInfo()
            {
                TravelTranslationalInfoId = Guid.NewGuid(),
                Title = travelForAdding.enTitle,
                Description = travelForAdding.enDescription,
                Travel = newTravel,
                Language = travelForAdding.Language
            };

            travelTranslationalService.Add(newEnTravelInfo);
            
            HttpPostedFileBase image_1 = travelForAdding.UploadedImage_1;
            GenerateImage(image_1, newTravel);

            HttpPostedFileBase image_2 = travelForAdding.UploadedImage_2;
            GenerateImage(image_2, newTravel);

            HttpPostedFileBase image_3 = travelForAdding.UploadedImage_3;
            GenerateImage(image_3, newTravel);

            HttpPostedFileBase image_4 = travelForAdding.UploadedImage_4;
            GenerateImage(image_4, newTravel);

            return this.RedirectToAction("Details", "Travels", new { id = newTravel.TravelId });
        }

        private void GenerateImage(HttpPostedFileBase image, Travel newTravel)
        {
            var imageContent = new byte[image.ContentLength];

            byte[] imageData = null;

            using (var binaryReader = new BinaryReader(image.InputStream))
            {
                imageData = binaryReader.ReadBytes(image.ContentLength);
            }

            TravelImage newTravelImage = new TravelImage
            {
                TravelImageId = Guid.NewGuid(),
                Content = imageData,
                Travel = newTravel
            };

            travelImageService.Add(newTravelImage);
        }

        [HttpGet]
        public ActionResult All()
        {
            return this.View();
        }
    }
}