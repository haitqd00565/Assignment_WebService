using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.ModelBinding;

namespace Assignment_WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AdminService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AdminService.svc or AdminService.svc.cs at the Solution Explorer and start debugging.
    public class AdminService : IAdminService
    {
        dbDataContext db = new dbDataContext();
        // AdminService
        public bool AddAdmin(Admin admin)
        {
            try
            {
                var encriptionMd5 = Encription.MD5Hash(admin.Password);
                admin.Password = encriptionMd5;
                db.Admin.InsertOnSubmit(admin);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteAdmin(int idA)
        {
            try
            {
                Admin adminToDelete = (from admin in db.Admin where admin.Id == idA select admin).Single();
                db.Admin.DeleteOnSubmit(adminToDelete);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool LoginAdmin(Admin admin)
        {
            var result = db.Admin.Where(x => x.Email == admin.Email && x.Password == Encription.MD5Hash(admin.Password)).FirstOrDefault();
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            } 

            
        }

        public List<Admin> GetAdminList()
        {
            try
            {
                return (from admin in db.Admin select admin).ToList();
            }
            catch
            {
                return null;
            }
        }
        public bool UpdateAdmin(Admin newAdmin)
        {
            try
            {
                Admin adminToUpdate = (from admin in db.Admin where admin.Id == newAdmin.Id select admin).Single();
                adminToUpdate.Password = newAdmin.Password;
                adminToUpdate.UserName = newAdmin.UserName;
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // CategoryPlaceService
        public bool AddCatePlace(CategoryPlace categoryPlace)
        {
            try
            {
                db.CategoryPlace.InsertOnSubmit(categoryPlace);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCatePlace(int idCP)
        {
            CategoryPlace deleteToCatePlace = (from catePlace in db.CategoryPlace where catePlace.Id == idCP select catePlace).Single();
            db.CategoryPlace.DeleteOnSubmit(deleteToCatePlace);
            db.SubmitChanges();
            return true;
        }

        public List<CategoryPlace> GetCatePlaceList()
        {
            try
            {
                return (from catePlace in db.CategoryPlace select catePlace).ToList();
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateCatePlace(CategoryPlace newCategoryPlace)
        {
            CategoryPlace categoryPlace = (from catePlace in db.CategoryPlace where catePlace.Id == newCategoryPlace.Id select catePlace).Single();
            categoryPlace.Name = newCategoryPlace.Name;
            categoryPlace.Image = newCategoryPlace.Image;
            db.SubmitChanges();
            return true;
        }

        // PlaceService
        public List<Place> GetPlaceList()
        {
            try
            {
                return (from place in db.Place select place).ToList();
            }
            catch
            {
                return null;
            }
        }
        public Place GetPlaceById(int id)
        {
            var query = db.Place.Where(p => p.Id == id).FirstOrDefault();
            return query;
        }
        public bool AddPlace(Place place)
        {
            var result = db.CategoryPlace.Where(x => x.Id == place.CategoryPlaceId).FirstOrDefault();
            if (result == null)
            {
                return false;
            }
            else
            {
                db.Place.InsertOnSubmit(place);
                db.SubmitChanges();
                return true;
            }
        }

        public bool UpdatePlace(Place newPlace)
        {
            Place updateToPlace = (from place in db.Place where place.Id == newPlace.Id select place).FirstOrDefault();
            updateToPlace.Description = newPlace.Description;
            updateToPlace.Name = newPlace.Name;
            updateToPlace.Information = newPlace.Information;
            db.SubmitChanges();
            return true;
        }

        public bool DeletePlace(int idP)
        {
            Place deleteToPlace = (from place in db.Place where place.Id == idP select place).FirstOrDefault();
            db.Place.DeleteOnSubmit(deleteToPlace);
            db.SubmitChanges();
            return true;
        }

        //ImageService
        public bool AddImage(Image image)
        {
            var result = db.Place.Where(x => x.Id == image.PlaceId).FirstOrDefault();
            if (result == null)
            {
                return false;
            }
            else
            {
                db.Image.InsertOnSubmit(image);
                db.SubmitChanges();
                return true;
            }
        }

        public bool DeleteImage(int idI)
        {
            Image deleteImage = (from image in db.Image where image.Id == idI select image).FirstOrDefault();
            db.Image.DeleteOnSubmit(deleteImage);
            db.SubmitChanges();
            return true;
        }

        public List<Image> GetImageList()
        {
            try
            {
                return (from image in db.Image select image).ToList();
            }
            catch
            {
                return null;
            }
        }
        public List<Image> GetImageListById(int id)
        {
            try
            {
                return (from image in db.Image where image.PlaceId == id select image).ToList();

            }
            catch
            {
                return null;
            }
        }

        public bool UpdateImage(Image newImage)
        {
            Image updateImage = (from image in db.Image where image.Id == newImage.Id select image).FirstOrDefault();
            updateImage.Image1 = newImage.Image1;
            db.SubmitChanges();
            return true;
        }
    }
}
