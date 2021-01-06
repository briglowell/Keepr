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
  }
}