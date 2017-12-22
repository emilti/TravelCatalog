﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using TravelAlbum.Models;

namespace TravelAlbum.Web.Models.ImageModels
{
    public class ImagesListViewModel
    {
        private ICollection<Mountain> mountains;       

        public ImagesListViewModel()
        {
            this.singleImagePreviews = new List<ImagePreviewOutputViewModel>();
            this.Mountains = new HashSet<Mountain>();
            this.MountainsIds = new HashSet<Guid>();
        }

        public IEnumerable<Guid> MountainsIds { get; set; }

        public IEnumerable<SelectListItem> MountainsDropDown { get; set; }

        public List<ImagePreviewOutputViewModel> singleImagePreviews { get; set; }   
        
        public Sorting Sorting { get; set; }

        [DisplayName("Планини")]
        public virtual ICollection<Mountain> Mountains
        {
            get { return this.mountains; }
            set { this.mountains = value; }
        }
    }
}