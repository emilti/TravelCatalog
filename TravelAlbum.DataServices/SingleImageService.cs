﻿using Bytes2you.Validation;
using System.Collections.Generic;
using System.Linq;
using TravelAlbum.Data.Contracts;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices
{
    public class SingleImageService : ISingleImageService
    {

        private readonly IEfDbSetWrapper<SingleImage> singleImageSetWrapper;

        private readonly ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges;

        public SingleImageService(IEfDbSetWrapper<SingleImage> singleImageSetWrapper, ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges)
        {
            Guard.WhenArgument(singleImageSetWrapper, "singleImageSetWrapper").IsNull().Throw();
            Guard.WhenArgument(travelAlbumEfDbContextSaveChanges, "travelAlbumEfDbContextSaveChanges").IsNull().Throw();

            this.singleImageSetWrapper = singleImageSetWrapper;
            this.travelAlbumEfDbContextSaveChanges = travelAlbumEfDbContextSaveChanges;
        }

        // public Travel GetById(Guid? id)
        // {
        //     Travel result = null;
        // 
        //     Travel travel = this.travelSetWrapper.GetById(id);
        //     if (travel != null)
        //     {
        //         result = travel;
        //     }
        // 
        //     return result;
        // }
        // 
        // public TravelTranslationalInfo GetTravelByTitle(string searchTerm)
        // {
        //     // IQueryable<Travel> travels = travelSetWrapper.All;
        //     // var trnaslatedTravels = travels.SelectMany(x => x.TranslatedTravels);
        //     // TravelTranslationalInfo translatedTravelByTitle= trnaslatedTravels.FirstOrDefault(a => a.Title.Contains(searchTerm));
        //     // Travel travel = translatedTravelByTitle.Travel;
        //     return null;
        // }
        // 

        public void Add(SingleImage singleImage)
        {
            singleImageSetWrapper.Add(singleImage);
            this.travelAlbumEfDbContextSaveChanges.SaveChanges();
        }

        public IEnumerable<SingleImage> GetLatesSingleImages(int pageIndex)
        {
            IQueryable<SingleImage> singleImages = singleImageSetWrapper.All;
            var orderedSingleImages = singleImages.OrderByDescending(a => a.CreatedOn).Skip(2 * pageIndex).Take(2).ToList();
            return orderedSingleImages;
        }         
    }
}