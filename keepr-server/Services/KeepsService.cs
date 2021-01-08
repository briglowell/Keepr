using System;
using System.Collections.Generic;
using System.Linq;
using keepr_server.Models;
using keepr_server.Repositories;

namespace keepr_server.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }

    public IEnumerable<Keep> Get()
    {
      return _repo.Get();
    }
    internal IEnumerable<Keep> GetKeepsByProfile(string profileId, string userId)
    {
      return _repo.getKeepsByProfile(profileId).ToList().FindAll(k => k.CreatorId == userId);
    }
    internal Keep Create(Keep newKeep)
    {
      newKeep.Id = _repo.Create(newKeep);
      return newKeep;
    }

    internal Keep Edit(Keep editData, string userId)
    {
      Keep original = _repo.GetOne(editData.Id);
      if (original == null) { throw new Exception("Bad Id"); }
      if (original.CreatorId != userId)
      {
        throw new Exception("Not the User : Access Denied");
      }

      if (editData.Name == null)
      {
        editData.Name = original.Name;
      }
      if (editData.Img == null)
      {
        editData.Img = original.Img;
      }
      if (editData.Description == null)
      {
        editData.Description = original.Description;
      }
      if (editData.Views == 0)
      {
        editData.Views = original.Views;
      }
      if (editData.Keeps == 0)
      {
        editData.Keeps = original.Keeps;
      }
      if (editData.Shares == 0)
      {
        editData.Shares = original.Shares;
      }

      _repo.Edit(editData);

      return _repo.GetOne(editData.Id);
    }

    internal string Delete(int id)
    {
      if (_repo.Delete(id))
      {
        return "deleted successfully";
      }
      throw new Exception
      ("something went wrong, not deleted");
    }
  }
}
