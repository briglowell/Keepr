using System;
using System.Collections.Generic;
using System.Linq;
using keepr_server.Models;
using keepr_server.Repositories;

namespace keepr_server.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }
    internal IEnumerable<Vault> GetVaultsByProfile(string profileId, string userId)
    {
      return _repo.getVaultsByProfile(profileId).ToList().FindAll(v => v.CreatorId == userId || !v.IsPrivate);
    }

    public IEnumerable<Vault> Get()
    {
      return _repo.Get();
    }

    internal Vault GetVaultById(int vaultId)
    {
      Vault res = _repo.GetOne(vaultId);
      if (res == null) { throw new Exception("Bad Id"); }
      if (res.IsPrivate == true)
      {
        throw new Exception("Vault is Private : Access Denied");
      }
      return res;
    }

    public Vault Create(Vault newVault)
    {
      newVault.Id = _repo.Create(newVault);
      return newVault;
    }

    internal Vault Edit(Vault editData, string userId)
    {
      Vault original = _repo.GetOne(editData.Id);
      if (original == null) { throw new Exception("Bad Id"); }
      if (original.CreatorId != userId)
      {
        throw new Exception("Not the User : Access Denied");
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