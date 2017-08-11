namespace EFCodeFirstToAnExistingDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using BMX.Repositories;

    public class PartsOnlineRecordRepository : ILinqRepository<PartsOnlineRecord, int>
    {
        public long Count()
        {
            int count;

            using (var context = new BMXContext())
            {
                count = context.PartsOnlineRecords.Count();
            }

            return count;
        }

        public long Count(Expression<Func<PartsOnlineRecord, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<PartsOnlineRecord, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(PartsOnlineRecord entity)
        {
            throw new NotImplementedException();
        }

        public PartsOnlineRecord Get(Expression<Func<PartsOnlineRecord, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public PartsOnlineRecord Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PartsOnlineRecord> GetAllList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PartsOnlineRecord> GetAllList(Expression<Func<PartsOnlineRecord, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Insert(PartsOnlineRecord entity)
        {
            throw new NotImplementedException();
        }

        public void Update(PartsOnlineRecord entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Expression<Func<PartsOnlineRecord, bool>> predicate, PartsOnlineRecord entity)
        {
            throw new NotImplementedException();
        }
    }
}
