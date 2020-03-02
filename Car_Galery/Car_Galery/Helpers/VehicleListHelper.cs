using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Car_Galery.Models;
using Car_Galery.Context;
using Car_Galery.Entities;
using Car_Galery.Managers.Abstract;

namespace Car_Galery.Helpers
{
    public class VehicleListHelper
    {
        public static IQueryable Filter(FilterModel fm, IUnitOfWork unitOfWork, bool control)
        {

            var query = unitOfWork.GetRepository<Vehicle>().GetAll(v=>v.Rentable == control && v.Rented == false).AsQueryable();
            #region Filter
            if(fm.Filtered == true)
            {
                if (fm.TypeId != 0 && fm.TypeId != null)
                {
                    query = query.Where(v => v.TypeId == fm.TypeId);
                }


                if (fm.BrandId != 0 && fm.BrandId != null)
                {
                    query = query.Where(v => v.BrandId == fm.BrandId);
                }

                if (fm.ModelId != 0 && fm.ModelId != null)
                {
                    query = query.Where(v => v.ModelId == fm.ModelId);
                }

                if(!String.IsNullOrWhiteSpace(fm.FuelType))
                {
                    query = query.Where(v => v.Fuel == fm.FuelType);
                }

                if(!String.IsNullOrWhiteSpace(fm.Transmission))
                {
                    query = query.Where(v => v.Transmission == fm.Transmission);
                }

                if(!String.IsNullOrWhiteSpace(fm.Year))
                {
                    query = query.Where(v => v.Year == fm.Year);
                }

                if (fm.MinPrice != null)
                {
                    query = query.Where(v => v.Price >= fm.MinPrice);
                }

                if (fm.MaxPrice != null)
                {
                    query = query.Where(v => v.Price <= fm.MaxPrice);
                }

                if (fm.MinKm != null)
                {
                    query = query.Where(v => v.Km >= fm.MinKm);
                }

                if (fm.MaxKm != null)
                {
                    query = query.Where(v => v.Km <= fm.MaxKm);
                }

            }
            #endregion

            #region Brand Categories
            else if (fm.BrandId != 0 && fm.BrandId != null)
            {
                query = query.Where(v => v.BrandId == fm.BrandId);

                if (fm.ModelId != 0 && fm.ModelId != null)
                {
                    query = query.Where(v => v.ModelId == fm.ModelId);
                }
            }
            #endregion

            #region Search Text
            if(!String.IsNullOrWhiteSpace(fm.SearchText))
            {
                query = query.Where(v => v.Name.Contains(fm.SearchText));
            }
            #endregion

            #region Sort
            if (!String.IsNullOrWhiteSpace(fm.SortBy))
            {
                switch (fm.SortBy)
                {
                    case "Name":
                        query = query.OrderBy(v => v.Name);
                        break;
                    case  "Price":
                        query = query.OrderBy(v => v.Price);
                        break;
                    case "Km":
                        query = query.OrderBy(v => v.Km);
                        break;

                }
            }
            #endregion

            return query;
        }
    }
}