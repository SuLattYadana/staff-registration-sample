using StaffRegistrationWebApp.Data;
using StaffRegistrationWebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StaffRegistrationWebApp.Repository
{
    public class StaffRepository
    {
        private AppDBContext _ctx;
        public StaffRepository(AppDBContext AppDbCtx)
        {
            _ctx = AppDbCtx;
        }

        public IQueryable<Stafftb> GetStaff()
        {
            return _ctx.Staffs.AsQueryable();
        }

        
        public Stafftb Get(AppDBContext ctx, int id)
        {
            return _ctx.Staffs.Where(a => a.ID == id).FirstOrDefault();
        }

        public Stafftb AddStaffObj(Stafftb staffObj)
        {
            Stafftb obj = null;
            obj = _ctx.Staffs.Add(staffObj);
            _ctx.SaveChanges();
            if (_ctx.SaveChanges() > 0)
            {
                return obj;
            }
            return obj;

        }

        public Stafftb UpdateStaffObj(Stafftb staffObj)
        {
            Stafftb updateEntity = _ctx.Staffs.FirstOrDefault(a => a.ID == staffObj.ID);

            updateEntity.Name = staffObj.Name;
            updateEntity.DateOfBirth = staffObj.DateOfBirth;
            updateEntity.Phone = staffObj.Phone;
            updateEntity.Address = staffObj.Address;
            updateEntity.State = staffObj.State;
            updateEntity.Township = staffObj.Township;
            updateEntity.Email = staffObj.Email;
            updateEntity.UserName = staffObj.UserName;
            updateEntity.Role = staffObj.Role;
            updateEntity.JobTile = staffObj.JobTile;
            updateEntity.Department = staffObj.Department;
            updateEntity.DateOfJoin = staffObj.DateOfJoin;
            updateEntity.IsAdmin = staffObj.IsAdmin;
            updateEntity.Nationality = staffObj.Nationality;
            updateEntity.Religion = staffObj.Religion;
            updateEntity.Qualification = staffObj.Qualification;


            using (AppDBContext context = new AppDBContext())
            {
                context.Entry<Stafftb>(updateEntity).State = EntityState.Modified;
                if (_ctx.SaveChanges() > 0)
                {
                    return updateEntity;
                }
                return null;
            }
        }

        public List<Stafftb> Delete(int id)
        {
        
            Stafftb obj = _ctx.Staffs.FirstOrDefault(a => a.ID == id);
            _ctx.Staffs.Remove(obj);
            using (AppDBContext entityContext = new AppDBContext())
            {
                Stafftb entity = Get(entityContext, id);
                entityContext.Entry<Stafftb>(entity).State = EntityState.Deleted;
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return _ctx.Staffs.ToList();
                }
                return null;
            }
        }
    }
}