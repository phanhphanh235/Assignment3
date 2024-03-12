using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberDAO
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Member> GetMemberList()
        {
            List<Member> Members;
            try
            {
                using SaleManagementContext mem = new SaleManagementContext();
                Members = mem.Members.ToList();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Members;
        }
        
        public Member GetMemberByID(int memberID)
        {
            List<Member> MemberList = GetMemberList();

            Member member = MemberList.SingleOrDefault(pro => pro.MemberId == memberID);
            return member;
        }
        public List<Member> GetMemberByIDList(int memid)
        {
            List<Member> MemberList = GetMemberList();
            Member member = MemberList.SingleOrDefault(pro => pro.MemberId == memid);
            MemberList.Clear();
            MemberList.Add(member);
            return MemberList;
        }
        public Member GetMemberByName(string memberName)
        {
            List<Member> MemberList = GetMemberList();

            Member member = MemberList.SingleOrDefault(pro => pro.CompanyName == memberName);
            return member;
        }
        public void AddNew(Member member)
        {
            try
            {
                using SaleManagementContext mem = new SaleManagementContext();
                mem.Members.Add(member);
                mem.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Member member)
        {
            try
            {
                using SaleManagementContext mem = new SaleManagementContext();
                mem.Entry<Member>(member).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                mem.SaveChanges();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Remove(int MemberID)
        {
            try
            {
                using SaleManagementContext mem = new SaleManagementContext();
                var e = mem.Members.SingleOrDefault(m => m.MemberId == MemberID);
                mem.Members.Remove(e);
                mem.SaveChanges();
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Member> GetMemberByCityAndCountry(string city, string country)
        {
            List<Member> Flist = new List<Member>();
            List<Member> MemberList = GetMemberList();
            for (int i = 1; i<= MemberList.Count; i++)
            {
                if (MemberList[i-1].City == city && MemberList[i-1].Country == country)
                {
                    Flist.Add(MemberList[i-1]);
                }
            }
            return Flist;
        }

    }
}
