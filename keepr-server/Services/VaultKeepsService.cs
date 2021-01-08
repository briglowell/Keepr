using System;
using System.Collections.Generic;
using keepr_server.Models;
using keepr_server.Repositories;

namespace keepr_server.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    private readonly VaultsRepository _vr;
    public VaultKeepsService(VaultKeepsRepository repo, VaultsRepository vr)
    {
      _repo = repo;
      _vr = vr;
    }

    internal IEnumerable<Keep> GetKeepsByVaultId(int vaultId)
    {
      return _repo.GetKeepsByVaultId(vaultId);

    }

    public VaultKeep Create(VaultKeep newVk, string userId)
    {
      Vault original = _vr.GetOne(newVk.VaultId);
      if (original == null) { throw new Exception("Bad Id"); }
      if (original.CreatorId != userId)
      {
        throw new Exception("Not the User : Access Denied");
      }
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
      if (_repo.Delete(id))
      {
        return "deleted succesfully";
      }
      return "did not remove succesfully";
    }

    internal string DeleteAllByVaultId(int id)
    {
      if (_repo.DeleteAllByVaultId(id))
      {
        return "deleted successfully";
      }
      throw new Exception
      ("something went wrong, not deleted");
    }
  }
}
