using Assignment2.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.BLL
{
    public class Album: Base
    {
        private IRepository<Album> _repository;
        // Not yet implemented
        public bool CopyToFolder { get; set; }  
        public Album()
        {
            _repository = new Repository<Album>(_context);
        }

        public void Save()
        {
            // TODO Go through servicelayer
            // See IDataErrorInfo region in MyGames
            _repository.Save(this);
        }
    }
}
