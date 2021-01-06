using System;
using System.Collections.Generic;
using keepr_server.Models;
using keepr_server.Repositories;

namespace keepr_server.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    internal IEnumerable<Keep> GetKeepsByVaultId(int vaultId)
    {
      return _repo.GetKeepsByVaultId(vaultId);

    }

    public VaultKeep Create(VaultKeep newVk)
    {
      newVk.Id = _repo.Create(newVk);
      return newVk;
    }

    public string Delete(int id, string userId)
    {
      VaultKeep original = _repo.Get(id);
      if (original == null) { throw new Exception("Bad Id"); }
      if (original.CreatorId != userId)
      {
        throw new Exception("Not the User : Access Denied");
      }
      if (_repo.Remove(id))
      {
        return "deleted succesfully";
      }
      return "did not remove succesfully";
    }
  }
}
