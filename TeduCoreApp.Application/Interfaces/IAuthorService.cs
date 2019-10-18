using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Application.ViewModels;

namespace TeduCoreApp.Application.Interfaces
{
    public interface IAuthorService
    {
        AuthorViewModel Add(AuthorViewModel authorVm);

        void Update(AuthorViewModel authorVm);

        void Delete(int id);

        List<AuthorViewModel> GetAll();


        AuthorViewModel GetById(int id);

        void Save();
    }
}
