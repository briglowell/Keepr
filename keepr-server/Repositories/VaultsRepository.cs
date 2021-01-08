using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr_server.Models;

namespace keepr_server.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;
    private readonly string populateCreator = "SELECT vault.*, profile.* FROM vaults vault INNER JOIN profiles profile ON vault.creatorId = profile.id ";

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    public IEnumerable<Vault> Get()
    {
      string sql = populateCreator + "WHERE isPrivate = 0";
      return _db.Query<Vault, Profile, Vault>(sql, (blog, profile) => { blog.Creator = profile; return blog; }, splitOn: "id");
    }

    internal IEnumerable<Vault> getVaultsByProfile(string profileId)
    {
      string sql = @"
        SELECT
        vault.*,
        profile.*
        FROM vaults vault
        JOIN profiles profile ON vault.creatorId = profile.id
        WHERE vault.creatorId = @profileId; ";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; }, new { profileId }, splitOn: "id");
    }

    internal Vault GetOne(int id)
    {
      string sql = @"SELECT * from vaults WHERE id = @id";
      // string sql = populateCreator + "WHERE vault.id = @id";
      return _db.QueryFirstOrDefault<Vault>(sql, new { id });
    }

    internal int Create(Vault newVault)
    {
      string sql = @"
            INSERT INTO vaults 
            (name, description, creatorId, isPrivate)
            VALUES
            (@Name, @Description, @CreatorId, @IsPrivate);
            SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newVault);
    }

    internal void Edit(Vault editData)
    {
      string sql = @"
        UPDATE vaults
        SET
        name = @Name,
        description = @Description,
        isPrivate = @IsPrivate
        WHERE id = @Id;";
      _db.Execute(sql, editData);
    }

    internal bool Delete(int id)
    {
      string sql = "DELETE FROM vaults WHERE id = @Id LIMIT 1;";
      int affectedRows = _db.Execute(sql, new { id });
      // returns a bool
      return affectedRows > 0;
    }
  }
}